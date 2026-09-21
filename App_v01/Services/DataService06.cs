using UDP_App.Models;
using System.Text;
using System.Text.Json;

namespace UDP_App.Services;

public class DataService06
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };
    private readonly IWebHostEnvironment _env;

    public DataService06(IWebHostEnvironment env) => _env = env;

    public string ToJson(Section06Model model) => JsonSerializer.Serialize(model, JsonOpts);

    public string ToCsv(Section06Model model)
    {
        var sb = new StringBuilder();
        sb.AppendLine("field_name,value");
        foreach (var prop in typeof(Section06Model).GetProperties())
        {
            var attr = (System.Text.Json.Serialization.JsonPropertyNameAttribute?)
                Attribute.GetCustomAttribute(prop, typeof(System.Text.Json.Serialization.JsonPropertyNameAttribute));
            var key = attr?.Name ?? prop.Name;
            if (key is "TblRows" or "TblData") continue;
            sb.AppendLine($"{key},{CsvEscape(prop.GetValue(model) as string)}");
        }
        foreach (var (k, v) in model.TblData)
            sb.AppendLine($"{k},{CsvEscape(v)}");
        return sb.ToString();
    }

    public string ToPlainText(Section06Model model)
    {
        var sb = new StringBuilder();
        void L(string h, string? v) { sb.AppendLine(h); sb.AppendLine(v ?? string.Empty); sb.AppendLine(); }
        L("6 Trial Intervention and Concomitant Therapy Overview", model.C218747);
        L("6.1 Description of Investigational Trial Intervention",   model.C218751);
        L("6.2 Rationale for Dose and Regimen",                      model.C218752);
        L("6.3 Investigational Trial Intervention Administration",    model.C218753);
        L("6.4 Dose Modification",                                   model.C218754);
        L("6.5 Management of Overdose",                              model.C218755);
        L("6.6.1 Preparation",                                       model.C176274);
        L("6.6.2 Storage and Handling",                              model.C115525);
        L("6.6.3 Accountability",                                    model.C176267);
        L("6.7.1 Participant Assignment",                            model.C218756);
        L("6.7.2 Randomisation",                                     model.C25196);
        L("6.7.3 Measures to Maintain Blinding",                     model.C189349);
        L("6.7.4 Emergency Unblinding",                              model.C218757);
        L("6.8 Intervention Adherence",                              model.C218758);
        L("6.9 Noninvestigational Trial Intervention",               model.C218759);
        L("6.9.1 Background Trial Intervention",                     model.C222329);
        L("6.9.2 Rescue Therapy",                                    model.C222330);
        L("6.9.3 Other Noninvestigational Trial Intervention",       model.C218761);
        L("6.10 Concomitant Therapy",                                model.C53630);
        L("6.10.1 Prohibited Concomitant Therapy",                   model.C218762);
        L("6.10.2 Permitted Concomitant Therapy",                    model.C218763);
        if (model.TblData.Any())
        {
            sb.AppendLine("6 Intervention Table");
            foreach (var (k, v) in model.TblData) sb.AppendLine($"  {k}: {v}");
        }
        return sb.ToString();
    }

    public string ToFsh(Section06Model model)
    {
        var mainPath  = Path.Combine(_env.ContentRootPath, "Templates", "Section_06_Template.fsh");
        var tablePath = Path.Combine(_env.ContentRootPath, "Templates", "Section_06_Table_Template.fsh");
        string main, tbl;
        try { main = File.ReadAllText(mainPath); }
        catch { return $"Error: could not read {mainPath}"; }
        try { tbl  = File.ReadAllText(tablePath); }
        catch { return $"Error: could not read {tablePath}"; }

        var fields = new Dictionary<string, string?>
        {
            {"C218747",model.C218747},{"C218751",model.C218751},{"C218752",model.C218752},
            {"C218753",model.C218753},{"C218754",model.C218754},{"C218755",model.C218755},
            {"C176274",model.C176274},{"C115525",model.C115525},{"C176267",model.C176267},
            {"C218756",model.C218756},{"C25196",model.C25196},  {"C189349",model.C189349},
            {"C218757",model.C218757},{"C218758",model.C218758},{"C218759",model.C218759},
            {"C222329",model.C222329},{"C222330",model.C222330},{"C218761",model.C218761},
            {"C53630",model.C53630},  {"C218762",model.C218762},{"C218763",model.C218763},
        };
        foreach (var (k, v) in fields)
            main = main.Replace("{{" + k + "}}", v ?? string.Empty);

        var rowCount   = Math.Max(1, model.TblRows);
        var entries    = new StringBuilder();
        var rowBlocks  = new StringBuilder();
        var tblCodes   = new[] {"C93729","C172457","C177930","C98747","C42636","C142517","C94394","C38114","C15697","C218748","C218749","C218750"};

        for (int r = 1; r <= rowCount; r++)
        {
            var rowId = $"InterventionTable-Row{r}";
            entries.AppendLine($"  * entry[+] = Reference({rowId})");
            var rowBlock = tbl.Replace("{{TABLE_ROW_X}}", rowId);
            foreach (var code in tblCodes)
            {
                var val = model.TblData.TryGetValue($"{code}_r{r}", out var v) ? v : string.Empty;
                rowBlock = rowBlock.Replace("{{" + code + "}}", val);
            }
            rowBlocks.AppendLine(); rowBlocks.Append(rowBlock);
        }
        main = main.Replace("{{REPEAT  * entry[+] = Reference({{TABLE_ROW_X}})}}", entries.ToString().TrimEnd());
        return main + rowBlocks.ToString();
    }

    public Section06Model? LoadSample()
    {
        try { return FromJson(File.ReadAllText(Path.Combine(_env.ContentRootPath, "Data", "section_06_sample.json"))); }
        catch { return null; }
    }

    public Section06Model? FromJson(string json)
    {
        try { return JsonSerializer.Deserialize<Section06Model>(json, JsonOpts); }
        catch { return null; }
    }

    private static string CsvEscape(string? v) =>
        string.IsNullOrEmpty(v) ? "\"\"" : "\"" + v.Replace("\"", "\"\"") + "\"";
}
