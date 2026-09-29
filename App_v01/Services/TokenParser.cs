namespace UDP_App.Services;

/// <summary>
/// Parses and substitutes {{TOKEN}} / {{REPEAT...}} / REPEAT{{ }} template tokens.
/// Matching is done by bracket-counting so nested {{ }} pairs are handled correctly.
/// </summary>
public static class TokenParser
{
    /// <summary>
    /// Find the index just after the closing '}}' that matches the '{{' at <paramref name="start"/>.
    /// Returns -1 if unmatched.
    /// </summary>
    public static int FindTokenEnd(string text, int start)
    {
        int depth = 0, i = start;
        while (i < text.Length - 1)
        {
            if (text[i] == '{' && text[i + 1] == '{')  { depth++; i += 2; }
            else if (text[i] == '}' && text[i + 1] == '}') { depth--; i += 2; if (depth == 0) return i; }
            else i++;
        }
        return -1;
    }

    /// <summary>
    /// Find the next '{{' at or after <paramref name="from"/>.
    /// Returns -1 if none found.
    /// </summary>
    public static int FindTokenStart(string text, int from = 0)
    {
        int i = text.IndexOf("{{", from, StringComparison.Ordinal);
        return i;
    }

    /// <summary>
    /// Replace all simple (non-REPEAT) tokens in <paramref name="template"/> using
    /// <paramref name="getValue"/>. Nested tokens are resolved inner-first.
    /// </summary>
    public static string Substitute(string template, Func<string, string?> getValue)
    {
        var sb = new System.Text.StringBuilder();
        int pos = 0;
        while (pos < template.Length)
        {
            int open = FindTokenStart(template, pos);
            if (open < 0) { sb.Append(template, pos, template.Length - pos); break; }

            int close = FindTokenEnd(template, open);
            if (close < 0) { sb.Append(template, pos, template.Length - pos); break; }

            // Append literal text before this token
            sb.Append(template, pos, open - pos);

            // Extract token content (between outer {{ and }})
            string content = template.Substring(open + 2, close - open - 4);

            // If content itself contains {{ }}, resolve inner tokens first
            string resolvedContent = content.Contains("{{")
                ? Substitute(content, getValue)
                : content;

            // Skip REPEAT tokens — those are handled by SubstituteRepeat
            if (resolvedContent.StartsWith("REPEAT", StringComparison.Ordinal))
            {
                sb.Append("{{").Append(resolvedContent).Append("}}");
            }
            else
            {
                var val = getValue(resolvedContent);
                sb.Append(val ?? string.Empty);
            }

            pos = close;
        }
        return sb.ToString();
    }

    /// <summary>
    /// Find a {{REPEAT<content>}} token in <paramref name="template"/>,
    /// expand it by substituting {{TABLE_ROW_X}} with each row id,
    /// and return (expandedLines, matchStart, matchLength).
    /// Leading horizontal whitespace before {{REPEAT is included in the match and discarded.
    /// Returns null if no REPEAT token found.
    /// </summary>
    public static (string expanded, int matchStart, int matchLength)? FindAndExpandRepeat(
        string template, IEnumerable<string> rowIds)
    {
        int i = 0;
        while (i < template.Length)
        {
            int open = FindTokenStart(template, i);
            if (open < 0) break;

            int close = FindTokenEnd(template, open);
            if (close < 0) break;

            string content = template.Substring(open + 2, close - open - 4);
            if (content.StartsWith("REPEAT", StringComparison.Ordinal))
            {
                // Find start of line (position after preceding newline) to capture leading whitespace
                int lineStart = open;
                while (lineStart > 0 && template[lineStart - 1] != '\n' && template[lineStart - 1] != '\r')
                    lineStart--;

                // Content after "REPEAT" is the per-row line pattern
                string rowPattern = content.Substring("REPEAT".Length);
                var sb = new System.Text.StringBuilder();
                bool first = true;
                foreach (var rowId in rowIds)
                {
                    if (!first) sb.AppendLine();
                    first = false;
                    // Substitute {{TABLE_ROW_X}} within the pattern
                    sb.Append(Substitute("{{" + rowPattern + "}}", key =>
                        key == "TABLE_ROW_X" ? rowId : key));
                }
                return (sb.ToString(), lineStart, close - lineStart);
            }

            i = close;
        }
        return null;
    }

