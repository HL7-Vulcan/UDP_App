using UDP_App.Models;
using System.Text;
using System.Text.Json;

namespace UDP_App.Services;

public class DataService13
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };
    private readonly IWebHostEnvironment _env;
    public DataService13(IWebHostEnvironment env) { _env = env; }

    public string ToJson(Section13Model m) => JsonSerializer.Serialize(m, JsonOpts);
    public Section13Model? FromJson(string json)
    {
        try { return JsonSerializer.Deserialize<Section13Model>(json, JsonOpts); }
        catch { return null; }
    }
    public string ToCsv(Section13Model m)
    {
        var sb = new StringBuilder();
        sb.AppendLine("field_name,value");
        sb.AppendLine($"C218837,{CsvEscape(m.C218837 ?? string.Empty)}");
        return sb.ToString();
    }
    public string ToPlainText(Section13Model m)
    {
        var sb = new StringBuilder();
        sb.AppendLine("13 Appendix: Glossary of Terms and Abbreviations");
        sb.AppendLine(m.C218837 ?? string.Empty);
        return sb.ToString();
    }
    public string ToFsh(Section13Model m)
    {
        var path = Path.Combine(_env.ContentRootPath, "Templates", "Section_13_Template.fsh");
        try { return File.ReadAllText(path).Replace("{{C218837}}", m.C218837 ?? string.Empty); }
        catch { return "// Section 13 FSH template not found."; }
    }
    public (Section13Model? Model, string? Error) LoadSample()
    {
        var path = Path.Combine(_env.ContentRootPath, "Data", "section_13_sample.json");
        if (!File.Exists(path))
            return (null, "Sample data file not found: Data/section_13_sample.json");
        try
        {
            var model = JsonSerializer.Deserialize<Section13Model>(File.ReadAllText(path), JsonOpts);
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
