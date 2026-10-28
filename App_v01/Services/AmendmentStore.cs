using System.Text.Json;
using UDP_App.Models;

namespace UDP_App.Services;

/// <summary>
/// Scoped service: one instance per Blazor circuit (browser tab).
/// Persists multiple amendment sets as a JSON file within the session's workspace.
///
/// Storage path: {SessionWorkspace.SessionRoot}/amendments.json
/// </summary>
public class AmendmentStore
{
    private readonly string _filePath;
    private readonly ILogger<AmendmentStore> _logger;
    private List<AmendmentRecord> _records = new();

    private static readonly JsonSerializerOptions _jsonOpts = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public AmendmentStore(SessionWorkspace workspace, ILogger<AmendmentStore> logger)
    {
        _logger   = logger;
        _filePath = Path.Combine(
            Path.GetDirectoryName(workspace.ProjectRoot)!,
            "amendments.json");

        Load();
    }

    // ── Public API ────────────────────────────────────────────────────

    /// <summary>All amendment sets for this session, ordered by creation time.</summary>
    public IReadOnlyList<AmendmentRecord> All => _records.AsReadOnly();

    /// <summary>Add a new empty amendment set with the given name.</summary>
    public AmendmentRecord Add(string name = "New Amendment")
    {
        var rec = new AmendmentRecord { Name = name.Trim().Length > 0 ? name.Trim() : "New Amendment" };
        _records.Add(rec);
        Save();
        return rec;
    }

    /// <summary>Save changes to an existing record (matched by Id).</summary>
    public void Update(AmendmentRecord record)
    {
        var idx = _records.FindIndex(r => r.Id == record.Id);
        if (idx < 0) return;
        record.UpdatedAt = DateTime.UtcNow;
        _records[idx] = record;
        Save();
    }

    /// <summary>Delete a record by Id. Returns true if found and removed.</summary>
    public bool Delete(string id)
    {
        var removed = _records.RemoveAll(r => r.Id == id) > 0;
        if (removed) Save();
        return removed;
    }

    /// <summary>Find a record by Id.</summary>
    public AmendmentRecord? GetById(string id) =>
        _records.FirstOrDefault(r => r.Id == id);

    /// <summary>
    /// Creates two sample amendment sets for demonstration purposes.
    /// Returns the first of the two created records.
    /// </summary>
    public AmendmentRecord LoadSample()
    {
        var a1 = new AmendmentRecord
        {
            Name     = "Amendment 1 — Safety Update",
            C218694  = "$NCIT#C218486 \"This is the first protocol amendment.\"",
            C181232  = "2.0",
            C93813   = "2025-03-14",
            C218477  = "AMD-001",
            C218673  = "$NCIT#C68846 \"Global\"",
            C218478  = "120",
            C218695  = "$NCIT#C68846 \"Globally\"",
            C218696  = "$NCIT#C218493 \"New Safety Information Available\"",
            C42581   = "Amendment to update inclusion/exclusion criteria following a new safety signal identified in interim data review.",
            C218698  = "$NCIT#C49488 \"Yes\"",
            C218699  = "New laboratory finding requires additional monitoring visits for enrolled participants.",
            C218700  = "$NCIT#C49487 \"No\"",
            ChgRows  = 2,
            ChgData  = new Dictionary<string, string>
            {
                ["C218483_r1"] = "Updated exclusion criterion 4 to exclude patients with elevated liver enzymes (>3× ULN).",
                ["C181233_r1"] = "New safety signal identified in interim data review.",
                ["C218479_r1"] = "$NCIT#C217345 \"Section 4\"",
                ["C218483_r2"] = "Added additional safety monitoring visit at Week 12.",
                ["C181233_r2"] = "Required by updated safety monitoring plan.",
                ["C218479_r2"] = "$NCIT#C217346 \"Section 5\"",
            }
        };

        var a2 = new AmendmentRecord
        {
            Name     = "Amendment 2 — Recruitment Change",
            C218694  = "$NCIT#C218487 \"This protocol has been amended previously. Details of prior amendments are presented in Section 12.3 Prior Protocol Amendment(s).\"",
            C181232  = "3.0",
            C93813   = "2025-09-01",
            C218477  = "AMD-002",
            C218673  = "$NCIT#C217026 \"Not Global\"",
            C20108   = "$iso3166#GB \"UK\"",
            C218478  = "187",
            C218695  = "$NCIT#C41065 \"Locally\"",
            C218696  = "$NCIT#C218500 \"Recruitment Difficulty\"",
            C218697  = "$NCIT#C218496 \"Change In Strategy\"",
            C42581   = "Expansion of eligible sites and relaxation of age criterion to address below-target recruitment.",
            C218698  = "$NCIT#C49487 \"No\"",
            C218700  = "$NCIT#C49487 \"No\"",
            ChgRows  = 1,
            ChgData  = new Dictionary<string, string>
            {
                ["C218483_r1"] = "Widened age eligibility from 18–65 to 18–75 years.",
                ["C181233_r1"] = "Recruitment modelling indicates insufficient eligible participants under current criterion.",
                ["C218479_r1"] = "$NCIT#C217344 \"Section 3\"",
            }
        };

        _records.Add(a1);
        _records.Add(a2);
        Save();
        return a1;
    }

    // ── Persistence ───────────────────────────────────────────────────

    private void Load()
    {
        try
        {
            if (!File.Exists(_filePath)) return;
            var json = File.ReadAllText(_filePath);
            _records = JsonSerializer.Deserialize<List<AmendmentRecord>>(json, _jsonOpts) ?? new();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not load amendments from {Path}", _filePath);
            _records = new();
        }
    }

    private void Save()
    {
        try
        {
            var json = JsonSerializer.Serialize(_records, _jsonOpts);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not save amendments to {Path}", _filePath);
        }
    }
}
