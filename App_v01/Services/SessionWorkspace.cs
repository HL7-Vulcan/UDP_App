using Microsoft.AspNetCore.Hosting;

namespace UDP_App.Services;

/// <summary>
/// Scoped service: one instance per Blazor circuit (browser tab).
/// Creates an isolated working directory for this session so simultaneous
/// users cannot interfere with each other's file operations.
///
/// The workspace is structured as:
///   {TempRoot}/{SessionId}/
///     SushiProject/          ← per-session copy of the static scaffold
///       input/fsh/
///         Examples/          ← user .fsh files written here before SUSHI runs
///       sushi-config.yaml
///       ...
///     BuildOutput/           ← FHIR JSON written here after SUSHI; what Build page reads
///
/// On Dispose (circuit disconnect) the entire {SessionId}/ tree is deleted.
/// </summary>
public sealed class SessionWorkspace : IDisposable
{
    public string SessionId { get; } = Guid.NewGuid().ToString("N");

    /// <summary>Root of the static SushiProject bundled with the app (read-only, shared).</summary>
    public string SourceProjectRoot { get; }

    /// <summary>Per-session copy of SushiProject used for FSH editing and SUSHI execution.</summary>
    public string ProjectRoot { get; }

    /// <summary>Per-session FSH Examples folder (writable, starts empty).</summary>
    public string ExamplesFolder => Path.Combine(ProjectRoot, "input", "fsh", "Examples");

    /// <summary>Per-session fsh-generated/resources folder (SUSHI output).</summary>
    public string GeneratedResourcesFolder => Path.Combine(ProjectRoot, "fsh-generated", "resources");

    /// <summary>
    /// Per-session Build output folder — where generated FHIR JSON is copied after a
    /// successful SUSHI run, and what the Build page reads. Replaces the global
    /// SettingsService.Settings.ExamplesFolder for file I/O so sessions are isolated.
    /// </summary>
    public string BuildOutputFolder { get; }

    private bool _disposed;

    public SessionWorkspace(IWebHostEnvironment env, ILogger<SessionWorkspace> logger)
    {
        SourceProjectRoot = Path.Combine(env.ContentRootPath, "SushiProject");

        var sessionRoot   = Path.Combine(Path.GetTempPath(), "udp_sessions", SessionId);
        ProjectRoot       = Path.Combine(sessionRoot, "SushiProject");
        BuildOutputFolder = Path.Combine(sessionRoot, "BuildOutput");

        try
        {
            CopyScaffold(SourceProjectRoot, ProjectRoot);
            Directory.CreateDirectory(ExamplesFolder);
            Directory.CreateDirectory(BuildOutputFolder);
            logger.LogInformation("SessionWorkspace created: {Path}", sessionRoot);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create SessionWorkspace at {Path}", sessionRoot);
        }
    }

    /// <summary>
    /// Recursively copies the SushiProject scaffold to the session directory,
    /// skipping the Examples folder content (we want an empty Examples folder)
    /// and skipping fsh-generated/ (runtime output).
    /// </summary>
    private static void CopyScaffold(string source, string dest)
    {
        if (!Directory.Exists(source)) return;

        Directory.CreateDirectory(dest);

        foreach (var file in Directory.GetFiles(source))
            File.Copy(file, Path.Combine(dest, Path.GetFileName(file)), overwrite: true);

        foreach (var dir in Directory.GetDirectories(source))
        {
            var dirName = Path.GetFileName(dir);

            // Skip generated output — SUSHI recreates it
            if (dirName.Equals("fsh-generated", StringComparison.OrdinalIgnoreCase))
                continue;

            // Skip Examples content — session starts empty
            if (dirName.Equals("Examples", StringComparison.OrdinalIgnoreCase))
                continue;

            CopyScaffold(dir, Path.Combine(dest, dirName));
        }
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        // Best-effort cleanup — don't throw on logout/disconnect
        var sessionDir = Directory.GetParent(ProjectRoot)?.FullName;
        if (sessionDir != null && Directory.Exists(sessionDir))
        {
            try { Directory.Delete(sessionDir, recursive: true); }
            catch { /* ignore — OS will clean temp eventually */ }
        }
    }
}
