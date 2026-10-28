using UDP_App.Models;
using System.Text;
using System.Text.Json;

namespace UDP_App.Services;

public class DataService12
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };
    private readonly IWebHostEnvironment _env;

    public DataService12(IWebHostEnvironment env)
    {
        _env = env;
    }

    public string ToJson(Section12Model model) =>
        JsonSerializer.Serialize(model, JsonOpts);

    public Section12Model? FromJson(string json)
    {
        try { return JsonSerializer.Deserialize<Section12Model>(json, JsonOpts); }
        catch { return null; }
    }

    public string ToCsv(Section12Model model)
    {
        var sb = new StringBuilder();
        sb.AppendLine("field_name,value");
        sb.AppendLine($"C25294,{CsvEscape(model.C25294 ?? string.Empty)}");
        sb.AppendLine($"C218836,{CsvEscape(model.C218836 ?? string.Empty)}");
        foreach (var (k, v) in model.CrData)
            sb.AppendLine($"{k},{CsvEscape(v)}");
        foreach (var (k, v) in model.AmendData)
            sb.AppendLine($"{k},{CsvEscape(v)}");
        foreach (var (k, v) in model.ChgData)
            sb.AppendLine($"{k},{CsvEscape(v)}");
        return sb.ToString();
    }

    public string ToPlainText(Section12Model model)
    {
        var sb = new StringBuilder();
        sb.AppendLine("12.1 Clinical Laboratory Tests");
        sb.AppendLine(model.C25294 ?? string.Empty);
        sb.AppendLine();

        if (model.CrRows > 0)
        {
            sb.AppendLine("12.2 Country/Region-Specific Differences");
            for (int r = 1; r <= model.CrRows; r++)
            {
                sb.AppendLine($"  Row {r}:");
                sb.AppendLine($"    Identifier:     {model.CrData.GetValueOrDefault($"C218832_r{r}", string.Empty)}");
                sb.AppendLine($"    Requirements:   {model.CrData.GetValueOrDefault($"C218833_r{r}", string.Empty)}");
                sb.AppendLine($"    Clarifications: {model.CrData.GetValueOrDefault($"C218834_r{r}", string.Empty)}");
            }
            sb.AppendLine();
        }

        sb.AppendLine("12.3 Prior Protocol Amendment(s)");
        for (int r = 0; r <= model.AmendRows; r++)
        {
            var id   = r == 0 ? "Original Protocol" : model.AmendData.GetValueOrDefault($"C218477_r{r}", string.Empty);
            var date = model.AmendData.GetValueOrDefault($"C132352_r{r}", string.Empty);
            sb.AppendLine($"  {id} | Approved: {date}");
        }

        if (!string.IsNullOrWhiteSpace(model.C218836))
        {
            sb.AppendLine($"  Alternate location: {model.C218836}");
        }

        sb.AppendLine();
        return sb.ToString();
    }

    public string ToFsh(Section12Model model)
    {
        var templatePath = Path.Combine(_env.ContentRootPath, "Templates", "Section_12_Template.fsh");
        string template;
        try { template = File.ReadAllText(templatePath); }
        catch { return "// Section 12 FSH template not found."; }

        template = template.Replace("{{C25294}}",   model.C25294  ?? string.Empty);
        template = template.Replace("{{C218836}}", model.C218836 ?? string.Empty);
        return template;
    }

    public Section12Model? LoadSample()
    {
        var path = Path.Combine(_env.ContentRootPath, "Data", "section_12_sample.json");
        try
        {
            if (!File.Exists(path)) return BuildSample();
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Section12Model>(json, JsonOpts);
        }
        catch { return BuildSample(); }
    }

    private Section12Model BuildSample()
    {
        var m = new Section12Model
        {
            C25294   = "Standard haematology, biochemistry, and urinalysis will be performed at a central laboratory. Details of laboratory tests and normal ranges are provided in the Laboratory Manual.",
            C218836  = string.Empty,
            CrRows   = 1,
            AmendRows = 1,
        };

        m.CrData["C218832_r1"] = "Japan";
        m.CrData["C218833_r1"] = "Additional pharmacokinetic sampling required per Japanese regulatory guidelines.";
        m.CrData["C218834_r1"] = "Samples to be taken at Weeks 4 and 8 in addition to standard schedule.";

        // Original Protocol row 0
        m.AmendData["C132352_r0"] = "2022-06-01";

        // Amendment 1
        m.AmendData["C218477_r1"] = "EX2010/12(a)";
        m.AmendData["C132352_r1"] = "2023-11-15";
        m.AmendData["C218695_r1"] = "45";
        m.AmendData["C218695e_r1"] = "$NCIT#C68846 \"Globally\"";

        m.ChgRows["1"] = 2;
        m.ChgData["C218483_a1_r1"] = "Updated exclusion criterion 4 to exclude patients with elevated liver enzymes (>3× ULN).";
        m.ChgData["C181233_a1_r1"] = "New safety signal identified in interim data review.";
        m.ChgData["C218479_a1_r1"] = "$NCIT#C217345 \"Section 4\"";
        m.ChgData["C218483_a1_r2"] = "Added additional safety monitoring visit at Week 12.";
        m.ChgData["C181233_a1_r2"] = "Required by updated safety monitoring plan.";
        m.ChgData["C218479_a1_r2"] = "$NCIT#C217346 \"Section 5\"";

        return m;
    }

    private static string CsvEscape(string v)
    {
        if (v.Contains(',') || v.Contains('"') || v.Contains('\n'))
            return "\"" + v.Replace("\"", "\"\"") + "\"";
        return v;
    }
}
