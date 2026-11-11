using System.Diagnostics;
using System.IO.Compression;

namespace UDP_App.Services;

public class SushiRunResult
{
    public bool Success { get; set; }
    public string Output { get; set; } = "";
    public string Error { get; set; } = "";
    /// <summary>Files NOT in the support-files list — the study-specific outputs.</summary>
    public List<SushiOutputFile> GeneratedFiles { get; set; } = new();
    /// <summary>Files that are part of the known support-files baseline.</summary>
    public List<SushiOutputFile> SupportFiles { get; set; } = new();
    /// <summary>All generated files (GeneratedFiles + SupportFiles).</summary>
    public List<SushiOutputFile> Files => GeneratedFiles.Concat(SupportFiles).ToList();
}

public class SushiOutputFile
{
    public string FileName { get; set; } = "";
    public string FullPath { get; set; } = "";
}

public class SushiService
{
    private readonly SessionWorkspace      _workspace;
    private readonly ILogger<SushiService> _logger;
    private readonly SettingsService       _settings;

    public SushiService(SessionWorkspace workspace, ILogger<SushiService> logger, SettingsService settings)
    {
        _workspace = workspace;
        _logger    = logger;
        _settings  = settings;
    }

    /// <summary>
    /// The known "support files" baseline — files always generated from the static FSH
    /// definitions (profiles, value sets, code systems, IG). Any file NOT in this set
    /// is a study-specific output produced from the user-supplied FSH.
    /// </summary>
    public static readonly HashSet<string> SupportFileNames = new(StringComparer.OrdinalIgnoreCase)
    {
        "CodeSystem-ncit-cs.json",
        "ImplementationGuide-hl7.fhir.uv.UDP_Examples_ID.json",
        "StructureDefinition-m11-amendment-detail.json",
        "StructureDefinition-m11-amendment-scope-impact.json",
        "StructureDefinition-m11-approval.json",
        "StructureDefinition-m11-composition-order.json",
        "StructureDefinition-m11-confidentiality-statement.json",
        "StructureDefinition-m11-design-table.json",
        "StructureDefinition-m11-estimands.json",
        "StructureDefinition-m11-intercurrent-events.json",
        "StructureDefinition-m11-intervention-table.json",
        "StructureDefinition-m11-intervention.json",
        "StructureDefinition-m11-objective-table.json",
        "StructureDefinition-m11-objective.json",
        "StructureDefinition-m11-protocol-amendment.json",
        "StructureDefinition-m11-protocol-design.json",
        "StructureDefinition-m11-r5-group-ext.json",
        "StructureDefinition-m11-r5-group.json",
        "StructureDefinition-m11-r5-groupCharacteristic-ext.json",
        "StructureDefinition-m11-reporting-table.json",
        "StructureDefinition-m11-reporting.json",
        "StructureDefinition-m11-research-study-narratives.json",
        "StructureDefinition-m11-research-study-profile.json",
        "StructureDefinition-m11-research-study.json",
        "StructureDefinition-narrative-elements.json",
        "StructureDefinition-usdm-estimand-treatment.json",
        "ValueSet-m11-amendment-details-statement-vs.json",
        "ValueSet-m11-amendment-scope-enrollment-vs.json",
        "ValueSet-m11-arm-type-vs.json",
        "ValueSet-m11-blinded-roles-vs.json",
        "ValueSet-m11-blinded-roles-vs.json",
        "ValueSet-m11-control-type-vs.json",
        "ValueSet-m11-country-region-vs.json",
        "ValueSet-m11-event-type-vs.json",
        "ValueSet-m11-imp-nimp-type-vs.json",
        "ValueSet-m11-independent-committees-vs.json",
        "ValueSet-m11-intervention-type-vs.json",
        "ValueSet-m11-intervention-assignment-method-vs.json",
        "ValueSet-m11-interventionModel-vs.json",
        "ValueSet-m11-phase-vs.json",
        "ValueSet-m11-population-type-vs.json",
        "ValueSet-m11-site-distribution-vs.json",
        "ValueSet-m11-site-geographic-scope-vs.json",
        "ValueSet-m11-source-code-vs.json",
        "ValueSet-m11-study-amendment-reason-vs.json",
        "ValueSet-m11-study-amendment-scope-vs.json",
        "ValueSet-udp-target-Maximum-vs.json",
        "ValueSet-m11-trial-blind-schema-vs.json",
        "ValueSet-m11-use-code-vs.json",
        "ValueSet-m11-yes-no-vs.json",
        "ValueSet-udp-address-purpose-type-vs.json",
        "ValueSet-udp-identifier-type-vs.json",
        "ValueSet-udp-narrative-elements-vs.json",
        "ValueSet-udp-party-role-type-vs.json",
        "ValueSet-udp-related-artifact-type-vs.json",
        "ValueSet-udp-section-codes-03-vs.json",
        "ValueSet-udp-section-codes-vs.json",
        "ValueSet-udp-study-title-type-vs.json",
    };

