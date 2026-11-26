using System.Diagnostics;
using System.Text.Json;
using UDP_App.Models;

namespace UDP_App.Services;

/// <summary>
/// Generates Word (.docx) files from section data models using a Node.js script.
/// Returns raw bytes suitable for browser download.
/// </summary>
public class WordDocService
{
    private readonly IWebHostEnvironment _env;

    public WordDocService(IWebHostEnvironment env)
    {
        _env = env;
    }

    // ── Public entry points ──────────────────────────────────────────────────

    /// <summary>Generate a docx for a single section from its stored JSON.</summary>
    public async Task<(byte[]? Bytes, string? Error)> GenerateSection(string sectionKey, string json)
    {
        // Load amendment sample data for sections that need it (Title Page and Section 12)
        var amendments = (sectionKey is "tp" or "s12") ? LoadAmendments() : new List<AmendmentRecord>();
        var blocks = BuildSectionBlocks(sectionKey, json, amendments);
        if (blocks is null) return (null, $"Unknown section key: {sectionKey}");
        return await RunNode(blocks);
    }

    /// <summary>Generate a combined docx for all sections from a dictionary of stored JSON values.</summary>
    public async Task<(byte[]? Bytes, string? Error)> GenerateAll(Dictionary<string, string> jsonByKey)
    {
        var allBlocks = new List<DocBlock>();
        bool firstSection = true;
        var amendments = LoadAmendments();

        // Order: Title Page, then sections 1–14 (skipping 2–3 which may differ), skip amendments standalone
        var order = new[] { "tp", "s1", "s2", "s3", "s4", "s5", "s6", "s7", "s8", "s9", "s10", "s11", "s12", "s13", "s14" };
        foreach (var key in order)
        {
            if (!jsonByKey.TryGetValue(key, out var json) || string.IsNullOrWhiteSpace(json))
                continue;
            var blocks = BuildSectionBlocks(key, json, amendments);
            if (blocks is null) continue;

            if (!firstSection)
                allBlocks.Add(new DocBlock { Type = "pageBreak" });
            allBlocks.AddRange(blocks);
            firstSection = false;
        }

        if (allBlocks.Count == 0)
            return (null, "No section data available to export.");

        return await RunNode(allBlocks);
    }

    /// <summary>Returns the same blocks used to generate the docx, for in-page preview.</summary>
    public List<DocBlockPreview>? GetPreviewBlocks(string key, string json)
    {
        var amendments = LoadAmendments();
        var blocks = BuildSectionBlocks(key, json, amendments);
        return blocks?.Select(b => new DocBlockPreview(b.Type, b.Text, b.Label, b.Col1, b.Col2, b.Col3, b.Col4)).ToList();
    }

    public sealed record DocBlockPreview(string Type, string? Text, string? Label = null, string? Col1 = null, string? Col2 = null, string? Col3 = null, string? Col4 = null);

    /// <summary>Generate a docx showing all amendment details, mirroring AmendmentDetails.razor.</summary>
    public async Task<(byte[]? Bytes, string? Error)> GenerateAmendments()
    {
        var amendments = LoadAmendments();
        if (amendments.Count == 0)
            return (null, "No amendment sample data found.");
        var blocks = BuildAmendmentDetails(amendments);
        return await RunNode(blocks);
    }

    /// <summary>Preview blocks for the amendments document.</summary>
    public List<DocBlockPreview> GetAmendmentPreviewBlocks()
    {
        var amendments = LoadAmendments();
        return BuildAmendmentDetails(amendments).Select(b => new DocBlockPreview(b.Type, b.Text, b.Label, b.Col1, b.Col2, b.Col3, b.Col4)).ToList();
    }

    /// <summary>Loads amendment sample data from file. Returns empty list if not found/invalid.</summary>
    private List<AmendmentRecord> LoadAmendments()
    {
        try
        {
            var json = GetAmendmentSampleJson();
            if (string.IsNullOrWhiteSpace(json)) return new();
            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<List<AmendmentRecord>>(json, opts) ?? new();
        }
        catch { return new(); }
    }

    // ── Sample data helpers ──────────────────────────────────────────────────

    /// <summary>Returns sample JSON for all sections, keyed by section key.</summary>
    public Dictionary<string, string> GetAllSampleJson()
    {
        var result = new Dictionary<string, string>();
        foreach (var key in new[] { "tp","s1","s2","s3","s4","s5","s6","s7","s8","s9","s10","s11","s12","s13","s14" })
        {
            var json = GetSampleJson(key);
            if (json != null) result[key] = json;
        }
        return result;
    }

    /// <summary>Returns raw sample JSON for the given section key, or null if not found.</summary>
    public string? GetSampleJson(string key)
    {
        var filename = key switch
        {
            "tp"  => "title_page_sample.json",
            "s1"  => "section_01_sample.json",
            "s2"  => "section_02_sample.json",
            "s3"  => "section_03_sample.json",
            "s4"  => "section_04_sample.json",
            "s5"  => "section_05_sample.json",
            "s6"  => "section_06_sample.json",
            "s7"  => "section_07_sample.json",
            "s8"  => "section_08_sample.json",
            "s9"  => "section_09_sample.json",
            "s10" => "section_10_sample.json",
            "s11" => "section_11_sample.json",
            "s12" => "section_12_sample.json",
            "s13" => "section_13_sample.json",
            "s14" => "section_14_sample.json",
            _     => null
        };
        if (filename == null) return null;
        var path = Path.Combine(_env.ContentRootPath, "Data", filename);
        return File.Exists(path) ? File.ReadAllText(path) : null;
    }

    /// <summary>Returns the raw amendment sample JSON, or null if not found.</summary>
    public string? GetAmendmentSampleJson()
    {
        var path = Path.Combine(_env.ContentRootPath, "Data", "amendment_sample.json");
        return File.Exists(path) ? File.ReadAllText(path) : null;
    }

    // ── Block builder ────────────────────────────────────────────────────────

    private List<DocBlock>? BuildSectionBlocks(string key, string json, List<AmendmentRecord>? amendments = null)
    {
        amendments ??= new();
        try
        {
            return key switch
            {
                "tp"  => BuildTitlePage(Deser<TitlePageModel>(json)!),
                "s1"  => BuildSection01(Deser<Section01Model>(json)!),
                "s2"  => BuildSection02(Deser<Section02Model>(json)!),
                "s3"  => BuildSection03(Deser<Section03Model>(json)!),
                "s4"  => BuildSection04(Deser<Section04Model>(json)!),
                "s5"  => BuildSection05(Deser<Section05Model>(json)!),
                "s6"  => BuildSection06(Deser<Section06Model>(json)!),
                "s7"  => BuildSection07(Deser<Section07Model>(json)!),
                "s8"  => BuildSection08(Deser<Section08Model>(json)!),
                "s9"  => BuildSection09(Deser<Section09Model>(json)!),
                "s10" => BuildSection10(Deser<Section10Model>(json)!),
                "s11" => BuildSection11(Deser<Section11Model>(json)!),
                "s12" => BuildSection12(Deser<Section12Model>(json)!, amendments),
                "s13" => BuildSection13(Deser<Section13Model>(json)!),
                "s14" => BuildSection14(Deser<Section14Model>(json)!),
                _ => null
            };
        }
        catch { return null; }
    }

