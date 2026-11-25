using UDP_App.Models;
using System.Text;
using System.Text.Json;

namespace UDP_App.Services;

public class DataService14
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };
    private readonly IWebHostEnvironment _env;
    public DataService14(IWebHostEnvironment env) { _env = env; }

    public string ToJson(Section14Model m) => JsonSerializer.Serialize(m, JsonOpts);
    public Section14Model? FromJson(string json)
    {
        try { return JsonSerializer.Deserialize<Section14Model>(json, JsonOpts); }
        catch { return null; }
    }
    public string ToCsv(Section14Model m)
    {
        var sb = new StringBuilder();
        sb.AppendLine("field_name,value");
        sb.AppendLine($"C184397,{CsvEscape(m.C184397 ?? string.Empty)}");
        return sb.ToString();
    }
    public string ToPlainText(Section14Model m)
    {
        var sb = new StringBuilder();
        sb.AppendLine("14 Appendix: References");
        sb.AppendLine(m.C184397 ?? string.Empty);
        return sb.ToString();
    }
    public string ToFsh(Section14Model m)
    {
        var path = Path.Combine(_env.ContentRootPath, "Templates", "Section_14_Template.fsh");
        try { return File.ReadAllText(path).Replace("{{C184397}}", m.C184397 ?? string.Empty); }
        catch { return "// Section 14 FSH template not found."; }
    }
    public (Section14Model? Model, string? Error) LoadSample()
    {
        var path = Path.Combine(_env.ContentRootPath, "Data", "section_14_sample.json");
        if (!File.Exists(path))
            return (null, "Sample data file not found: Data/section_14_sample.json");
        try
        {
            var model = JsonSerializer.Deserialize<Section14Model>(File.ReadAllText(path), JsonOpts);
            return (model, null);
        }
        catch (Exception ex)
        {
            return (null, $"Failed to load sample data: {ex.Message}");
        }
    }
    private static string CsvEscape(string v) =>
        (v.Contains(',') || v.Contains('"') || v.Contains('\n')) ? "\"" + v.Replace("\"", "\"\"") + "\"" : v;
}
