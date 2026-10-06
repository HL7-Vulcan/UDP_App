using Microsoft.AspNetCore.Hosting;
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
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<SushiService> _logger;

    public SushiService(IWebHostEnvironment env, ILogger<SushiService> logger)
    {
        _env = env;
        _logger = logger;
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
        "ValueSet-m11-blindedRoles-vs.json",
        "ValueSet-m11-controlType-vs.json",
        "ValueSet-m11-country-region-vs.json",
        "ValueSet-m11-event-type-vs.json",
        "ValueSet-m11-imp-nimp-type-vs.json",
        "ValueSet-m11-independentCommittees-vs.json",
        "ValueSet-m11-intervention-type-vs.json",
        "ValueSet-m11-interventionAssignmentMethod-vs.json",
        "ValueSet-m11-interventionModel-vs.json",
        "ValueSet-m11-phase-vs.json",
        "ValueSet-m11-populationType-vs.json",
        "ValueSet-m11-siteDistribution-vs.json",
        "ValueSet-m11-siteGeographicscope-vs.json",
        "ValueSet-m11-source-code-vs.json",
        "ValueSet-m11-study-amendment-reason-vs.json",
        "ValueSet-m11-study-amendment-scope-vs.json",
        "ValueSet-m11-target-Maximum-vs.json",
        "ValueSet-m11-trialBlindSchema-vs.json",
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

    // Path to the bundled SushiProject folder (contains sushi-config.yaml, input/fsh/**, etc.)
    public string ProjectRoot => Path.Combine(_env.ContentRootPath, "SushiProject");

    // The Examples subfolder where the generated .fsh file is written before running sushi
    public string ExamplesFolder => Path.Combine(ProjectRoot, "input", "fsh", "Examples");

    // The fsh-generated/resources output folder
    public string GeneratedResourcesFolder => Path.Combine(ProjectRoot, "fsh-generated", "resources");

    private static readonly SemaphoreSlim _nodeInstallLock = new(1, 1);

    const string NodeBin   = "/home/node/bin/node";
    const string NpmBin    = "/home/node/bin/npm";
    const string SushiPath = "/home/bin/sushi";

    /// <summary>
    /// On Azure App Service the app container does not have Node.js or fsh-sushi —
    /// only the Kudu/SCM sidecar does. This installs both into /home (the only
    /// persistent, writable volume) on first call, and returns immediately on
    /// subsequent calls once the binaries are confirmed present.
    /// </summary>
    private static async Task EnsureNodeAndSushiInstalledAsync(Action<string>? onLine, CancellationToken ct)
    {
        bool needNode  = !CanExecute(NodeBin);
        bool needSushi = !File.Exists(SushiPath);

        if (!needNode && !needSushi) return;

        await _nodeInstallLock.WaitAsync(ct);
        try
        {
            needNode  = !CanExecute(NodeBin);
            needSushi = !File.Exists(SushiPath);
            if (!needNode && !needSushi) return;

            var script = new System.Text.StringBuilder();
            script.AppendLine("#!/bin/bash");
            script.AppendLine("set -e");

            if (needNode)
            {
                onLine?.Invoke("[INFO] Node.js not found — installing to /home/node (this takes ~30s on first run)...");
                script.AppendLine("""
                    NODE_VERSION=20
                    ARCH=x64
                    DEST=/home/node
                    mkdir -p "$DEST"
                    cd /tmp
                    INDEX=$(curl -sS "https://nodejs.org/dist/latest-v${NODE_VERSION}.x/" 2>/dev/null)
                    TAR=$(echo "$INDEX" | grep -oP "node-v[\d.]+-linux-${ARCH}\.tar\.gz" | head -1)
                    if [ -z "$TAR" ]; then echo "ERROR: could not find Node tarball"; exit 1; fi
                    echo "[node] Downloading $TAR ..."
                    curl -sS "https://nodejs.org/dist/latest-v${NODE_VERSION}.x/$TAR" -o "$TAR"
                    tar -xzf "$TAR" --strip-components=1 -C "$DEST"
                    rm "$TAR"
                    echo "[node] Installed: $($DEST/bin/node --version)"
                    """);
            }

            if (needSushi)
            {
                onLine?.Invoke("[INFO] fsh-sushi not found — installing to /home/bin ...");
                script.AppendLine("""
                    export PATH="/home/node/bin:$PATH"
                    echo "[sushi] Installing fsh-sushi ..."
                    /home/node/bin/npm install -g fsh-sushi --prefix /home 2>&1
                    echo "[sushi] Installed: $(/home/bin/sushi --version 2>/dev/null || echo 'version check failed')"
                    """);
            }

            var scriptPath = Path.Combine(Path.GetTempPath(), "install_node_sushi.sh");
            await File.WriteAllTextAsync(scriptPath, script.ToString(), ct);

            var psi = new ProcessStartInfo
            {
                FileName               = "/bin/sh",
                Arguments              = scriptPath,
                RedirectStandardOutput = true,
                RedirectStandardError  = true,
                UseShellExecute        = false,
            };

            using var proc = Process.Start(psi);
            if (proc != null)
            {
                proc.OutputDataReceived += (_, e) => { if (e.Data != null) onLine?.Invoke($"[install] {e.Data}"); };
                proc.ErrorDataReceived  += (_, e) => { if (e.Data != null) onLine?.Invoke($"[install] {e.Data}"); };
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
                await proc.WaitForExitAsync(ct);

                if (proc.ExitCode == 0)
                    onLine?.Invoke("[INFO] Installation complete.");
                else
                    onLine?.Invoke($"[WARN] Install script exited with code {proc.ExitCode}.");
            }

            try { File.Delete(scriptPath); } catch { }
        }
        finally
        {
            _nodeInstallLock.Release();
        }
    }

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

            // 1b. On Azure the app container has no Node.js or sushi — install both on demand.
            if (!OperatingSystem.IsWindows())
                await EnsureNodeAndSushiInstalledAsync(onLine, ct);

            // 2. Locate sushi executable (and node if needed)
            var sushiInfo = FindSushiExecutable();
            if (sushiInfo == null)
            {
                var msg = "SUSHI not found. Please install it with: npm install -g fsh-sushi\n" +
                          "See https://fshschool.org/docs/sushi/installation/";
                onLine?.Invoke(msg);
                result.Error = msg;
                return result;
            }

            var (sushiExe, argPrefix) = sushiInfo.Value;

            // 3. Run sushi in the project root
            var psi = new ProcessStartInfo
            {
                FileName               = sushiExe,
                Arguments              = string.IsNullOrEmpty(argPrefix) ? "" : argPrefix,
                WorkingDirectory       = ProjectRoot,
                RedirectStandardOutput = true,
                RedirectStandardError  = true,
                UseShellExecute        = false,
                CreateNoWindow         = true,
            };

            // Diagnostics — show environment before we do anything
            onLine?.Invoke($"[INFO] OS: {System.Runtime.InteropServices.RuntimeInformation.OSDescription}");
            onLine?.Invoke($"[INFO] App PATH: {Environment.GetEnvironmentVariable("PATH") ?? "(null)"}");
            onLine?.Invoke($"[INFO] /usr/local/bin/node FileInfo.Exists: {new FileInfo("/usr/local/bin/node").Exists}");
            onLine?.Invoke($"[INFO] /usr/bin/node FileInfo.Exists: {new FileInfo("/usr/bin/node").Exists}");
            try { onLine?.Invoke($"[INFO] CanOpenRead /usr/local/bin/node: {CanExecute("/usr/local/bin/node")}"); } catch { }

            // Find node explicitly — Azure App Service strips PATH so 'node' isn't
            // reachable even though it is installed somewhere on disk.
            var nodeExeFound = FindNodeExecutable();
            onLine?.Invoke($"[INFO] Running: {sushiExe} {argPrefix}");
            onLine?.Invoke($"[INFO] node found at: {nodeExeFound ?? "NOT FOUND"}");

            // /home/bin/sushi IS the JS file (starts with #!/usr/bin/env node).
            // If node was found, invoke: node /home/bin/sushi  (no js-entry lookup needed)
            if (nodeExeFound != null && (sushiExe.EndsWith("/sushi") || sushiExe.EndsWith("\\sushi")))
            {
                // Check first line to decide if it's already JS or a shell wrapper
                var firstLine = "";
                try { firstLine = File.ReadLines(sushiExe).FirstOrDefault() ?? ""; } catch { }
                onLine?.Invoke($"[INFO] sushi first line: {firstLine}");

                if (firstLine.Contains("node"))
                {
                    // It IS the JS entry — invoke directly
                    psi.FileName  = nodeExeFound;
                    psi.Arguments = $"\"{sushiExe}\"";
                    onLine?.Invoke($"[INFO] Invoking as: {nodeExeFound} \"{sushiExe}\"");
                }
                else
                {
                    var jsEntry = FindSushiJsEntry(sushiExe);
                    onLine?.Invoke($"[INFO] sushi JS entry: {jsEntry ?? "NOT FOUND"}");
                    if (jsEntry != null)
                    {
                        psi.FileName  = nodeExeFound;
                        psi.Arguments = $"\"{jsEntry}\"";
                    }
                }

                // Ensure node's directory is on the child PATH too
                var nodeDir = Path.GetDirectoryName(nodeExeFound) ?? "";
                var currentPath2 = Environment.GetEnvironmentVariable("PATH") ?? "";
                psi.Environment["PATH"] = nodeDir + ":/usr/local/bin:/usr/bin:/bin:" + currentPath2;
            }
            else if (nodeExeFound == null)
            {
                // Last-ditch: force-set PATH to all plausible node locations
                // and just try running sushi directly (it will fail with a useful error)
                onLine?.Invoke("[WARN] node not found — forcing PATH and attempting direct run");
                psi.Environment["PATH"] =
                    "/usr/local/bin:/usr/bin:/bin:/usr/local/sbin:/home/bin:/opt/nodejs/bin:/usr/local/nodejs/bin";
            }

            using var proc = new Process { StartInfo = psi };
            proc.Start();

            // Stream stdout and stderr concurrently, calling onLine for each line
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
            var stdErrTask = DrainAsync(proc.StandardError, "ERR");

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

    /// <summary>Reads one generated resource file as bytes for download.</summary>
    public byte[]? ReadGeneratedFile(string fileName)
    {
        // Sanitise: only allow simple filenames, no path traversal
        if (fileName.Contains('/') || fileName.Contains('\\') || fileName.Contains(".."))
            return null;

        var path = Path.Combine(GeneratedResourcesFolder, fileName);
        return File.Exists(path) ? File.ReadAllBytes(path) : null;
    }

    /// <summary>
    /// Returns the content of sushi-config.yaml for display/editing.
    /// </summary>
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
        var fshRoot = Path.Combine(ProjectRoot, "input", "fsh");
        if (!Directory.Exists(fshRoot)) return new();
        return Directory.GetFiles(fshRoot, "*.fsh", SearchOption.AllDirectories)
            .Where(p => !p.Contains(Path.DirectorySeparatorChar + "Examples" + Path.DirectorySeparatorChar))
            .Select(p => Path.GetRelativePath(fshRoot, p))
            .OrderBy(n => n)
            .ToList();
    }

    // ── Examples folder management ─────────────────────────────────────────────

    /// <summary>
    /// Saves FSH content into the Examples folder (used when a section saves in FHIR format).
    /// Returns the full path written.
    /// </summary>
    public string SaveToExamples(string fileName, string content)
    {
        Directory.CreateDirectory(ExamplesFolder);
        var safe = Path.GetFileName(fileName); // strip any directory component
        if (!safe.EndsWith(".fsh", StringComparison.OrdinalIgnoreCase))
            safe += ".fsh";
        var path = Path.Combine(ExamplesFolder, safe);
        File.WriteAllText(path, content);
        return safe;
    }

    /// <summary>Lists all .fsh files currently in the Examples folder.</summary>
    public List<string> ListExampleFiles()
    {
        if (!Directory.Exists(ExamplesFolder)) return new();
        return Directory.GetFiles(ExamplesFolder, "*.fsh")
            .Select(Path.GetFileName)
            .OfType<string>()
            .OrderBy(n => n)
            .ToList();
    }

    /// <summary>Deletes a .fsh file from the Examples folder. Returns false if not found / path unsafe.</summary>
    public bool DeleteExampleFile(string fileName)
    {
        var safe = Path.GetFileName(fileName);
        if (string.IsNullOrWhiteSpace(safe) || safe.Contains("..")) return false;
        var path = Path.Combine(ExamplesFolder, safe);
        if (!File.Exists(path)) return false;
        File.Delete(path);
        return true;
    }

    /// <summary>Imports a .fsh file into the Examples folder from supplied content bytes.</summary>
    public string ImportExampleFile(string fileName, byte[] content)
    {
        Directory.CreateDirectory(ExamplesFolder);
        var safe = Path.GetFileName(fileName);
        if (!safe.EndsWith(".fsh", StringComparison.OrdinalIgnoreCase))
            safe += ".fsh";
        File.WriteAllBytes(Path.Combine(ExamplesFolder, safe), content);
        return safe;
    }

    // ── Helpers ────────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns (executable, arguments-prefix) to invoke SUSHI.
    /// On Linux/Azure we may need to call "node /path/to/sushi-cli.js" directly
    /// because the .NET process PATH often lacks the npm-global bin directory.
    /// </summary>
    private static (string exe, string argPrefix)? FindSushiExecutable()
    {
        if (OperatingSystem.IsWindows())
        {
            // Windows: sushi.cmd is a batch wrapper; invoke via cmd /c
            var inPath = FindOnPath("sushi.cmd");
            if (inPath != null) return ("cmd.exe", $"/c \"{inPath}\"");

            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var candidates = new[]
            {
                Path.Combine(appData, "npm", "sushi.cmd"),
                @"C:\Program Files\nodejs\sushi.cmd",
                @"C:\Users\Default\AppData\Roaming\npm\sushi.cmd",
            };
            var found = candidates.FirstOrDefault(File.Exists);
            return found != null ? ("cmd.exe", $"/c \"{found}\"") : null;
        }
        else
        {
            // Linux/macOS: try sushi script on PATH or well-known locations.
            // If node isn't on the .NET process PATH the script will fail with
            // "env: 'node': No such file or directory", so we find node and the
            // sushi JS entry-point and invoke them directly as a fallback.

            // 1. Try sushi directly (works when node is on PATH)
            var sushiCandidates = new[]
            {
                FindOnPath("sushi"),
                "/home/bin/sushi",            // Azure App Service npm --prefix /home
                "/usr/local/bin/sushi",
                "/usr/bin/sushi",
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                             ".npm-global", "bin", "sushi"),
            };

            var sushiScript = sushiCandidates.FirstOrDefault(p => p != null && File.Exists(p));

            // 2. Find node executable
            var nodeCandidates = new[]
            {
                FindOnPath("node"),
                "/usr/local/bin/node",
                "/usr/bin/node",
                "/home/bin/node",
                "/opt/nodejs/bin/node",
                "/usr/local/nodejs/bin/node",
            };
            var nodeExe = nodeCandidates.FirstOrDefault(p => p != null && File.Exists(p));

            if (nodeExe != null && sushiScript != null)
            {
                // Check if the sushi "script" is itself a JS file (starts with #!/usr/bin/env node).
                // When installed with --prefix /home, npm puts the compiled JS directly in /home/bin/sushi
                // with no separate wrapper — so we run: node /home/bin/sushi
                var firstLine = "";
                try { firstLine = File.ReadLines(sushiScript).FirstOrDefault() ?? ""; } catch { }

                if (firstLine.Contains("node"))
                {
                    // It's a JS file — invoke it directly with node
                    return (nodeExe, $"\"{sushiScript}\"");
                }

                // It's a shell wrapper — try to extract the JS entry point from it
                var jsEntry = FindSushiJsEntry(sushiScript);
                if (jsEntry != null)
                    return (nodeExe, $"\"{jsEntry}\"");

                // Fallback: run the shell script as-is (works locally where env finds node)
                return (sushiScript, "");
            }

            // Last resort: try running the script anyway (works locally)
            if (sushiScript != null) return (sushiScript, "");

            return null;
        }
    }

    /// <summary>
    /// Reads the sushi shell wrapper and extracts the path to the JS entry point,
    /// e.g. /home/lib/node_modules/fsh-sushi/built/app.js
    /// </summary>
    private static string? FindSushiJsEntry(string sushiScriptPath)
    {
        try
        {
            var dir = Path.GetDirectoryName(sushiScriptPath) ?? "";

            // 1. Relative paths from the bin/ directory containing the sushi wrapper
            var relatives = new[]
            {
                "../lib/node_modules/fsh-sushi/built/app.js",
                "../lib/node_modules/fsh-sushi/dist/app.js",
            };
            foreach (var rel in relatives)
            {
                var candidate = Path.GetFullPath(Path.Combine(dir, rel));
                if (File.Exists(candidate)) return candidate;
            }

            // 2. Absolute well-known locations (covers Azure --prefix /home install)
            var absolutes = new[]
            {
                "/home/lib/node_modules/fsh-sushi/built/app.js",
                "/home/lib/node_modules/fsh-sushi/dist/app.js",
                "/usr/local/lib/node_modules/fsh-sushi/built/app.js",
                "/usr/local/lib/node_modules/fsh-sushi/dist/app.js",
                "/usr/lib/node_modules/fsh-sushi/built/app.js",
            };
            var abs = absolutes.FirstOrDefault(File.Exists);
            if (abs != null) return abs;

            // 3. Parse the shell script for the node invocation line
            var lines = File.ReadAllLines(sushiScriptPath);
            foreach (var line in lines)
            {
                var m = System.Text.RegularExpressions.Regex.Match(
                    line, @"node\s+[""']?([^\s""']+\.js)[""']?");
                if (m.Success)
                {
                    var raw = m.Groups[1].Value
                        .Replace("$basedir", dir)
                        .Replace("$dir", dir);
                    if (File.Exists(raw)) return raw;
                }
            }
        }
        catch { /* ignore */ }
        return null;
    }

    /// <summary>
    /// Finds the node executable by checking PATH and then a broad set of known
    /// install locations, including Azure App Service's non-standard paths.
    /// </summary>
    private static string? FindNodeExecutable()
    {
        // Strategy 1: try 'which node' via shell — works if Process.Start is allowed
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName               = "/bin/sh",
                Arguments              = "-c \"which node 2>/dev/null || command -v node 2>/dev/null\"",
                RedirectStandardOutput = true,
                RedirectStandardError  = true,
                UseShellExecute        = false,
            };
            psi.Environment["PATH"] =
                "/home/node/bin:/usr/local/bin:/usr/bin:/bin:/usr/local/sbin:/usr/sbin:/sbin:/home/bin";

            using var proc = Process.Start(psi);
            if (proc != null)
            {
                var output = proc.StandardOutput.ReadToEnd().Trim();
                proc.WaitForExit();
                if (!string.IsNullOrEmpty(output) && output.Contains("node"))
                    return output.Split('\n')[0].Trim();
            }
        }
        catch { /* fall through */ }

        // Strategy 2: resolve symlinks manually via /proc/self/fd or realpath
        // On Azure, /usr/local/bin/node is a symlink. FileInfo.Exists() doesn't care about
        // whether it's a symlink — if the target exists the symlink reports Exists=true.
        // The real problem is File.Exists() does not handle dangling symlinks well.
        // Try each candidate using both FileInfo and a direct open attempt.
        var candidates = new[]
        {
            "/home/node/bin/node",          // installed by startup.sh into /home (persistent)
            "/usr/local/bin/node",
            "/usr/bin/node",
            "/home/bin/node",
            "/opt/nodejs/bin/node",
            "/usr/local/nodejs/bin/node",
            "/usr/local/nvm/versions/node/*/bin/node",  // nvm-style
        };

        foreach (var pattern in candidates)
        {
            if (pattern.Contains('*'))
            {
                // Glob expansion
                var dir = Path.GetDirectoryName(pattern) ?? "";
                var file = Path.GetFileName(pattern);
                if (Directory.Exists(Path.GetDirectoryName(dir)))
                {
                    foreach (var d in Directory.GetDirectories(Path.GetDirectoryName(dir) ?? "", "*"))
                    {
                        var p = Path.Combine(d, "bin", "node");
                        if (CanExecute(p)) return p;
                    }
                }
                continue;
            }
            if (CanExecute(pattern)) return pattern;
        }

        return null;
    }

    private static bool CanExecute(string path)
    {
        try
        {
            // FileInfo.Exists follows symlinks (unlike File.Exists in some edge cases)
            var fi = new FileInfo(path);
            if (!fi.Exists) return false;
            // Extra check: try to open the file — proves it's readable even if it's a symlink
            using var fs = File.OpenRead(path);
            return fs.CanRead;
        }
        catch { return false; }
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
