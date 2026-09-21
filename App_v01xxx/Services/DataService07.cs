using UDP_App.Models;
using System.Text;
using System.Text.Json;

namespace UDP_App.Services;

public class DataService07
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };
    private readonly IWebHostEnvironment _env;

    public DataService07(IWebHostEnvironment env) => _env = env;

    public string ToJson(Section07Model model) => JsonSerializer.Serialize(model, JsonOpts);

    public string ToCsv(Section07Model model)
    {
        var sb = new StringBuilder();
        sb.AppendLine("field_name,value");
        sb.AppendLine($"C218764,{CsvEscape(model.C218764)}");
        sb.AppendLine($"C218765,{CsvEscape(model.C218765)}");
        sb.AppendLine($"C218766,{CsvEscape(model.C218766)}");
        sb.AppendLine($"C218767,{CsvEscape(model.C218767)}");
        sb.AppendLine($"C218768,{CsvEscape(model.C218768)}");
        return sb.ToString();
    }

    public string ToPlainText(Section07Model model)
    {
        var sb = new StringBuilder();
        void L(string h, string? v) { sb.AppendLine(h); sb.AppendLine(v ?? string.Empty); sb.AppendLine(); }
        L("7.1.1 Permanent Discontinuation of Trial Intervention", model.C218764);
        L("7.1.2 Temporary Discontinuation of Trial Intervention", model.C218765);
        L("7.1.3 Rechallenge",                                     model.C218766);
        L("7.2 Participant Discontinuation or Withdrawal",         model.C218767);
        L("7.3 Management of Loss to Follow-Up",                   model.C218768);
        return sb.ToString();
    }

    public string ToFsh(Section07Model model)
    {
        var path = Path.Combine(_env.ContentRootPath, "Templates", "Section_07_Template.fsh");
        string template;
        try { template = File.ReadAllText(path); }
        catch { return $"Error: could not read {path}"; }
        var fields = new Dictionary<string, string?>
        {
            {"C218764",model.C218764},{"C218765",model.C218765},{"C218766",model.C218766},
            {"C218767",model.C218767},{"C218768",model.C218768},
        };
        foreach (var (k, v) in fields)
            template = template.Replace("{{" + k + "}}", v ?? string.Empty);
        return template;
    }

    public Section07Model? LoadSample()
    {
        try { return FromJson(File.ReadAllText(Path.Combine(_env.ContentRootPath, "Data", "section_07_sample.json"))); }
        catch { return null; }
    }

    public Section07Model? FromJson(string json)
    {
        try { return JsonSerializer.Deserialize<Section07Model>(json, JsonOpts); }
        catch { return null; }
    }

    private static string CsvEscape(string? v) =>
        string.IsNullOrEmpty(v) ? "\"\"" : "\"" + v.Replace("\"", "\"\"") + "\"";
}
