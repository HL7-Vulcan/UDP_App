using UDP_App.Models;
using System.Text;
using System.Text.Json;

namespace UDP_App.Services;

public class DataService05
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };
    private readonly IWebHostEnvironment _env;

    public DataService05(IWebHostEnvironment env) => _env = env;

    private static readonly string[] RowFields =
        { "name", "title", "description", "type", "membership", "combinationMethod" };

    private static readonly Dictionary<string, string> CharTemplateKeys = new()
    {
        { "char_code",    "\"code\""        },
        { "char_low",     "\"low\""         },
        { "char_high",    "\"high\""        },
        { "char_exclude", "exclude"         },
        { "char_desc",    "\"description\"" },
    };

    public int GetCharCount(Dictionary<string, string> data, int row) =>
        data.TryGetValue($"CharCount_r{row}", out var v) && int.TryParse(v, out var n) ? Math.Max(1, n) : 1;

    // Convenience overloads used by the razor (incl / excl)
    public int GetInclCharCount(Section05Model model, int row) => GetCharCount(model.InclData, row);
    public int GetExclCharCount(Section05Model model, int row) => GetCharCount(model.ExclData, row);

    public string ToJson(Section05Model model) => JsonSerializer.Serialize(model, JsonOpts);

    public string ToCsv(Section05Model model)
    {
        var sb = new StringBuilder();
        sb.AppendLine("field_name,value");
        void Row(string k, string? v) => sb.AppendLine($"{k},{CsvEscape(v)}");
        Row("C218739", model.C218739); Row("C25532",  model.C25532);
        Row("C25370",  model.C25370);  Row("C218740", model.C218740);
        Row("C218741", model.C218741); Row("C218742", model.C218742);
        Row("C218743", model.C218743); Row("C218744", model.C218744);
        Row("C218745", model.C218745); Row("C218746", model.C218746);
        Row("C49628",  model.C49628);  Row("C179373", model.C179373);
        foreach (var (k, v) in model.InclData) Row($"incl_{k}", v);
        foreach (var (k, v) in model.ExclData) Row($"excl_{k}", v);
        return sb.ToString();
    }

    public string ToPlainText(Section05Model model)
    {
        var sb = new StringBuilder();
        void L(string h, string? v) { sb.AppendLine(h); sb.AppendLine(v ?? string.Empty); sb.AppendLine(); }
        L("5.1 Description of Trial Population and Rationale", model.C218739);
        L("5.2 Inclusion Criteria",                            model.C25532);
        AppendGroupsText(sb, "Inclusion", model.InclData, Math.Max(1, model.InclRows));
        L("5.3 Exclusion Criteria",                            model.C25370);
        AppendGroupsText(sb, "Exclusion", model.ExclData, Math.Max(1, model.ExclRows));
        L("5.4.1 Definitions Related to Childbearing Potential", model.C218740);
        L("5.4.2 Contraception Requirements",                  model.C218741);
        L("5.5 Lifestyle Restrictions",                        model.C218742);
        L("5.5.1 Meals and Dietary Restrictions",              model.C218743);
        L("5.5.2 Caffeine, Alcohol, Tobacco, and Other Restrictions", model.C218744);
        L("5.5.3 Physical Activity Restrictions",              model.C218745);
        L("5.5.4 Other Activity Restrictions",                 model.C218746);
        L("5.6.1 Screen Failure",                              model.C49628);
        L("5.6.2 Rescreening",                                 model.C179373);
        return sb.ToString();
    }

    private void AppendGroupsText(StringBuilder sb, string label,
                                  Dictionary<string, string> data, int rowCount)
    {
        for (int r = 1; r <= rowCount; r++)
        {
            sb.AppendLine($"{label} Group {r}");
            foreach (var f in RowFields)
                sb.AppendLine($"  {f}: {Get(data, $"{f}_r{r}")}");
            var charCount = GetCharCount(data, r);
            for (int c = 1; c <= charCount; c++)
            {
                sb.AppendLine($"  Characteristic {c}");
                sb.AppendLine($"    code: {Get(data, $"char_code_r{r}_c{c}")}");
                sb.AppendLine($"    low:  {Get(data, $"char_low_r{r}_c{c}")}");
                sb.AppendLine($"    high: {Get(data, $"char_high_r{r}_c{c}")}");
                sb.AppendLine($"    excl: {Get(data, $"char_exclude_r{r}_c{c}")}");
                sb.AppendLine($"    desc: {Get(data, $"char_desc_r{r}_c{c}")}");
            }
            sb.AppendLine();
        }
    }

    public string ToFsh(Section05Model model)
    {
        var mainPath = Path.Combine(_env.ContentRootPath, "Templates", "Section_05_Template.fsh");
        var eligPath = Path.Combine(_env.ContentRootPath, "Templates", "Section_05_Eligibility_Template.fsh");
        string main, eligTpl;
        try { main    = File.ReadAllText(mainPath); }
        catch { return $"Error: could not read {mainPath}"; }
        try { eligTpl = File.ReadAllText(eligPath); }
        catch { return $"Error: could not read {eligPath}"; }

        // Fixed fields
        var fields = new Dictionary<string, string?>
        {
            {"C218739",model.C218739},{"C25532",model.C25532},  {"C25370",model.C25370},
            {"C218740",model.C218740},{"C218741",model.C218741},{"C218742",model.C218742},
            {"C218743",model.C218743},{"C218744",model.C218744},{"C218745",model.C218745},
            {"C218746",model.C218746},{"C49628",model.C49628},  {"C179373",model.C179373},
        };
        foreach (var (k, v) in fields)
            main = main.Replace("{{" + k + "}}", v ?? string.Empty);

        // Build inclusion group entries + instances
        var (inclEntries, inclBlocks) = BuildGroupBlocks(eligTpl, model.InclData,
            Math.Max(1, model.InclRows), "eligibility-group");

        // Build exclusion group entries + instances (reuse same template)
        var (exclEntries, exclBlocks) = BuildGroupBlocks(eligTpl, model.ExclData,
            Math.Max(1, model.ExclRows), "exclusion-group");

        // Replace the single REPEAT block in main template with inclusion entries
        const string mainRepeat = "{{REPEAT * entry[+] = Reference(eligibility-group-{{TABLE_ROW_X}})}}";
        var allEntries = inclEntries.ToString().TrimEnd() + "\n" + exclEntries.ToString().TrimEnd();
        main = main.Replace(mainRepeat, allEntries);

        return main + inclBlocks.ToString() + exclBlocks.ToString();
    }

    private (StringBuilder entries, StringBuilder blocks) BuildGroupBlocks(
        string eligTpl, Dictionary<string, string> data, int rowCount, string prefix)
    {
        var entries = new StringBuilder();
        var blocks  = new StringBuilder();

        for (int r = 1; r <= rowCount; r++)
        {
            var name = Get(data, $"name_r{r}").Replace(" ", "-").ToLowerInvariant();
            var rowId = string.IsNullOrWhiteSpace(name) ? $"{prefix}-row{r}" : $"{prefix}-{name}";

            entries.AppendLine($" * entry[+] = Reference({rowId})");

            var block = eligTpl.Replace("{{TABLE_ROW_X}}", rowId);
            foreach (var f in RowFields)
                block = block.Replace("{{" + f + "}}", Get(data, $"{f}_r{r}"));

            // Expand REPEAT{{ ... }} characteristic block
            const string repeatOpen  = "REPEAT{{\n";
            const string repeatClose = "\n}}";
            var repStart = block.IndexOf(repeatOpen);
            var repEnd   = block.IndexOf(repeatClose, repStart >= 0 ? repStart : 0);
            if (repStart >= 0 && repEnd >= 0)
            {
                var charTpl = block.Substring(repStart + repeatOpen.Length,
                                              repEnd - repStart - repeatOpen.Length);
                var charSb = new StringBuilder();
                var charCount = GetCharCount(data, r);
                for (int c = 1; c <= charCount; c++)
                {
                    var cb = charTpl;
                    foreach (var (sk, tk) in CharTemplateKeys)
                        cb = cb.Replace("{{" + tk + "}}", Get(data, $"{sk}_r{r}_c{c}"));
                    if (c > 1) charSb.AppendLine();
                    charSb.Append(cb);
                }
                block = block.Substring(0, repStart)
                       + charSb
                       + block.Substring(repEnd + repeatClose.Length);
            }

            blocks.AppendLine(); blocks.Append(block);
        }
        return (entries, blocks);
    }

    public Section05Model? LoadSample()
    {
        try { return FromJson(File.ReadAllText(Path.Combine(_env.ContentRootPath, "Data", "section_05_sample.json"))); }
        catch { return null; }
    }

    public Section05Model? FromJson(string json)
    {
        try { return JsonSerializer.Deserialize<Section05Model>(json, JsonOpts); }
        catch { return null; }
    }

    private static string Get(Dictionary<string, string> d, string key) =>
        d.TryGetValue(key, out var v) ? v : string.Empty;

    private static string CsvEscape(string? v) =>
        string.IsNullOrEmpty(v) ? "\"\"" : "\"" + v.Replace("\"", "\"\"") + "\"";
}