    // Delegates path resolution to the per-session workspace
    public string ProjectRoot             => _workspace.ProjectRoot;
    public string ExamplesFolder          => _workspace.ExamplesFolder;
    public string GeneratedResourcesFolder => _workspace.GeneratedResourcesFolder;

    // The static (shared, read-only) SushiProject — used for static FSH file listings
    private string SourceProjectRoot => _workspace.SourceProjectRoot;

    /// <summary>
    /// Runs SUSHI on the project. If <paramref name="fshFileName"/> and
    /// <paramref name="fshContent"/> are supplied the file is written to the Examples
    /// folder first; otherwise SUSHI compiles whatever files are already there.
    /// Calls <paramref name="onLine"/> for each line of output as it arrives.
    /// </summary>
    public async Task<SushiRunResult> RunAsync(
        string? fshFileName,
        string? fshContent,
        Action<string>? onLine = null,
        CancellationToken ct = default)
    {
        var result = new SushiRunResult();
        var allLines = new System.Text.StringBuilder();

        try
        {
            // 1. Optionally write a new/updated FSH file into the Examples folder
            if (!string.IsNullOrWhiteSpace(fshFileName) && !string.IsNullOrWhiteSpace(fshContent))
            {
                Directory.CreateDirectory(ExamplesFolder);
                var fshPath = Path.Combine(ExamplesFolder, fshFileName);
                await File.WriteAllTextAsync(fshPath, fshContent, ct);
            }

            // 2. Locate SUSHI — configured path takes priority, then PATH search
            var sushiInfo = FindSushiExecutable(_settings.Settings.SushiExePath, onLine);
            if (sushiInfo == null)
            {
                string[] lines = OperatingSystem.IsWindows()
                    ? new[]
                    {
                        "SUSHI not found.",
                        "Install it via the Kudu console:  npm install -g fsh-sushi --prefix D:\\home",
                        "Then set the SUSHI Executable Path in Setup to D:\\home\\sushi.cmd",
                    }
                    : new[]
                    {
                        "SUSHI not found.",
                        "Install it with:  npm install -g fsh-sushi",
                        "Then set the SUSHI Executable Path in Setup if needed.",
                        "See https://fshschool.org/docs/sushi/installation/",
                    };
                foreach (var l in lines) onLine?.Invoke("[ERROR] " + l);
                result.Error = string.Join(" ", lines);
                return result;
            }

            var (sushiExe, sushiArgs) = sushiInfo.Value;

            // 3. Run SUSHI in the project root
            var psi = new ProcessStartInfo
            {
                FileName               = sushiExe,
                Arguments              = sushiArgs,
                WorkingDirectory       = ProjectRoot,
                RedirectStandardOutput = true,
                RedirectStandardError  = true,
                UseShellExecute        = false,
                CreateNoWindow         = true,
            };

            onLine?.Invoke($"[INFO] Running: {sushiExe} {sushiArgs}".TrimEnd());

            using var proc = new Process { StartInfo = psi };
            proc.Start();

            async Task DrainAsync(StreamReader reader, string prefix)
            {
                string? line;
                while ((line = await reader.ReadLineAsync(ct)) != null)
                {
                    var tagged = string.IsNullOrEmpty(prefix) ? line : $"[{prefix}] {line}";
                    allLines.AppendLine(tagged);
                    onLine?.Invoke(tagged);
                }
            }

            var stdOutTask = DrainAsync(proc.StandardOutput, "");
            var stdErrTask = DrainAsync(proc.StandardError, "");

            await Task.WhenAll(stdOutTask, stdErrTask);
            await proc.WaitForExitAsync(ct);

            result.Output  = allLines.ToString();
            result.Success = proc.ExitCode == 0;

            // 4. Collect generated resource files, split into study outputs vs support baseline
            if (Directory.Exists(GeneratedResourcesFolder))
            {
                var all = Directory.GetFiles(GeneratedResourcesFolder, "*.json")
                    .Select(p => new SushiOutputFile
                    {
                        FileName = Path.GetFileName(p),
                        FullPath = p
                    })
                    .OrderBy(f => f.FileName)
                    .ToList();

                result.GeneratedFiles = all
                    .Where(f => !SupportFileNames.Contains(f.FileName))
                    .ToList();
                result.SupportFiles = all
                    .Where(f => SupportFileNames.Contains(f.FileName))
                    .ToList();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SUSHI run failed");
            result.Error = ex.Message;
            onLine?.Invoke($"[ERROR] {ex.Message}");
        }

        return result;
    }

    /// <summary>Zips all generated resource files and returns the zip as bytes.</summary>
    public byte[]? ReadAllGeneratedAsZip(IEnumerable<string> fileNames)
    {
        var files = fileNames.ToList();
        if (files.Count == 0) return null;

        using var ms  = new System.IO.MemoryStream();
        using var zip = new System.IO.Compression.ZipArchive(ms, System.IO.Compression.ZipArchiveMode.Create, leaveOpen: true);
        foreach (var fileName in files)
        {
            var safe = Path.GetFileName(fileName);
            if (string.IsNullOrEmpty(safe)) continue;
            var path = Path.Combine(GeneratedResourcesFolder, safe);
            if (!File.Exists(path)) continue;
            var entry = zip.CreateEntry(safe, System.IO.Compression.CompressionLevel.Optimal);
            using var es = entry.Open();
            using var fs = File.OpenRead(path);
            fs.CopyTo(es);
        }
        zip.Dispose();
        return ms.ToArray();
    }

    /// <summary>Reads one generated resource file as bytes for download.</summary>
    public byte[]? ReadGeneratedFile(string fileName)
    {
        if (fileName.Contains('/') || fileName.Contains('\\') || fileName.Contains(".."))
            return null;
        var path = Path.Combine(GeneratedResourcesFolder, fileName);
        return File.Exists(path) ? File.ReadAllBytes(path) : null;
    }

    /// <summary>Returns the content of sushi-config.yaml for display/editing.</summary>
    public string ReadConfig()
    {
        var path = Path.Combine(ProjectRoot, "sushi-config.yaml");
        return File.Exists(path) ? File.ReadAllText(path) : "";
    }

    /// <summary>Saves edited sushi-config.yaml content.</summary>
    public void SaveConfig(string content)
    {
        var path = Path.Combine(ProjectRoot, "sushi-config.yaml");
        File.WriteAllText(path, content);
    }

    /// <summary>Lists the static .fsh files in input/fsh (not Examples) for display.</summary>
    public List<string> ListStaticFshFiles()
    {
        var fshRoot = Path.Combine(SourceProjectRoot, "input", "fsh");
        if (!Directory.Exists(fshRoot)) return new();
        return Directory.GetFiles(fshRoot, "*.fsh", SearchOption.AllDirectories)
            .Where(p => !p.Contains(Path.DirectorySeparatorChar + "Examples" + Path.DirectorySeparatorChar))
            .Select(p => Path.GetRelativePath(fshRoot, p))
            .OrderBy(n => n)
            .ToList();
    }

    /// <summary>Reads a single static FSH file (relative path from fsh root) as bytes.</summary>
    public byte[]? ReadStaticFshFile(string relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath) || relativePath.Contains("..")) return null;
        var fshRoot = Path.Combine(SourceProjectRoot, "input", "fsh");
        var full    = Path.GetFullPath(Path.Combine(fshRoot, relativePath));
        if (!full.StartsWith(fshRoot, StringComparison.OrdinalIgnoreCase)) return null;
        return File.Exists(full) ? File.ReadAllBytes(full) : null;
    }

    /// <summary>Zips all static FSH files and returns the zip bytes.</summary>
    public byte[]? ReadAllStaticFshAsZip()
    {
        var fshRoot = Path.Combine(SourceProjectRoot, "input", "fsh");
        if (!Directory.Exists(fshRoot)) return null;
        var files = Directory.GetFiles(fshRoot, "*.fsh", SearchOption.AllDirectories)
            .Where(p => !p.Contains(Path.DirectorySeparatorChar + "Examples" + Path.DirectorySeparatorChar))
            .OrderBy(p => p).ToList();
        if (files.Count == 0) return null;

        using var ms  = new System.IO.MemoryStream();
        using var zip = new System.IO.Compression.ZipArchive(ms, System.IO.Compression.ZipArchiveMode.Create, leaveOpen: true);
        foreach (var file in files)
        {
            var entryName = Path.GetRelativePath(fshRoot, file).Replace('\\', '/');
            var entry = zip.CreateEntry(entryName, System.IO.Compression.CompressionLevel.Optimal);
            using var es = entry.Open();
            using var fs = File.OpenRead(file);
            fs.CopyTo(es);
        }
        zip.Dispose();
        return ms.ToArray();
    }

    // ── Examples folder management ─────────────────────────────────────────────

    public byte[]? ReadExampleFile(string fileName)
    {
        var safe = Path.GetFileName(fileName);
        if (string.IsNullOrEmpty(safe) || safe.Contains("..")) return null;
        var path = Path.Combine(ExamplesFolder, safe);
        return File.Exists(path) ? File.ReadAllBytes(path) : null;
    }

    public byte[]? ReadAllExamplesAsZip()
    {
        if (!Directory.Exists(ExamplesFolder)) return null;
        var files = Directory.GetFiles(ExamplesFolder, "*.fsh", SearchOption.TopDirectoryOnly)
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

    public string SaveToExamples(string fileName, string content)
    {
        Directory.CreateDirectory(ExamplesFolder);
        var safe = Path.GetFileName(fileName);
        if (!safe.EndsWith(".fsh", StringComparison.OrdinalIgnoreCase))
            safe += ".fsh";
        var path = Path.Combine(ExamplesFolder, safe);
        File.WriteAllText(path, content);
        return safe;
    }

    public int CopyGeneratedToMainExamples(IEnumerable<SushiOutputFile> studyFiles)
    {
        var dest = _workspace.BuildOutputFolder;
        if (string.IsNullOrWhiteSpace(dest)) return 0;
        Directory.CreateDirectory(dest);
        int count = 0;
        foreach (var file in studyFiles)
        {
            if (!File.Exists(file.FullPath)) continue;
            File.Copy(file.FullPath, Path.Combine(dest, file.FileName), overwrite: true);
            count++;
        }
        return count;
    }

    public List<string> ListExampleFiles()
    {
        if (!Directory.Exists(ExamplesFolder)) return new();
        return Directory.GetFiles(ExamplesFolder, "*.fsh")
            .Select(Path.GetFileName)
            .OfType<string>()
            .OrderBy(n => n)
            .ToList();
    }

    public bool DeleteExampleFile(string fileName)
    {
        var safe = Path.GetFileName(fileName);
        if (string.IsNullOrWhiteSpace(safe) || safe.Contains("..")) return false;
        var path = Path.Combine(ExamplesFolder, safe);
        if (!File.Exists(path)) return false;
        File.Delete(path);
        return true;
    }

    public string ImportExampleFile(string fileName, byte[] content)
    {
        Directory.CreateDirectory(ExamplesFolder);
        var safe = Path.GetFileName(fileName);
        if (!safe.EndsWith(".fsh", StringComparison.OrdinalIgnoreCase))
            safe += ".fsh";
        File.WriteAllBytes(Path.Combine(ExamplesFolder, safe), content);
        return safe;
    }

    public int ClearExamplesFolder()
    {
        if (!Directory.Exists(ExamplesFolder)) return 0;
        int count = 0;
        foreach (var file in Directory.GetFiles(ExamplesFolder, "*.fsh", SearchOption.TopDirectoryOnly))
        {
            File.Delete(file);
            count++;
        }
        return count;
    }

    // ── Helpers ────────────────────────────────────────────────────────────────

    /// <summary>
    /// Locates the SUSHI executable. Checks the configured path first, then searches PATH.
    /// Returns (executable, arguments) ready to pass to ProcessStartInfo, or null if not found.
    /// </summary>
    private static (string exe, string args)? FindSushiExecutable(string? configuredPath, Action<string>? onLine)
    {
        if (OperatingSystem.IsWindows())
        {
            // 1. Configured path
            if (!string.IsNullOrWhiteSpace(configuredPath))
            {
                if (File.Exists(configuredPath))
                {
                    onLine?.Invoke($"[INFO] Using configured SUSHI path: {configuredPath}");
                    return ("cmd.exe", $"/c \"{configuredPath}\"");
                }
                onLine?.Invoke($"[WARN] Configured SUSHI path not found: {configuredPath}");
            }

            // 2. Search PATH
            var fromPath = FindOnPath("sushi.cmd") ?? FindOnPath("sushi");
            if (fromPath != null)
            {
                onLine?.Invoke($"[INFO] Found SUSHI on PATH: {fromPath}");
                return ("cmd.exe", $"/c \"{fromPath}\"");
            }

            return null;
        }
        else
        {
            // 1. Configured path
            if (!string.IsNullOrWhiteSpace(configuredPath))
            {
                if (File.Exists(configuredPath))
                {
                    onLine?.Invoke($"[INFO] Using configured SUSHI path: {configuredPath}");
                    return (configuredPath, "");
                }
                onLine?.Invoke($"[WARN] Configured SUSHI path not found: {configuredPath}");
            }

            // 2. Search PATH
            var fromPath = FindOnPath("sushi");
            if (fromPath != null)
            {
                onLine?.Invoke($"[INFO] Found SUSHI on PATH: {fromPath}");
                return (fromPath, "");
            }

            return null;
        }
    }

    private static string? FindOnPath(string exe)
    {
        var path = Environment.GetEnvironmentVariable("PATH") ?? "";
        foreach (var dir in path.Split(Path.PathSeparator))
        {
            var full = Path.Combine(dir.Trim(), exe);
            if (File.Exists(full)) return full;
        }
        return null;
    }
}
