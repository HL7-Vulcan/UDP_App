using UDP_App.Models;
using System.Text;
using System.Text.Json;
using System.Linq;

namespace UDP_App.Services;

public class DataService
{
    private static readonly JsonSerializerOptions JsonOpts = new() { WriteIndented = true };
    private readonly IWebHostEnvironment _env;

    public DataService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public string ToJson(Section02Model model) =>
        JsonSerializer.Serialize(model, JsonOpts);

    public string ToCsv(Section02Model model)
    {
        var sb = new StringBuilder();
        sb.AppendLine("field_name,value");
        sb.AppendLine($"C146997,{CsvEscape(model.C146997)}");
        sb.AppendLine($"C218721,{CsvEscape(model.C218721)}");
        sb.AppendLine($"C218722,{CsvEscape(model.C218722)}");
        sb.AppendLine($"C218723,{CsvEscape(model.C218723)}");
        sb.AppendLine($"C218724,{CsvEscape(model.C218724)}");
        sb.AppendLine($"C218725,{CsvEscape(model.C218725)}");
        return sb.ToString();
    }

    public string ToPlainText(Section02Model model)
    {
        var sb = new StringBuilder();
        sb.AppendLine("2.1 Purpose of Trial");
        sb.AppendLine(model.C146997 ?? string.Empty);
        sb.AppendLine();
        sb.AppendLine("2.2.1 Risk Summary – Trial Intervention");
        sb.AppendLine(model.C218721 ?? string.Empty);
        sb.AppendLine();
        sb.AppendLine("2.2.1 Risk Summary – Trial Procedures");
        sb.AppendLine(model.C218722 ?? string.Empty);
        sb.AppendLine();
        sb.AppendLine("2.2.1 Risk Summary – Other");
        sb.AppendLine(model.C218723 ?? string.Empty);
        sb.AppendLine();
        sb.AppendLine("2.2.2 Benefit Summary");
        sb.AppendLine(model.C218724 ?? string.Empty);
        sb.AppendLine();
        sb.AppendLine("2.2.3 Overall Risk-Benefit Assessment");
        sb.AppendLine(model.C218725 ?? string.Empty);
        return sb.ToString();
    }

    public string ToFsh(Section02Model model)
    {
        // Load the template file from the Templates folder
        var templatePath = Path.Combine(_env.ContentRootPath, "Templates", "Section_02_Template.fsh");

        string template;
        try
        {
            template = File.ReadAllText(templatePath);
        }
        catch
        {
            return $"Error: could not read template file at {templatePath}";
        }

        // Replace {{FieldName}} tokens with field values
        var fieldValues = new Dictionary<string, string?>
        {
            { "C146997", model.C146997 },
            { "C218721", model.C218721 },
            { "C218722", model.C218722 },
            { "C218723", model.C218723 },
            { "C218724", model.C218724 },
            { "C218725", model.C218725 },
        };

        foreach (var (key, value) in fieldValues)
        {
            template = template.Replace("{{" + key + "}}", value ?? string.Empty);
        }

        return template;
    }

    public Section02Model? LoadSample()
    {
        var samplePath = Path.Combine(_env.ContentRootPath, "Data", "section_02_sample.json");
        try
        {
            var json = File.ReadAllText(samplePath);
            return FromJson(json);
        }
        catch
        {
            return null;
        }
    }

    public Section02Model? FromJson(string json)
    {
        try { return JsonSerializer.Deserialize<Section02Model>(json, JsonOpts); }
        catch { return null; }
    }

    // ─── Section 03 ──────────────────────────────────────────────────────────────

    public string ToJson(Section03Model model) =>
        JsonSerializer.Serialize(model, JsonOpts);

    public Section03Model? FromJsonSection03(string json)
    {
        try { return JsonSerializer.Deserialize<Section03Model>(json, JsonOpts); }
        catch { return null; }
    }