    private static T? Deser<T>(string json)
    {
        var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return JsonSerializer.Deserialize<T>(json, opts);
    }

    // ── Dedicated section builders ────────────────────────────────────────────
    // Each builder mirrors the heading hierarchy shown on that section's Razor page.

    private static List<DocBlock> BuildTitlePage(TitlePageModel m)
    {
        var blocks = new List<DocBlock>();
        blocks.Add(new DocBlock { Type = "h1", Text = "Title Page" });
        // Mirror tp-tbl on page: 2-column label|value table rows.
        // Stacked sub-fields (e.g. Sponsor Name + Address) are joined with newline in one cell.
        void TR(string label, string? val)
        {
            if (string.IsNullOrWhiteSpace(val)) return;
            blocks.Add(new DocBlock { Type = "tableRow", Label = label, Text = val });
        }
        TR("Sponsor Confidentiality Statement:", m.C181236);
        TR("Full Title:", m.C132346);
        TR("Trial Acronym:", m.C94108);
        TR("Sponsor Protocol Identifier:", m.C132351);
        TR("Version Number:", m.C181232);
        TR("Version Date:", m.C93813);
        TR("Amendment Identifier:", m.C218477);
        TR("Sponsor's Investigational Product Code(s):", m.C218675);
        TR("Investigational Product Name(s):", Join(m.C97054, m.C71898));
        TR("Trial Phase:", m.C48281);
        TR("Short Title:", m.C94105);
        TR("Sponsor Name and Address:", Join(m.C222495, m.C218677));
        TR("Co-Sponsor Name and Address:", Join(m.C218678, m.C218679));
        TR("Local Sponsor Name and Address:", Join(m.C218680, m.C218681));
        TR("Device Manufacturer Name and Address:", Join(m.C218682, m.C218683));
        TR("EU CT Number:", m.C218684);
        TR("FDA IND Number:", m.C218685);
        TR("IDE Number:", m.C218686);
        TR("jRCT Number:", m.C218687);
        TR("NCT Number:", m.C172240);
        TR("NMPA IND Number:", m.C218688);
        TR("WHO/UTN Number:", m.C218689);
        TR("Other Identifier:", m.C218690);
        TR("Sponsor Signatory:", m.C222014);
        TR("Medical Expert Contact:", m.C218693);

        // Amendment Details section — mirrors TitlePage.razor exactly, including tabular layout.
        // All fields come from TitlePageModel (m). Shown when the protocol is an amendment.
        if (!string.IsNullOrWhiteSpace(m.C218477) || !string.IsNullOrWhiteSpace(m.C218694))
        {
            blocks.Add(new DocBlock { Type = "h2", Text = "Amendment Details" });

            // Table 1 (single row): Amendment Status — mirrors first tp-tbl on page
            blocks.Add(new DocBlock { Type = "tableRow", Label = "Amendment Status:", Text = m.C218694 ?? string.Empty });

            // Italic paragraph between the two tables
            blocks.Add(new DocBlock { Type = "para", Text = "The table below describes the current amendment. Complete only if an amendment applies." });

            // Table 2: main amendment fields — mirrors second/third tp-tbl on page.
            // Cells where the page stacks two sub-fields combine them with a newline.
            TR("Amendment Identifier:", m.C218477);
            TR("Amendment Scope:", m.C218673);
            // Approx Enrolled and Scope Enrollment Definition are stacked in one cell on the page
            TR("Approx. Enrolled at Time of Sponsor Approval:",
                Join(m.C218478, m.C218695));
            // Primary Reason and Other text are stacked in one cell on the page
            TR("Primary Reason for Amendment:", Join(m.C218696, m.C17649));
            // Secondary Reason and Other text are stacked in one cell on the page
            TR("Secondary Reason for Amendment:", Join(m.C218697, m.C17649_2));
            TR("Amendment Summary:", m.C42581);
            // Safety impact and explanation are stacked in one cell on the page
            TR("Substantial Impact on Safety or Rights?", Join(m.C218698, m.C218699));
            // Reliability impact and explanation are stacked in one cell on the page
            TR("Substantial Impact on Reliability and Robustness?", Join(m.C218700, m.C218701));

            // Overview of Changes in the Current Amendment — h2 + 3-column table, mirrors page
            blocks.Add(new DocBlock { Type = "h2", Text = "Overview of Changes in the Current Amendment" });
            if (m.ChgRows > 0)
            {
                // Header row
                blocks.Add(new DocBlock { Type = "tableRow3h", Col1 = "Description of Change", Col2 = "Brief Rationale for Change", Col3 = "Section # and Name" });
                for (int r = 1; r <= m.ChgRows; r++)
                {
                    var desc = m.ChgData.TryGetValue($"C218483_r{r}", out var d)  ? d  : string.Empty;
                    var rat  = m.ChgData.TryGetValue($"C181233_r{r}", out var rv) ? rv : string.Empty;
                    var sec  = m.ChgData.TryGetValue($"C218479_r{r}", out var sv) ? sv : string.Empty;
                    var secDisplay = System.Text.RegularExpressions.Regex.Match(sec, "\"([^\"]+)\"") is var mx && mx.Success ? mx.Groups[1].Value : sec;
                    blocks.Add(new DocBlock { Type = "tableRow3", Col1 = desc, Col2 = rat, Col3 = secDisplay });
                }
            }
        }

        return blocks;
    }

