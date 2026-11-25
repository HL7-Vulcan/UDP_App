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

    /// <summary>
    /// Returns the model ready for export. When C218836 (alternate location) is set,
    /// change detail data (ChgRows / ChgData) is omitted — OPTION 1 applies in FSH.
    /// </summary>
    private static Section12Model ExportModel(Section12Model model)
    {
        if (string.IsNullOrWhiteSpace(model.C218836))
            return model;

        // Shallow copy, clearing change detail collections
        return new Section12Model
        {
            C25294          = model.C25294,
            CrRows          = model.CrRows,
            CrData          = model.CrData,
            AmendRows       = model.AmendRows,
            AmendData       = model.AmendData,
            OrigProtocolDate = model.OrigProtocolDate,
            C218836         = model.C218836,
            ChgRows         = new Dictionary<string, int>(),
            ChgData         = new Dictionary<string, string>(),
        };
    }

    public string ToJson(Section12Model model) =>
        JsonSerializer.Serialize(ExportModel(model), JsonOpts);

    public Section12Model? FromJson(string json)
    {
        try { return JsonSerializer.Deserialize<Section12Model>(json, JsonOpts); }
        catch { return null; }
    }

    public string ToCsv(Section12Model model)
    {
        var m  = ExportModel(model);
        var sb = new StringBuilder();
        sb.AppendLine("field_name,value");
        sb.AppendLine($"C25294,{CsvEscape(m.C25294 ?? string.Empty)}");
        sb.AppendLine($"C218836,{CsvEscape(m.C218836 ?? string.Empty)}");
        foreach (var (k, v) in m.CrData)
            sb.AppendLine($"{k},{CsvEscape(v)}");
        foreach (var (k, v) in m.AmendData)
            sb.AppendLine($"{k},{CsvEscape(v)}");
        foreach (var (k, v) in m.ChgData)
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
        var mainPath  = Path.Combine(_env.ContentRootPath, "Templates", "Section_12_Template.fsh");
        var tablePath = Path.Combine(_env.ContentRootPath, "Templates", "Section_12_Table_Template.fsh");

        string main, tbl;
        try { main = File.ReadAllText(mainPath);  }
        catch { return "// Section 12 FSH template not found."; }
        try { tbl  = File.ReadAllText(tablePath); }
        catch { return "// Section 12 table template not found."; }

        // ── Fixed field substitutions in main template ─────────────────────
        main = main.Replace("{{C25294}}",  TokenParser.FhirEscape(model.C25294));
        main = main.Replace("{{C218836}}", TokenParser.FhirEscape(model.C218836));

        // C218672 = original protocol indicator — omit the whole line if empty
        var origIndicator = model.AmendData.TryGetValue("C218672", out var ci) ? ci : string.Empty;
        if (string.IsNullOrWhiteSpace(origIndicator))
            main = RemoveLinesContaining(main, "{{C218672}}");
        else
            main = main.Replace("{{C218672}}", TokenParser.FhirEscape(origIndicator));

        var tableInstances = new StringBuilder();

        // Helper: look up a value from AmendData
        string GetAmend(string dataKey, string suffix) =>
            model.AmendData.TryGetValue($"{dataKey}_{suffix}", out var v) ? v : string.Empty;

        // ── Section 12.2: Country/Region-Specific Differences ───────────────
        //    ONE instance (TABLE12.2_ROW_X), REPEAT expands once per country row.
        //    Per-row fields: C218832_r{n}, C218833_r{n}, C218834_r{n}
        var crCount = Math.Max(0, model.CrRows);
        if (crCount > 0)
        {
            const string inst12_2 = "Section12-2";
            var block = ExtractInstanceBlock(tbl, "TABLE12.2_ROW_X");
            block = block.Replace("{{TABLE12.2_ROW_X}}", inst12_2);
            // Static identifier tokens outside REPEAT — drop only the pre-REPEAT lines
            block = RemoveLinesBeforeRepeat(block, "{{C132351}}");
            block = RemoveLinesBeforeRepeat(block, "{{C218477}}");
            // Static instance-level tokens outside REPEAT — substitute or drop
            var cr12_2_origProto = model.AmendData.TryGetValue("C218672", out var cr12_2_op) ? cr12_2_op : string.Empty;
            // versionDate: prefer r0 (original protocol date), fall back to r1 if r0 absent
            var cr12_2_date = !string.IsNullOrWhiteSpace(model.OrigProtocolDate)
                ? model.OrigProtocolDate
                : string.Empty;
            block = SubstituteOrOmitLine(block, "{{C218672}}", TokenParser.FhirEscape(cr12_2_origProto));
            block = SubstituteOrOmitLine(block, "{{C93813}}",  TokenParser.FhirEscape(cr12_2_date));

            var crIter = Enumerable.Range(1, crCount).Select(r =>
            {
                Func<string, string?> fv = key => key switch
                {
                    "C218832" => NullIfEmpty(TokenParser.FhirEscape(model.CrData.TryGetValue($"C218832_r{r}", out var v) ? v : string.Empty)),
                    "C218833" => NullIfEmpty(TokenParser.FhirEscape(model.CrData.TryGetValue($"C218833_r{r}", out var v) ? v : string.Empty)),
                    "C218834" => NullIfEmpty(TokenParser.FhirEscape(model.CrData.TryGetValue($"C218834_r{r}", out var v) ? v : string.Empty)),
                    _ => null,
                };
                return (fv, Enumerable.Empty<string>());
            });

            var expanded = TokenParser.ExpandOuterRepeat(block, crIter);
            if (expanded.HasValue)
            {
                var (exp, ms, ml) = expanded.Value;
                block = block.Remove(ms, ml).Insert(ms, exp);
            }

            main = main.Replace("* entry[+] = Reference({{TABLE12.2_ROW_X}})",
                                 $"* entry[+] = Reference({inst12_2})");
            tableInstances.AppendLine(); tableInstances.Append(block);
        }
        else
        {
            main = RemoveLinesContaining(main, "{{TABLE12.2_ROW_X}}");
        }

        // ── Section 12.3a: Prior amendment summary ─────────────────────────
        //    ONE instance (TABLE12.3a_ROW_X), REPEAT expands once per amendment row.
        //    Row 0 = Original Protocol (identifier fixed); rows 1..n from AmendData.
        //    Template tokens → storage keys:
        //      {{C218477}} → "Original Protocol" (r0) or C218477_r{n}
        //      {{C93813}}  → C132352_r{n}  (approval date)
        //      {{C218478}} → C218695_r{n}  (approx enrolled)
        //      {{C218695}} → C218695e_r{n} (enrollment scope)
        var amendCount = Math.Max(1, model.AmendRows);
        {
            const string inst12_3a = "Section12-3a";
            var block = ExtractInstanceBlock(tbl, "TABLE12.3a_ROW_X");
            block = block.Replace("{{TABLE12.3a_ROW_X}}", inst12_3a);
            // Static identifier tokens outside REPEAT — drop only the pre-REPEAT lines
            block = RemoveLinesBeforeRepeat(block, "{{C132351}}");
            block = RemoveLinesBeforeRepeat(block, "{{C218477}}");

            // Build iterations: rows 1..amendCount only (Original Protocol is not an amendment)
            var amendIter = Enumerable.Range(1, amendCount).Select(r =>
            {
                var id477    = GetAmend("C218477",  $"r{r}");
                var date     = GetAmend("C132352",  $"r{r}");
                var enrol    = GetAmend("C218695",  $"r{r}");
                var scope    = GetAmend("C218695e", $"r{r}");
                var scope673 = GetAmend("C218673",  $"r{r}");

                // Skip row if all data fields are empty
                if (string.IsNullOrWhiteSpace(id477) && string.IsNullOrWhiteSpace(date) &&
                    string.IsNullOrWhiteSpace(enrol) && string.IsNullOrWhiteSpace(scope))
                    return ((Func<string, string?>?)null, Enumerable.Empty<string>());

                var origProto = model.AmendData.TryGetValue("C218672", out var opv) ? opv : string.Empty;
                Func<string, string?> fv = key => key switch
                {
                    "C218477" => NullIfEmpty(TokenParser.FhirEscape(id477)),
                    "C93813"  => NullIfEmpty(TokenParser.FhirEscape(date)),
                    "C218478" => NullIfEmpty(TokenParser.FhirEscape(enrol)),
                    "C218695" => NullIfEmpty(TokenParser.FhirEscape(scope)),
                    "C218672" => NullIfEmpty(TokenParser.FhirEscape(origProto)),
                    "C218673" => NullIfEmpty(TokenParser.FhirEscape(scope673)),
                    _ => null,
                };
                return ((Func<string, string?>?)fv, Enumerable.Empty<string>());
            }).Where(t => t.Item1 is not null)
              .Select(t => (t.Item1!, t.Item2));

            var expanded = TokenParser.ExpandOuterRepeat(block, amendIter);
            if (expanded.HasValue)
            {
                var (exp, ms, ml) = expanded.Value;
                block = block.Remove(ms, ml).Insert(ms, exp);
            }

            main = main.Replace("* entry[+] = Reference({{TABLE12.3a_ROW_X}})",
                                 $"* entry[+] = Reference({inst12_3a})");
            tableInstances.AppendLine(); tableInstances.Append(block);
        }

        // ── Section 12.3b: Change details ──────────────────────────────────
        //    ONE instance (TABLE12.3b_ROW_X).
        //    Outer REPEAT: one iteration per amendment.
        //      Outer fields: {{C218477}} = amendment identifier, {{C132352}} = approval date.
        //    Inner REPEAT (nested inside outer): one iteration per change row.
        //      Inner fields: {{C218483}}, {{C181233}}, {{C218479}}.
        //
        //    ExpandOuterRepeat handles the outer REPEAT.  For each outer iteration it
        //    calls FindAndExpandRepeat(body, iceIds) to expand inner _X-token REPEATs,
        //    but the inner REPEAT here uses field-value tokens, not _X tokens.  So we
        //    expand the inner REPEAT manually per amendment and splice the result into
        //    the outer body before substituting the outer tokens.  We do this by
        //    providing a pre-rendered body string via a custom outer iteration that
        //    returns the already-interpolated body when asked for a special sentinel key.
        //
        //    Simpler implementation: extract the outer REPEAT body from the template,
        //    then for each amendment expand the inner REPEAT within that body, substitute
        //    the outer tokens, and accumulate the result.  Finally splice the whole
        //    accumulated expansion back using the outer REPEAT span coordinates.

        {
            const string inst12_3b = "Section12-3b";
            var block3b = ExtractInstanceBlock(tbl, "TABLE12.3b_ROW_X");
            block3b = block3b.Replace("{{TABLE12.3b_ROW_X}}", inst12_3b);
            block3b = RemoveLinesBeforeRepeat(block3b, "{{C132351}}");
            block3b = RemoveLinesBeforeRepeat(block3b, "{{C218477}}");

            var altLocation = model.C218836 ?? string.Empty;
            var useOption1  = !string.IsNullOrWhiteSpace(altLocation);

            // Resolve {{OPTION 1 ... }} / {{OPTION 2 ... }} blocks
            block3b = ResolveOption(block3b, 1, useOption1);
            block3b = ResolveOption(block3b, 2, !useOption1);

            if (useOption1)
            {
                // OPTION 1: per-amendment block with detailsLocation — no inner {{REPEAT}}.
                // After ResolveOption strips the {{OPTION 1 … }} delimiters, block3b IS the
                // body template. We stamp it out once per amendment row.
                // Only the most recent amendment (highest index) is used for OPTION 1.
                var origProto1 = model.AmendData.TryGetValue("C218672", out var op1) ? op1 : string.Empty;
                var latestId      = GetAmend("C218477", $"r{amendCount}");
                var latestScope   = GetAmend("C218673",  $"r{amendCount}");
                var latestVersion = GetAmend("C132352",  $"r{amendCount}");  // maps to {{C93813}} token

                if (!string.IsNullOrWhiteSpace(latestId))
                {
                    block3b = SubstituteOrOmitLine(block3b, "{{C218477}}", TokenParser.FhirEscape(latestId));
                    block3b = SubstituteOrOmitLine(block3b, "{{C218673}}", TokenParser.FhirEscape(latestScope));
                    block3b = SubstituteOrOmitLine(block3b, "{{C218672}}", TokenParser.FhirEscape(origProto1));
                    block3b = SubstituteOrOmitLine(block3b, "{{C93813}}",  TokenParser.FhirEscape(latestVersion));
                    block3b = block3b.Replace("{{C218836}}", TokenParser.FhirEscape(altLocation));

                    main = main.Replace("* entry[+] = Reference({{TABLE12.3b_ROW_X}})",
                                        $"* entry[+] = Reference({inst12_3b})");
                    tableInstances.AppendLine(); tableInstances.Append(block3b);
                }
                else
                {
                    main = RemoveLinesContaining(main, "{{TABLE12.3b_ROW_X}}");
                }
            }
            else
            {
                // OPTION 2: per-amendment REPEAT with nested change rows
                var outerBody = ExtractRepeatBody(block3b);
                var outerSpan = TokenParser.ExpandOuterRepeat(block3b,
                    new (Func<string, string?>, IEnumerable<string>)[] {
                        (key => string.Empty, Enumerable.Empty<string>())
                    });

                if (outerSpan.HasValue)
                {
                    var (_, outerMs, outerMl) = outerSpan.Value;
                    var amendExpanded = new StringBuilder();
                    var anyAmend = false;

                    for (int a = 1; a <= amendCount; a++)
                    {
                        var chgCount = model.ChgRows.TryGetValue(a.ToString(), out var cr) ? cr : 0;
                        if (chgCount == 0) continue;

                        var aId   = GetAmend("C218477", $"r{a}");
                        var aDate = GetAmend("C132352",  $"r{a}");

                        var innerIter = Enumerable.Range(1, chgCount).Select(m =>
                        {
                            var desc = model.ChgData.TryGetValue($"C218483_a{a}_r{m}", out var dv) ? dv : string.Empty;
                            var rat  = model.ChgData.TryGetValue($"C181233_a{a}_r{m}", out var rv) ? rv : string.Empty;
                            var sec  = model.ChgData.TryGetValue($"C218479_a{a}_r{m}", out var sv) ? sv : string.Empty;

                            if (string.IsNullOrWhiteSpace(desc) && string.IsNullOrWhiteSpace(rat) &&
                                string.IsNullOrWhiteSpace(sec))
                                return ((Func<string, string?>?)null, Enumerable.Empty<string>());

                            Func<string, string?> fv = key => key switch
                            {
                                "C218483" => NullIfEmpty(TokenParser.FhirEscape(desc)),
                                "C181233" => NullIfEmpty(TokenParser.FhirEscape(rat)),
                                "C218479" => NullIfEmpty(TokenParser.FhirEscape(sec)),
                                _ => null,
                            };
                            return ((Func<string, string?>?)fv, Enumerable.Empty<string>());
                        }).Where(t => t.Item1 is not null).Select(t => (t.Item1!, t.Item2)).ToList();

                        if (innerIter.Count == 0) continue;

                        var iterBody = outerBody;
                        var innerResult = TokenParser.ExpandOuterRepeat(iterBody, innerIter);
                        if (innerResult.HasValue)
                        {
                            var (innerExp, ims, iml) = innerResult.Value;
                            iterBody = iterBody.Remove(ims, iml).Insert(ims, innerExp);
                        }

                        var origProto3b  = model.AmendData.TryGetValue("C218672", out var op3b) ? op3b : string.Empty;
                        var aVersionDate = GetAmend("C132352", $"r{a}");
                        var aScope673    = GetAmend("C218673",  $"r{a}");
                        iterBody = SubstituteOrOmitLine(iterBody, "{{C218477}}", TokenParser.FhirEscape(aId));
                        iterBody = SubstituteOrOmitLine(iterBody, "{{C218673}}", TokenParser.FhirEscape(aScope673));
                        iterBody = SubstituteOrOmitLine(iterBody, "{{C132352}}", TokenParser.FhirEscape(aDate));
                        iterBody = SubstituteOrOmitLine(iterBody, "{{C218672}}", TokenParser.FhirEscape(origProto3b));
                        iterBody = SubstituteOrOmitLine(iterBody, "{{C93813}}",  TokenParser.FhirEscape(aVersionDate));

                        if (anyAmend) amendExpanded.AppendLine();
                        anyAmend = true;
                        amendExpanded.Append(iterBody.TrimEnd());
                    }

                    if (anyAmend)
                    {
                        block3b = block3b.Remove(outerMs, outerMl).Insert(outerMs, amendExpanded.ToString());
                        main = main.Replace("* entry[+] = Reference({{TABLE12.3b_ROW_X}})",
                                            $"* entry[+] = Reference({inst12_3b})");
                        tableInstances.AppendLine(); tableInstances.Append(block3b);
                    }
                    else
                    {
                        main = RemoveLinesContaining(main, "{{TABLE12.3b_ROW_X}}");
                    }
                }
                else
                {
                    main = RemoveLinesContaining(main, "{{TABLE12.3b_ROW_X}}");
                }
            }
        }

        return main + tableInstances.ToString();
    }

    // Returns null if the value is empty/whitespace — signals DropEmptyTokenLines to remove the line.
    private static string? NullIfEmpty(string value) =>
        string.IsNullOrWhiteSpace(value) ? null : value;

    /// <summary>
    /// Extracts the body text of the {{OPTION 1 ...}} block (between the opening line and closing }}).
    /// </summary>
    private static string ExtractOption1Body(string text)
    {
        const string open = "{{OPTION 1";
        int openPos = text.IndexOf(open, StringComparison.Ordinal);
        if (openPos < 0) return string.Empty;

        int bodyStart = text.IndexOf('\n', openPos);
        if (bodyStart < 0) return string.Empty;
        bodyStart++;

        // Find matching }} using depth counting
        int depth = 1, i = openPos + 2;
        while (i < text.Length - 1 && depth > 0)
        {
            if (text[i] == '{' && text[i + 1] == '{') { depth++; i += 2; }
            else if (text[i] == '}' && text[i + 1] == '}') { depth--; if (depth > 0) i += 2; }
            else i++;
        }
        if (depth != 0) return string.Empty;

        int closePos = i;
        int bodyEnd = closePos;
        while (bodyEnd > bodyStart && text[bodyEnd - 1] == '\n') bodyEnd--;
        if (bodyEnd > bodyStart && text[bodyEnd - 1] == '\r') bodyEnd--;

        return text.Substring(bodyStart, bodyEnd - bodyStart);
    }

    /// <summary>
    /// Returns the (start, length) span of the OPTION 1 body within the text
    /// (the region that ExtractOption1Body returns, before closing }}), for splicing.
    /// </summary>
    private static (int start, int length)? FindOption1Span(string text)
    {
        const string open = "{{OPTION 1";
        int openPos = text.IndexOf(open, StringComparison.Ordinal);
        if (openPos < 0) return null;

        int bodyStart = text.IndexOf('\n', openPos);
        if (bodyStart < 0) return null;
        bodyStart++;

        int depth = 1, i = openPos + 2;
        while (i < text.Length - 1 && depth > 0)
        {
            if (text[i] == '{' && text[i + 1] == '{') { depth++; i += 2; }
            else if (text[i] == '}' && text[i + 1] == '}') { depth--; if (depth > 0) i += 2; }
            else i++;
        }
        if (depth != 0) return null;

        int closePos = i;
        int bodyEnd = closePos;
        while (bodyEnd > bodyStart && text[bodyEnd - 1] == '\n') bodyEnd--;
        if (bodyEnd > bodyStart && text[bodyEnd - 1] == '\r') bodyEnd--;

        return (bodyStart, bodyEnd - bodyStart);
    }

    /// <summary>
    /// Extracts the content between the first {{REPEAT and its matching closing }},
    /// stripping the REPEAT keyword and the outer {{ }} delimiters.
    /// Returns the body text (the per-iteration template), or empty string if not found.
    /// </summary>
    private static string ExtractRepeatBody(string template)
    {
        // Find {{REPEAT
        int open = template.IndexOf("{{REPEAT", StringComparison.Ordinal);
        if (open < 0) return string.Empty;

        // The body starts after the newline following the opening {{REPEAT...}} line
        int bodyStart = template.IndexOf('\n', open);
        if (bodyStart < 0) return string.Empty;
        bodyStart++; // skip the newline itself

        // Find the matching closing }} — the one that closes this REPEAT.
        // We need to account for nested {{ }} pairs.
        int depth = 1;
        int i = open + 2; // start scanning after the opening {{
        while (i < template.Length - 1 && depth > 0)
        {
            if (template[i] == '{' && template[i + 1] == '{') { depth++; i += 2; }
            else if (template[i] == '}' && template[i + 1] == '}') { depth--; if (depth > 0) i += 2; }
            else i++;
        }
        // i is now at the closing }} of the outermost REPEAT
        if (depth != 0) return string.Empty;

        int bodyEnd = i; // closing }} starts here
        // Walk back to trim the newline before }}
        while (bodyEnd > bodyStart && template[bodyEnd - 1] == '\n') bodyEnd--;
        if (bodyEnd > bodyStart && template[bodyEnd - 1] == '\r') bodyEnd--;

        return template.Substring(bodyStart, bodyEnd - bodyStart);
    }

    /// <summary>
    /// Resolves an {{OPTION N ... }} block: if <paramref name="keep"/> is true the delimiters
    /// are stripped and the body is kept; otherwise the entire block including delimiters is removed.
    /// </summary>
    private static string ResolveOption(string text, int optionNumber, bool keep)
    {
        var open = $"{{{{OPTION {optionNumber}";

        int openPos = text.IndexOf(open, StringComparison.Ordinal);
        if (openPos < 0) return text;

        // Find the end of the opening line
        int bodyStart = text.IndexOf('\n', openPos);
        if (bodyStart < 0) return text;
        bodyStart++;

        // Find the matching closing }} using depth counting to handle nested {{ }} blocks
        int depth = 1;
        int i = openPos + 2; // start scanning after the opening {{
        while (i < text.Length - 1 && depth > 0)
        {
            if (text[i] == '{' && text[i + 1] == '{') { depth++; i += 2; }
            else if (text[i] == '}' && text[i + 1] == '}') { depth--; if (depth > 0) i += 2; }
            else i++;
        }
        if (depth != 0) return text; // unmatched — leave unchanged

        int closePos = i; // position of the closing }}
        int afterClose = closePos + 2;
        if (afterClose < text.Length && text[afterClose] == '\n') afterClose++;

        if (keep)
        {
            // Keep body, remove opening line and closing }}
            var body = text.Substring(bodyStart, closePos - bodyStart);
            return text.Substring(0, openPos) + body + text.Substring(afterClose);
        }
        else
        {
            // Remove entire block
            return text.Substring(0, openPos) + text.Substring(afterClose);
        }
    }

    /// <summary>
    /// Removes all lines from <paramref name="text"/> that contain <paramref name="marker"/>.
    /// </summary>
    private static string RemoveLinesContaining(string text, string marker)
    {
        var lines = text.Split('\n');
        var kept  = lines.Where(l => !l.Contains(marker, StringComparison.Ordinal));
        return string.Join('\n', kept);
    }

    /// <summary>
    /// Removes lines containing <paramref name="marker"/> only from the portion of
    /// <paramref name="text"/> that precedes the first {{REPEAT block.
    /// Lines inside the REPEAT body are left intact for the expansion phase.
    /// </summary>
    private static string RemoveLinesBeforeRepeat(string text, string marker)
    {
        int repeatPos = text.IndexOf("{{REPEAT", StringComparison.Ordinal);
        if (repeatPos < 0) return RemoveLinesContaining(text, marker);

        string before = text.Substring(0, repeatPos);
        string after  = text.Substring(repeatPos);
        return RemoveLinesContaining(before, marker) + after;
    }

    /// <summary>
    /// If <paramref name="value"/> is non-empty, replaces <paramref name="token"/> with it.
    /// If empty, removes every line that contains <paramref name="token"/>.
    /// </summary>
    private static string SubstituteOrOmitLine(string text, string token, string value) =>
        string.IsNullOrEmpty(value)
            ? RemoveLinesContaining(text, token)
            : text.Replace(token, value);

    // ── Helpers ───────────────────────────────────────────────────────────

    /// <summary>
    /// Extracts the FSH Instance block whose id placeholder matches <paramref name="idToken"/>
    /// from a multi-block table template.  Returns the block text (from "Instance:" to the
    /// blank line / next "Instance:" boundary) with the leading comment lines included.
    /// </summary>
    private static string ExtractInstanceBlock(string template, string idToken)
    {
        // Find "Instance: {{<idToken>}}" line
        var anchor = $"Instance: {{{{{idToken}}}}}";
        int pos = template.IndexOf(anchor, StringComparison.Ordinal);
        if (pos < 0) return $"// Block for {idToken} not found in table template\n";

        // Walk back to include any comment lines (//---) immediately preceding
        int blockStart = pos;
        while (blockStart > 0)
        {
            int lineBegin = blockStart - 1;
            while (lineBegin > 0 && template[lineBegin - 1] != '\n') lineBegin--;
            var prevLine = template.Substring(lineBegin, blockStart - lineBegin).Trim();
            if (prevLine.StartsWith("//", StringComparison.Ordinal) || prevLine.Length == 0)
                blockStart = lineBegin;
            else
                break;
        }

        // Find the end: next "Instance:" line or end of string
        int nextInstance = template.IndexOf("\nInstance:", pos + anchor.Length, StringComparison.Ordinal);
        int blockEnd = nextInstance >= 0 ? nextInstance + 1 : template.Length;

        return template.Substring(blockStart, blockEnd - blockStart).TrimEnd();
    }

    /// <summary>
    /// Replaces the first occurrence of <paramref name="oldValue"/> in
    /// <paramref name="source"/> with <paramref name="newValue"/>.
    /// </summary>
    private static string ReplaceFirstOccurrence(string source, string oldValue, string newValue)
    {
        int idx = source.IndexOf(oldValue, StringComparison.Ordinal);
        if (idx < 0) return source;
        return source.Remove(idx, oldValue.Length).Insert(idx, newValue);
    }

    public (Section12Model? Model, string? Error) LoadSample()
    {
        var path = Path.Combine(_env.ContentRootPath, "Data", "section_12_sample.json");
        if (!File.Exists(path))
            return (null, "Sample data file not found: Data/section_12_sample.json");

        Section12Model? model;
        try
        {
            model = JsonSerializer.Deserialize<Section12Model>(File.ReadAllText(path), JsonOpts);
        }
        catch (Exception ex)
        {
            return (null, $"Failed to load sample data: {ex.Message}");
        }
        if (model is null) return (null, "section_12_sample.json deserialized to null");

        // ── Merge amendment data from amendment_sample.json ───────────────────
        // Amendment rows and change details live in the amendment store, not in
        // section_12_sample.json.  Apply the same mapping that the razor
        // component's ApplyImport uses when the user imports a record.
        var amendPath = Path.Combine(_env.ContentRootPath, "Data", "amendment_sample.json");
        if (File.Exists(amendPath))
        {
            try
            {
                // AmendmentRecord mirrors the top-level fields of each array element.
                var records = JsonSerializer.Deserialize<List<AmendmentRecord>>(
                    File.ReadAllText(amendPath), JsonOpts);

                if (records is { Count: > 0 })
                {
                    model.AmendRows = records.Count;

                    for (int i = 0; i < records.Count; i++)
                    {
                        var rec = records[i];
                        var r   = i + 1;   // 1-based row index

                        // Amendment summary (12.3a) fields
                        model.AmendData[$"C218477_r{r}"]  = rec.C218477  ?? string.Empty;
                        model.AmendData[$"C132352_r{r}"]  = rec.C93813   ?? string.Empty;  // approval date
                        model.AmendData[$"C218695_r{r}"]  = rec.C218478  ?? string.Empty;  // approx enrolled
                        model.AmendData[$"C218695e_r{r}"] = rec.C218695  ?? string.Empty;  // enrollment scope
                        model.AmendData[$"C218673_r{r}"]  = rec.C218673  ?? string.Empty;  // amendment scope

                        // Change-detail rows (12.3b)
                        var chgCount = rec.ChgRows;
                        model.ChgRows[r.ToString()] = chgCount;
                        for (int cr = 1; cr <= chgCount; cr++)
                        {
                            model.ChgData[$"C218483_a{r}_r{cr}"] =
                                rec.ChgData.TryGetValue($"C218483_r{cr}", out var desc) ? desc : string.Empty;
                            model.ChgData[$"C181233_a{r}_r{cr}"] =
                                rec.ChgData.TryGetValue($"C181233_r{cr}", out var rat)  ? rat  : string.Empty;
                            model.ChgData[$"C218479_a{r}_r{cr}"] =
                                rec.ChgData.TryGetValue($"C218479_r{cr}", out var sec)  ? sec  : string.Empty;
                        }
                    }
                }
            }
            catch
            {
                // amendment data is optional — ignore parse errors and proceed
            }
        }

        return (model, null);
    }

    // Minimal POCO for deserialising one element of amendment_sample.json.
    // Only the fields that Section 12 needs are mapped; everything else is ignored.
    private sealed class AmendmentRecord
    {
        [System.Text.Json.Serialization.JsonPropertyName("C218477")] public string? C218477 { get; set; }  // amendment identifier
        [System.Text.Json.Serialization.JsonPropertyName("C93813")]  public string? C93813  { get; set; }  // approval date
        [System.Text.Json.Serialization.JsonPropertyName("C218478")] public string? C218478 { get; set; }  // approx enrolled
        [System.Text.Json.Serialization.JsonPropertyName("C218695")] public string? C218695 { get; set; }  // enrollment scope description
        [System.Text.Json.Serialization.JsonPropertyName("C218673")] public string? C218673 { get; set; }  // amendment scope (global / not global)
        [System.Text.Json.Serialization.JsonPropertyName("ChgRows")] public int     ChgRows { get; set; }
        [System.Text.Json.Serialization.JsonPropertyName("ChgData")] public Dictionary<string, string> ChgData { get; set; } = new();
    }

    private static string CsvEscape(string v)
    {
        if (v.Contains(',') || v.Contains('"') || v.Contains('\n'))
            return "\"" + v.Replace("\"", "\"\"") + "\"";
        return v;
    }
}
