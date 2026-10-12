using UDP_App.Models;
using System.Text;
using System.Text.Json;

namespace UDP_App.Services;

public class DataService01
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };
    private readonly IWebHostEnvironment _env;

    public DataService01(IWebHostEnvironment env) => _env = env;

    public string ToJson(Section01Model model) => JsonSerializer.Serialize(model, JsonOpts);

    public string ToCsv(Section01Model model)
    {
        var sb = new StringBuilder();
        sb.AppendLine("field_name,value");
        foreach (var prop in typeof(Section01Model).GetProperties())
        {
            var attr = (System.Text.Json.Serialization.JsonPropertyNameAttribute?)
                Attribute.GetCustomAttribute(prop, typeof(System.Text.Json.Serialization.JsonPropertyNameAttribute));
            var key = attr?.Name ?? prop.Name;
            sb.AppendLine($"{key},{CsvEscape(prop.GetValue(model) as string)}");
        }
        return sb.ToString();
    }

    public string ToPlainText(Section01Model model)
    {
        var sb = new StringBuilder();
        void L(string h, string? v) { sb.AppendLine(h); sb.AppendLine(v ?? string.Empty); sb.AppendLine(); }
        L("1.1.1 Primary and Secondary Objectives and Estimands", model.C218839);
        L("1.1.2 Overall Design – Table ID",                      model.TABLE_X);
        L("1.2 Trial Schema",                                      model.C218720);
        L("1.3 Schedule of Activities",                            model.C132349);
        sb.AppendLine("── Design Table ──");
        L("Intervention Model",                   model.C98746);
        L("Population Type",                      model.C218703);
        L("Control Type",                         model.C49647);
        L("Condition",                            model.C112038);
        L("Minimum Age",                          model.C49693);
        L("Maximum Age",                          model.C49694);
        L("Intervention Assignment Method",       model.C218475);
        L("Randomisation Type",                   model.C223137);
        L("Other Assignment Method",              model.CC223138);
        L("Stratification Indicator",             model.C223136);
        L("Site Distribution",                    model.C218704);
        L("Site Geographic Scope",                model.C218705);
        L("Master Protocol Indicator",            model.C218707);
        L("Adaptive Trial Design Indicator",      model.C218706);
        L("Number of Arms",                       model.C98771);
        L("Trial Blind Schema",                   model.C49658);
        L("Blinded Roles",                        model.C218709);
        L("Target-Maximum",                       model.C218710);
        L("Number of Participants",               model.C49692);
        L("Alt. Duration Description",            model.C218714);
        L("Total Planned Duration (Participation)",model.C218715);
        L("Independent Committees",               model.C218718);
        L("Other Committees",                     model.C218719);
        sb.AppendLine("── Comparator Product ──");
        L("Comparator Product ID",   model.COMPARATOR_PRODUCT);
        L("Sponsor IP Code",         model.C218675);
        L("Nonproprietary Name",     model.C97054);
        return sb.ToString();
    }

    public string ToFsh(Section01Model model)
    {
        var mainPath  = Path.Combine(_env.ContentRootPath, "Templates", "Section_01_Template.fsh");
        var tablePath = Path.Combine(_env.ContentRootPath, "Templates", "Section_01_Table_Template.fsh");
        var compPath  = Path.Combine(_env.ContentRootPath, "Templates", "Section_01_Comparator_Product.fsh");
        string main, tbl, comp;
        try { main = File.ReadAllText(mainPath);  } catch { return $"Error: could not read {mainPath}"; }
        try { tbl  = File.ReadAllText(tablePath); } catch { return $"Error: could not read {tablePath}"; }
        try { comp = File.ReadAllText(compPath);  } catch { return $"Error: could not read {compPath}"; }

        var tableId      = string.IsNullOrWhiteSpace(model.TABLE_X)             ? "DesignTable-01"       : model.TABLE_X!;
        var comparatorId = string.IsNullOrWhiteSpace(model.COMPARATOR_PRODUCT)  ? "ComparatorProduct-01" : model.COMPARATOR_PRODUCT!;

        // Main composition template
        foreach (var (k, v) in new Dictionary<string, string?>
        {
            {"C218839", model.C218839}, {"TABLE_X", tableId},
            {"C218720", model.C218720}, {"C132349", model.C132349},
        })
            main = main.Replace("{{" + k + "}}", v ?? string.Empty);

        // Design table template
        foreach (var (k, v) in new Dictionary<string, string?>
        {
            {"TABLE_X",    tableId},       {"COMPARATOR_PRODUCT", comparatorId},
            {"C98746",     model.C98746},  {"C218703",  model.C218703},
            {"C49647",     model.C49647},  {"C112038",  model.C112038},
            {"C49693",     model.C49693},  {"C49694",   model.C49694},
            {"C218475",    model.C218475}, {"C223137",  model.C223137},
            {"CC223138",   model.CC223138},{"C223136",  model.C223136},
            {"C218704",    model.C218704}, {"C218705",  model.C218705},
            {"C218707",    model.C218707}, {"C218706",  model.C218706},
            {"C98771",     model.C98771},  {"C49658",   model.C49658},
            {"C218709",    model.C218709}, {"C218710",  model.C218710},
            {"C49692",     model.C49692},  {"C218714",  model.C218714},
            {"C218715",    model.C218715}, {"C218718",  model.C218718},
            {"C218719",    model.C218719},
        })
            tbl = tbl.Replace("{{" + k + "}}", v ?? string.Empty);

        // Comparator product template
        foreach (var (k, v) in new Dictionary<string, string?>
        {
            {"COMPARATOR_PRODUCT", comparatorId},
            {"C218675", model.C218675},
            {"C97054",  model.C97054},
        })
            comp = comp.Replace("{{" + k + "}}", v ?? string.Empty);

        return main + "\r\n" + tbl + "\r\n" + comp;
    }

    public Section01Model? LoadSample()
    {
        try { return FromJson(File.ReadAllText(Path.Combine(_env.ContentRootPath, "Data", "section_01_sample.json"))); }
        catch { return null; }
    }

    public Section01Model? FromJson(string json)
    {
        try { return JsonSerializer.Deserialize<Section01Model>(json, JsonOpts); }
        catch { return null; }
    }

    private static string CsvEscape(string? v) =>
        string.IsNullOrEmpty(v) ? "\"\"" : "\"" + v.Replace("\"", "\"\"") + "\"";
}