    private static List<DocBlock> BuildSection01(Section01Model m)
    {
        var blocks = new List<DocBlock>();
        blocks.Add(new DocBlock { Type = "h1", Text = "1.  Protocol Summary" });
        blocks.Add(new DocBlock { Type = "h2", Text = "1.1  Protocol Synopsis" });
        blocks.Add(new DocBlock { Type = "h3", Text = "1.1.1  Primary and Secondary Objectives and Estimands" });
        if (!string.IsNullOrWhiteSpace(m.C218839))
            blocks.Add(new DocBlock { Type = "para", Text = m.C218839 });

        blocks.Add(new DocBlock { Type = "h3", Text = "1.1.2  Overall Design" });
        blocks.Add(new DocBlock { Type = "para", Text = "Key aspects of the trial design are summarised below." });

        // Design table — mirrors the 4-column dtbl on the page, rendered as a 2-column tableRow table.
        // Helper to strip FHIR/NCIT code prefix and return the display label.
        static string Display(string? val)
        {
            if (string.IsNullOrWhiteSpace(val)) return string.Empty;
            // Extract quoted display value: e.g. $NCIT#C82639 "Parallel Group" → Parallel Group
            var m2 = System.Text.RegularExpressions.Regex.Match(val, "\"([^\"]+)\"");
            return m2.Success ? m2.Groups[1].Value : val;
        }
        static string DisplayList(List<string>? val)
        {
            if (val == null || val.Count == 0) return string.Empty;
            return string.Join(", ", val.Select(s => {
                var m2 = System.Text.RegularExpressions.Regex.Match(s, "\"([^\"]+)\"");
                return m2.Success ? m2.Groups[1].Value : s;
            }));
        }

        // 4-column design table — mirrors the dtbl on the page exactly.
        // tableRow4span = label col + value spanning remaining 3 cols (Intervention row)
        // tableRow4     = lbl | val | lbl | val  (Label=lbl1, Text=val1, Col3=lbl2, Col4=val2)
        void TR4(string lbl1, string? val1, string lbl2, string? val2) =>
            blocks.Add(new DocBlock { Type = "tableRow4", Label = lbl1, Text = val1 ?? string.Empty, Col3 = lbl2, Col4 = val2 ?? string.Empty });

        void TR4Span(string lbl, string? val) =>
            blocks.Add(new DocBlock { Type = "tableRow4span", Label = lbl, Text = val ?? string.Empty });

        // Age — stored as FSH quantity e.g. "18 $NCIT#C29848 \"Years\""; extract qty + unit display
        static string AgeDisplay(string? stored)
        {
            if (string.IsNullOrWhiteSpace(stored)) return string.Empty;
            var qty = stored.Split(' ')[0];
            var m2 = System.Text.RegularExpressions.Regex.Match(stored, "\"([^\"]+)\"");
            var unit = m2.Success ? m2.Groups[1].Value : string.Empty;
            return string.IsNullOrWhiteSpace(unit) ? qty : $"{qty} {unit}";
        }

        // Row 1: Intervention (label col + value spanning all 3 remaining cols)
        TR4Span("Intervention:", Join(m.C218675, m.C97054));

        // Row 2: Intervention Model | Population Type
        TR4("Intervention Model:", Display(m.C98746), "Population Type:", Display(m.C218703));

        // Row 3: Control Type | Population Diagnosis or Condition
        TR4("Control Type:", Display(m.C49647), "Population Diagnosis or Condition:", Display(m.C112038));

        // Row 4: Control Description | Population Age (min + max stacked in one cell)
        var minAge = AgeDisplay(m.C49693);
        var maxAge = AgeDisplay(m.C49694);
        var ageVal = (string.IsNullOrWhiteSpace(minAge) && string.IsNullOrWhiteSpace(maxAge))
            ? string.Empty : $"min: {minAge}\nmax: {maxAge}";
        TR4("Control Description:", m.C97054, "Population Age:", ageVal);

        // Row 5: Intervention Assignment Method | Site Distribution
        TR4("Intervention Assignment Method:", Display(m.C218475), "Site Distribution:", Display(m.C218704));

        // Row 6: Randomisation Type / Other Intervention | Geographic Scope
        var assignDetail = Join(m.C223137, m.C223138);
        TR4("Randomisation Type / Other Intervention:", assignDetail, "Geographic Scope:", Display(m.C218705));

        // Row 7: Stratification | Master Protocol
        TR4("Stratification:", Display(m.C223136), "Master Protocol:", Display(m.C218707));

        // Row 8: Drug/Device Combination | Adaptive Trial Design
        TR4("Drug/Device Combination Product Indicator:", Display(m.C218708), "Adaptive Trial Design:", Display(m.C218706));

        // Post-table paragraph fields (inline on the page)
        if (!string.IsNullOrWhiteSpace(m.C98771))
            blocks.Add(new DocBlock { Type = "para", Text = $"Number of Arms: {m.C98771}" });

        if (!string.IsNullOrWhiteSpace(m.C49658))
            blocks.Add(new DocBlock { Type = "para", Text = $"Trial Blind Schema: {Display(m.C49658)}" });

        // Blinded Roles — multi-select list
        var blindedRoles = DisplayList(m.C218709);
        if (!string.IsNullOrWhiteSpace(blindedRoles))
            blocks.Add(new DocBlock { Type = "para", Text = $"Blinded Roles: The following roles will not be made aware of the treatment group assignment: {blindedRoles}" });

        // Number of Participants
        var targetDisplay = Display(m.C218710);
        if (!string.IsNullOrWhiteSpace(m.C49692))
            blocks.Add(new DocBlock { Type = "para", Text = $"Number of Participants: A {targetDisplay} of {m.C49692} participants." });

        // Duration — intervention
        if (!string.IsNullOrWhiteSpace(m.C218712))
            blocks.Add(new DocBlock { Type = "para", Text = $"Duration: Total planned duration of trial intervention for each participant: {m.C218712} {Display(m.C218713)}." });

        // Duration — participation
        if (!string.IsNullOrWhiteSpace(m.C218715))
            blocks.Add(new DocBlock { Type = "para", Text = $"Total planned duration of trial participation for each participant: {m.C218715} {Display(m.C218716)}." });

        if (!string.IsNullOrWhiteSpace(m.C218714))
            blocks.Add(new DocBlock { Type = "para", Text = $"Alternate Duration Description: {m.C218714}" });

        // Committees
        var indCommittees = DisplayList(m.C218718);
        var otherCommittee = m.C218719;
        if (!string.IsNullOrWhiteSpace(indCommittees) || !string.IsNullOrWhiteSpace(otherCommittee))
        {
            blocks.Add(new DocBlock { Type = "para", Text = "Committees:" });
            if (!string.IsNullOrWhiteSpace(indCommittees))
                blocks.Add(new DocBlock { Type = "para", Text = $"Independent Committees: {indCommittees}." });
            if (!string.IsNullOrWhiteSpace(otherCommittee))
                blocks.Add(new DocBlock { Type = "para", Text = $"Other Committees: {otherCommittee}." });
        }

        blocks.Add(new DocBlock { Type = "h3", Text = "1.2  Trial Schema" });
        if (!string.IsNullOrWhiteSpace(m.C218720))
            blocks.Add(new DocBlock { Type = "para", Text = m.C218720 });

        blocks.Add(new DocBlock { Type = "h3", Text = "1.3  Schedule of Activities" });
        if (!string.IsNullOrWhiteSpace(m.C132349))
            blocks.Add(new DocBlock { Type = "para", Text = m.C132349 });

        return blocks;
    }

    private static List<DocBlock> BuildSection02(Section02Model m)
    {
        var blocks = new List<DocBlock>();
        blocks.Add(new DocBlock { Type = "h1", Text = "2.  Introduction" });
        void Add(string heading, string? val, string level = "h2") { Emit(blocks, heading, val, level); }

        Add("2.1  Purpose of Trial", m.C146997);
        Add("2.2  Assessment of Risks and Benefits", null);
        Add("Risk of Trial Intervention", m.C218721, "h3");
        Add("Risk of Trial Procedures", m.C218722, "h3");
        Add("Other Risks", m.C218723, "h3");
        Add("Benefit Summary", m.C218724, "h3");
        Add("Overall Risk–Benefit Assessment", m.C218725, "h3");
        return blocks;
    }

