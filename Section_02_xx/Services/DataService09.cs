using Section_02.Models;
using System.Text;
using System.Text.Json;

namespace Section_02.Services;

public class DataService09
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };

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

        Line("9.1.1 Definitions of Adverse Events",               model.field_9_01_C218476);
        Line("9.1.2 Definitions of Serious Adverse Events",        model.field_9_02_C218781);
        Line("9.1.3 Definitions of Product Complaints",            model.field_9_03_C218782);
        Line("9.1.3.1 Medical Device Product Complaints",          model.field_9_04_C218783);
        Line("9.2.1 Timing",                                       model.field_9_12_C218791);
        Line("9.2.2 Identification",                               model.field_9_13_C218792);
        Line("9.2.2 Severity",                                     model.field_9_14_C25676);
        Line("9.2.2 Causality",                                    model.field_9_15_C82552);
        Line("9.2.2 Recording",                                    model.field_9_16_C218793);
        Line("9.2.2 Follow-up",                                    model.field_9_17_C218794);
        Line("9.2.3 Reporting",                                    model.field_9_18_C218795);
        Line("9.2.3.1 Regulatory Reporting Requirements",          model.field_9_19_C218796);
        Line("9.2.4 Adverse Events of Special Interest",           model.field_9_20_C217358);
        Line("9.2.5 Disease-related Events",                       model.field_9_21_C218797);
        Line("9.3.1 Participants Who Become Pregnant",             model.field_9_22_C218798);
        Line("9.3.2 Partners Who Become Pregnant",                 model.field_9_23_C218799);
        Line("9.4 Special Safety Situations",                      model.field_9_24_C218800);

        if (model.RptTableData.Any())
        {
            sb.AppendLine("9.2 Reporting Table");
            foreach (var (k, v) in model.RptTableData)
                sb.AppendLine($"  {k}: {v}");
        }
        return sb.ToString();
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
