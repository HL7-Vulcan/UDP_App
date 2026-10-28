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
    public Section14Model? LoadSample()
    {
        var path = Path.Combine(_env.ContentRootPath, "Data", "section_14_sample.json");
        try { if (File.Exists(path)) return JsonSerializer.Deserialize<Section14Model>(File.ReadAllText(path), JsonOpts); }
        catch { }
        return new Section14Model
        {
            C184397 = "1. ICH E6(R2) Good Clinical Practice: Integrated Addendum. International Council for Harmonisation of Technical Requirements for Pharmaceuticals for Human Use; 2016.\n2. World Medical Association. Declaration of Helsinki: Ethical Principles for Medical Research Involving Human Subjects. JAMA. 2013;310(20):2191–2194.\n3. ICH E9(R1) Statistical Principles for Clinical Trials: Addendum on Estimands and Sensitivity Analysis. International Council for Harmonisation; 2019."
        };
    }
    private static string CsvEscape(string v) =>
        (v.Contains(',') || v.Contains('"') || v.Contains('\n')) ? "\"" + v.Replace("\"", "\"\"") + "\"" : v;
}
