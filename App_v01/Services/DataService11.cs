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
        void Row(string key, string? val) => sb.AppendLine($"{key},{CsvEscape(val ?? string.Empty)}");
        Row("C218811", m.C218811);
        Row("C218812", m.C218812);
        Row("C218813", m.C218813);
        Row("C218814", m.C218814);
        Row("C184390", m.C184390);
        Row("C218815", m.C218815);
        Row("C218816", m.C218816);
        Row("C218817", m.C218817);
        Row("C218818", m.C218818);
        Row("C218819", m.C218819);
        Row("C218820", m.C218820);
        Row("C218821", m.C218821);
        Row("C218822", m.C218822);
        Row("C218823", m.C218823);
        Row("C218824", m.C218824);
        Row("C218825", m.C218825);
        Row("C218826", m.C218826);
        Row("C222675", m.C222675);
        Row("C218827", m.C218827);
        Row("C218828", m.C218828);
        Row("C218829", m.C218829);
        Row("C218830", m.C218830);
        Row("C218831", m.C218831);
        return sb.ToString();
    }

    public string ToPlainText(Section11Model m)
    {
        var sb = new StringBuilder();
        void Sec(string heading, string? val)
        {
            sb.AppendLine(heading);
            sb.AppendLine(val ?? string.Empty);
            sb.AppendLine();
        }
        sb.AppendLine("11  TRIAL OVERSIGHT AND OTHER GENERAL CONSIDERATIONS");
        sb.AppendLine();
        Sec("11.1  Regulatory and Ethical Considerations", m.C218811);
        Sec("11.2  Trial Oversight", m.C218812);
        Sec("11.2.1  Investigator Responsibilities", m.C218813);
        Sec("11.2.2  Sponsor Responsibilities", m.C218814);
        Sec("11.3  Informed Consent Process – Informed Consent", m.C184390);
        Sec("11.3  Assent", m.C218815);
        Sec("11.3  Emergency Consent", m.C218816);
        Sec("11.3.1  Informed Consent for Rescreening", m.C218817);
        Sec("11.3.2  Informed Consent for Use of Remaining Samples in Exploratory Research", m.C218818);
        Sec("11.4  Committees", m.C218819);
        Sec("11.5  Insurance and Indemnity", m.C218820);
        Sec("11.6  Risk-Based Quality Management", m.C218821);
        Sec("11.7  Data Governance", m.C218822);
        Sec("11.8  Data Protection", m.C218823);
        Sec("11.9  Source Records", m.C218824);
        Sec("11.9  Investigator Expectations", m.C218825);
        Sec("11.9  Trial Monitor Expectations", m.C218826);
        Sec("11.9  Record Identification", m.C222675);
        Sec("11.10  Protocol Deviations", m.C218827);
        Sec("11.11  Early Site Closure – Decision Rights", m.C218828);
        Sec("11.11  Investigator Criteria", m.C218829);
        Sec("11.11  Responsibilities", m.C218830);
        Sec("11.12  Data Dissemination", m.C218831);
        return sb.ToString();
    }

    public string ToFsh(Section11Model m)
    {
        var path = Path.Combine(_env.ContentRootPath, "Templates", "Section_11_Template.fsh");
        try
        {
            var text = File.ReadAllText(path);
            text = text.Replace("{{C218811}}", HtmlEscape(m.C218811));
            text = text.Replace("{{C218812}}", HtmlEscape(m.C218812));
            text = text.Replace("{{C218813}}", HtmlEscape(m.C218813));
            text = text.Replace("{{C218814}}", HtmlEscape(m.C218814));
            text = text.Replace("{{C184390}}", HtmlEscape(m.C184390));
            text = text.Replace("{{C218815}}", HtmlEscape(m.C218815));
            text = text.Replace("{{C218816}}", HtmlEscape(m.C218816));
            text = text.Replace("{{C218817}}", HtmlEscape(m.C218817));
            text = text.Replace("{{C218818}}", HtmlEscape(m.C218818));
            text = text.Replace("{{C218819}}", HtmlEscape(m.C218819));
            text = text.Replace("{{C218820}}", HtmlEscape(m.C218820));
            text = text.Replace("{{C218821}}", HtmlEscape(m.C218821));
            text = text.Replace("{{C218822}}", HtmlEscape(m.C218822));
            text = text.Replace("{{C218823}}", HtmlEscape(m.C218823));
            text = text.Replace("{{C218824}}", HtmlEscape(m.C218824));
            text = text.Replace("{{C218825}}", HtmlEscape(m.C218825));
            text = text.Replace("{{C218826}}", HtmlEscape(m.C218826));
            text = text.Replace("{{C222675}}", HtmlEscape(m.C222675));
            text = text.Replace("{{C218827}}", HtmlEscape(m.C218827));
            text = text.Replace("{{C218828}}", HtmlEscape(m.C218828));
            text = text.Replace("{{C218829}}", HtmlEscape(m.C218829));
            text = text.Replace("{{C218830}}", HtmlEscape(m.C218830));
            text = text.Replace("{{C218831}}", HtmlEscape(m.C218831));
            return text;
        }
        catch { return "// Section 11 FSH template not found."; }
    }

    public (Section11Model? Model, string? Error) LoadSample()
    {
        var path = Path.Combine(_env.ContentRootPath, "Data", "section_11_sample.json");
        if (!File.Exists(path))
            return (null, "Sample data file not found: Data/section_11_sample.json");
        try
        {
            var model = JsonSerializer.Deserialize<Section11Model>(File.ReadAllText(path), JsonOpts);
            return (model, null);
        }
        catch (Exception ex)
        {
            return (null, $"Failed to load sample data: {ex.Message}");
        }
    }

    // FSH template fields go into XHTML divs — escape HTML special characters
    private static string HtmlEscape(string? v)
    {
        if (string.IsNullOrEmpty(v)) return string.Empty;
        return v.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
    }

    private static string CsvEscape(string v) =>
        (v.Contains(',') || v.Contains('"') || v.Contains('\n')) ? "\"" + v.Replace("\"", "\"\"") + "\"" : v;
}
