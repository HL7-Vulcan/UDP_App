using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using UDP_App.Models;

namespace UDP_App.Services;

public record FhirResourceBlock(
    string FileName,
    string ResourceType,
    string Id,
    string Json);

public class BuildService
{
    private readonly IWebHostEnvironment _env;
    private readonly IHttpClientFactory  _http;
    private readonly SettingsService     _settings;

    public BuildService(IWebHostEnvironment env, IHttpClientFactory http, SettingsService settings)
    {
        _env      = env;
        _http     = http;
        _settings = settings;
    }

    /// <summary>
    /// POSTs the generated bundle.json from the Examples folder to the FHIR server as a transaction Bundle.
    /// Returns the HTTP status code, reason phrase, and (on failure) the full response body so that
    /// FHIR OperationOutcome diagnostics can be displayed to the user.
    /// </summary>
    public async Task<(int StatusCode, string Reason, string? ResponseBody)> SendBundleToServerAsync(string examplesFolder)
    {
        if (!Path.IsPathRooted(examplesFolder))
            examplesFolder = Path.Combine(_env.ContentRootPath, examplesFolder);

        var bundlePath = Path.Combine(examplesFolder, "bundle.json");
        if (!File.Exists(bundlePath))
            throw new FileNotFoundException("bundle.json not found. Generate the bundle first.", bundlePath);

        var bundleJson = await File.ReadAllTextAsync(bundlePath);

        var s      = _settings.Settings;
        var client = _http.CreateClient();
        var token  = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{s.Username}:{s.Password}"));
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", token);

        var url     = s.BaseUrl.TrimEnd('/') + "/Bundle";
        var content = new StringContent(bundleJson, Encoding.UTF8, "application/fhir+json");

        // Build a diagnostic summary of exactly what we are about to send
        var requestDump = new System.Text.StringBuilder();
        requestDump.AppendLine($"PUT {url}");
        requestDump.AppendLine($"Authorization: Basic {token[..Math.Min(8, token.Length)]}…  (truncated)");
        requestDump.AppendLine($"Content-Type: {content.Headers.ContentType}");
        requestDump.AppendLine($"Body length: {Encoding.UTF8.GetByteCount(bundleJson)} bytes");
        requestDump.AppendLine($"Body preview: {bundleJson[..Math.Min(200, bundleJson.Length)]}");

        var response   = await client.PostAsync(url, content);
        var statusCode = (int)response.StatusCode;
        var reason     = response.ReasonPhrase ?? string.Empty;

        string? body = null;
        try { body = await response.Content.ReadAsStringAsync(); }
        catch { }

        // Prepend the request dump to the body so it appears in the diagnostics panel
        var fullDiag = requestDump.ToString() + "\n--- RESPONSE BODY ---\n" + (body ?? "(empty)");

        return (statusCode, reason, fullDiag);
    }

    /// <summary>
    /// Extracts human-readable diagnostic lines from a FHIR OperationOutcome JSON body.
    /// Returns null if the body is not an OperationOutcome or contains no issues.
    /// </summary>
    public static IReadOnlyList<string>? ExtractOperationOutcomeDiagnostics(string? body)
    {
        if (string.IsNullOrWhiteSpace(body)) return null;

        // The body may be prefixed with a request-dump; extract just the JSON part for parsing
        const string marker = "\n--- RESPONSE BODY ---\n";
        var markerIdx = body.IndexOf(marker, StringComparison.Ordinal);
        var jsonPart  = markerIdx >= 0 ? body[(markerIdx + marker.Length)..] : body;

        // Always return the full dump as the first line so the UI shows request details too
        var lines = new List<string> { body };

        try
        {
            using var doc = JsonDocument.Parse(jsonPart);
            var root = doc.RootElement;
            if (!root.TryGetProperty("resourceType", out var rt) || rt.GetString() != "OperationOutcome")
                return lines;
            if (!root.TryGetProperty("issue", out var issues)) return lines;

            foreach (var issue in issues.EnumerateArray())
            {
                var severity    = issue.TryGetProperty("severity",    out var s)   ? s.GetString()   : null;
                var code        = issue.TryGetProperty("code",        out var c)   ? c.GetString()   : null;
                var diagnostics = issue.TryGetProperty("diagnostics", out var d)   ? d.GetString()   : null;
                var text        = issue.TryGetProperty("details",     out var det) &&
                                  det.TryGetProperty("text",          out var t)   ? t.GetString()   : null;

                var msg = diagnostics ?? text;
                if (string.IsNullOrWhiteSpace(msg)) continue;

                var prefix = (severity != null && code != null) ? $"[{severity}/{code}] " :
                             severity != null ? $"[{severity}] " : "";
                lines.Add(prefix + msg);
            }
        }
        catch { /* return what we have */ }

        return lines;
    }