    private static List<DocBlock> BuildSection03(Section03Model m)
    {
        var blocks = new List<DocBlock>();
        blocks.Add(new DocBlock { Type = "h1", Text = "3.  Trial Objectives and Associated Estimands" });

        // Mirror ObjectiveBlockComponent.razor layout:
        //   h3 "Primary/Secondary/Exploratory Objective #N"
        //   para  = ObjectiveText
        //   tableRow3h "Estimand Characteristic | Description" header
        //   tableRow3  per estimand field
        //   tableRow3h "Intercurrent Event | Strategy" header (if any ICE rows have data)
        //   tableRow3  per ICE row
        void AddObjectiveGroup(List<ObjectiveBlock>? list, string label)
        {
            if (list == null || list.Count == 0) return;
            for (int i = 0; i < list.Count; i++)
            {
                var obj = list[i];
                // Skip entirely empty blocks
                bool hasData = !string.IsNullOrWhiteSpace(obj.ObjectiveText)
                    || !string.IsNullOrWhiteSpace(obj.C70833)
                    || !string.IsNullOrWhiteSpace(obj.C49236)
                    || !string.IsNullOrWhiteSpace(obj.C25212)
                    || !string.IsNullOrWhiteSpace(obj.C188853)
                    || (obj.IceRows?.Any(r => !string.IsNullOrWhiteSpace(r.C188856) || !string.IsNullOrWhiteSpace(r.C188857)) ?? false);
                if (!hasData) continue;

                // Heading: "Primary Objective #1"
                blocks.Add(new DocBlock { Type = "h3", Text = $"{label} #{i + 1}" });

                // Objective description text
                if (!string.IsNullOrWhiteSpace(obj.ObjectiveText))
                    blocks.Add(new DocBlock { Type = "para", Text = obj.ObjectiveText });

                // Estimands table — 2-col label|value (mirrors est-tbl on page)
                bool hasEstimand = !string.IsNullOrWhiteSpace(obj.C70833)
                    || !string.IsNullOrWhiteSpace(obj.C49236)
                    || !string.IsNullOrWhiteSpace(obj.C25212)
                    || !string.IsNullOrWhiteSpace(obj.C188853);
                if (hasEstimand)
                {
                    if (!string.IsNullOrWhiteSpace(obj.C70833))
                        blocks.Add(new DocBlock { Type = "tableRow", Label = "Population", Text = obj.C70833 });
                    if (!string.IsNullOrWhiteSpace(obj.C49236))
                        blocks.Add(new DocBlock { Type = "tableRow", Label = "Treatment", Text = obj.C49236 });
                    if (!string.IsNullOrWhiteSpace(obj.C25212))
                        blocks.Add(new DocBlock { Type = "tableRow", Label = "Endpoint", Text = obj.C25212 });
                    if (!string.IsNullOrWhiteSpace(obj.C188853))
                        blocks.Add(new DocBlock { Type = "tableRow", Label = "Population-level summary", Text = obj.C188853 });
                }

                // ICE table — 2-col equal-width (Intercurrent Event | Strategy), only if any rows have data
                var iceRows = obj.IceRows?.Where(r =>
                    !string.IsNullOrWhiteSpace(r.C188856) || !string.IsNullOrWhiteSpace(r.C188857)).ToList();
                if (iceRows != null && iceRows.Count > 0)
                {
                    blocks.Add(new DocBlock { Type = "tableRow2h", Col1 = "Intercurrent Event", Col2 = "Strategy" });
                    foreach (var ice in iceRows)
                        blocks.Add(new DocBlock { Type = "tableRow2", Col1 = ice.C188856 ?? string.Empty, Col2 = ice.C188857 ?? string.Empty });
                }
            }
        }

        blocks.Add(new DocBlock { Type = "h2", Text = "3.1  Primary Objective(s) and Associated Estimand(s)" });
        blocks.Add(new DocBlock { Type = "h3", Text = "3.1.1  Primary Objective(s) and Associated Estimand(s)" });
        AddObjectiveGroup(m.PrimaryObjectives, "Primary Objective");

        blocks.Add(new DocBlock { Type = "h2", Text = "3.2  Secondary Objective(s) and Associated Estimand(s)" });
        blocks.Add(new DocBlock { Type = "h3", Text = "3.2.1  Secondary Objective(s) and Associated Estimand(s)" });
        AddObjectiveGroup(m.SecondaryObjectives, "Secondary Objective");

        blocks.Add(new DocBlock { Type = "h2", Text = "3.3  Exploratory Objective(s)" });
        blocks.Add(new DocBlock { Type = "h3", Text = "3.3.1  Exploratory Objective(s)" });
        AddObjectiveGroup(m.ExploratoryObjectives, "Exploratory Objective");

        return blocks;
    }

    private static List<DocBlock> BuildSection04(Section04Model m)
    {
        var blocks = new List<DocBlock>();
        blocks.Add(new DocBlock { Type = "h1", Text = "4.  Trial Design" });
        void Add(string heading, string? val, string level = "h2") { Emit(blocks, heading, val, level); }

        Add("4.1  Description of Trial Design", null);
        Add("Overall Description of Trial Design and Description of Intervention Model", m.C147139, "h3");
        Add("Description of Trial Duration", m.C218726, "h3");
        Add("Description of Method of Assignment to Trial Intervention", m.C219658, "h3");
        Add("Description of Level and Method of Blinding", m.C218727, "h3");
        Add("Additional Description of Trial Design", m.C218728, "h3");
        Add("Stakeholder Input into Design", m.C218729, "h3");
        Add("4.2  Rationale for Trial Design", m.C142705);
        Add("4.2.1  Rationale for Estimand(s)", m.C218730, "h3");
        Add("4.2.2  Rationale for Intervention Model", m.C215629, "h3");
        Add("4.2.3  Rationale for Control Type", m.C218731, "h3");
        Add("4.2.4  Rationale for Trial Duration", m.C218732, "h3");
        Add("4.2.5  Rationale for Adaptive or Novel Trial Design", m.C218733, "h3");
        Add("Rationale for Interim Analysis", m.C218734, "h3");
        Add("4.2.6  Rationale for Other Trial Design Aspects", m.C218735, "h3");
        Add("4.3  Trial Stopping Rules", m.C142698);
        Add("4.4  Start of Trial and End of Trial", null);
        Add("Start of Trial", m.C218736, "h3");
        Add("End of Trial", m.C218737, "h3");
        Add("Access to Trial Intervention After End of Trial", m.C218738, "h3");
        return blocks;
    }

    private static List<DocBlock> BuildSection05(Section05Model m)
    {
        var blocks = new List<DocBlock>();
        blocks.Add(new DocBlock { Type = "h1", Text = "5.  Trial Population" });
        void Add(string heading, string? val, string level = "h2") { Emit(blocks, heading, val, level); }

        Add("5.1  Description of Trial Population and Rationale", m.C218739);
        Add("5.2  Inclusion Criteria", m.C25532);
        Add("5.3  Exclusion Criteria", m.C25370);
        Add("5.4  Contraception", null);
        Add("5.4.1  Definitions Related to Childbearing Potential", m.C218740, "h3");
        Add("5.4.2  Contraception Requirements", m.C218741, "h3");
        Add("5.5  Lifestyle Restrictions", m.C218742);
        Add("5.5.1  Meals and Dietary Restrictions", m.C218743, "h3");
        Add("5.5.2  Caffeine, Alcohol, Tobacco, and Other Restrictions", m.C218744, "h3");
        Add("5.5.3  Physical Activity Restrictions", m.C218745, "h3");
        Add("5.5.4  Other Activity Restrictions", m.C218746, "h3");
        Add("5.6  Screen Failure and Rescreening", null);
        Add("5.6.1  Screen Failure", m.C49628, "h3");
        Add("5.6.2  Rescreening", m.C179373, "h3");
        return blocks;
    }

