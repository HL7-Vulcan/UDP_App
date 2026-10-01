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
            // Named references — return empty when defining field is blank
            // so DropEmptyTokenLines suppresses the Reference(...) line
            {"SponsorOrganization",      !string.IsNullOrWhiteSpace(model.C222495) ? "SponsorOrganization" : null},
            {"SponsorPractitioner",      !string.IsNullOrWhiteSpace(model.C222495) ? "SponsorPractitioner" : null},
            {"Sponsor-Expert-Practitioner", !string.IsNullOrWhiteSpace(model.C218693) ? "SponsorExpertPractitioner" : null},
            {"Protocol-Amendment",       "Amendment-001"},
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

        // Pass 1: expand only the single-line REPEAT for InvestigationalMedicinalProduct.
        // We do NOT use FindAndExpandRepeat here because it matches any REPEAT block
        // (including multi-line ones). Instead expand just the specific token inline.
        var result = tpl;
        result = ExpandOrSuppress(result, "InvestigationalMedicinalProduct",
            expand: true,
            rowIds: new[] { "InvestigationalMedicinalProduct" });

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
            new[] { "Amendment-001" });

        // Before substitution, tokenise the hardcoded Sponsor reference lines
        // so DropEmptyTokenLines can suppress them when the defining fields are blank.
        result = result
            .Replace("Reference(SponsorOrganization)", "Reference({{SponsorOrganization}})")
            .Replace("Reference(SponsorPractitioner)", "Reference({{SponsorPractitioner}})");

        // Pass 3: substitute all remaining scalar tokens
        result = TokenParser.Substitute(TokenParser.DropEmptyTokenLines(result, getValue), getValue);

        // Pass 4: drop [=] continuation lines whose [+] opener was suppressed
        result = DropOrphanedContinuationLines(result);

        // Append Amendment instance (if applicable)
        if (!string.IsNullOrWhiteSpace(model.C218694) && model.C218694 != "U101")
            result += BuildAmendmentInstance(model);

        // Append Product instance
        result += BuildProductInstance(model);

        // Append Organisation and Practitioner instances
        result += BuildOrgAndPractitionerInstances(model);

        return result;
    }

    private string BuildAmendmentInstance(TitlePageModel model)
    {
        var tplPath = Path.Combine(_env.ContentRootPath, "Templates", "Amendment_Template.fsh");
        string tpl;
        try { tpl = File.ReadAllText(tplPath); } catch { return string.Empty; }

        var baseFields = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
        {
            {"Protocol-Amendment", "Amendment-001"},
            {"C132351", model.C132351}, {"C218477", model.C218477},
            {"C181232", model.C181232}, {"C93813",  model.C93813},
            {"C218673", model.C218673}, {"C218674", model.C218674},
            {"C83081",  model.C83081},  {"C218695", model.C218695},
            {"C218874", model.C218874}, {"C218696", model.C218696},
            {"C218697", model.C218697}, {"C17649",  model.C17649},
            {"C42581",  model.C42581},  {"C218698", model.C218698},
            {"C218699", model.C218699}, {"C218700", model.C218700},
            {"C218701", model.C218701},
        };

        // Find the REPEAT block and extract its inner template
        int repIdx = tpl.IndexOf("{{REPEAT", StringComparison.Ordinal);
        if (repIdx >= 0)
        {
            // braceOpen points at the first { of {{REPEAT so FindTokenEnd counts correctly
            int braceOpen  = repIdx;
            int braceClose = TokenParser.FindTokenEnd(tpl, braceOpen);
            if (braceClose >= 0)
            {
                // Inner content is after "{{REPEAT" and before the closing "}}"
                int innerStart = repIdx + "{{REPEAT".Length;
                string innerTpl = tpl.Substring(innerStart, braceClose - innerStart - 2);

                // Expand one block per change row
                var rowsSb = new System.Text.StringBuilder();
                int chgCount = Math.Max(1, model.ChgRows);
                for (int r = 1; r <= chgCount; r++)
                {
                    if (r > 1) rowsSb.AppendLine();
                    var rowFields = new Dictionary<string, string?>(baseFields, StringComparer.OrdinalIgnoreCase)
                    {
                        ["C218483"] = GetChg(model, $"C218483_r{r}"),
                        ["C181233"] = GetChg(model, $"C181233_r{r}"),
                        ["C218479"] = GetChg(model, $"C218479_r{r}"),
                    };
                    string? getRow(string key) =>
                        rowFields.TryGetValue(key.Trim(), out var v) ? v : null;
                    rowsSb.Append(TokenParser.Substitute(TokenParser.DropEmptyTokenLines(innerTpl, getRow), getRow));
                }

                // Replace the REPEAT block (from its line start) with the expanded rows
                int lineStart = repIdx;
                while (lineStart > 0 && tpl[lineStart - 1] != '\n' && tpl[lineStart - 1] != '\r')
                    lineStart--;
                // blockEnd: skip past trailing whitespace/tabs on the closing }} line, then the newline
                int blockEnd = braceClose;
                while (blockEnd < tpl.Length && (tpl[blockEnd] == ' ' || tpl[blockEnd] == '\t'))
                    blockEnd++;
                if (blockEnd < tpl.Length && tpl[blockEnd] == '\r') blockEnd++;
                if (blockEnd < tpl.Length && tpl[blockEnd] == '\n') blockEnd++;

                tpl = tpl.Substring(0, lineStart) + rowsSb + tpl.Substring(blockEnd);
            }
        }

        string? get(string key) =>
            baseFields.TryGetValue(key.Trim(), out var v) ? v : null;

        return "\r\n" + TokenParser.Substitute(TokenParser.DropEmptyTokenLines(tpl, get), get);
    }

    private string BuildProductInstance(TitlePageModel model)
    {
        var tplPath = Path.Combine(_env.ContentRootPath, "Templates", "Product_Template.fsh");
        string tpl;
        try { tpl = File.ReadAllText(tplPath); } catch { return string.Empty; }

        var fields = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
        {
            {"MedicinalProductDefinition", "MedicinalProductDefinition"},
            {"C218675", model.C218675},
            {"C97054",  model.C97054},
            {"C71898",  model.C71898},
        };

        string? get(string key) =>
            fields.TryGetValue(key.Trim(), out var v) ? v : null;

        return "\r\n" + TokenParser.Substitute(TokenParser.DropEmptyTokenLines(tpl, get), get);
    }

    // Removes lines using [=] indexer (continuation) when the preceding [+] line was dropped
    private static string DropOrphanedContinuationLines(string text)
    {
        var lines = text.Split('\n');
        var result = new System.Text.StringBuilder();
        bool lastWasParty = false; // true if last non-empty line had [+].party

        foreach (var line in lines)
        {
            var trimmed = line.TrimEnd('\r').TrimEnd();

            // Check if this is a [=].role or [=].* continuation of an associatedParty
            bool isContinuation = trimmed.Contains("[=].") &&
                                  (trimmed.Contains("associatedParty") || trimmed.Contains(".role"));

            if (isContinuation && !lastWasParty)
            {
                // The [+] line was dropped — skip this [=] continuation too
                continue;
            }

            // Track whether this line was a [+].party line (not dropped)
            bool isPartyPlus = trimmed.Contains("[+].party") || trimmed.Contains("[+].role");
            if (!string.IsNullOrWhiteSpace(trimmed))
                lastWasParty = isPartyPlus || (isContinuation && lastWasParty);

            result.Append(line.TrimEnd('\r'));
            result.Append('\n');
        }
        return result.ToString().TrimEnd('\n');
    }

    private static string GetChg(TitlePageModel model, string key) =>
        model.ChgData.TryGetValue(key, out var v) ? v : string.Empty;

    private string BuildOrgAndPractitionerInstances(TitlePageModel model)
    {
        var orgPath  = Path.Combine(_env.ContentRootPath, "Templates", "Organization_Template.fsh");
        var pracPath = Path.Combine(_env.ContentRootPath, "Templates", "Practitioner_Template.fsh");
        string orgTpl, pracTpl;
        try { orgTpl  = File.ReadAllText(orgPath);  } catch { return string.Empty; }
        try { pracTpl = File.ReadAllText(pracPath); } catch { return string.Empty; }

        // Split each template on the instance boundary comment
        var orgInstances  = SplitInstances(orgTpl);
        var pracInstances = SplitInstances(pracTpl);

        var fields = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
        {
            {"C222495", model.C222495}, {"C218677", model.C218677},
            {"C218678", model.C218678}, {"C218679", model.C218679},
            {"C218680", model.C218680}, {"C218681", model.C218681},
            {"C218682", model.C218682}, {"C218683", model.C218683},
            {"C218693", model.C218693}, {"C222063", model.C222063},
        };

        string? get(string key) =>
            fields.TryGetValue(key.Trim(), out var v) ? v : null;

        var sb = new System.Text.StringBuilder();
        sb.AppendLine();
        sb.AppendLine("//---------------------------------------------------------------");
        sb.AppendLine("// Organisations");

        // Ordered candidates: (templateName, nameValue, addressValue, required)
        var orgCandidates = new (string tpl, string name, string addr, bool req)[]
        {
            ("SponsorOrganization",      model.C222495 ?? "", model.C218677 ?? "", true),
            ("CoSponsorOrganization",    model.C218678 ?? "", model.C218679 ?? "", !string.IsNullOrWhiteSpace(model.C218678)),
            ("LocalSponsorOrganization", model.C218680 ?? "", model.C218681 ?? "", !string.IsNullOrWhiteSpace(model.C218680)),
            ("DevicesOrganization",      model.C218682 ?? "", model.C218683 ?? "", !string.IsNullOrWhiteSpace(model.C218682)),
        };
        AppendDeduplicatedInstances(sb, orgInstances, orgCandidates, get);
        AppendInstance(sb, orgInstances, "Exemplar-Regulator-Organization", get, always: true);

        sb.AppendLine();
        sb.AppendLine("//---------------------------------------------------------------");
        sb.AppendLine("// Practitioners");

        var pracCandidates = new (string tpl, string name, string addr, bool req)[]
        {
            ("SponsorPractitioner",       model.C222495 ?? "", model.C218677 ?? "", true),
            ("CoSponsorPractitioner",     model.C218678 ?? "", model.C218679 ?? "", !string.IsNullOrWhiteSpace(model.C218678)),
            ("LocalSponsorPractitioner",  model.C218680 ?? "", model.C218681 ?? "", !string.IsNullOrWhiteSpace(model.C218680)),
            ("SponsorExpertPractitioner", model.C218693 ?? "", model.C222063 ?? "", true),
        };
        AppendDeduplicatedInstances(sb, pracInstances, pracCandidates, get);

        return sb.ToString();
    }

    // Appends instances, merging identical name+address into one block,
    // giving conflicts a numeric suffix (-2, -3 …).
    private static void AppendDeduplicatedInstances(
        System.Text.StringBuilder sb,
        Dictionary<string, string> templates,
        (string tpl, string name, string addr, bool req)[] candidates,
        Func<string, string?> get)
    {
        // key: "name|||addr" (case-insensitive) → instance name already written
        var emitted = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        var suffixCounter = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        foreach (var (tplName, nameVal, addrVal, req) in candidates)
        {
            if (!req) continue;

            var identityKey = $"{nameVal.Trim()}|||{addrVal.Trim()}";

            if (emitted.ContainsKey(identityKey))
                continue;  // identical identity already written — skip duplicate

            // Choose instance name, adding suffix if the template name is already used
            string instanceName = tplName;
            if (suffixCounter.TryGetValue(tplName, out int cnt))
            {
                suffixCounter[tplName] = cnt + 1;
                instanceName = $"{tplName}-{cnt + 1}";
            }
            else
            {
                suffixCounter[tplName] = 1;
            }

            emitted[identityKey] = instanceName;

            if (!templates.TryGetValue(tplName, out var block)) continue;

            // If the instance name differs from the template name, rewrite the Instance: line
            if (!string.Equals(instanceName, tplName, StringComparison.Ordinal))
                block = System.Text.RegularExpressions.Regex.Replace(
                    block, @"^Instance:\s*" + System.Text.RegularExpressions.Regex.Escape(tplName),
                    $"Instance: {instanceName}",
                    System.Text.RegularExpressions.RegexOptions.Multiline);

            sb.AppendLine();
            sb.AppendLine(TokenParser.Substitute(TokenParser.DropEmptyTokenLines(block, get), get));
        }
    }

    // Split a template file into named instance blocks keyed by instance name
    private static Dictionary<string, string> SplitInstances(string tpl)
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        // Each block starts with "Instance: <name>" (possibly after comment lines)
        var lines  = tpl.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n');
        var current = new System.Text.StringBuilder();
        string? currentName = null;

        foreach (var line in lines)
        {
            var trimmed = line.TrimStart();
            if (trimmed.StartsWith("Instance:", StringComparison.OrdinalIgnoreCase))
            {
                // Save previous block
                if (currentName != null)
                    result[currentName] = current.ToString().TrimEnd();
                currentName = trimmed.Substring("Instance:".Length).Trim();
                current.Clear();
                current.AppendLine(line);
            }
            else
            {
                current.AppendLine(line);
            }
        }
        if (currentName != null)
            result[currentName] = current.ToString().TrimEnd();

        return result;
    }

    private static void AppendInstance(System.Text.StringBuilder sb,
        Dictionary<string, string> instances, string name,
        Func<string, string?> get, bool always)
    {
        if (!always) return;
        if (!instances.TryGetValue(name, out var block)) return;
        sb.AppendLine();
        sb.AppendLine(TokenParser.Substitute(TokenParser.DropEmptyTokenLines(block, get), get));
    }

    // Find the REPEAT block containing the sentinel and either expand it (one row per rowId)
    // or strip it entirely, depending on the expand flag.
    private static string ExpandOrSuppress(string tpl, string sentinel, bool expand,
        IEnumerable<string> rowIds)
    {
        int idx = tpl.IndexOf("{{REPEAT", StringComparison.Ordinal);
        while (idx >= 0)
        {
            int braceClose = TokenParser.FindTokenEnd(tpl, idx);
            if (braceClose < 0) break;
            int innerStart = idx + "{{REPEAT".Length;
            string inner   = tpl.Substring(innerStart, braceClose - innerStart - 2);

            if (inner.Contains(sentinel, StringComparison.Ordinal))
            {
                int lineStart = idx;
                while (lineStart > 0 && tpl[lineStart - 1] != '\n' && tpl[lineStart - 1] != '\r')
                    lineStart--;
                int blockEnd = braceClose;
                while (blockEnd < tpl.Length && (tpl[blockEnd] == ' ' || tpl[blockEnd] == '\t'))
                    blockEnd++;
                if (blockEnd < tpl.Length && tpl[blockEnd] == '\r') blockEnd++;
                if (blockEnd < tpl.Length && tpl[blockEnd] == '\n') blockEnd++;

                if (expand)
                {
                    var ids = rowIds.ToList();
                    var sb  = new System.Text.StringBuilder();
                    for (int ri = 0; ri < ids.Count; ri++)
                    {
                        if (ri > 0) sb.AppendLine();
                        var rowId = ids[ri];
                        // Substitute: TABLE_ROW_X → rowId, sentinel token → rowId,
                        // any other key containing sentinel in its name → rowId
                        sb.Append(TokenParser.Substitute(inner, key =>
                            key == "TABLE_ROW_X"                                        ? rowId :
                            key.Equals(sentinel, StringComparison.OrdinalIgnoreCase)    ? rowId :
                            key));
                    }
                    return tpl.Substring(0, lineStart) + sb + tpl.Substring(blockEnd);
                }
                else
                {
                    return tpl.Substring(0, lineStart) + tpl.Substring(blockEnd);
                }
            }
            idx = tpl.IndexOf("{{REPEAT", idx + 1, StringComparison.Ordinal);
        }
        return tpl;
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