    /// <summary>
    /// Resolves a folder path (absolute or relative to the app content root),
    /// scans all .json files, extracts every top-level object that has a
    /// "resourceType" field, and returns them as FhirResourceBlock records.
    /// </summary>
    public IReadOnlyList<FhirResourceBlock> ScanFolder(string folderPath)
    {
        var results = new List<FhirResourceBlock>();

        if (string.IsNullOrWhiteSpace(folderPath)) return results;

        // Resolve relative paths against the app content root
        if (!Path.IsPathRooted(folderPath))
            folderPath = Path.Combine(_env.ContentRootPath, folderPath);

        if (!Directory.Exists(folderPath)) return results;

        foreach (var file in Directory.EnumerateFiles(folderPath, "*.json", SearchOption.TopDirectoryOnly)
                                      .OrderBy(f => f))
        {
            try
            {
                var raw  = File.ReadAllText(file);
                var name = Path.GetFileName(file);
                ExtractBlocks(raw, name, results);
            }
            catch { /* skip unreadable files */ }
        }

        return results;
    }

    private static void ExtractBlocks(string raw, string fileName, List<FhirResourceBlock> results)
    {
        using var doc = JsonDocument.Parse(raw, new JsonDocumentOptions { AllowTrailingCommas = true });
        var root = doc.RootElement;

        switch (root.ValueKind)
        {
            case JsonValueKind.Object:
                TryAddBlock(root, fileName, results);
                break;

            case JsonValueKind.Array:
                foreach (var item in root.EnumerateArray())
                    if (item.ValueKind == JsonValueKind.Object)
                        TryAddBlock(item, fileName, results);
                break;
        }
    }

    /// <summary>
    /// Reads all FHIR resource blocks from the Examples folder, applies the
    /// Bundle_Template.json template, and writes bundle.json to the Examples folder.
    /// Returns the path of the written file.
    /// </summary>
    public string GenerateBundle(string examplesFolder, string templatesFolder)
    {
        // Resolve relative paths
        if (!Path.IsPathRooted(examplesFolder))
            examplesFolder = Path.Combine(_env.ContentRootPath, examplesFolder);
        if (!Path.IsPathRooted(templatesFolder))
            templatesFolder = Path.Combine(_env.ContentRootPath, templatesFolder);

        var templatePath = Path.Combine(templatesFolder, "Bundle_Template.json");
        if (!File.Exists(templatePath))
            throw new FileNotFoundException($"Template not found: {templatePath}");

        var templateRaw = File.ReadAllText(templatePath);

        // The REPEAT block starts after <<<REPEAT (plus the newline) and ends just before
        // the closing >>> that sits on its own line (i.e. "\n>>>").
        // We search for "\n>>>" so we don't confuse it with <<<JSON_BLOCK>>> inside the template.
        const string repeatStartMarker = "<<<REPEAT";
        const string repeatEndMarker   = "\n>>>";

        var startIdx = templateRaw.IndexOf(repeatStartMarker, StringComparison.Ordinal);
        if (startIdx < 0)
            throw new InvalidOperationException("Template missing <<<REPEAT marker.");

        // Skip past "<<<REPEAT" and the newline that follows it
        var entryStart = startIdx + repeatStartMarker.Length;
        if (entryStart < templateRaw.Length && templateRaw[entryStart] == '\r') entryStart++;
        if (entryStart < templateRaw.Length && templateRaw[entryStart] == '\n') entryStart++;

        var endIdx = templateRaw.IndexOf(repeatEndMarker, entryStart, StringComparison.Ordinal);
        if (endIdx < 0)
            throw new InvalidOperationException("Template missing closing \\n>>> for REPEAT block.");

        // Entry template: everything between the post-<<<REPEAT newline and the \n>>>
        var entryTemplate = templateRaw.Substring(entryStart, endIdx - entryStart);

        // Scan all resources (excluding bundle.json itself to avoid self-reference)
        var blocks = ScanFolder(examplesFolder)
            .Where(b => !string.Equals(b.FileName, "bundle.json", StringComparison.OrdinalIgnoreCase))
            .ToList();

        // Build one entry per resource, replacing the two tokens
        var entries = blocks.Select(b =>
        {
            var blockId = $"{b.ResourceType}/{b.Id}";
            return entryTemplate
                .Replace("<<<JSON_BLOCK>>>", b.Json)
                .Replace("<<<BLOCK_ID>>>",   blockId);
        });

        var joinedEntries = string.Join(",\n", entries);

        // Replace the entire <<<REPEAT...>>> region (including its own newline prefix) with the joined entries
        var fullRepeat = templateRaw.Substring(startIdx, endIdx - startIdx + repeatEndMarker.Length);
        var outputJson = templateRaw.Replace(fullRepeat, joinedEntries);

        var outputPath = Path.Combine(examplesFolder, "bundle.json");
        File.WriteAllText(outputPath, outputJson);
        return outputPath;
    }

