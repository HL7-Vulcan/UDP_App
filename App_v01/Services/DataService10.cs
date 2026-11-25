using UDP_App.Models;
using System.Text;
using System.Text.Json;

namespace UDP_App.Services;

public class DataService10
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };
    private readonly IWebHostEnvironment _env;
    public DataService10(IWebHostEnvironment env) { _env = env; }

    public string ToJson(Section10Model m) => JsonSerializer.Serialize(m, JsonOpts);

    public Section10Model? FromJson(string json)
    {
        try { return JsonSerializer.Deserialize<Section10Model>(json, JsonOpts); }
        catch { return null; }
    }

    public string ToCsv(Section10Model m)
    {
        var sb = new StringBuilder();
        sb.AppendLine("field_name,value");
        sb.AppendLine($"C164387,{CsvEscape(m.C164387)}");
        sb.AppendLine($"C218801,{CsvEscape(m.C218801)}");
        sb.AppendLine($"C218802,{CsvEscape(m.C218802)}");
        sb.AppendLine($"C218807,{CsvEscape(m.C218807)}");
        sb.AppendLine($"C218808,{CsvEscape(m.C218808)}");
        sb.AppendLine($"C218809,{CsvEscape(m.C218809)}");
        sb.AppendLine($"C142582,{CsvEscape(m.C142582)}");
        sb.AppendLine($"C218810,{CsvEscape(m.C218810)}");
        sb.AppendLine($"C115467,{CsvEscape(m.C115467)}");
        foreach (var (k, v) in m.PriData) sb.AppendLine($"{k},{CsvEscape(v)}");
        foreach (var (k, v) in m.SecData) sb.AppendLine($"{k},{CsvEscape(v)}");
        return sb.ToString();
    }

    public string ToPlainText(Section10Model m)
    {
        var sb = new StringBuilder();
        void L(string h, string? v) { sb.AppendLine(h); sb.AppendLine(v ?? string.Empty); sb.AppendLine(); }

        L("10 Statistical Considerations – General Considerations", m.C164387);
        L("10.1 Analysis Sets",                                     m.C218801);
        L("10.2 Statistical Analyses – Primary Efficacy Analysis",  m.C218802);
        L("10.2 Multiple Comparisons/Multiplicity",                 m.C218807);
        L("10.2 Additional Analysis",                               m.C218808);
        L("10.2 Safety Analysis",                                   m.C218809);
        L("10.3 Interim Analysis",                                  m.C142582);
        L("10.4 Sample Size",                                       m.C218810);
        L("10.5 Additional Statistical Considerations",             m.C115467);

        if (m.PriRows > 0)
        {
            sb.AppendLine("── Primary Objectives ──");
            for (int r = 1; r <= m.PriRows; r++)
            {
                sb.AppendLine($"  Objective {r}:");
                sb.AppendLine($"    Estimand (C218482):                  {m.PriData.GetValueOrDefault($"C218482_p{r}", string.Empty)}");
                sb.AppendLine($"    Statistical Analysis Method (C218803): {m.PriData.GetValueOrDefault($"C218803_p{r}", string.Empty)}");
                sb.AppendLine($"    Handling of Data (C218804):          {m.PriData.GetValueOrDefault($"C218804_p{r}", string.Empty)}");
                sb.AppendLine($"    Sensitivity Analysis (C218480):      {m.PriData.GetValueOrDefault($"C218480_p{r}", string.Empty)}");
                sb.AppendLine($"    Supplementary Analysis (C218481):    {m.PriData.GetValueOrDefault($"C218481_p{r}", string.Empty)}");
            }
            sb.AppendLine();
        }

        if (m.SecRows > 0)
        {
            sb.AppendLine("── Secondary Objectives ──");
            for (int r = 1; r <= m.SecRows; r++)
            {
                sb.AppendLine($"  Objective {r}:");
                sb.AppendLine($"    Estimand (C218482):                  {m.SecData.GetValueOrDefault($"C218482_s{r}", string.Empty)}");
                sb.AppendLine($"    Statistical Analysis (C218805):      {m.SecData.GetValueOrDefault($"C218805_s{r}", string.Empty)}");
                sb.AppendLine($"    Handling of Data (C218806):          {m.SecData.GetValueOrDefault($"C218806_s{r}", string.Empty)}");
                sb.AppendLine($"    Sensitivity Analysis (C218480):      {m.SecData.GetValueOrDefault($"C218480_s{r}", string.Empty)}");
                sb.AppendLine($"    Supplementary Analysis (C218481):    {m.SecData.GetValueOrDefault($"C218481_s{r}", string.Empty)}");
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    public string ToFsh(Section10Model m)
    {
        var path = Path.Combine(_env.ContentRootPath, "Templates", "Section_10_Template.fsh");
        string tpl;
        try { tpl = File.ReadAllText(path); }
        catch { return "// Section 10 FSH template not found."; }

        // ── Fixed field substitutions ────────────────────────────────────────
        // Build a lookup for the top-level fields; ExpandOuterRepeat's Substitute
        // call will also use this map, so row-specific keys fall through to empty.
        var topLevel = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["C164387"] = TokenParser.FhirEscape(m.C164387),
            ["C218801"] = TokenParser.FhirEscape(m.C218801),
            ["C218802"] = TokenParser.FhirEscape(m.C218802),
            ["C218807"] = TokenParser.FhirEscape(m.C218807),
            ["C218808"] = TokenParser.FhirEscape(m.C218808),
            ["C218809"] = TokenParser.FhirEscape(m.C218809),
            ["C142582"] = TokenParser.FhirEscape(m.C142582),
            ["C218810"] = TokenParser.FhirEscape(m.C218810),
            ["C115467"] = TokenParser.FhirEscape(m.C115467),
        };

        // ── Section 10.4 REPEAT: one iteration per primary objective row ─────
        // Each iteration supplies its own field-value resolver from PriData.
        var priCount = Math.Max(1, m.PriRows);
        var priIterations = Enumerable.Range(1, priCount)
            .Select(r =>
            {
                Func<string, string?> fv = key => key switch
                {
                    "C218482" => TokenParser.FhirEscape(m.PriData.TryGetValue($"C218482_p{r}", out var v) ? v : string.Empty),
                    "C218803" => TokenParser.FhirEscape(m.PriData.TryGetValue($"C218803_p{r}", out var v) ? v : string.Empty),
                    "C218804" => TokenParser.FhirEscape(m.PriData.TryGetValue($"C218804_p{r}", out var v) ? v : string.Empty),
                    "C218480" => TokenParser.FhirEscape(m.PriData.TryGetValue($"C218480_p{r}", out var v) ? v : string.Empty),
                    "C218481" => TokenParser.FhirEscape(m.PriData.TryGetValue($"C218481_p{r}", out var v) ? v : string.Empty),
                    _ => topLevel.TryGetValue(key, out var tv) ? tv : null,
                };
                return (fv, Enumerable.Empty<string>());
            });

        var priResult = TokenParser.ExpandOuterRepeat(tpl, priIterations);
        if (priResult.HasValue)
        {
            var (expanded, ms, ml) = priResult.Value;
            tpl = tpl.Remove(ms, ml).Insert(ms, expanded);
        }

        // ── Section 10.5 REPEAT: one iteration per secondary objective row ───
        var secCount = Math.Max(1, m.SecRows);
        var secIterations = Enumerable.Range(1, secCount)
            .Select(r =>
            {
                Func<string, string?> fv = key => key switch
                {
                    "C218482" => TokenParser.FhirEscape(m.SecData.TryGetValue($"C218482_s{r}", out var v) ? v : string.Empty),
                    "C218805" => TokenParser.FhirEscape(m.SecData.TryGetValue($"C218805_s{r}", out var v) ? v : string.Empty),
                    "C218806" => TokenParser.FhirEscape(m.SecData.TryGetValue($"C218806_s{r}", out var v) ? v : string.Empty),
                    "C218480" => TokenParser.FhirEscape(m.SecData.TryGetValue($"C218480_s{r}", out var v) ? v : string.Empty),
                    "C218481" => TokenParser.FhirEscape(m.SecData.TryGetValue($"C218481_s{r}", out var v) ? v : string.Empty),
                    _ => topLevel.TryGetValue(key, out var tv) ? tv : null,
                };
                return (fv, Enumerable.Empty<string>());
            });

        var secResult = TokenParser.ExpandOuterRepeat(tpl, secIterations);
        if (secResult.HasValue)
        {
            var (expanded, ms, ml) = secResult.Value;
            tpl = tpl.Remove(ms, ml).Insert(ms, expanded);
        }

        // ── Substitute remaining top-level tokens ────────────────────────────
        tpl = TokenParser.Substitute(tpl, key =>
            topLevel.TryGetValue(key, out var v) ? v : string.Empty);

        return tpl;
    }

    public (Section10Model? Model, string? Error) LoadSample()
    {
        var path = Path.Combine(_env.ContentRootPath, "Data", "section_10_sample.json");
        if (!File.Exists(path))
            return (null, "Sample data file not found: Data/section_10_sample.json");
        try
        {
            var model = JsonSerializer.Deserialize<Section10Model>(File.ReadAllText(path), JsonOpts);
            return (model, null);
        }
        catch (Exception ex)
        {
            return (null, $"Failed to load sample data: {ex.Message}");
        }
    }

    private static string CsvEscape(string? v)
    {
        if (string.IsNullOrEmpty(v)) return "\"\"";
        return (v.Contains(',') || v.Contains('"') || v.Contains('\n'))
            ? "\"" + v.Replace("\"", "\"\"") + "\""
            : v;
    }
}
