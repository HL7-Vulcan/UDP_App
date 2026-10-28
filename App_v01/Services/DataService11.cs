using UDP_App.Models;
using System.Text;
using System.Text.Json;

namespace UDP_App.Services;

public class DataService11
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };
    private readonly IWebHostEnvironment _env;
    public DataService11(IWebHostEnvironment env) { _env = env; }

    public string ToJson(Section11Model m) => JsonSerializer.Serialize(m, JsonOpts);
    public Section11Model? FromJson(string json)
    {
        try { return JsonSerializer.Deserialize<Section11Model>(json, JsonOpts); }
        catch { return null; }
    }
    public string ToCsv(Section11Model m)
    {
        var sb = new StringBuilder();
        sb.AppendLine("field_name,value");
        sb.AppendLine($"C218811,{CsvEscape(m.C218811 ?? string.Empty)}");
        return sb.ToString();
    }
    public string ToPlainText(Section11Model m)
    {
        var sb = new StringBuilder();
        sb.AppendLine("11 Trial Oversight and Other General Considerations");
        sb.AppendLine(m.C218811 ?? string.Empty);
        return sb.ToString();
    }
    public string ToFsh(Section11Model m)
    {
        var path = Path.Combine(_env.ContentRootPath, "Templates", "Section_11_Template.fsh");
        try { return File.ReadAllText(path).Replace("{{C218811}}", m.C218811 ?? string.Empty); }
        catch { return "// Section 11 FSH template not found."; }
    }
    public Section11Model? LoadSample()
    {
        var path = Path.Combine(_env.ContentRootPath, "Data", "section_11_sample.json");
        try { if (File.Exists(path)) return JsonSerializer.Deserialize<Section11Model>(File.ReadAllText(path), JsonOpts); }
        catch { }
        return new Section11Model
        {
            C218811 = "This trial will be conducted in accordance with the ethical principles of the Declaration of Helsinki, ICH E6(R2) Good Clinical Practice (GCP), and applicable regulatory requirements. Independent ethics committee (IEC) or institutional review board (IRB) approval will be obtained before the trial commences at each site. Written informed consent will be obtained from all participants prior to any trial-specific procedures."
        };
    }
    private static string CsvEscape(string v) =>
        (v.Contains(',') || v.Contains('"') || v.Contains('\n')) ? "\"" + v.Replace("\"", "\"\"") + "\"" : v;
}
