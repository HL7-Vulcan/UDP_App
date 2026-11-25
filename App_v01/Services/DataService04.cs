using UDP_App.Models;
using System.Text;
using System.Text.Json;

namespace UDP_App.Services;

public class DataService04
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };
    private readonly IWebHostEnvironment _env;

    public DataService04(IWebHostEnvironment env) { _env = env; }

    public string ToJson(Section04Model m) => JsonSerializer.Serialize(m, JsonOpts);

    public Section04Model? FromJson(string json)
    {
        try { return JsonSerializer.Deserialize<Section04Model>(json, JsonOpts); }
        catch { return null; }
    }

    public string ToCsv(Section04Model m)
    {
        var sb = new StringBuilder();
        sb.AppendLine("field_name,value");
        void Row(string k, string? v) => sb.AppendLine($"{k},{CsvEscape(v ?? string.Empty)}");
        Row("C147139", m.C147139); Row("C218726", m.C218726); Row("C219658", m.C219658);
        Row("C218727", m.C218727); Row("C218728", m.C218728); Row("C218729", m.C218729);
        Row("C142705", m.C142705); Row("C218730", m.C218730); Row("C215629", m.C215629);
        Row("C218731", m.C218731); Row("C218732", m.C218732); Row("C218733", m.C218733);
        Row("C218734", m.C218734); Row("C218735", m.C218735);
        Row("C142698", m.C142698); Row("C218736", m.C218736);
        Row("C218737", m.C218737); Row("C218738", m.C218738);
        return sb.ToString();
    }

    public string ToPlainText(Section04Model m)
    {
        var sb = new StringBuilder();
        void Line(string h, string? v) { sb.AppendLine(h); sb.AppendLine(v ?? string.Empty); sb.AppendLine(); }
        Line("4.1 Trial Design & Intervention Model",       m.C147139);
        Line("4.1 Trial Duration",                          m.C218726);
        Line("4.1 Method of Assignment",                    m.C219658);
        Line("4.1 Level and Method of Blinding",            m.C218727);
        Line("4.1 Additional Description",                  m.C218728);
        Line("4.1 Stakeholder Input",                       m.C218729);
        Line("4.2 Overall Rationale",                       m.C142705);
        Line("4.2.1 Rationale for Estimand(s)",             m.C218730);
        Line("4.2.2 Rationale for Intervention Model",      m.C215629);
        Line("4.2.3 Rationale for Control Type",            m.C218731);
        Line("4.2.4 Rationale for Trial Duration",          m.C218732);
        Line("4.2.5 Rationale for Adaptive/Novel Design",   m.C218733);
        Line("4.2.5 Rationale for Interim Analysis",        m.C218734);
        Line("4.2.6 Rationale for Other Aspects",           m.C218735);
        Line("4.3 Trial Stopping Rules",                    m.C142698);
        Line("4.4 Start of Trial",                          m.C218736);
        Line("4.4 End of Trial",                            m.C218737);
        Line("4.4 Access After End of Trial",               m.C218738);
        return sb.ToString();
    }

    public string ToFsh(Section04Model m)
    {
        var path = Path.Combine(_env.ContentRootPath, "Templates", "Section_04_Template.fsh");
        try
        {
            var t = File.ReadAllText(path);
            t = t.Replace("{{C147139}}", m.C147139  ?? string.Empty);
            t = t.Replace("{{C218726}}", m.C218726  ?? string.Empty);
            t = t.Replace("{{C219658}}", m.C219658  ?? string.Empty);
            t = t.Replace("{{C218727}}", m.C218727  ?? string.Empty);
            t = t.Replace("{{C218728}}", m.C218728  ?? string.Empty);
            t = t.Replace("{{C218729}}", m.C218729  ?? string.Empty);
            t = t.Replace("{{C142705}}", m.C142705  ?? string.Empty);
            t = t.Replace("{{C218730}}", m.C218730  ?? string.Empty);
            t = t.Replace("{{C215629}}", m.C215629  ?? string.Empty);
            t = t.Replace("{{C218731}}", m.C218731  ?? string.Empty);
            t = t.Replace("{{C218732}}", m.C218732  ?? string.Empty);
            t = t.Replace("{{C218733}}", m.C218733  ?? string.Empty);
            t = t.Replace("{{C218734}}", m.C218734  ?? string.Empty);
            t = t.Replace("{{C218735}}", m.C218735  ?? string.Empty);
            t = t.Replace("{{C142698}}", m.C142698  ?? string.Empty);
            t = t.Replace("{{C218736}}", m.C218736  ?? string.Empty);
            t = t.Replace("{{C218737}}", m.C218737  ?? string.Empty);
            t = t.Replace("{{C218738}}", m.C218738  ?? string.Empty);
            return t;
        }
        catch { return "// Section 4 FSH template not found."; }
    }

    public (Section04Model? Model, string? Error) LoadSample()
    {
        var path = Path.Combine(_env.ContentRootPath, "Data", "section_04_sample.json");
        if (!File.Exists(path))
            return (null, $"Sample data file not found: Data/section_04_sample.json");
        try
        {
            var model = JsonSerializer.Deserialize<Section04Model>(File.ReadAllText(path), JsonOpts);
            return (model, null);
        }
        catch (Exception ex)
        {
            return (null, $"Failed to load sample data: {ex.Message}");
        }
    }

    private static string CsvEscape(string v) =>
        (v.Contains(',') || v.Contains('"') || v.Contains('\n'))
            ? "\"" + v.Replace("\"", "\"\"") + "\""
            : v;
}