    private static List<DocBlock> BuildSection06(Section06Model m)
    {
        var blocks = new List<DocBlock>();
        blocks.Add(new DocBlock { Type = "h1", Text = "6.  Trial Intervention and Concomitant Therapy" });
        void Add(string heading, string? val, string level = "h2") { Emit(blocks, heading, val, level); }

        Add("Overview of Trial Interventions", m.C218747);

        // Intervention table — mirrors intv-tbl on page.
        // 12 columns (too wide for A4); rendered as 2-col tableRow groups per arm row.
        // Column codes: C93729=ArmName, C172457=ArmType, C177930=INN, C98747=Type,
        //   C42636=DoseForm, C142517=Strength, C94394=DosageLevel, C38114=Route,
        //   C15697=Regimen, C218748=Use, C218749=IMP/NIMP, C218750=Sourcing
        if (m.TblRows > 0 && m.TblData.Count > 0)
        {
            static string G(Dictionary<string, string> d, string key) => d.TryGetValue(key, out var v) ? v : string.Empty;
            static string DV(string? s) {
                if (string.IsNullOrWhiteSpace(s)) return string.Empty;
                var mx = System.Text.RegularExpressions.Regex.Match(s, "\"([^\"]+)\"");
                return mx.Success ? mx.Groups[1].Value : s;
            }
            void TR6(string lbl, string? val) => blocks.Add(new DocBlock { Type = "tableRow", Label = lbl, Text = val ?? string.Empty });

            for (int r = 1; r <= m.TblRows; r++)
            {
                var armName = G(m.TblData, $"C93729_r{r}");
                if (string.IsNullOrWhiteSpace(armName) && m.TblRows == 1) armName = "Arm 1";
                blocks.Add(new DocBlock { Type = "h3", Text = $"Arm: {armName}" });
                TR6("Arm Type:",           DV(G(m.TblData, $"C172457_r{r}")));
                TR6("INN/Proprietary Name:", G(m.TblData, $"C177930_r{r}"));
                TR6("Intervention Type:", DV(G(m.TblData, $"C98747_r{r}")));
                TR6("Dose Form:",         DV(G(m.TblData, $"C42636_r{r}")));
                TR6("Strength:",          G(m.TblData, $"C142517_r{r}"));
                TR6("Dosage Level:",       G(m.TblData, $"C94394_r{r}"));
                TR6("Route:",             DV(G(m.TblData, $"C38114_r{r}")));
                TR6("Regimen:",            G(m.TblData, $"C15697_r{r}"));
                TR6("Use:",               DV(G(m.TblData, $"C218748_r{r}")));
                TR6("IMP/NIMP:",          DV(G(m.TblData, $"C218749_r{r}")));
                TR6("Sourcing:",          DV(G(m.TblData, $"C218750_r{r}")));
            }
        }

        Add("6.1  Description of Investigational Trial Intervention", m.C218751);
        Add("6.2  Rationale for Investigational Trial Intervention Dose and Regimen", m.C218752);
        Add("6.3  Investigational Trial Intervention Administration", m.C218753);
        Add("6.4  Investigational Trial Intervention Dose Modification", m.C218754);
        Add("6.5  Management of Investigational Trial Intervention Overdose", m.C218755);
        Add("6.6  Preparation, Storage, Handling and Accountability", null);
        Add("6.6.1  Preparation of Investigational Trial Intervention", m.C176274, "h3");
        Add("6.6.2  Storage and Handling of Investigational Trial Intervention", m.C115525, "h3");
        Add("6.6.3  Accountability of Investigational Trial Intervention", m.C176267, "h3");
        Add("6.7  Investigational Trial Intervention Assignment, Randomisation and Blinding", null);
        Add("6.7.1  Participant Assignment to Investigational Trial Intervention", m.C218756, "h3");
        Add("6.7.2  Randomisation", m.C25196, "h3");
        Add("6.7.3  Measures to Maintain Blinding", m.C189349, "h3");
        Add("6.7.4  Emergency Unblinding at the Site", m.C218757, "h3");
        Add("6.8  Investigational Trial Intervention Adherence", m.C218758);
        Add("6.9  Description of Noninvestigational Trial Intervention", m.C218759);
        Add("6.9.1  Background Trial Intervention", m.C222329, "h3");
        Add("6.9.2  Rescue Therapy", m.C222330, "h3");
        Add("6.9.3  Other Noninvestigational Trial Intervention", m.C218761, "h3");
        Add("6.10  Concomitant Therapy", m.C53630);
        Add("6.10.1  Prohibited Concomitant Therapy", m.C218762, "h3");
        Add("6.10.2  Permitted Concomitant Therapy", m.C218763, "h3");
        return blocks;
    }

    private static List<DocBlock> BuildSection07(Section07Model m)
    {
        var blocks = new List<DocBlock>();
        blocks.Add(new DocBlock { Type = "h1", Text = "7.  Participant Discontinuation of Trial Intervention and Discontinuation or Withdrawal from Trial" });
        void Add(string heading, string? val, string level = "h2") { Emit(blocks, heading, val, level); }

        Add("7.1  Discontinuation of Trial Intervention for Individual Participants", null);
        Add("7.1.1  Permanent Discontinuation of Trial Intervention", m.C218764, "h3");
        Add("7.1.2  Temporary Discontinuation of Trial Intervention", m.C218765, "h3");
        Add("7.1.3  Rechallenge", m.C218766, "h3");
        Add("7.2  Participant Discontinuation or Withdrawal from the Trial", m.C218767);
        Add("7.3  Management of Loss to Follow-Up", m.C218768);
        return blocks;
    }

    private static List<DocBlock> BuildSection08(Section08Model m)
    {
        var blocks = new List<DocBlock>();
        blocks.Add(new DocBlock { Type = "h1", Text = "8.  Trial Assessments and Procedures" });
        void Add(string heading, string? val, string level = "h2") { Emit(blocks, heading, val, level); }

        Add("8.1  Trial Assessments and Procedures Considerations", m.C218769);
        Add("8.2  Screening/Baseline Assessments and Procedures", null);
        Add("8.2.1  Screening Assessments and Procedures", m.C218770, "h3");
        Add("8.2.2  Baseline Assessments and Procedures", m.C218771, "h3");
        Add("8.3  Efficacy Assessments and Procedures", m.C218772);
        Add("8.4  Safety Assessments and Procedures", m.C218773);
        Add("8.4.1  Physical Examination", m.C20989, "h3");
        Add("8.4.2  Vital Signs", m.C154628, "h3");
        Add("8.4.3  Electrocardiograms", m.C168186, "h3");
        Add("8.4.4  Clinical Laboratory Assessments", m.C218774, "h3");
        Add("8.4.5  Pregnancy Testing", m.C92949, "h3");
        Add("8.4.6  Suicidal Ideation and Behaviour Risk Monitoring", m.C218775, "h3");
        Add("8.5  Pharmacokinetics", m.C218776);
        Add("8.6  Biomarkers", null);
        Add("8.6.1  Genetics and Pharmacogenomics", m.C218777, "h3");
        Add("8.6.2  Pharmacodynamic Biomarkers", m.C218778, "h3");
        Add("8.6.3  Other Biomarkers", m.C218779, "h3");
        Add("8.7  Immunogenicity Assessments", m.C218780);
        Add("8.8  Medical Resource Utilisation and Health Economics", m.C176849);
        return blocks;
    }

