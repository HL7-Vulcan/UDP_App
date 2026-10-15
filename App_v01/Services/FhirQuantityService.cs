namespace UDP_App.Services;

/// <summary>
/// Shared service for building FSH Quantity literals from paired qty + unit fields.
///
/// Unit fields store the full FSH-coded unit token as selected from a picklist,
/// e.g. $NCIT#C29846 "Months".  This service combines them with the numeric
/// quantity to produce a valid FSH Quantity literal:
///   24 $NCIT#C29844 "Weeks"
/// </summary>
public class FhirQuantityService
{
    /// <summary>
    /// Combines a numeric quantity string and a full FSH unit token into an FSH Quantity literal.
    /// </summary>
    /// <param name="qty">Numeric part, e.g. "24"</param>
    /// <param name="fshUnit">Full FSH unit token, e.g. $NCIT#C29846 "Months"</param>
    /// <returns>FSH Quantity literal, e.g. "24 $NCIT#C29846 \"Months\""
    ///          or empty string if qty is blank.</returns>
    public string Build(string? qty, string? fshUnit)
    {
        if (string.IsNullOrWhiteSpace(qty)) return string.Empty;
        if (string.IsNullOrWhiteSpace(fshUnit)) return qty.Trim();
        return $"{qty.Trim()} {fshUnit.Trim()}";
    }

    /// <summary>
    /// Post-processes a substituted FSH template block and removes any lines
    /// that are empty or contain only whitespace after token substitution.
    /// Also removes lines that end with "= " or "= " (assignment with no value)
    /// which indicate an empty substitution on a property line.
    /// Collapses runs of more than one blank line into a single blank line.
    /// </summary>
    public string StripEmptyLines(string fsh)
    {
        var lines = fsh.Split('\n');
        var result = new System.Text.StringBuilder();
        int consecutiveBlanks = 0;

        foreach (var rawLine in lines)
        {
            var line = rawLine.TrimEnd('\r');

            // Skip lines that are just whitespace
            if (string.IsNullOrWhiteSpace(line))
            {
                consecutiveBlanks++;
                if (consecutiveBlanks <= 1)
                    result.AppendLine();
                continue;
            }

            // Skip FSH property lines whose value is empty after substitution.
            // These look like:  * some.path =
            // or:               * some.path.valueString = ""
            var trimmed = line.TrimStart();
            if (trimmed.StartsWith("*"))
            {
                // Empty assignment: ends with "= " or just "="
                var eqIdx = trimmed.LastIndexOf('=');
                if (eqIdx >= 0)
                {
                    var afterEq = trimmed.Substring(eqIdx + 1).Trim();
                    // Strip inline comment before checking emptiness
                    var commentIdx = afterEq.IndexOf("//", StringComparison.Ordinal);
                    var valueOnly = commentIdx >= 0
                        ? afterEq.Substring(0, commentIdx).Trim()
                        : afterEq;

                    // Empty, or empty quoted string
                    if (valueOnly == string.Empty ||
                        valueOnly == "\"\"" ||
                        afterEq.TrimStart().StartsWith("//"))
                    {
                        consecutiveBlanks = 0;
                        continue;
                    }
                }
            }

            consecutiveBlanks = 0;
            result.AppendLine(line);
        }

        return result.ToString();
    }
}
