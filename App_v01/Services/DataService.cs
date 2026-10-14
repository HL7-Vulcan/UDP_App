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
    ///
    /// Structure (updated):
    ///   - Main template: one outer {{REPEAT}} per objective type (primary/secondary/exploratory).
    ///     Each iteration emits a section[+] block referencing the ESTIMANDS_TABLE_X instance.
    ///     ICE entry references in the main template are commented out — ICE rows now live
    ///     inside the ESTIMANDS instance as extension[intercurrentEvents] repeats.
    ///   - Instance template: one Instance block (ESTIMANDS_TABLE_X) containing:
    ///       * A {{REPEAT}} for treatment rows (no _X token → expands once per treatment string)
    ///       * A {{REPEAT}} for ICE rows (no _X token → we expand manually per ICE row)
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

        var typeList = new[]
        {
            ("primary",     model.PrimaryObjectives,     "C85826"),
            ("secondary",   model.SecondaryObjectives,   "C85827"),
            ("exploratory", model.ExploratoryObjectives, "C163559"),
        };

        // ── Expand the three outer REPEAT blocks in the main template ─────────
        // ICE is no longer referenced in the main template (commented out in 3.1,
        // still present in 3.2/3.3 but INTERCURRENTEVENTS_ROW_X resolves to null → dropped).
        foreach (var (typeName, objectives, objTextField) in typeList)
        {
            var iterations = objectives.Select((obj, i) =>
            {
                int idx         = i + 1;
                var estimandsId = $"Estimands-{typeName}-{idx:D2}";

                bool estimandsHasData = !string.IsNullOrWhiteSpace(obj.C70833)
                                     || !string.IsNullOrWhiteSpace(obj.C49236)
                                     || !string.IsNullOrWhiteSpace(obj.C25212)
                                     || !string.IsNullOrWhiteSpace(obj.C188853)
                                     || obj.IceRows.Any(r =>
                                            !string.IsNullOrWhiteSpace(r.C188856) ||
                                            !string.IsNullOrWhiteSpace(r.C188857));

                var fields = new Dictionary<string, string?>
                {
                    { "ESTIMANDS_TABLE_X", estimandsHasData ? estimandsId : null },
                    { objTextField,        EscapeXml(obj.ObjectiveText) },
                };

                Func<string, string?> resolver = key =>
                    fields.TryGetValue(key, out var v) ? v : null;

                // No ICE ids needed in the main template any more
                return (resolver, Enumerable.Empty<string>());
            });

            var result = TokenParser.ExpandOuterRepeat(mainTemplate, iterations);
            if (result.HasValue)
            {
                var (expanded, mStart, mLen) = result.Value;
                mainTemplate = mainTemplate.Remove(mStart, mLen).Insert(mStart, expanded);
            }
        }

        // ── Extract the single active Instance block from the instance template ─
        // The template now has one Instance block (ESTIMANDS_TABLE_X) plus commented-out
        // legacy sections. SplitOnDividers returns only active (non-fully-commented) sections.
        var instanceSections = SplitOnDividers(instanceTemplate);
        var estimandsSection = instanceSections.FirstOrDefault(s =>
            s.Contains("Instance: {{ESTIMANDS_TABLE_X}}"));

        // ── Append one ESTIMANDS Instance block per objective ─────────────────
        var sb = new StringBuilder();
        sb.AppendLine(mainTemplate);

        foreach (var (typeName, objectives, _) in typeList)
        {
            if (objectives.Count == 0) continue;
            sb.AppendLine();
            sb.AppendLine($"// ── {char.ToUpper(typeName[0])}{typeName[1..]} Objective — Estimands Instances ──────────────────────────");

            for (int i = 0; i < objectives.Count; i++)
            {
                var obj         = objectives[i];
                int idx         = i + 1;
                var estimandsId = $"Estimands-{typeName}-{idx:D2}";

                if (estimandsSection == null) continue;

                var estimandsBlock = estimandsSection;

                // ── Expand {{REPEAT}} for ICE rows first ──────────────────────
                // Must be done before the generic FindAndExpandRepeat loop because
                // the ICE REPEAT has no _X token and would otherwise be emitted only
                // once (as a "emit-once" block) with unresolved tokens.
                estimandsBlock = ExpandIceRepeat(estimandsBlock, obj.IceRows);

                // ── Expand remaining {{REPEAT}} blocks (e.g. treatment/C49236) ─
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

                // ── Substitute remaining tokens and drop empty lines ──────────
                var estimandsFields = new Dictionary<string, string?>
                {
                    { "ESTIMANDS_TABLE_X", estimandsId },
                    { "C70833",            EscapeXml(obj.C70833)  },
                    { "C49236",            EscapeXml(obj.C49236)  },
                    { "C25212",            EscapeXml(obj.C25212)  },
                    { "C188853",           EscapeXml(obj.C188853) },
                };
                Func<string, string?> estimandsResolver = key =>
                    estimandsFields.TryGetValue(key, out var v) ? v : null;

                estimandsBlock = TokenParser.DropEmptyTokenLines(estimandsBlock, estimandsResolver);
                estimandsBlock = TokenParser.Substitute(estimandsBlock, estimandsResolver);

                if (HasAnyDataLine(estimandsBlock))
                    sb.AppendLine(estimandsBlock);
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// Finds the ICE {{REPEAT...}} block inside the estimands template and replaces it
    /// with one copy of the row pattern per non-empty ICE row, substituting C188856/C188857.
    /// The REPEAT block has no _X tokens so FindAndExpandRepeat treats it as "emit once" —
    /// we handle it here instead.
    /// </summary>
    private static string ExpandIceRepeat(string block, List<IceRow> iceRows)
    {
        // Find the REPEAT block that contains C188856 (ICE event field)
        int i = 0;
        while (i < block.Length)
        {
            int open = TokenParser.FindTokenStart(block, i);
            if (open < 0) break;
            int close = TokenParser.FindTokenEnd(block, open);
            if (close < 0) break;

            string content = block.Substring(open + 2, close - open - 4);
            if (content.StartsWith("REPEAT", StringComparison.Ordinal) &&
                content.Contains("C188856"))
            {
                // Determine match extent (include leading whitespace and trailing newline)
                int lineStart = open;
                while (lineStart > 0 && block[lineStart - 1] != '\n' && block[lineStart - 1] != '\r')
                    lineStart--;
                int blockEnd = close;
                while (blockEnd < block.Length && (block[blockEnd] == ' ' || block[blockEnd] == '\t'))
                    blockEnd++;
                if (blockEnd < block.Length && block[blockEnd] == '\r') blockEnd++;
                if (blockEnd < block.Length && block[blockEnd] == '\n') blockEnd++;

                string rowPattern = content.Substring("REPEAT".Length);

                // Build expansion: one copy per non-empty ICE row
                var expansion = new StringBuilder();
                bool first = true;
                foreach (var row in iceRows)
                {
                    if (string.IsNullOrWhiteSpace(row.C188856) && string.IsNullOrWhiteSpace(row.C188857))
                        continue;
                    if (!first) expansion.AppendLine();
                    first = false;
                    var iceFields = new Dictionary<string, string?>
                    {
                        { "C188856", EscapeXml(row.C188856) },
                        { "C188857", EscapeXml(row.C188857) },
                    };
                    Func<string, string?> resolver = key =>
                        iceFields.TryGetValue(key, out var v) ? v : null;
                    var substituted = TokenParser.DropEmptyTokenLines(rowPattern, resolver);
                    substituted = TokenParser.Substitute(substituted, resolver);
                    expansion.Append(substituted);
                }

                return block.Remove(lineStart, blockEnd - lineStart)
                            .Insert(lineStart, expansion.ToString());
            }

            i = close;
        }
        return block; // no ICE REPEAT found — return unchanged
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

    /// <summary>
    /// Returns true if the substituted block contains at least one FSH data line
    /// (i.e. a line with .valueString = "...") where the quoted value is non-empty.
    /// Used to decide whether to emit an ESTIMANDS or ICE instance block.
    /// </summary>
    private static bool HasAnyDataLine(string block)
    {
        foreach (var raw in block.Split('\n'))
        {
            var line = raw.TrimEnd('\r').Trim();
            // Match lines of the form: * ...valueString = "..."
            int eq = line.IndexOf(".valueString = \"", StringComparison.Ordinal);
            if (eq < 0) continue;
            int valStart = eq + ".valueString = \"".Length;
            int valEnd   = line.LastIndexOf('"');
            if (valEnd > valStart && !string.IsNullOrWhiteSpace(line.Substring(valStart, valEnd - valStart)))
                return true;
        }
        return false;
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