    public string ToCsv(Section03Model model)
    {
        var sb = new StringBuilder();
        sb.AppendLine("field_id,value");
        AppendObjTypeCsv(sb, model.PrimaryObjectives,     "primary",     "01", "C85826");
        AppendObjTypeCsv(sb, model.SecondaryObjectives,   "secondary",   "02", "C85827");
        AppendObjTypeCsv(sb, model.ExploratoryObjectives, "exploratory", "03", "C163559");
        return sb.ToString();
    }

    private static void AppendObjTypeCsv(StringBuilder sb, List<ObjectiveBlock> list,
                                          string typeName, string prefix, string objCode)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var b   = list[i];
            int idx = i + 1;
            sb.AppendLine($"field_3_{prefix}_{idx:D2}_01_{objCode},{CsvEscape(b.ObjectiveText)}");
            sb.AppendLine($"field_3_{prefix}_{idx:D2}_02_C70833,{CsvEscape(b.C70833)}");
            sb.AppendLine($"field_3_{prefix}_{idx:D2}_03_C49236,{CsvEscape(b.C49236)}");
            sb.AppendLine($"field_3_{prefix}_{idx:D2}_04_C25212,{CsvEscape(b.C25212)}");
            sb.AppendLine($"field_3_{prefix}_{idx:D2}_05_C188853,{CsvEscape(b.C188853)}");
            for (int r = 0; r < b.IceRows.Count; r++)
            {
                int rowNum = r + 1;
                sb.AppendLine($"field_3_{prefix}_ice_{idx}_{rowNum}_C188856,{CsvEscape(b.IceRows[r].C188856)}");
                sb.AppendLine($"field_3_{prefix}_ice_{idx}_{rowNum}_C188857,{CsvEscape(b.IceRows[r].C188857)}");
            }
        }
    }

    public string ToPlainText(Section03Model model)
    {
        var sb = new StringBuilder();
        AppendObjTypePlain(sb, "3.1 Primary Objective(s)",     model.PrimaryObjectives);
        AppendObjTypePlain(sb, "3.2 Secondary Objective(s)",   model.SecondaryObjectives);
        AppendObjTypePlain(sb, "3.3 Exploratory Objective(s)", model.ExploratoryObjectives);
        return sb.ToString();
    }

    private static void AppendObjTypePlain(StringBuilder sb, string heading, List<ObjectiveBlock> list)
    {
        sb.AppendLine(heading);
        sb.AppendLine(new string('-', heading.Length));
        for (int i = 0; i < list.Count; i++)
        {
            var b = list[i];
            sb.AppendLine($"Objective #{i + 1}");
            sb.AppendLine(b.ObjectiveText ?? string.Empty);
            sb.AppendLine();
            sb.AppendLine("  Estimands");
            sb.AppendLine($"  Population:               {b.C70833}");
            sb.AppendLine($"  Treatment:                {b.C49236}");
            sb.AppendLine($"  Endpoint:                 {b.C25212}");
            sb.AppendLine($"  Population-level summary: {b.C188853}");
            sb.AppendLine();
            if (b.IceRows.Any(r => !string.IsNullOrWhiteSpace(r.C188856) || !string.IsNullOrWhiteSpace(r.C188857)))
            {
                sb.AppendLine("  Intercurrent Events");
                for (int r = 0; r < b.IceRows.Count; r++)
                    sb.AppendLine($"  [{r + 1}] Event: {b.IceRows[r].C188856}  |  Strategy: {b.IceRows[r].C188857}");
                sb.AppendLine();
            }
        }
        sb.AppendLine();
    }

    /// <summary>
    /// Generates FSH output for Section 03.
    /// Reads Section_03_Template.fsh and Section_03_Primary_Template.fsh from Templates/.
    /// </summary>
    public string ToFsh(Section03Model model)
    {
        var templatesDir = Path.Combine(_env.ContentRootPath, "Templates");
        string mainTemplate, instanceTemplate;
        try
        {
            mainTemplate     = File.ReadAllText(Path.Combine(templatesDir, "Section_03_Template.fsh"));
            instanceTemplate = File.ReadAllText(Path.Combine(templatesDir, "Section_03_Primary_Template.fsh"));
        }
        catch (Exception ex)
        {
            return $"Error: could not read Section 03 template files. {ex.Message}";
        }

        // ── Expand the three outer REPEAT blocks in the main template ─────────
        // Each outer REPEAT has no _X token; it fires once per objective instance
        // with all field tokens substituted inline.  Any inner {{REPEAT...}} block
        // (e.g. ICE entry references) is expanded first using that objective's iceIds.
        //
        // The three REPEATs appear in document order: primary, secondary, exploratory.
        var typeList = new[]
        {
            ("primary",     model.PrimaryObjectives,     "C85826"),
            ("secondary",   model.SecondaryObjectives,   "C85827"),
            ("exploratory", model.ExploratoryObjectives, "C163559"),
        };

        foreach (var (typeName, objectives, objTextField) in typeList)
        {
            var iterations = objectives.Select((obj, i) =>
            {
                int idx         = i + 1;
                var prefix      = typeName;
                var estimandsId = $"Estimands-{prefix}-{idx:D2}";
                var iceIds      = obj.IceRows
                    .Select((row, r) => new { row, r })
                    .Where(x => !string.IsNullOrWhiteSpace(x.row.C188856) ||
                                !string.IsNullOrWhiteSpace(x.row.C188857))
                    .Select(x => $"ICE-{prefix}-{idx:D2}-{x.r + 1:D2}")
                    .ToList();

                var fields = new Dictionary<string, string>
                {
                    { "ESTIMANDS_TABLE_X", estimandsId },
                    { objTextField,        EscapeXml(obj.ObjectiveText) },
                    { "C70833",            EscapeXml(obj.C70833)  },
                    { "C49236",            EscapeXml(obj.C49236)  },
                    { "C25212",            EscapeXml(obj.C25212)  },
                    { "C188853",           EscapeXml(obj.C188853) },
                };

                Func<string, string?> resolver = key =>
                    fields.TryGetValue(key, out var v) ? v : null;

                return (resolver, (IEnumerable<string>)iceIds);
            });

            var result = TokenParser.ExpandOuterRepeat(mainTemplate, iterations);
            if (result.HasValue)
            {
                var (expanded, mStart, mLen) = result.Value;
                mainTemplate = mainTemplate.Remove(mStart, mLen).Insert(mStart, expanded);
            }
        }

        // ── Split instance template into individual Instance sections ────────────
        // Section_03_Primary_Template.fsh has active sections for ESTIMANDS_TABLE_X
        // and INTERCURRENTEVENTS_ROW_X, separated by "//---" dividers.
        // We extract each section by its leading Instance token so we can substitute
        // into just that section rather than the whole file.
        var instanceSections = SplitOnDividers(instanceTemplate);
        var estimandsSection = instanceSections.FirstOrDefault(s =>
            s.Contains("Instance: {{ESTIMANDS_TABLE_X}}"));
        var iceSection = instanceSections.FirstOrDefault(s =>
            s.Contains("Instance: {{INTERCURRENTEVENTS_ROW_X}}"));

        // ── Append ESTIMANDS and ICE Instance blocks ──────────────────────────
        var sb = new StringBuilder();
        sb.AppendLine(mainTemplate);

        foreach (var (typeName, objectives, _) in typeList)
        {
            if (objectives.Count == 0) continue;
            sb.AppendLine();
            sb.AppendLine($"// ── {char.ToUpper(typeName[0])}{typeName[1..]} Objective — Estimands & ICE Instances ──────────────────────");

            for (int i = 0; i < objectives.Count; i++)
            {
                var obj         = objectives[i];
                int idx         = i + 1;
                var prefix      = typeName;
                var estimandsId = $"Estimands-{prefix}-{idx:D2}";

                // Estimands instance
                if (estimandsSection != null)
                {
                    var estimandsFields = new Dictionary<string, string>
                    {
                        { "ESTIMANDS_TABLE_X", estimandsId },
                        { "PRIMARY_TABLE_X",   estimandsId },
                        { "C70833",            EscapeXml(obj.C70833)  },
                        { "C49236",            EscapeXml(obj.C49236)  },
                        { "C25212",            EscapeXml(obj.C25212)  },
                        { "C188853",           EscapeXml(obj.C188853) },
                    };
                    var estimandsBlock = estimandsSection;
                    // Expand any REPEAT inside the estimands section (e.g. treatment repeats)
                    bool anyRepeat = true;
                    while (anyRepeat)
                    {
                        var rpt = TokenParser.FindAndExpandRepeat(estimandsBlock,
                            Enumerable.Repeat(estimandsId, 1));
                        if (rpt.HasValue)
                        {
                            var (exp, ms, ml) = rpt.Value;
                            estimandsBlock = estimandsBlock.Remove(ms, ml).Insert(ms, exp);
                        }
                        else anyRepeat = false;
                    }
                    estimandsBlock = TokenParser.Substitute(estimandsBlock, key =>
                        estimandsFields.TryGetValue(key, out var v) ? v : null);
                    sb.AppendLine(estimandsBlock);
                }

                // ICE instances
                if (iceSection != null)
                {
                    int iceNum = 0;
                    for (int r = 0; r < obj.IceRows.Count; r++)
                    {
                        var row = obj.IceRows[r];
                        if (string.IsNullOrWhiteSpace(row.C188856) && string.IsNullOrWhiteSpace(row.C188857))
                            continue;
                        iceNum++;
                        var iceId     = $"ICE-{prefix}-{idx:D2}-{iceNum:D2}";
                        var iceFields = new Dictionary<string, string>
                        {
                            { "INTERCURRENTEVENTS_ROW_X", iceId },
                            { "C188856", EscapeXml(row.C188856) },
                            { "C188857", EscapeXml(row.C188857) },
                        };
                        var iceBlock = TokenParser.Substitute(iceSection, key =>
                            iceFields.TryGetValue(key, out var v) ? v : null);
                        sb.AppendLine(iceBlock);
                    }
                }
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Splits a template on "//---" divider lines, returning each non-empty section.
    /// Commented-out sections (where every non-blank, non-divider line starts with "//")
    /// are excluded so only active Instance blocks are returned.
    /// </summary>
    private static List<string> SplitOnDividers(string template)
    {
        var result = new List<string>();
        var current = new StringBuilder();
        foreach (var rawLine in template.Split('\n'))
        {
            var line = rawLine.TrimEnd('\r');
            if (line.StartsWith("//---", StringComparison.Ordinal) && current.Length > 0)
            {
                AddIfActive(result, current.ToString());
                current.Clear();
            }
            current.AppendLine(line);
        }
        if (current.Length > 0)
            AddIfActive(result, current.ToString());
        return result;

        static void AddIfActive(List<string> list, string section)
        {
            // A section is "active" if it has at least one non-blank line that does
            // not start with "//" (i.e. not fully commented out).
            foreach (var l in section.Split('\n'))
            {
                var t = l.TrimEnd('\r').Trim();
                if (t.Length > 0 && !t.StartsWith("//", StringComparison.Ordinal))
                {
                    list.Add(section);
                    return;
                }
            }
        }
    }

    private static string EscapeXml(string? value) =>
        (value ?? string.Empty)
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;");

    // ─── Shared helpers ───────────────────────────────────────────────────────

    private static string CsvEscape(string? value)
    {
        if (string.IsNullOrEmpty(value)) return "\"\"";
        return "\"" + value.Replace("\"", "\"\"") + "\"";
    }
}
