using Section_02.Models;
using System.Text;
using System.Text.Json;

namespace Section_02.Services;

public class DataService
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };
    private readonly IWebHostEnvironment _env;

    public DataService(IWebHostEnvironment env)
    {
        _env = env;
    }

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
        // Load the template file from the Templates folder
        var templatePath = Path.Combine(_env.ContentRootPath, "Templates", "Section_02.Template.fsh");

        string template;
        try
        {
            template = File.ReadAllText(templatePath);
        }
        catch
        {
            return $"Error: could not read template file at {templatePath}";
        }

        // Replace {{FieldName}} tokens with field values
        var fieldValues = new Dictionary<string, string?>
        {
            { "C146997", model.C146997 },
            { "C218721", model.C218721 },
            { "C218722", model.C218722 },
            { "C218723", model.C218723 },
            { "C218724", model.C218724 },
            { "C218725", model.C218725 },
        };

        foreach (var (key, value) in fieldValues)
        {
            template = template.Replace("{{" + key + "}}", value ?? string.Empty);
        }

        return template;
    }

    public Section02Model? LoadSample()
    {
        var samplePath = Path.Combine(_env.ContentRootPath, "Data", "section_02_sample.json");
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