    private static List<DocBlock> BuildSection09(Section09Model m)
    {
        var blocks = new List<DocBlock>();
        blocks.Add(new DocBlock { Type = "h1", Text = "9.  Adverse Events, Serious Adverse Events, Product Complaints, Pregnancy and Postpartum Information, and Special Safety Situations" });
        void Add(string heading, string? val, string level = "h2") { Emit(blocks, heading, val, level); }

        Add("9.1  Definitions", null);
        Add("9.1.1  Definitions of Adverse Events", m.C218476, "h3");
        Add("9.1.2  Definitions of Serious Adverse Events", m.C218781, "h3");
        Add("9.1.3  Definitions of Product Complaints", m.C218782, "h3");
        Add("Medical Device Product Complaints", m.C218783, "h3");
        Add("9.2  Timing and Procedures for Collection and Reporting", null);
        blocks.Add(new DocBlock { Type = "para", Text = "This table describes the timing and procedures for collecting and reporting events." });
        // Reporting table — mirrors rpt-tbl on page (7 columns).
        // Columns: C218784=EventType, C222263=OtherEventType, C218785=SituationalScope,
        //   C218786=ReportablePeriodStart, C218787=ReportablePeriodEnd,
        //   C218788=TimingForReporting, C218789=MethodForReporting, C218790=BackupMethod
        if (m.RptRows > 0 && m.RptTableData.Count > 0)
        {
            static string G(Dictionary<string, string> d, string key) => d.TryGetValue(key, out var v) ? v : string.Empty;
            static string DV(string? s) {
                if (string.IsNullOrWhiteSpace(s)) return string.Empty;
                var mx = System.Text.RegularExpressions.Regex.Match(s, "\"([^\"]+)\"");
                return mx.Success ? mx.Groups[1].Value : s;
            }
            blocks.Add(new DocBlock { Type = "tableRow3h", Col1 = "Event Type / Situational Scope", Col2 = "Reportable Period Start / End", Col3 = "Timing / Method / Backup Method" });
            for (int r = 1; r <= m.RptRows; r++)
            {
                var evtType = DV(G(m.RptTableData, $"C218784_r{r}"));
                var otherEvt = G(m.RptTableData, $"C222263_r{r}");
                var col1 = Join(!string.IsNullOrWhiteSpace(evtType) ? evtType : otherEvt,
                                G(m.RptTableData, $"C218785_r{r}"));
                var col2 = Join(G(m.RptTableData, $"C218786_r{r}"), G(m.RptTableData, $"C218787_r{r}"));
                var col3 = Join(Join(G(m.RptTableData, $"C218788_r{r}"), G(m.RptTableData, $"C218789_r{r}")),
                                G(m.RptTableData, $"C218790_r{r}"));
                blocks.Add(new DocBlock { Type = "tableRow3", Col1 = col1, Col2 = col2, Col3 = col3 });
            }
        }

        Add("9.2.1  Timing", m.C218791, "h3");
        Add("9.2.2  Collection Procedures", null, "h3");
        Add("Identification", m.C218792, "h3");
        Add("Severity", m.C25676, "h3");
        Add("Causality", m.C82552, "h3");
        Add("Recording", m.C218793, "h3");
        Add("Follow-up", m.C218794, "h3");
        Add("9.2.3  Reporting", m.C218795, "h3");
        Add("Regulatory Reporting Requirements", m.C218796, "h3");
        Add("9.2.4  Adverse Events of Special Interest", m.C217358, "h3");
        Add("9.2.5  Disease-related Events or Outcomes Not Qualifying as AEs or SAEs", m.C218797, "h3");
        Add("9.3  Pregnancy and Postpartum Information", null);
        Add("9.3.1  Participants Who Become Pregnant During the Trial", m.C218798, "h3");
        Add("9.3.2  Participants Whose Partners Become Pregnant During the Trial", m.C218799, "h3");
        Add("9.4  Special Safety Situations", m.C218800);
        return blocks;
    }

    private static List<DocBlock> BuildSection10(Section10Model m)
    {
        var blocks = new List<DocBlock>();
        blocks.Add(new DocBlock { Type = "h1", Text = "10.  Statistical Considerations" });
        void Add(string heading, string? val, string level = "h2") { Emit(blocks, heading, val, level); }

        Add("10.  Statistical Considerations", m.C164387);
        Add("10.1  Analysis Sets", m.C218801);
        Add("10.2  Statistical Analyses", null);
        Add("Primary Efficacy Analysis", m.C218802, "h3");
        Add("Multiple Comparisons/Multiplicity", m.C218807, "h3");
        Add("Additional Analysis", m.C218808, "h3");
        Add("Safety Analysis", m.C218809, "h3");
        Add("10.3  Interim Analysis", m.C142582);
        Add("10.4  Sample Size", m.C218810);
        Add("10.5  Additional Statistical Considerations", m.C115467);
        return blocks;
    }

    private static List<DocBlock> BuildSection12(Section12Model m, List<AmendmentRecord> amendments)
    {
        var blocks = new List<DocBlock>();
        blocks.Add(new DocBlock { Type = "h1", Text = "12.  Appendix: Supporting Detail" });
        void Add(string heading, string? val, string level = "h2") { Emit(blocks, heading, val, level); }

        Add("12.1  Clinical Laboratory Tests", m.C25294);
        Add("12.2  Country/Region-Specific Differences", null);
        // Country/Region table — mirrors s12-tbl on page (3 columns):
        // Country/Region Identifier | Country/Region-Specific Requirements | Country/Region-Specific Protocol Clarifications
        if (m.CrRows > 0)
        {
            blocks.Add(new DocBlock { Type = "tableRow3h",
                Col1 = "Country/Region Identifier",
                Col2 = "Country/Region-Specific Requirements",
                Col3 = "Country/Region-Specific Protocol Clarifications" });
            for (int r = 1; r <= m.CrRows; r++)
            {
                var id  = m.CrData.GetValueOrDefault($"C218832_r{r}") ?? string.Empty;
                var req = m.CrData.GetValueOrDefault($"C218833_r{r}") ?? string.Empty;
                var cla = m.CrData.GetValueOrDefault($"C218834_r{r}") ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(id) || !string.IsNullOrWhiteSpace(req) || !string.IsNullOrWhiteSpace(cla))
                    blocks.Add(new DocBlock { Type = "tableRow3", Col1 = id, Col2 = req, Col3 = cla });
            }
        }