    private static void TryAddBlock(JsonElement obj, string fileName, List<FhirResourceBlock> results)
    {
        // Must have resourceType
        if (!obj.TryGetProperty("resourceType", out var rtProp) ||
            rtProp.ValueKind != JsonValueKind.String)
            return;

        var resourceType = rtProp.GetString() ?? string.Empty;
        if (string.IsNullOrEmpty(resourceType)) return;

        var id = obj.TryGetProperty("id", out var idProp) && idProp.ValueKind == JsonValueKind.String
                 ? idProp.GetString() ?? "(no id)"
                 : "(no id)";

        var pretty = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });

        results.Add(new FhirResourceBlock(fileName, resourceType, id, pretty));
    }

    /// <summary>
    /// Deletes all .json files from the Examples folder.
    /// Returns the number of files deleted.
    /// </summary>
    public int ClearExamplesFolder(string folderPath)
    {
        if (string.IsNullOrWhiteSpace(folderPath)) return 0;
        if (!Path.IsPathRooted(folderPath))
            folderPath = Path.Combine(_env.ContentRootPath, folderPath);
        if (!Directory.Exists(folderPath)) return 0;

        int count = 0;
        foreach (var file in Directory.GetFiles(folderPath, "*.json", SearchOption.TopDirectoryOnly))
        {
            File.Delete(file);
            count++;
        }
        return count;
    }

    /// <summary>
    /// Reads a single .json file from the examples folder and returns its bytes.
    /// Returns null if the file does not exist or the path is unsafe.
    /// </summary>
    public byte[]? ReadExampleFile(string folderPath, string fileName)
    {
        if (string.IsNullOrWhiteSpace(folderPath)) return null;
        var safe = Path.GetFileName(fileName);
        if (string.IsNullOrEmpty(safe) || safe.Contains("..")) return null;
        if (!Path.IsPathRooted(folderPath))
            folderPath = Path.Combine(_env.ContentRootPath, folderPath);
        var path = Path.Combine(folderPath, safe);
        return File.Exists(path) ? File.ReadAllBytes(path) : null;
    }

    /// <summary>
    /// Zips all .json files in the examples folder and returns the zip as bytes.
    /// </summary>
    public byte[]? ReadFolderAsZip(string folderPath)
    {
        if (string.IsNullOrWhiteSpace(folderPath)) return null;
        if (!Path.IsPathRooted(folderPath))
            folderPath = Path.Combine(_env.ContentRootPath, folderPath);
        if (!Directory.Exists(folderPath)) return null;

        var files = Directory.GetFiles(folderPath, "*.json", SearchOption.TopDirectoryOnly)
                             .OrderBy(f => f).ToList();
        if (files.Count == 0) return null;

        using var ms  = new System.IO.MemoryStream();
        using var zip = new System.IO.Compression.ZipArchive(ms, System.IO.Compression.ZipArchiveMode.Create, leaveOpen: true);
        foreach (var file in files)
        {
            var entry = zip.CreateEntry(Path.GetFileName(file), System.IO.Compression.CompressionLevel.Optimal);
            using var es = entry.Open();
            using var fs = File.OpenRead(file);
            fs.CopyTo(es);
        }
        zip.Dispose();
        return ms.ToArray();
    }
}
