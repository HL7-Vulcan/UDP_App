using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using UDP_Browse.Models;

namespace UDP_Browse.Services;

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
    /// PUTs the generated bundle.json from the Examples folder to the FHIR server as a transaction Bundle.
    /// Returns the HTTP status code and reason phrase.
    /// </summary>
    public async Task<(int StatusCode, string Reason)> SendBundleToServerAsync(string examplesFolder)
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

        // FHIR transaction bundles are sent via POST to the server base URL
        var content  = new StringContent(bundleJson, Encoding.UTF8, "application/fhir+json");
        var response = await client.PostAsync(s.BaseUrl.TrimEnd('/'), content);

        return ((int)response.StatusCode, response.ReasonPhrase ?? string.Empty);
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
}
