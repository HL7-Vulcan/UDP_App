using System.Text.Json;
using UDP_App.Models;

namespace UDP_App.Services;

/// <summary>
/// Singleton service holding all picklist (value set) definitions.
/// Value sets are seeded from defaults and can be edited at runtime.
/// Persistence via browser localStorage is handled at the Blazor layer.
/// </summary>
public class TerminologyService
{
    private readonly Dictionary<string, ValueSet> _valueSets = new();

    public TerminologyService()
    {
        Seed();
    }

    // ── Public API ────────────────────────────────────────────────────────────

    /// <summary>All value sets, ordered by ID.</summary>
    public IReadOnlyList<ValueSet> AllValueSets =>
        _valueSets.Values.OrderBy(v => v.Id).ToList();

    /// <summary>Returns the value set for the given ID, or null if not found.</summary>
    public ValueSet? Get(string id) =>
        _valueSets.TryGetValue(id, out var vs) ? vs : null;

    /// <summary>Returns options for a value set, or empty if not found.</summary>
    public IReadOnlyList<PicklistOption> OptionsFor(string valueSetId)
    {
        var vs = Get(valueSetId);
        return vs is not null ? vs.Options : new List<PicklistOption>();
    }

    /// <summary>Adds or replaces a value set.</summary>
    public void Upsert(ValueSet vs)
    {
        _valueSets[vs.Id] = vs;
    }

    /// <summary>Adds an option to an existing value set.</summary>
    public void AddOption(string valueSetId, PicklistOption option)
    {
        if (_valueSets.TryGetValue(valueSetId, out var vs))
            vs.Options.Add(option);
    }

    /// <summary>Removes an option by code from a value set.</summary>
    public void RemoveOption(string valueSetId, string code)
    {
        if (_valueSets.TryGetValue(valueSetId, out var vs))
            vs.Options.RemoveAll(o => o.Code == code);
    }

    /// <summary>Moves an option up or down within a value set.</summary>
    public void MoveOption(string valueSetId, string code, int delta)
    {
        if (!_valueSets.TryGetValue(valueSetId, out var vs)) return;
        var idx = vs.Options.FindIndex(o => o.Code == code);
        if (idx < 0) return;
        var newIdx = Math.Clamp(idx + delta, 0, vs.Options.Count - 1);
        if (newIdx == idx) return;
        var item = vs.Options[idx];
        vs.Options.RemoveAt(idx);
        vs.Options.Insert(newIdx, item);
    }

    /// <summary>Serialises all value sets to JSON for persistence.</summary>
    public string ToJson() =>
        JsonSerializer.Serialize(_valueSets.Values.ToList(),
            new JsonSerializerOptions { WriteIndented = true });

    /// <summary>Restores value sets from JSON (replaces defaults with saved data).</summary>
    public void FromJson(string json)
    {
        try
        {
            var list = JsonSerializer.Deserialize<List<ValueSet>>(json);
            if (list == null) return;
            _valueSets.Clear();
            foreach (var vs in list) _valueSets[vs.Id] = vs;
        }
        catch { /* leave existing data intact on parse error */ }
    }

    // ── Seed data ─────────────────────────────────────────────────────────────

    private void Seed()
    {
        Add(new ValueSet
        {
            Id = "C217046",
            Name = "Yes/No Indicator",
            Description = "General-purpose Yes / No indicator used for boolean trial design fields.",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C49488", Display = "Yes" },
                new() { System = "$NCIT", Code = "C49487", Display = "No"  },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-target-Maximum-vs",
            Name = "Target / Maximum",
            Description = "Indicates whether a participant count is a target or a maximum.",
            Options = new List<PicklistOption>
            {
                new() { System = "$UDP_Target", Code = "1", Display = "Target"  },
                new() { System = "$UDP_Target", Code = "2", Display = "Maximum" },
            }
        });

        Add(new ValueSet
        {
            Id = "C217281",
            Name = "Blinded Roles",
            Description = "Roles that will not be made aware of the treatment group assignment.",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C142710", Display = "Participant"        },
                new() { System = "$NCIT", Code = "C17445",  Display = "Care Provider"      },
                new() { System = "$NCIT", Code = "C25936",  Display = "Investigator"       },
                new() { System = "$NCIT", Code = "C207599", Display = "Outcomes Assessor"  },
                new() { System = "$NCIT", Code = "C70793",  Display = "Sponsor"            },
                new() { System = "$NCIT", Code = "C48660",  Display = "Not Applicable"     },
            }
        });

        Add(new ValueSet
        {
            Id = "C217282",
            Name = "Independent Committees",
            Description = "Independent oversight committees involved in the trial.",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C142578", Display = "Independent Data Monitoring Committee" },
                new() { System = "$NCIT", Code = "C215671", Display = "Dose Escalation Committee"            },
                new() { System = "$NCIT", Code = "C78726",  Display = "Endpoint Adjudication Committee"      },
                new() { System = "$NCIT", Code = "C17649",  Display = "Other"                                },
                new() { System = "$NCIT", Code = "C41132",  Display = "None"                                 },
            }
        });

        Add(new ValueSet
        {
            Id = "C217048",
            Name = "Time Unit",
            Description = "Units of time used for age and duration quantities.",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C25529", Display = "Hours"  },
                new() { System = "$NCIT", Code = "C25301", Display = "Days"   },
                new() { System = "$NCIT", Code = "C29844", Display = "Weeks"  },
                new() { System = "$NCIT", Code = "C29846", Display = "Months" },
                new() { System = "$NCIT", Code = "C29848", Display = "Years"  },
            }
        });

        Add(new ValueSet
        {
            Id = "C217278",
            Name = "Population Type",
            Description = "Characterises the study population by disease status.",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C218503", Display = "With Disease"    },
                new() { System = "$NCIT", Code = "C218504", Display = "Without Disease" },
            }
        });

        // ── Additional value sets will be added here as the app grows ─────────
    }

    private void Add(ValueSet vs) => _valueSets[vs.Id] = vs;
}
