using UDP_App.Services;

namespace UDP_App.Services;

public class FhirPipelineResult
{
    public bool   Success      { get; set; }
    public string Step         { get; set; } = "";   // which step failed
    public string ErrorMessage { get; set; } = "";
    public int    Filescopied  { get; set; }
}

/// <summary>
/// Orchestrates the three-step FHIR publish pipeline:
///   1. Save the section's FSH content to the Examples folder
///   2. Run SUSHI to compile FSH → FHIR JSON
///   3. Run GenerateBundle to bundle the generated resources
/// Each step is logged via <paramref name="onLine"/>. The pipeline stops immediately
/// if any step fails or reports an error.
/// </summary>
public class FhirPipelineService
{
    private readonly SushiService   _sushi;
    private readonly BuildService   _build;
    private readonly SettingsService _settings;

    public FhirPipelineService(SushiService sushi, BuildService build, SettingsService settings)
    {
        _sushi    = sushi;
        _build    = build;
        _settings = settings;
    }

    public async Task<FhirPipelineResult> RunAsync(
        string fshFileName,
        string fshContent,
        Action<string> onLine,
        CancellationToken ct = default)
    {
        var result = new FhirPipelineResult();

        // ── Step 1: Save FSH ─────────────────────────────────────────────
        onLine("[STEP 1/3] Saving FSH to Examples folder…");
        try
        {
            var saved = _sushi.SaveToExamples(fshFileName, fshContent);
            onLine($"[OK] Saved → {saved}");
        }
        catch (Exception ex)
        {
            result.Step         = "Save FSH";
            result.ErrorMessage = ex.Message;
            onLine($"[ERROR] {ex.Message}");
            return result;
        }

        // ── Step 2: Run SUSHI ────────────────────────────────────────────
        onLine("[STEP 2/3] Running SUSHI…");
        SushiRunResult sushiResult;
        try
        {
            sushiResult = await _sushi.RunAsync(null, null, onLine, ct);
        }
        catch (Exception ex)
        {
            result.Step         = "SUSHI";
            result.ErrorMessage = ex.Message;
            onLine($"[ERROR] {ex.Message}");
            return result;
        }

        if (!sushiResult.Success)
        {
            result.Step         = "SUSHI";
            result.ErrorMessage = string.IsNullOrEmpty(sushiResult.Error)
                ? "SUSHI exited with errors — see output above."
                : sushiResult.Error;
            onLine($"[FAILED] SUSHI did not complete successfully.");
            return result;
        }

        // Copy study-specific files to main Examples folder
        var copied = _sushi.CopyGeneratedToMainExamples(sushiResult.GeneratedFiles);
        if (copied > 0)
            onLine($"[INFO] Copied {copied} generated file{(copied == 1 ? "" : "s")} to Examples/");

        // ── Step 3: Generate Bundle ──────────────────────────────────────
        onLine("[STEP 3/3] Generating bundle…");
        try
        {
            var examplesFolder  = _settings.Settings.ExamplesFolder;
            var templatesFolder = "Templates";
            var outputPath = await Task.Run(
                () => _build.GenerateBundle(examplesFolder, templatesFolder), ct);
            onLine($"[OK] Bundle written → {outputPath}");
        }
        catch (Exception ex)
        {
            result.Step         = "Generate Bundle";
            result.ErrorMessage = ex.Message;
            onLine($"[ERROR] {ex.Message}");
            return result;
        }

        result.Success     = true;
        result.Filescopied = copied;
        onLine("[DONE] FHIR pipeline completed successfully.");
        return result;
    }
}
