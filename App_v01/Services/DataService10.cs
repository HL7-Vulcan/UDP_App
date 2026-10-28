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
        sb.AppendLine($"C164387,{CsvEscape(m.C164387 ?? string.Empty)}");
        return sb.ToString();
    }
    public string ToPlainText(Section10Model m)
    {
        var sb = new StringBuilder();
        sb.AppendLine("10 Statistical Considerations — General Considerations");
        sb.AppendLine(m.C164387 ?? string.Empty);
        return sb.ToString();
    }
    public string ToFsh(Section10Model m)
    {
        var path = Path.Combine(_env.ContentRootPath, "Templates", "Section_10_Template.fsh");
        try { return File.ReadAllText(path).Replace("{{C164387}}", m.C164387 ?? string.Empty); }
        catch { return "// Section 10 FSH template not found."; }
    }
    public Section10Model? LoadSample()
    {
        var path = Path.Combine(_env.ContentRootPath, "Data", "section_10_sample.json");
        try { if (File.Exists(path)) return JsonSerializer.Deserialize<Section10Model>(File.ReadAllText(path), JsonOpts); }
        catch { }
        return new Section10Model
        {
            C164387 = "Statistical analyses will be performed by the sponsor's biostatistics group using SAS version 9.4 or higher. The full statistical analysis plan (SAP) will be finalised and locked before unblinding of the trial data. All statistical tests will be two-sided with a significance level of 0.05 unless otherwise specified."
        };
    }
    private static string CsvEscape(string v) =>
        (v.Contains(',') || v.Contains('"') || v.Contains('\n')) ? "\"" + v.Replace("\"", "\"\"") + "\"" : v;
}
