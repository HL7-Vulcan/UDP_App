using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
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
    private readonly string _samplePath;
    private readonly ILogger<AmendmentStore> _logger;
    private List<AmendmentRecord> _records = new();

    private static readonly JsonSerializerOptions _jsonOpts = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    public AmendmentStore(SessionWorkspace workspace, IWebHostEnvironment env, ILogger<AmendmentStore> logger)
    {
        _logger     = logger;
        _filePath   = Path.Combine(
            Path.GetDirectoryName(workspace.ProjectRoot)!,
            "amendments.json");
        _samplePath = Path.Combine(env.ContentRootPath, "Data", "amendment_sample.json");

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
    /// Loads sample amendment sets from Data/amendment_sample.json.
    /// Returns (firstRecord, null) on success, or (null, errorMessage) on failure.
    /// </summary>
    public (AmendmentRecord? First, string? Error) LoadSample()
    {
        if (!File.Exists(_samplePath))
            return (null, "Sample data file not found: Data/amendment_sample.json");

        try
        {
            var json    = File.ReadAllText(_samplePath);
            var samples = JsonSerializer.Deserialize<List<AmendmentRecord>>(json, _jsonOpts);
            if (samples == null || samples.Count == 0)
                return (null, "Sample data file is empty or invalid.");

            foreach (var rec in samples)
            {
                // Assign fresh Ids and timestamps so they behave like new records
                rec.Id        = Guid.NewGuid().ToString();
                rec.CreatedAt = DateTime.UtcNow;
                rec.UpdatedAt = DateTime.UtcNow;
                _records.Add(rec);
            }

            Save();
            return (_records[^samples.Count], null);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not load amendment sample from {Path}", _samplePath);
            return (null, "Could not read sample data file: " + ex.Message);
        }
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
