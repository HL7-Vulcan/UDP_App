using Section_02.Models;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;

namespace Section_02.Services;

public class DataService
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };

    public string ToJson(Section02Model model) =>
        JsonSerializer.Serialize(model, JsonOpts);

    public string ToCsv(Section02Model model)
    {
        var sb = new StringBuilder();
        sb.AppendLine("field_name,value");
        sb.AppendLine($"C146997,{CsvEscape(model.C146997)}");
        sb.AppendLine($"C218721,{CsvEscape(model.C218721)}");
        sb.AppendLine($"C218722,{CsvEscape(model.C218722)}");
        sb.AppendLine($"C218723,{CsvEscape(model.C218723)}");
        sb.AppendLine($"C218724,{CsvEscape(model.C218724)}");
        sb.AppendLine($"C218725,{CsvEscape(model.C218725)}");
        return sb.ToString();
    }


    public string ToPlainText(Section02Model model)
    {
        var sb = new StringBuilder();
        sb.AppendLine("2.1 Purpose of Trial");
        sb.AppendLine(model.C146997 ?? string.Empty);
        sb.AppendLine();
        sb.AppendLine("2.2.1 Risk Summary – Trial Intervention");
        sb.AppendLine(model.C218721 ?? string.Empty);
        sb.AppendLine();
        sb.AppendLine("2.2.1 Risk Summary – Trial Procedures");
        sb.AppendLine(model.C218722 ?? string.Empty);
        sb.AppendLine();
        sb.AppendLine("2.2.1 Risk Summary – Other");
        sb.AppendLine(model.C218723 ?? string.Empty);
        sb.AppendLine();
        sb.AppendLine("2.2.2 Benefit Summary");
        sb.AppendLine(model.C218724 ?? string.Empty);
        sb.AppendLine();
        sb.AppendLine("2.2.3 Overall Risk-Benefit Assessment");
        sb.AppendLine(model.C218725 ?? string.Empty);
        return sb.ToString();
    }

    public string ToFsh(Section02Model model)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Instance: Narrative - Composition - M11Section02");
        sb.AppendLine("InstanceOf: m11 - research - study - narratives");
        sb.AppendLine("Title: \"Example Narrative Single Composition with a Section for Each M11 Section\"");
        sb.AppendLine("Usage: #example");
        sb.AppendLine("*status = #final");
        sb.AppendLine("2.1 Purpose of Trial");
        sb.AppendLine(model.C146997 ?? string.Empty);
        sb.AppendLine();
        sb.AppendLine("2.2.1 Risk Summary – Trial Intervention");
        sb.AppendLine(model.C218721 ?? string.Empty);
        sb.AppendLine();
        sb.AppendLine("2.2.1 Risk Summary – Trial Procedures");
        sb.AppendLine(model.C218722 ?? string.Empty);
        sb.AppendLine();
        sb.AppendLine("2.2.1 Risk Summary – Other");
        sb.AppendLine(model.C218723 ?? string.Empty);
        sb.AppendLine();
        sb.AppendLine("2.2.2 Benefit Summary");
        sb.AppendLine(model.C218724 ?? string.Empty);
        sb.AppendLine();
        sb.AppendLine("2.2.3 Overall Risk-Benefit Assessment");
        sb.AppendLine(model.C218725 ?? string.Empty);
        return sb.ToString();
    }

    public Section02Model? FromJson(string json)
    {
        try { return JsonSerializer.Deserialize<Section02Model>(json, JsonOpts); }
        catch { return null; }
    }

    private static string CsvEscape(string? value)
    {
        if (string.IsNullOrEmpty(value)) return "\"\"";
        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }
}