    /// <summary>
    /// Expand a REPEAT{{ }} block for a single characteristic, selecting the correct
    /// format sub-block (RANGE{{ }}, TYPE{{ }}, BOOLEAN{{ }}, QUANTITY{{ }}) based on
    /// <paramref name="format"/> ("range", "type", "bool", "qty"), then substituting
    /// all remaining tokens using <paramref name="getValue"/>.
    /// </summary>
    public static string ExpandCharacteristic(string eligTpl, string format,
        Func<string, string?> getValue)
    {
        var fmtKey = format?.ToLowerInvariant() switch
        {
            "type" => "TYPE",
            "bool" => "BOOLEAN",
            "qty"  => "QUANTITY",
            _      => "RANGE",
        };

        var sb  = new System.Text.StringBuilder();
        int pos = 0;

        while (pos < eligTpl.Length)
        {
            int repeatIdx = eligTpl.IndexOf("REPEAT{{", pos, StringComparison.Ordinal);
            if (repeatIdx < 0) break;

            int outerOpen  = repeatIdx + "REPEAT".Length;
            int outerClose = FindTokenEnd(eligTpl, outerOpen);
            if (outerClose < 0) break;

            string outerContent = eligTpl.Substring(outerOpen + 2, outerClose - outerOpen - 4);

            foreach (var fmt in new[] { "RANGE", "TYPE", "BOOLEAN", "QUANTITY" })
            {
                if (!outerContent.StartsWith(fmt + "{{", StringComparison.Ordinal)) continue;
                if (fmt == fmtKey)
                {
                    int innerOpen  = fmt.Length;
                    int innerClose = FindTokenEnd(outerContent, innerOpen);
                    if (innerClose >= 0)
                    {
                        string charTpl = outerContent.Substring(innerOpen + 2,
                                                                 innerClose - innerOpen - 4);
                        var filtered = DropEmptyTokenLines(charTpl, getValue);
                        sb.Append(Substitute(filtered, getValue));
                        sb.AppendLine();
                    }
                }
                break;
            }

            pos = outerClose;
            if (pos < eligTpl.Length && eligTpl[pos] == '\r') pos++;
            if (pos < eligTpl.Length && eligTpl[pos] == '\n') pos++;
        }

        return sb.ToString();
    }

    /// <summary>
    /// Drop a line from a multi-line block if it contains exactly one top-level token
    /// and that token's value (from <paramref name="getValue"/>) is empty or whitespace.
    /// Line endings are preserved exactly.
    /// </summary>
    public static string DropEmptyTokenLines(string block, Func<string, string?> getValue)
    {
        // Split preserving line endings by scanning character by character
        var lines = new List<(string content, string ending)>();
        int i = 0;
        while (i < block.Length)
        {
            int lineStart = i;
            while (i < block.Length && block[i] != '\r' && block[i] != '\n') i++;
            string content = block.Substring(lineStart, i - lineStart);
            string ending = "";
            if (i < block.Length)
            {
                if (block[i] == '\r' && i + 1 < block.Length && block[i + 1] == '\n') { ending = "\r\n"; i += 2; }
                else if (block[i] == '\r') { ending = "\r"; i++; }
                else { ending = "\n"; i++; }
            }
            lines.Add((content, ending));
        }

        var sb = new System.Text.StringBuilder();
        foreach (var (content, ending) in lines)
        {
            // Count top-level tokens on this line
            var tokens = new List<string>();
            int j = 0;
            while (j < content.Length)
            {
                int open = FindTokenStart(content, j);
                if (open < 0) break;
                int close = FindTokenEnd(content, open);
                if (close < 0) break;
                tokens.Add(content.Substring(open + 2, close - open - 4));
                j = close;
            }

            bool drop = tokens.Count == 1
                && string.IsNullOrWhiteSpace(getValue(tokens[0]));

            if (!drop) sb.Append(content).Append(ending);
        }
        return sb.ToString();
    }
}
