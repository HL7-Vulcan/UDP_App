using UDP_App.Models;
using System.Text;
using System.Text.Json;

namespace UDP_App.Services;

public class DataService09
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };
    private readonly IWebHostEnvironment _env;

    public DataService09(IWebHostEnvironment env)
    {
        _env = env;
    }

    public string ToJson(Section09Model model) =>
        JsonSerializer.Serialize(model, JsonOpts);

    public string ToCsv(Section09Model model)
    {
        var sb = new StringBuilder();
        sb.AppendLine("field_name,value");
        foreach (var prop in typeof(Section09Model).GetProperties())
        {
            var attr = prop.GetCustomAttributes(typeof(System.Text.Json.Serialization.JsonPropertyNameAttribute), false)
                           .Cast<System.Text.Json.Serialization.JsonPropertyNameAttribute>()
                           .FirstOrDefault();
            var key = attr?.Name ?? prop.Name;
            if (key == "RptRows" || key == "RptTableData") continue;
            var val = prop.GetValue(model) as string ?? string.Empty;
            sb.AppendLine($"{key},{CsvEscape(val)}");
        }
        // Table data
        foreach (var (k, v) in model.RptTableData)
            sb.AppendLine($"{k},{CsvEscape(v)}");
        return sb.ToString();
    }

    public string ToPlainText(Section09Model model)
    {
        var sb = new StringBuilder();
        void Line(string heading, string? val) { sb.AppendLine(heading); sb.AppendLine(val ?? string.Empty); sb.AppendLine(); }

        Line("9.1.1 Definitions of Adverse Events",               model.C218476);
        Line("9.1.2 Definitions of Serious Adverse Events",        model.C218781);
        Line("9.1.3 Definitions of Product Complaints",            model.C218782);
        Line("9.1.3.1 Medical Device Product Complaints",          model.C218783);
        Line("9.2.1 Timing",                                       model.C218791);
        Line("9.2.2 Identification",                               model.C218792);
        Line("9.2.2 Severity",                                     model.C25676);
        Line("9.2.2 Causality",                                    model.C82552);
        Line("9.2.2 Recording",                                    model.C218793);
        Line("9.2.2 Follow-up",                                    model.C218794);
        Line("9.2.3 Reporting",                                    model.C218795);
        Line("9.2.3.1 Regulatory Reporting Requirements",          model.C218796);
        Line("9.2.4 Adverse Events of Special Interest",           model.C217358);
        Line("9.2.5 Disease-related Events",                       model.C218797);
        Line("9.3.1 Participants Who Become Pregnant",             model.C218798);
        Line("9.3.2 Partners Who Become Pregnant",                 model.C218799);
        Line("9.4 Special Safety Situations",                      model.C218800);

        if (model.RptTableData.Any())
        {
            sb.AppendLine("9.2 Reporting Table");
            foreach (var (k, v) in model.RptTableData)
                sb.AppendLine($"  {k}: {v}");
        }
        return sb.ToString();
    }

    public string ToFsh(Section09Model model)
    {
        var mainTemplatePath  = Path.Combine(_env.ContentRootPath, "Templates", "Section_09.Template.fsh");
        var tableTemplatePath = Path.Combine(_env.ContentRootPath, "Templates", "Section_09_Table.Template.fsh");

        string mainTemplate, tableTemplate;
        try { mainTemplate  = File.ReadAllText(mainTemplatePath);  }
        catch { return $"Error: could not read template file at {mainTemplatePath}"; }
        try { tableTemplate = File.ReadAllText(tableTemplatePath); }
        catch { return $"Error: could not read template file at {tableTemplatePath}"; }

        // ── Fixed field substitutions ──────────────────────────────
        var fields = new Dictionary<string, string?>
        {
            { "C218476", model.C218476 },
            { "C218781", model.C218781 },
            { "C218782", model.C218782 },
            { "C218783", model.C218783 },
            { "C218791", model.C218791 },
            { "C218792", model.C218792 },
            { "C25676",  model.C25676  },
            { "C82552",  model.C82552  },
            { "C218793", model.C218793 },
            { "C218794", model.C218794 },
            { "C218795", model.C218795 },
            { "C218796", model.C218796 },
            { "C217358", model.C217358 },
            { "C218797", model.C218797 },
            { "C218798", model.C218798 },
            { "C218799", model.C218799 },
            { "C218800", model.C218800 },
        };

        foreach (var (key, value) in fields)
            mainTemplate = mainTemplate.Replace("{{" + key + "}}", value ?? string.Empty);

        // ── Table row substitutions ────────────────────────────────
        // Build one table instance per row, collect entry references
        var rowCount     = Math.Max(1, model.RptRows);
        var sb           = new System.Text.StringBuilder();
        var entryLines   = new System.Text.StringBuilder();

        for (int r = 1; r <= rowCount; r++)
        {
            var rowId = $"ReportingTable-Row{r}";
            entryLines.AppendLine($"  * entry[+] = Reference({rowId})");

            // Substitute {{TABLE_ROW_X}} with the row instance id
            var rowBlock = tableTemplate.Replace("{{TABLE_ROW_X}}", rowId);

            // Substitute per-row fields: {{C218784}} -> value from C218784_r{r}
            var rowCodes = new[] { "C218784","C222263","C218785","C218786","C218787","C218788","C218789","C218790" };
            foreach (var code in rowCodes)
            {
                var key = $"{code}_r{r}";
                var val = model.RptTableData.TryGetValue(key, out var v) ? v : string.Empty;
                rowBlock = rowBlock.Replace("{{" + code + "}}", val);
                // Table template uses C28784 (typo in template) - also replace that
                rowBlock = rowBlock.Replace("{{C28784}}", model.RptTableData.TryGetValue($"C218784_r{r}", out var v2) ? v2 : string.Empty);
            }

            sb.AppendLine();
            sb.Append(rowBlock);
        }

        // Replace the REPEAT block in the main template with actual entry lines
        var repeatPattern = "{{REPEAT  * entry[+] = Reference({{TABLE_ROW_X}})}}";
        mainTemplate = mainTemplate.Replace(repeatPattern, entryLines.ToString().TrimEnd());

        // Append all row instances at the end
        return mainTemplate + sb.ToString();
    }

    public Section09Model? LoadSample()
    {
        var samplePath = Path.Combine(_env.ContentRootPath, "Data", "section_09_sample.json");
        try
        {
            var json = File.ReadAllText(samplePath);
            return FromJson(json);
        }
        catch
        {
            return null;
        }
    }

    public Section09Model? FromJson(string json)
    {
        try { return JsonSerializer.Deserialize<Section09Model>(json, JsonOpts); }
        catch { return null; }
    }

    private static string CsvEscape(string? value)
    {
        if (string.IsNullOrEmpty(value)) return "\"\"";
        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }
}