        // 12.3 Prior Protocol Amendment(s) — mirrors Section12.razor exactly
        blocks.Add(new DocBlock { Type = "h2", Text = "12.3  Prior Protocol Amendment(s)" });

        // If alternate location is specified, show only that field (page hides all detail tables)
        if (!string.IsNullOrWhiteSpace(m.C218836))
        {
            blocks.Add(new DocBlock { Type = "tableRow", Label = "Alternate Location for Overview of Changes:", Text = m.C218836 });
        }
        else if (amendments.Count == 0)
        {
            blocks.Add(new DocBlock { Type = "para", Text = "This protocol has not been amended." });
        }
        else
        {
            // Introductory paragraph — matches Section12.razor text exactly
            if (amendments.Count == 1)
                blocks.Add(new DocBlock { Type = "para", Text = "This is the first protocol amendment." });
            else
                blocks.Add(new DocBlock { Type = "para", Text = "This protocol has been amended previously. The Protocol Amendment Summary of Changes for the current amendment is located directly before the Table of Contents. Prior amendment(s) to this protocol are listed in the table below, beginning with the most recent." });

            // Summary table — mirrors s12-tbl: Document | Sponsor Approval Date | Approx. Enrollment when Approved | Enrollment Scope
            // Rows most recent first (reversed), Original Protocol always last
            var reversed = Enumerable.Reverse(amendments).ToList();
            blocks.Add(new DocBlock { Type = "tableRow3h",
                Col1 = "Document",
                Col2 = "Sponsor Approval Date / Approx. Enrollment when Approved",
                Col3 = "Enrollment Scope" });
            foreach (var a in reversed)
            {
                var scopeDisplay = a.C218695 switch {
                    string s when s.Contains("Globally") => "Globally",
                    string s when s.Contains("Locally")  => "Locally",
                    _ => a.C218695 ?? string.Empty
                };
                blocks.Add(new DocBlock { Type = "tableRow3",
                    Col1 = a.C218477 ?? a.Name,
                    Col2 = Join(a.C93813, a.C218478),
                    Col3 = scopeDisplay });
            }
            // Original Protocol row (always last in table)
            blocks.Add(new DocBlock { Type = "tableRow3",
                Col1 = "Original Protocol",
                Col2 = m.OrigProtocolDate ?? string.Empty,
                Col3 = string.Empty });

            // Overview of Changes — one h3 + change table per amendment, most recent first
            foreach (var a in reversed)
            {
                var aLabel = a.C218477 ?? a.Name;
                var heading = string.IsNullOrWhiteSpace(a.C93813)
                    ? $"Overview of Changes — {aLabel}"
                    : $"Overview of Changes — {aLabel} (Sponsor approval date: {a.C93813})";
                blocks.Add(new DocBlock { Type = "h3", Text = heading });

                // Overview of changes table — mirrors s12-tbl: Description | Rationale | Section
                if (a.ChgRows > 0)
                {
                    blocks.Add(new DocBlock { Type = "tableRow3h",
                        Col1 = "Description of Change", Col2 = "Brief Rationale for Change", Col3 = "Section # and Name" });
                    for (int r = 1; r <= a.ChgRows; r++)
                    {
                        var desc = a.ChgData.TryGetValue($"C218483_r{r}", out var d)  ? d  : string.Empty;
                        var rat  = a.ChgData.TryGetValue($"C181233_r{r}", out var rv) ? rv : string.Empty;
                        var sec  = a.ChgData.TryGetValue($"C218479_r{r}", out var sv) ? sv : string.Empty;
                        var secDisplay = System.Text.RegularExpressions.Regex.Match(sec, "\"([^\"]+)\"") is var mx && mx.Success ? mx.Groups[1].Value : sec;
                        blocks.Add(new DocBlock { Type = "tableRow3", Col1 = desc, Col2 = rat, Col3 = secDisplay });
                    }
                }
            }
        }

