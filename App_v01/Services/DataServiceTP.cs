using UDP_App.Models;
using System.Text;
using System.Text.Json;

namespace UDP_App.Services;

public class DataServiceTP
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };
    private readonly IWebHostEnvironment _env;

    public DataServiceTP(IWebHostEnvironment env) => _env = env;

    public string ToJson(TitlePageModel model) => JsonSerializer.Serialize(model, JsonOpts);

    public string ToCsv(TitlePageModel model)
    {
        var sb = new StringBuilder();
        sb.AppendLine("field_name,value");
        foreach (var prop in typeof(TitlePageModel).GetProperties())
        {
            var attr = (System.Text.Json.Serialization.JsonPropertyNameAttribute?)
                Attribute.GetCustomAttribute(prop, typeof(System.Text.Json.Serialization.JsonPropertyNameAttribute));
            var key = attr?.Name ?? prop.Name;
            var val = prop.GetValue(model);
            if (val is string s) sb.AppendLine($"{key},{CsvEscape(s)}");
        }
        return sb.ToString();
    }

    public string ToPlainText(TitlePageModel model)
    {
        var sb = new StringBuilder();
        void L(string h, string? v) { sb.AppendLine(h); sb.AppendLine(v ?? string.Empty); sb.AppendLine(); }
        sb.AppendLine("TITLE PAGE"); sb.AppendLine(new string('-', 40));
        L("Confidentiality Statement",     model.C181236);
        L("Full Title",                    model.C132346);
        L("Trial Acronym",                 model.C94108);
        L("Sponsor Protocol Identifier",   model.C132351);
        L("Original Protocol",             model.C218672);
        L("Version Number",                model.C181232);
        L("Version Date",                  model.C93813);
        L("Amendment Identifier",          model.C218477);
        L("Amendment Scope",               model.C218673);
        L("Country",                       model.C20108);
        L("Region",                        model.C218674);
        L("Site Identifier",               model.C83081);
        L("Sponsor IP Code",               model.C218675);
        L("Nonproprietary Name",           model.C97054);
        L("Proprietary Name",              model.C71898);
        L("Trial Phase",                   model.C48281);
        L("Short Title",                   model.C94105);
        L("Sponsor Name",                  model.C222495);
        L("Sponsor Legal Address",         model.C218677);
        L("Co-Sponsor Name",               model.C218678);
        L("Co-Sponsor Legal Address",      model.C218679);
        L("Local Sponsor Name",            model.C218680);
        L("Local Sponsor Legal Address",   model.C218681);
        L("Device Manufacturer Name",      model.C218682);
        L("Device Manufacturer Address",   model.C218683);
        L("EU CT Number",                  model.C218684);
        L("FDA IND Number",                model.C218685);
        L("IDE Number",                    model.C218686);
        L("jRCT Number",                   model.C218687);
        L("NCT Number",                    model.C172240);
        L("NMPA IND Number",               model.C218688);
        L("WHO/UTN Number",                model.C218689);
        L("Other Regulatory Identifier",   model.C218690);
        L("Sponsor Approval Date",         model.C132352);
        L("Signature URL",                 model.C218484);
        L("Sponsor Signatory",             model.C222014);
        L("Signatory Location",            model.C222064);
        L("Medical Expert Contact",        model.C218693);
        L("Medical Expert Location",       model.C222063);
        sb.AppendLine("AMENDMENT DETAILS"); sb.AppendLine(new string('-', 40));
        L("Amendment Status",              model.C218694);
        L("Approx Enrolled",               model.C218478);
        L("Scope Enrollment Definition",   model.C218695);
        L("Primary Reason",                model.C218696);
        L("Secondary Reason",              model.C218697);
        L("Other Reason Text",             model.C17649);
        L("Amendment Summary",             model.C42581);
        L("Substantial Impact Safety",     model.C218698);
        L("Impact Safety Comment",         model.C218699);
        L("Substantial Impact Reliability",model.C218700);
        L("Impact Reliability Comment",    model.C218701);
        return sb.ToString();
    }

    public string ToFsh(TitlePageModel model)
    {
        var tplPath = Path.Combine(_env.ContentRootPath, "Templates", "Title_Page_Template.fsh");
        string tpl;
        try { tpl = File.ReadAllText(tplPath); } catch { return $"Error: could not read {tplPath}"; }

        // Build a lookup of all scalar tokens (including trimmed-key variants in the template)
        var fields = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
        {
            {"C181236",model.C181236}, {"C132346",model.C132346}, {"C94108", model.C94108},
            {"C132351",model.C132351}, {"C218672",model.C218672}, {"C181232",model.C181232},
            {"C93813", model.C93813},  {"C218477",model.C218477}, {"C218673",model.C218673},
            {"C20108", model.C20108},  {"C218674",model.C218674}, {"C83081", model.C83081},
            {"C218675",model.C218675}, {"C97054", model.C97054},  {"C71898", model.C71898},
            {"C48281", model.C48281},  {"C94105", model.C94105},  {"C222495",model.C222495},
            {"C218677",model.C218677}, {"C218678",model.C218678}, {"C218679",model.C218679},
            {"C218680",model.C218680}, {"C218681",model.C218681}, {"C218682",model.C218682},
            {"C218683",model.C218683}, {"C218684",model.C218684}, {"C218685",model.C218685},
            {"C218686",model.C218686}, {"C218687",model.C218687}, {"C172240",model.C172240},
            {"C218688",model.C218688}, {"C218689",model.C218689}, {"C218690",model.C218690},
            {"C132352",model.C132352}, {"C218484",model.C218484}, {"C222014",model.C222014},
            {"C222064",model.C222064}, {"C218693",model.C218693}, {"C222063",model.C222063},
            // Named references (static — reference IDs not stored as form fields)
            {"SponsorOrganization",      "SponsorOrganization"},
            {"SponsorPractitioner",      "SponsorPractitioner"},
            {"Sponsor-Expert-Practitioner", "Sponsor-Expert-Practitioner"},
            {"Protocol-Amendment",       "Protocol-Amendment"},
            // REPEAT-block inner tokens that reference named resources or are empty
            {"InvestigationalMedicinalProduct", "InvestigationalMedicinalProduct"},
            {"CoSponsorOrganization",    "CoSponsorOrganization"},
            {"CoSponsorPractitioner",    "CoSponsorPractitioner"},
            {"LocalSponsorOrganization", "LocalSponsorOrganization"},
            {"LocalSponsorPractitioner", "LocalSponsorPractitioner"},
            {"DevicesOrganization",      "DevicesOrganization"},
        };

        // Convert C222014 to base64 before substitution
        var c222014Raw = model.C222014 ?? string.Empty;
        fields["C222014"] = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(c222014Raw));

        // getValue: trim key (template has "C218690 " with trailing space on line 64)
        string? getValue(string key)
        {
            var k = key.Trim();
            return fields.TryGetValue(k, out var v) ? v : null;
        }

        // Determine which REPEAT blocks to expand vs. suppress based on model data
        // Multi-line REPEAT blocks: expand if the relevant model fields are non-empty,
        // suppress (remove the block entirely) if empty.
        // Single-line REPEAT (line 87): always expand once — IMP reference is static.

        // Pass 1: expand single-line REPEAT (InvestigationalMedicinalProduct)
        var result = tpl;
        while (true)
        {
            var r = TokenParser.FindAndExpandRepeat(result, new[] { "InvestigationalMedicinalProduct" });
            if (!r.HasValue) break;
            var (exp, ms, ml) = r.Value;
            result = result.Remove(ms, ml).Insert(ms, exp);
        }

        // Pass 2: expand or suppress multi-line REPEAT blocks
        // CoSponsor block: expand if C218678 non-empty
        result = ExpandOrSuppress(result, "CoSponsorOrganization",
            !string.IsNullOrWhiteSpace(model.C218678),
            new[] { "CoSponsorOrganization", "CoSponsorPractitioner" });

        // LocalSponsor block: expand if C218680 non-empty
        result = ExpandOrSuppress(result, "LocalSponsorOrganization",
            !string.IsNullOrWhiteSpace(model.C218680),
            new[] { "LocalSponsorOrganization", "LocalSponsorPractitioner" });

        // Devices block: expand if C218682 non-empty
        result = ExpandOrSuppress(result, "DevicesOrganization",
            !string.IsNullOrWhiteSpace(model.C218682),
            new[] { "DevicesOrganization" });

        // Amendment block: expand if C218694 non-empty and not "U101"
        result = ExpandOrSuppress(result, "Protocol-Amendment",
            !string.IsNullOrWhiteSpace(model.C218694) && model.C218694 != "U101",
            new[] { "Protocol-Amendment" });

        // Pass 3: substitute all remaining scalar tokens
        result = TokenParser.Substitute(result, getValue);

        return result;
    }

    // Expand a REPEAT block (first one whose content contains the sentinel token) once,
    // or strip it entirely if expand=false.
    private static string ExpandOrSuppress(string tpl, string sentinel, bool expand,
        IEnumerable<string> rowIds)
    {
        if (expand)
        {
            var r = TokenParser.FindAndExpandRepeat(tpl, rowIds);
            if (r.HasValue)
            {
                var (exp, ms, ml) = r.Value;
                return tpl.Remove(ms, ml).Insert(ms, exp);
            }
            return tpl;
        }
        else
        {
            // Strip the REPEAT block containing this sentinel
            int idx = tpl.IndexOf("{{REPEAT", StringComparison.Ordinal);
            while (idx >= 0)
            {
                int braceOpen  = idx + "REPEAT".Length;
                int braceClose = TokenParser.FindTokenEnd(tpl, braceOpen);
                if (braceClose < 0) break;
                string inner = tpl.Substring(braceOpen + 2, braceClose - braceOpen - 4);
                if (inner.Contains(sentinel, StringComparison.Ordinal))
                {
                    // Walk back to line start to remove the {{REPEAT line itself
                    int lineStart = idx;
                    while (lineStart > 0 && tpl[lineStart - 1] != '\n' && tpl[lineStart - 1] != '\r')
                        lineStart--;
                    // Consume trailing newline after closing }}
                    int blockEnd = braceClose;
                    if (blockEnd < tpl.Length && tpl[blockEnd] == '\r') blockEnd++;
                    if (blockEnd < tpl.Length && tpl[blockEnd] == '\n') blockEnd++;
                    return tpl.Substring(0, lineStart) + tpl.Substring(blockEnd);
                }
                idx = tpl.IndexOf("{{REPEAT", braceClose, StringComparison.Ordinal);
            }
            return tpl;
        }
    }

    public TitlePageModel? LoadSample()
    {
        try { return FromJson(File.ReadAllText(Path.Combine(_env.ContentRootPath, "Data", "title_page_sample.json"))); }
        catch { return null; }
    }

    public TitlePageModel? FromJson(string json)
    {
        try { return JsonSerializer.Deserialize<TitlePageModel>(json, JsonOpts); }
        catch { return null; }
    }

    private static string CsvEscape(string? v) =>
        string.IsNullOrEmpty(v) ? "\"\"" : "\"" + v.Replace("\"", "\"\"") + "\"";
}
