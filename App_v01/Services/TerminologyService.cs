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
            Id = "m11-yes-no-vs",
            CCode = "C217046",
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
            Id = "udp-target-Maximum-vs",
            CCode = "See value set notes",
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
            Id = "m11-blinded-roles-vs",
            CCode = "C217281",
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
            Id = "m11-independent-committees-vs",
            CCode = "C217282",
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
            Id = "m11-time-unit-vs",
            CCode = "C217048",
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
            Id = "m11-population-type-vs",
            CCode = "C217278",
            Name = "Population Type",
            Description = "Characterises the study population by disease status.",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C218503", Display = "With Disease"    },
                new() { System = "$NCIT", Code = "C218504", Display = "Without Disease" },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-phase-vs",
            CCode = "C217045",
            Name = "Trial Phase",
            Description = "ICH/NCIT coded phases of clinical investigation.",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C54721",  Display = "Early Phase 1"           },
                new() { System = "$NCIT", Code = "C15600",  Display = "Phase 1"                 },
                new() { System = "$NCIT", Code = "C15693",  Display = "Phase 1/Phase 2"         },
                new() { System = "$NCIT", Code = "C198366", Display = "Phase 1/Phase 2/Phase 3" },
                new() { System = "$NCIT", Code = "C198367", Display = "Phase 1/Phase 3"         },
                new() { System = "$NCIT", Code = "C15601",  Display = "Phase 2"                 },
                new() { System = "$NCIT", Code = "C15694",  Display = "Phase 2/Phase 3"         },
                new() { System = "$NCIT", Code = "C217024", Display = "Phase 2/Phase 3/Phase 4" },
                new() { System = "$NCIT", Code = "C15602",  Display = "Phase 3"                 },
                new() { System = "$NCIT", Code = "C217025", Display = "Phase 3/Phase 4"         },
                new() { System = "$NCIT", Code = "C15603",  Display = "Phase 4"                 },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-intervention-model-vs",
            CCode = "C217277",
            Name = "Trial Design",
            Description = "Overall allocation schema describing how participants are assigned to trial arms.",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C82640",  Display = "Single Group"   },
                new() { System = "$NCIT", Code = "C82639",  Display = "Parallel Group" },
                new() { System = "$NCIT", Code = "C82637",  Display = "Cross-over"     },
                new() { System = "$NCIT", Code = "C82638",  Display = "Factorial"      },
                new() { System = "$NCIT", Code = "C142568", Display = "Sequential"     },
                new() { System = "$NCIT", Code = "C17649",  Display = "Other"          },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-control-type-vs",
            CCode = "C217279",
            Name = "Control Type",
            Description = "Type of control used for the comparator arm.",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C49648",  Display = "Placebo"                    },
                new() { System = "$NCIT", Code = "C49649",  Display = "Active Comparator"          },
                new() { System = "$NCIT", Code = "C120841", Display = "Dose Response"              },
                new() { System = "$NCIT", Code = "C218505", Display = "Different Dose or Regimen"  },
                new() { System = "$NCIT", Code = "C218506", Display = "External"                   },
                new() { System = "$NCIT", Code = "C184727", Display = "Sham Procedure"             },
                new() { System = "$NCIT", Code = "C28280",  Display = "No Control"                 },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-trial-blind-schema-vs",
            CCode = "C217051",
            Name = "Trial Blind Schema",
            Description = "Extent of blinding applied to the trial.",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C15228",  Display = "Double Blind"    },
                new() { System = "$NCIT", Code = "C187674", Display = "Observer Blind"  },
                new() { System = "$NCIT", Code = "C49659",  Display = "Open Label"      },
                new() { System = "$NCIT", Code = "C28233",  Display = "Single Blind"    },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-intervention-assignment-method-vs",
            CCode = "C217280",
            Name = "Intervention Assignment Method",
            Description = "Method by which participants are assigned to trial interventions.",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C25196",  Display = "Randomisation"                       },
                new() { System = "$NCIT", Code = "C222801", Display = "No Intervention Assignment Method"    },
                new() { System = "$NCIT", Code = "C17649",  Display = "Other"                               },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-site-distribution-vs",
            CCode = "C217049",
            Name = "Site Distribution",
            Description = "Whether the trial is conducted at a single centre or multiple centres.",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C217004", Display = "Single-Centre" },
                new() { System = "$NCIT", Code = "C217005", Display = "Multicentre"   },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-site-geographic-scope-vs",
            CCode = "C217050",
            Name = "Country Scope",
            Description = "Whether the trial takes place in a single country or multiple countries.",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C217006", Display = "Single Country"     },
                new() { System = "$NCIT", Code = "C217007", Display = "Multiple Countries" },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-amendment-status-vs",
            CCode = "C217274",
            Name = "Amendment Status",
            Description = "Indicates whether this protocol is original, a first amendment, or a subsequent amendment.",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C218485", Display = "This protocol has not been amended."                                                                                                                             },
                new() { System = "$NCIT", Code = "C218486", Display = "This is the first protocol amendment."                                                                                                                          },
                new() { System = "$NCIT", Code = "C218487", Display = "This protocol has been amended previously. Details of prior amendments are presented in Section 12.3 Prior Protocol Amendment(s)." },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-study-amendment-scope-vs",
            CCode = "C217047",
            Name = "Amendment Scope",
            Description = "Whether the amendment scope applies globally or is limited to a specific country. C48660 \"Not Applicable\" has been added because the instruction to \"leave blank\" must be replaced by an explicit value in FHIR.",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C68846",  Display = "Global"         },
                new() { System = "$NCIT", Code = "C217026", Display = "Not Global"      },
                new() { System = "$NCIT", Code = "C48660",  Display = "Not Applicable"  },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-amendment-scope-enrollment-vs",
            CCode = "C217275",
            Name = "Enrollment Scope",
            Description = "Geographic scope of the enrollment impact of the amendment.",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C68846",  Display = "Globally"   },
                new() { System = "$NCIT", Code = "C41065",  Display = "Locally"    },
                new() { System = "$NCIT", Code = "C218489", Display = "By Cohort"  },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-protocol-section-vs",
            CCode = "C217272",
            Name = "Protocol Section",
            Description = "ICH M11 protocol sections referenced in the amendment change log.",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C217342", Display = "Section 1"                  },
                new() { System = "$NCIT", Code = "C217343", Display = "Section 2"                  },
                new() { System = "$NCIT", Code = "C217344", Display = "Section 3"                  },
                new() { System = "$NCIT", Code = "C217345", Display = "Section 4"                  },
                new() { System = "$NCIT", Code = "C217346", Display = "Section 5"                  },
                new() { System = "$NCIT", Code = "C217347", Display = "Section 6"                  },
                new() { System = "$NCIT", Code = "C217348", Display = "Section 7"                  },
                new() { System = "$NCIT", Code = "C217349", Display = "Section 8"                  },
                new() { System = "$NCIT", Code = "C217350", Display = "Section 9"                  },
                new() { System = "$NCIT", Code = "C217351", Display = "Section 10"                 },
                new() { System = "$NCIT", Code = "C217352", Display = "Section 11"                 },
                new() { System = "$NCIT", Code = "C217353", Display = "Section 12"                 },
                new() { System = "$NCIT", Code = "C217354", Display = "Section 13"                 },
                new() { System = "$NCIT", Code = "C217355", Display = "Section 14"                 },
                new() { System = "$NCIT", Code = "C217356", Display = "Section Title Page"         },
                new() { System = "$NCIT", Code = "C217357", Display = "Section Amendment Details"  },
            }
        });

        Add(new ValueSet
        {
            Id = "udp-country-vs",
            CCode = "See value set notes",
            Name = "Country",
            Description = "ISO 3166 country codes for sponsor country of record (placeholder list — extend as needed).",
            Options = new List<PicklistOption>
            {
                new() { System = "$iso3166", Code = "AU", Display = "Australia" },
                new() { System = "$iso3166", Code = "FR", Display = "France"    },
                new() { System = "$iso3166", Code = "DE", Display = "Germany"   },
                new() { System = "$iso3166", Code = "IN", Display = "India"     },
                new() { System = "$iso3166", Code = "GB", Display = "UK"        },
            }
        });

        Add(new ValueSet
        {
            Id = "udp-region-vs",
            CCode = "See value set notes",
            Name = "Region",
            Description = "ISO 3166-2 region/subdivision codes (placeholder list — extend as needed).",
            Options = new List<PicklistOption>
            {
                new() { System = "$iso3166-2", Code = "AU-NSW", Display = "New South Wales" },
                new() { System = "$iso3166-2", Code = "DE-BY",  Display = "Bavaria"         },
                new() { System = "$iso3166-2", Code = "FR-NOR", Display = "Normandy"        },
                new() { System = "$iso3166-2", Code = "IN-UP",  Display = "Uttar Pradesh"   },
                new() { System = "$iso3166-2", Code = "GB-WLS", Display = "Wales"           },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-study-amendment-reason-vs",
            CCode = "C217276",
            Name = "Amendment Reason",
            Description = "Reasons for protocol amendment (ICH M11 C218696 / C218697).",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C218490", Display = "Regulatory Agency Request To Amend" },
                new() { System = "$NCIT", Code = "C218491", Display = "New Regulatory Guidance" },
                new() { System = "$NCIT", Code = "C218492", Display = "IRB/IEC Feedback" },
                new() { System = "$NCIT", Code = "C218493", Display = "New Safety Information Available" },
                new() { System = "$NCIT", Code = "C218494", Display = "Manufacturing Change" },
                new() { System = "$NCIT", Code = "C218495", Display = "IMP Addition" },
                new() { System = "$NCIT", Code = "C218496", Display = "Change In Strategy" },
                new() { System = "$NCIT", Code = "C218497", Display = "Change In Standard Of Care" },
                new() { System = "$NCIT", Code = "C218498", Display = "New Data Available (Other Than Safety Data)" },
                new() { System = "$NCIT", Code = "C218499", Display = "Investigator/Site Feedback" },
                new() { System = "$NCIT", Code = "C218500", Display = "Recruitment Difficulty" },
                new() { System = "$NCIT", Code = "C218501", Display = "Inconsistency And/Or Error In The Protocol" },
                new() { System = "$NCIT", Code = "C218502", Display = "Protocol Design Error" },
                new() { System = "$NCIT", Code = "C17649",  Display = "Other" },
                new() { System = "$NCIT", Code = "C48660",  Display = "Not Applicable" },
            }
        });

        // ── Section 06 — Trial Intervention table column value sets ──────────

        Add(new ValueSet
        {
            Id = "m11-arm-type-vs",
            CCode = "C217283",
            Name = "Arm Type",
            Description = "Type of trial arm (ICH M11 C172457).",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C174267", Display = "Active Comparator Arm" },
                new() { System = "$NCIT", Code = "C174226", Display = "Control Arm" },
                new() { System = "$NCIT", Code = "C174266", Display = "Experimental Arm" },
                new() { System = "$NCIT", Code = "C174270", Display = "No Intervention Arm" },
                new() { System = "$NCIT", Code = "C174268", Display = "Placebo Comparator Arm" },
                new() { System = "$NCIT", Code = "C174269", Display = "Sham Comparator Arm" },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-intervention-type-vs",
            CCode = "C217284",
            Name = "Intervention Type",
            Description = "Type of trial intervention (ICH M11 C98747).",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C1909",   Display = "Drug" },
                new() { System = "$NCIT", Code = "C16830",  Display = "Medical Device" },
                new() { System = "$NCIT", Code = "C307",    Display = "Biologic" },
                new() { System = "$NCIT", Code = "C923",    Display = "Vaccine" },
                new() { System = "$NCIT", Code = "C218507", Display = "Non Surgical Procedure" },
                new() { System = "$NCIT", Code = "C15329",  Display = "Surgery" },
                new() { System = "$NCIT", Code = "C15313",  Display = "Radiation" },
                new() { System = "$NCIT", Code = "C15184",  Display = "Behavioral" },
                new() { System = "$NCIT", Code = "C15238",  Display = "Genetic" },
                new() { System = "$NCIT", Code = "C1505",   Display = "Dietary Supplement" },
                new() { System = "$NCIT", Code = "C54696",  Display = "Combination Product" },
                new() { System = "$NCIT", Code = "C18020",  Display = "Diagnostic" },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-use-code-vs",
            CCode = "C217285",
            Name = "Intervention Role",
            Description = "Role of the intervention within the trial (ICH M11 C218748).",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C41161",  Display = "Experimental Intervention" },
                new() { System = "$NCIT", Code = "C753",    Display = "Placebo" },
                new() { System = "$NCIT", Code = "C165835", Display = "Rescue Medicine" },
                new() { System = "$NCIT", Code = "C165822", Display = "Background Treatment" },
                new() { System = "$NCIT", Code = "C158128", Display = "Challenge Agent" },
                new() { System = "$NCIT", Code = "C18020",  Display = "Diagnostic" },
                new() { System = "$NCIT", Code = "C207614", Display = "Additional Required Treatment" },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-imp-nimp-type-vs",
            CCode = "C217286",
            Name = "IMP/NIMP Classification",
            Description = "Investigational Medicinal Product vs Non-Investigational Medicinal Product (ICH M11 C218749).",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C202579", Display = "IMP" },
                new() { System = "$NCIT", Code = "C156473", Display = "NIMP" },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-source-code-vs",
            CCode = "C217052",
            Name = "Sourcing",
            Description = "How the intervention product is sourced (ICH M11 C218750).",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C215659", Display = "Centrally Sourced" },
                new() { System = "$NCIT", Code = "C215660", Display = "Locally Sourced" },
            }
        });

        // ── Section 09 — Safety reporting event type ──────────────────────────

        Add(new ValueSet
        {
            Id = "m11-event-type-vs",
            CCode = "C217287",
            Name = "Safety Event Type",
            Description = "Type of reportable safety event (ICH M11 C218784).",
            Options = new List<PicklistOption>
            {
                new() { System = "$NCIT", Code = "C41331",  Display = "Adverse Event" },
                new() { System = "$NCIT", Code = "C41335",  Display = "Serious Adverse Event" },
                new() { System = "$NCIT", Code = "C218508", Display = "Trial Intervention Complaint" },
                new() { System = "$NCIT", Code = "C222331", Display = "Drug/Device Combination Product Complaint" },
                new() { System = "$NCIT", Code = "C25742",  Display = "Pregnancy Event" },
                new() { System = "$NCIT", Code = "C218510", Display = "Lactation Event" },
                new() { System = "$NCIT", Code = "C218511", Display = "Post-Partum Event" },
                new() { System = "$NCIT", Code = "C218512", Display = "Reportable Adverse Event of Special Interest" },
                new() { System = "$NCIT", Code = "C218513", Display = "Not Reportable Adverse Event of Special Interest" },
                new() { System = "$NCIT", Code = "C17649",  Display = "Other" },
            }
        });

        // ── Section 06 — Dose form and route (IDMP/EDQM placeholder value sets) ─

        Add(new ValueSet
        {
            Id = "m11-dose-form-vs",
            CCode = "See value set notes",
            Name = "Pharmaceutical Dose Form",
            Description = "Pharmaceutical dose form of the investigational product (EDQM/IDMP C42636).",
            Options = new List<PicklistOption>
            {
                new() { System = "$IDMP_DUMMY", Code = "f1", Display = "tablet" },
                new() { System = "$IDMP_DUMMY", Code = "f2", Display = "capsule" },
                new() { System = "$IDMP_DUMMY", Code = "f3", Display = "solution for injection" },
            }
        });

        Add(new ValueSet
        {
            Id = "m11-route-vs",
            CCode = "See value set notes",
            Name = "Route of Administration",
            Description = "Route of administration of the investigational product (EDQM/IDMP C38114).",
            Options = new List<PicklistOption>
            {
                new() { System = "$IDMP_DUMMY", Code = "R1", Display = "oral" },
                new() { System = "$IDMP_DUMMY", Code = "R2", Display = "subcutaneous injection" },
                new() { System = "$IDMP_DUMMY", Code = "R3", Display = "topical" },
            }
        });

        // ── udp-condition-vs ─────────────────────────────────────────────────────
        Add(new ValueSet
        {
            Id = "udp-condition-vs",
            CCode = "C112038",
            Name = "Population Diagnosis or Condition",
            Description = "SNOMED CT and MedDRA Preferred Term codes for the population diagnosis or condition. Starter set — extend with codes appropriate to the specific trial.",
            Options = new List<PicklistOption>
            {
                // --- SNOMED CT ---
                // Oncology
                new() { System = "$SCT", Code = "363346000", Display = "Malignant neoplastic disease (SCT)" },
                new() { System = "$SCT", Code = "254837009", Display = "Malignant neoplasm of breast (SCT)" },
                new() { System = "$SCT", Code = "93880001",  Display = "Primary malignant neoplasm of lung (SCT)" },
                new() { System = "$SCT", Code = "363418001", Display = "Malignant tumour of pancreas (SCT)" },
                new() { System = "$SCT", Code = "363449006", Display = "Malignant tumour of prostate (SCT)" },
                new() { System = "$SCT", Code = "94381002",  Display = "Non-Hodgkin lymphoma (SCT)" },
                // Cardiovascular
                new() { System = "$SCT", Code = "53741008",  Display = "Coronary arteriosclerosis (SCT)" },
                new() { System = "$SCT", Code = "44054006",  Display = "Diabetes mellitus type 2 (SCT)" },
                new() { System = "$SCT", Code = "38341003",  Display = "Hypertensive disorder (SCT)" },
                new() { System = "$SCT", Code = "84114007",  Display = "Heart failure (SCT)" },
                new() { System = "$SCT", Code = "49436004",  Display = "Atrial fibrillation (SCT)" },
                // Respiratory
                new() { System = "$SCT", Code = "195967001", Display = "Asthma (SCT)" },
                new() { System = "$SCT", Code = "13645005",  Display = "Chronic obstructive lung disease (SCT)" },
                // Neurological / Psychiatric
                new() { System = "$SCT", Code = "49049000",  Display = "Parkinson disease (SCT)" },
                new() { System = "$SCT", Code = "26929004",  Display = "Alzheimer disease (SCT)" },
                new() { System = "$SCT", Code = "35489007",  Display = "Depressive disorder (SCT)" },
                new() { System = "$SCT", Code = "69322001",  Display = "Psychotic disorder (SCT)" },
                // Inflammatory / Autoimmune
                new() { System = "$SCT", Code = "9014002",   Display = "Psoriasis (SCT)" },
                new() { System = "$SCT", Code = "56246009",  Display = "Plaque psoriasis (SCT)" },
                new() { System = "$SCT", Code = "69896004",  Display = "Rheumatoid arthritis (SCT)" },
                new() { System = "$SCT", Code = "24526004",  Display = "Inflammatory bowel disease (SCT)" },
                new() { System = "$SCT", Code = "34000006",  Display = "Crohn disease (SCT)" },
                new() { System = "$SCT", Code = "64766004",  Display = "Ulcerative colitis (SCT)" },
                // Infectious Disease
                new() { System = "$SCT", Code = "86406008",  Display = "Human immunodeficiency virus infection (SCT)" },
                new() { System = "$SCT", Code = "40122008",  Display = "Pneumonia (SCT)" },

                // --- MedDRA ---
                // Oncology
                new() { System = "$MedDRA", Code = "10028997", Display = "Neoplasm malignant (MedDRA)" },
                new() { System = "$MedDRA", Code = "10006187", Display = "Breast cancer (MedDRA)" },
                new() { System = "$MedDRA", Code = "10025040", Display = "Lung neoplasm malignant (MedDRA)" },
                new() { System = "$MedDRA", Code = "10033605", Display = "Pancreatic carcinoma (MedDRA)" },
                new() { System = "$MedDRA", Code = "10060862", Display = "Prostate cancer (MedDRA)" },
                new() { System = "$MedDRA", Code = "10029547", Display = "Non-Hodgkin's lymphoma (MedDRA)" },
                // Cardiovascular
                new() { System = "$MedDRA", Code = "10011078", Display = "Coronary artery disease (MedDRA)" },
                new() { System = "$MedDRA", Code = "10012601", Display = "Diabetes mellitus type 2 (MedDRA)" },
                new() { System = "$MedDRA", Code = "10020772", Display = "Hypertension (MedDRA)" },
                new() { System = "$MedDRA", Code = "10019280", Display = "Heart failure (MedDRA)" },
                new() { System = "$MedDRA", Code = "10003658", Display = "Atrial fibrillation (MedDRA)" },
                // Respiratory
                new() { System = "$MedDRA", Code = "10003553", Display = "Asthma (MedDRA)" },
                new() { System = "$MedDRA", Code = "10009033", Display = "Chronic obstructive pulmonary disease (MedDRA)" },
                // Neurological / Psychiatric
                new() { System = "$MedDRA", Code = "10061536", Display = "Parkinson's disease (MedDRA)" },
                new() { System = "$MedDRA", Code = "10001896", Display = "Alzheimer's disease (MedDRA)" },
                new() { System = "$MedDRA", Code = "10012378", Display = "Depression (MedDRA)" },
                new() { System = "$MedDRA", Code = "10037175", Display = "Psychotic disorder (MedDRA)" },
                // Inflammatory / Autoimmune
                new() { System = "$MedDRA", Code = "10037153", Display = "Psoriasis (MedDRA)" },
                new() { System = "$MedDRA", Code = "10034461", Display = "Plaque psoriasis (MedDRA)" },
                new() { System = "$MedDRA", Code = "10039073", Display = "Rheumatoid arthritis (MedDRA)" },
                new() { System = "$MedDRA", Code = "10021972", Display = "Inflammatory bowel disease (MedDRA)" },
                new() { System = "$MedDRA", Code = "10011401", Display = "Crohn's disease (MedDRA)" },
                new() { System = "$MedDRA", Code = "10045365", Display = "Ulcerative colitis (MedDRA)" },
                // Infectious Disease
                new() { System = "$MedDRA", Code = "10020161", Display = "HIV infection (MedDRA)" },
                new() { System = "$MedDRA", Code = "10035664", Display = "Pneumonia (MedDRA)" },
            }
        });

        // ── Additional value sets will be added here as the app grows ─────────
    }

    private void Add(ValueSet vs) => _valueSets[vs.Id] = vs;
}