        return blocks;
    }

    private static List<DocBlock> BuildSection13(Section13Model m)
    {
        var blocks = new List<DocBlock>();
        blocks.Add(new DocBlock { Type = "h1", Text = "13.  Appendix: Glossary of Terms and Abbreviations" });
        blocks.Add(new DocBlock { Type = "h2", Text = "Glossary" });
        if (!string.IsNullOrWhiteSpace(m.C218837))
            blocks.Add(new DocBlock { Type = "para", Text = m.C218837 });
        return blocks;
    }

    private static List<DocBlock> BuildSection14(Section14Model m)
    {
        var blocks = new List<DocBlock>();
        blocks.Add(new DocBlock { Type = "h1", Text = "14.  Appendix: References" });
        blocks.Add(new DocBlock { Type = "h2", Text = "References" });
        if (!string.IsNullOrWhiteSpace(m.C184397))
            blocks.Add(new DocBlock { Type = "para", Text = m.C184397 });
        return blocks;
    }

    // ── Amendment Details builder — mirrors AmendmentDetails.razor ───────────────
    // Shows the full per-amendment data for all amendment records.
    private static List<DocBlock> BuildAmendmentDetails(List<AmendmentRecord> amendments)
    {
        var blocks = new List<DocBlock>();
        blocks.Add(new DocBlock { Type = "h1", Text = "Amendment Details" });

        void TR(string label, string? val)
        {
            if (string.IsNullOrWhiteSpace(val)) return;
            blocks.Add(new DocBlock { Type = "tableRow", Label = label, Text = val });
        }

        foreach (var a in amendments)
        {
            // Amendment name as h2 heading (matches the name-edit field shown as h1 on the page)
            blocks.Add(new DocBlock { Type = "h2", Text = a.Name });

            // Amendment Details table — mirrors AmendmentDetails.razor tp-tbl tabular layout
            TR("Amendment Status:",                      a.C218694);
            TR("Version Number:",                        a.C181232);
            TR("Version Date:",                          a.C93813);
            TR("Amendment Identifier:",                  a.C218477);
            TR("Amendment Scope:",                       a.C218673);
            TR("Country:",                               a.C20108);
            TR("Region:",                                a.C218674);
            TR("Site Identifier:",                       a.C83081);
            TR("Approx. Enrolled at Time of Sponsor Approval:", a.C218478);
            TR("Amendment Scope Enrollment Definition:", a.C218695);
            TR("Primary Reason for Amendment:",          Join(a.C218696, a.C17649));
            TR("Secondary Reason for Amendment:",        Join(a.C218697, a.C17649_2));
            TR("Amendment Summary:",                     a.C42581);
            TR("Substantial Impact on Safety or Rights?", Join(a.C218698, a.C218699));
            TR("Substantial Impact on Reliability and Robustness?", Join(a.C218700, a.C218701));

            // Overview of Changes table — mirrors chg-tbl: Description | Rationale | Section
            blocks.Add(new DocBlock { Type = "h3", Text = "Overview of Changes in the Current Amendment" });
            if (a.ChgRows > 0)
            {
                blocks.Add(new DocBlock { Type = "tableRow3h",
                    Col1 = "Description of Change", Col2 = "Brief Rationale for Change", Col3 = "Section # and Name" });
                for (int r = 1; r <= a.ChgRows; r++)
                {
                    var desc = a.ChgData.TryGetValue($"C218483_r{r}", out var d)  ? d  : string.Empty;
                    var rat  = a.ChgData.TryGetValue($"C181233_r{r}", out var rv) ? rv : string.Empty;
                    var sec  = a.ChgData.TryGetValue($"C218479_r{r}", out var sv) ? sv : string.Empty;
                    var secDisplay = System.Text.RegularExpressions.Regex.Match(sec, "\"([^\"]+)\"") is var mx && mx.Success ? mx.Groups[1].Value : sec;
                    blocks.Add(new DocBlock { Type = "tableRow3", Col1 = desc, Col2 = rat, Col3 = secDisplay });
                }
            }
        }

        return blocks;
    }

    // Helper — adds a heading block then para blocks for multi-line text
    // Joins two optional values with a newline when both are present.
    // Used to combine stacked sub-fields that share a table cell on the page.
    private static string Join(string? a, string? b)
    {
        var parts = new[] { a, b }.Where(s => !string.IsNullOrWhiteSpace(s));
        return string.Join("\n", parts);
    }

    private static void Emit(List<DocBlock> blocks, string heading, string? text, string level = "h2")
    {
        blocks.Add(new DocBlock { Type = level, Text = heading });
        if (!string.IsNullOrWhiteSpace(text))
        {
            foreach (var line in text.Split('\n'))
            {
                var t = line.TrimEnd();
                if (!string.IsNullOrWhiteSpace(t))
                    blocks.Add(new DocBlock { Type = "para", Text = t });
            }
        }
    }

    private static List<DocBlock> BuildSection11(Section11Model m)
    {
        var blocks = new List<DocBlock>();
        blocks.Add(new DocBlock { Type = "h1", Text = "11  TRIAL OVERSIGHT AND OTHER GENERAL CONSIDERATIONS" });

        void Add(string heading, string? text, string level = "h2")
        {
            blocks.Add(new DocBlock { Type = level, Text = heading });
            if (!string.IsNullOrWhiteSpace(text))
            {
                foreach (var para in text.Split('\n'))
                    if (!string.IsNullOrWhiteSpace(para))
                        blocks.Add(new DocBlock { Type = "para", Text = para.Trim() });
            }
        }

        Add("11.1  Regulatory and Ethical Considerations", m.C218811);
        Add("11.2  Trial Oversight", m.C218812);
        Add("11.2.1  Investigator Responsibilities", m.C218813, "h3");
        Add("11.2.2  Sponsor Responsibilities", m.C218814, "h3");
        Add("11.3  Informed Consent Process", null);
        Add("Informed Consent", m.C184390, "h3");
        if (!string.IsNullOrWhiteSpace(m.C218815)) Add("Assent", m.C218815, "h3");
        if (!string.IsNullOrWhiteSpace(m.C218816)) Add("Emergency Consent", m.C218816, "h3");
        if (!string.IsNullOrWhiteSpace(m.C218817)) Add("11.3.1  Informed Consent for Rescreening", m.C218817);
        if (!string.IsNullOrWhiteSpace(m.C218818)) Add("11.3.2  Informed Consent for Use of Remaining Samples", m.C218818);
        Add("11.4  Committees", m.C218819);
        Add("11.5  Insurance and Indemnity", m.C218820);
        Add("11.6  Risk-Based Quality Management", m.C218821);
        Add("11.7  Data Governance", m.C218822);
        Add("11.8  Data Protection", m.C218823);
        Add("11.9  Source Records", m.C218824);
        Add("Investigator Expectations", m.C218825, "h3");
        Add("Trial Monitor Expectations", m.C218826, "h3");
        Add("Record Identification", m.C222675, "h3");
        Add("11.10  Protocol Deviations", m.C218827);
        Add("11.11  Early Site Closure", null);
        Add("Decision Rights", m.C218828, "h3");
        Add("Investigator Criteria", m.C218829, "h3");
        Add("Responsibilities", m.C218830, "h3");
        Add("11.12  Data Dissemination", m.C218831);
        return blocks;
    }

    // ── Node.js runner ───────────────────────────────────────────────────────

    private (bool Ok, string? Error) CheckNpmPackages()
    {
        var docxModules = Path.Combine(_env.ContentRootPath, "Scripts", "node_modules", "docx");
        if (!Directory.Exists(docxModules))
            return (false, "The 'docx' npm package is not installed. Run 'npm install' in the App_v01/Scripts folder.");
        return (true, null);
    }

    private async Task<(byte[]? Bytes, string? Error)> RunNode(List<DocBlock> blocks)
    {
        var (ok, pkgErr) = CheckNpmPackages();
        if (!ok) return (null, pkgErr);

        var tempDir  = Path.Combine(Path.GetTempPath(), "worddoc_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        var scriptPath = Path.Combine(_env.ContentRootPath, "Scripts", "make_docx.js");
        var dataPath   = Path.Combine(tempDir, "blocks.json");
        var outPath    = Path.Combine(tempDir, "output.docx");

        try
        {
            await File.WriteAllTextAsync(dataPath, JsonSerializer.Serialize(blocks));

            var psi = new ProcessStartInfo("node", $"\"{scriptPath}\" \"{dataPath}\" \"{outPath}\"")
            {
                RedirectStandardOutput = true,
                RedirectStandardError  = true,
                UseShellExecute        = false,
                CreateNoWindow         = true
            };

            using var proc = Process.Start(psi)!;
            // Read stdout and stderr concurrently to avoid deadlock on full buffers
            var stdoutTask = proc.StandardOutput.ReadToEndAsync();
            var stderrTask = proc.StandardError.ReadToEndAsync();
            await proc.WaitForExitAsync();
            var stderr = await stderrTask;
            await stdoutTask;

            if (proc.ExitCode != 0)
                return (null, $"Node.js error: {stderr.Trim()}");

            if (!File.Exists(outPath))
                return (null, "Node.js script ran but output file not created.");

            var bytes = await File.ReadAllBytesAsync(outPath);
            return (bytes, null);
        }
        finally
        {
            try { Directory.Delete(tempDir, recursive: true); } catch { }
        }
    }

    // ── Types ────────────────────────────────────────────────────────────────

    private sealed class DocBlock
    {
        public string  Type  { get; set; } = "para"; // h1|h2|h3|label|para|pageBreak|tableRow|tableRow3|tableRow3h|tableRow4|tableRow4span
        public string? Text  { get; set; }            // para/label/h*/tableRow value/tableRow4span value
        // tableRow / tableRow4span: Label = left label, Text = right value
        public string? Label { get; set; }
        // tableRow3/tableRow3h: Col1/Col2/Col3
        // tableRow4: Label=lbl1, Text=val1, Col3=lbl2, Col4=val2
        public string? Col1  { get; set; }
        public string? Col2  { get; set; }
        public string? Col3  { get; set; }
        public string? Col4  { get; set; }
    }
}
