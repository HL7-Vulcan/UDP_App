using UDP_App.Models;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Text.RegularExpressions;

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
        { "char_code",    "code"        },
        { "char_low",     "low"         },
        { "char_high",    "high"        },
        { "char_type",    "type"        },
        { "char_bool",    "boolean"     },
        { "char_qty",     "quantity"    },
        { "char_exclude", "exclude"     },
        { "char_desc",    "description" },
    };

    public int GetCharCount(Dictionary<string, string> data, int row) =>
        data.TryGetValue($"CharCount_r{row}", out var v) && int.TryParse(v, out var n) ? Math.Max(1, n) : 1;

    // Returns ordered characteristic indices from CharOrder_r{row} (new) or falls back to CharCount (legacy)
    private static List<int> GetCharOrderList(Dictionary<string, string> data, int row)
    {
        if (data.TryGetValue($"CharOrder_r{row}", out var order) && !string.IsNullOrEmpty(order))
            return order.Split(',').Select(int.Parse).ToList();
        if (data.TryGetValue($"CharCount_r{row}", out var cv) && int.TryParse(cv, out var n) && n > 0)
            return Enumerable.Range(1, n).ToList();
        return new List<int>();
    }

    // Remove all REPEAT{{...}} blocks from a template string, leaving only fixed lines
    private static string StripRepeatBlocks(string tpl)
    {
        var sb  = new System.Text.StringBuilder();
        int pos = 0;
        while (pos < tpl.Length)
        {
            int ri = tpl.IndexOf("REPEAT{{", pos, StringComparison.Ordinal);
            if (ri < 0) { sb.Append(tpl, pos, tpl.Length - pos); break; }
            // Append everything before this REPEAT block (strip the line it's on)
            int lineStart = ri;
            while (lineStart > 0 && tpl[lineStart - 1] != '\n') lineStart--;
            sb.Append(tpl, pos, lineStart - pos);
            int outerOpen  = ri + "REPEAT".Length;
            int outerClose = TokenParser.FindTokenEnd(tpl, outerOpen);
            if (outerClose < 0) break;
            pos = outerClose;
            if (pos < tpl.Length && tpl[pos] == '\r') pos++;
            if (pos < tpl.Length && tpl[pos] == '\n') pos++;
        }
        return sb.ToString();
    }

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

    /// <summary>Returns (fshStandard, fshR5) — both FHIR outputs from the same model.</summary>
    public (string Standard, string R5) ToFsh(Section05Model model)
    {
        var mainPath  = Path.Combine(_env.ContentRootPath, "Templates", "Section_05_Template.fsh");
        var eligPath  = Path.Combine(_env.ContentRootPath, "Templates", "Section_05_Eligibility_Template.fsh");
        var eligR5Path = Path.Combine(_env.ContentRootPath, "Templates", "Section_05_R5_Eligibility_Template.fsh");

        string main, eligTpl, eligR5Tpl;
        try { main      = File.ReadAllText(mainPath);   } catch { main      = $"Error: could not read {mainPath}";   }
        try { eligTpl   = File.ReadAllText(eligPath);   } catch { eligTpl   = $"Error: could not read {eligPath}";   }
        try { eligR5Tpl = File.ReadAllText(eligR5Path); } catch { eligR5Tpl = $"Error: could not read {eligR5Path}"; }

        // Fixed fields — applied to main template
        var fields = new Dictionary<string, string?>
        {
            {"C218739",model.C218739},{"C25532",model.C25532},  {"C25370",model.C25370},
            {"C218740",model.C218740},{"C218741",model.C218741},{"C218742",model.C218742},
            {"C218743",model.C218743},{"C218744",model.C218744},{"C218745",model.C218745},
            {"C218746",model.C218746},{"C49628",model.C49628},  {"C179373",model.C179373},
        };
        foreach (var (k, v) in fields)
            main = main.Replace("{{" + k + "}}", v ?? string.Empty);

        // Collect row ids
        var inclRowIds = Enumerable.Range(1, Math.Max(1, model.InclRows)).Select(r =>
        {
            var name = Get(model.InclData, $"name_r{r}").Replace(" ", "-").ToLowerInvariant();
            return string.IsNullOrWhiteSpace(name) ? $"eligibility-group-row{r}" : $"eligibility-group-{name}";
        }).ToList();
        var exclRowIds = Enumerable.Range(1, Math.Max(1, model.ExclRows)).Select(r =>
        {
            var name = Get(model.ExclData, $"name_r{r}").Replace(" ", "-").ToLowerInvariant();
            return string.IsNullOrWhiteSpace(name) ? $"exclusion-group-row{r}" : $"exclusion-group-{name}";
        }).ToList();

        // Expand REPEAT in main template (shared by both outputs)
        var repeatResult = TokenParser.FindAndExpandRepeat(main, inclRowIds.Concat(exclRowIds));
        if (repeatResult.HasValue)
        {
            var (expanded, mStart, mLen) = repeatResult.Value;
            main = main.Remove(mStart, mLen).Insert(mStart, expanded);
        }

        // Build group instance blocks for both eligibility templates
        var inclBlocks   = BuildGroupBlocks(eligTpl,   model.InclData, Math.Max(1, model.InclRows), inclRowIds);
        var exclBlocks   = BuildGroupBlocks(eligTpl,   model.ExclData, Math.Max(1, model.ExclRows), exclRowIds);
        var inclBlocksR5 = BuildGroupBlocks(eligR5Tpl, model.InclData, Math.Max(1, model.InclRows), inclRowIds);
        var exclBlocksR5 = BuildGroupBlocks(eligR5Tpl, model.ExclData, Math.Max(1, model.ExclRows), exclRowIds);

        return (main + inclBlocks + exclBlocks,
                main + inclBlocksR5 + exclBlocksR5);
    }

    private string BuildGroupBlocks(
        string eligTpl, Dictionary<string, string> data, int rowCount, List<string> rowIds)
    {
        var blocks = new System.Text.StringBuilder();

        for (int r = 1; r <= rowCount; r++)
        {
            var rowId = rowIds[r - 1];

            // Step 1: Build the characteristic lines by expanding each characteristic
            // using the correct REPEAT{{FMT{{...}}}} block from the template.
            var charList = GetCharOrderList(data, r);
            var charSb   = new System.Text.StringBuilder();
            foreach (var ci in charList)
            {
                var fmt = Get(data, $"char_fmt_r{r}_c{ci}");
                var charVals = new Dictionary<string, string>();
                foreach (var (sk, tk) in CharTemplateKeys)
                    charVals[tk] = Get(data, $"{sk}_r{r}_c{ci}");

                charSb.Append(TokenParser.ExpandCharacteristic(eligTpl, fmt, key =>
                    charVals.TryGetValue(key, out var v) ? v : null));
            }

            // Step 2: Strip all REPEAT{{...}} blocks from the template, then substitute
            // the remaining simple tokens and splice the expanded characteristics in.
            var block = StripRepeatBlocks(eligTpl);
            block = block.TrimEnd() + "\r\n" + charSb;

            block = TokenParser.Substitute(block, key =>
                key == "TABLE_ROW_X" ? rowId :
                RowFields.Contains(key) ? Get(data, $"{key}_r{r}") :
                null);

            blocks.AppendLine(); blocks.Append(block);
        }
        return blocks.ToString();
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
