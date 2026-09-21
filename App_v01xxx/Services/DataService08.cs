using UDP_App.Models;
using System.Text;
using System.Text.Json;

namespace UDP_App.Services;

public class DataService08
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };
    private readonly IWebHostEnvironment _env;

    public DataService08(IWebHostEnvironment env) => _env = env;

    public string ToJson(Section08Model model) => JsonSerializer.Serialize(model, JsonOpts);

    public string ToCsv(Section08Model model)
    {
        var sb = new StringBuilder();
        sb.AppendLine("field_name,value");
        foreach (var prop in typeof(Section08Model).GetProperties())
        {
            var attr = (System.Text.Json.Serialization.JsonPropertyNameAttribute?)
                Attribute.GetCustomAttribute(prop, typeof(System.Text.Json.Serialization.JsonPropertyNameAttribute));
            sb.AppendLine($"{attr?.Name ?? prop.Name},{CsvEscape(prop.GetValue(model) as string)}");
        }
        return sb.ToString();
    }

    public string ToPlainText(Section08Model model)
    {
        var sb = new StringBuilder();
        void L(string h, string? v) { sb.AppendLine(h); sb.AppendLine(v ?? string.Empty); sb.AppendLine(); }
        L("8.1 Trial Assessments and Procedures Considerations", model.C218769);
        L("8.2.1 Screening Assessments and Procedures",          model.C218770);
        L("8.2.2 Baseline Assessments and Procedures",           model.C218771);
        L("8.3 Efficacy Assessments and Procedures",             model.C218772);
        L("8.4 Safety Assessments and Procedures",               model.C218773);
        L("8.4.1 Physical Examination",                          model.C20989);
        L("8.4.2 Vital Signs",                                   model.C154628);
        L("8.4.3 Electrocardiograms",                            model.C168186);
        L("8.4.4 Clinical Laboratory Assessments",               model.C218774);
        L("8.4.5 Pregnancy Testing",                             model.C92949);
        L("8.4.6 Suicidal Ideation and Behaviour Risk Monitoring",model.C218775);
        L("8.5 Pharmacokinetics",                                model.C218776);
        L("8.6.1 Genetics and Pharmacogenomics",                 model.C218777);
        L("8.6.2 Pharmacodynamic Biomarkers",                    model.C218778);
        L("8.6.3 Other Biomarkers",                              model.C218779);
        L("8.7 Immunogenicity Assessments",                      model.C218780);
        L("8.8 Medical Resource Utilisation and Health Economics",model.C176849);
        return sb.ToString();
    }

    public string ToFsh(Section08Model model)
    {
        var path = Path.Combine(_env.ContentRootPath, "Templates", "Section_08_Template.fsh");
        string template;
        try { template = File.ReadAllText(path); }
        catch { return $"Error: could not read {path}"; }
        var fields = new Dictionary<string, string?>
        {
            {"C218769",model.C218769},{"C218770",model.C218770},{"C218771",model.C218771},
            {"C218772",model.C218772},{"C218773",model.C218773},{"C20989",model.C20989},
            {"C154628",model.C154628},{"C168186",model.C168186},{"C218774",model.C218774},
            {"C92949",model.C92949},  {"C218775",model.C218775},{"C218776",model.C218776},
            {"C218777",model.C218777},{"C218778",model.C218778},{"C218779",model.C218779},
            {"C218780",model.C218780},{"C176849",model.C176849},
        };
        foreach (var (k, v) in fields)
            template = template.Replace("{{" + k + "}}", v ?? string.Empty);
        return template;
    }

    public Section08Model? LoadSample()
    {
        try { return FromJson(File.ReadAllText(Path.Combine(_env.ContentRootPath, "Data", "section_08_sample.json"))); }
        catch { return null; }
    }

    public Section08Model? FromJson(string json)
    {
        try { return JsonSerializer.Deserialize<Section08Model>(json, JsonOpts); }
        catch { return null; }
    }

    private static string CsvEscape(string? v) =>
        string.IsNullOrEmpty(v) ? "\"\"" : "\"" + v.Replace("\"", "\"\"") + "\"";
}
