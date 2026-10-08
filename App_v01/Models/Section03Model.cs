using System.Text.Json.Serialization;

namespace UDP_App.Models;

// ─── Intercurrent Event row ───────────────────────────────────────────────────
public class IceRow
{
    [JsonPropertyName("C188856")]
    public string? C188856 { get; set; }  // Intercurrent Event

    [JsonPropertyName("C188857")]
    public string? C188857 { get; set; }  // Strategy
}

// ─── One objective block (primary / secondary / exploratory) ──────────────────
public class ObjectiveBlock
{
    // Objective description — code differs per type, stored generically
    [JsonPropertyName("objectiveText")]
    public string? ObjectiveText { get; set; }

    // Estimand fields (shared across all types)
    [JsonPropertyName("C70833")]
    public string? C70833 { get; set; }   // Population

    [JsonPropertyName("C49236")]
    public string? C49236 { get; set; }   // Treatment

    [JsonPropertyName("C25212")]
    public string? C25212 { get; set; }   // Endpoint

    [JsonPropertyName("C188853")]
    public string? C188853 { get; set; }  // Population-level summary

    // ICE rows (repeating)
    [JsonPropertyName("iceRows")]
    public List<IceRow> IceRows { get; set; } = new() { new IceRow() };
}

// ─── Top-level Section 03 model ───────────────────────────────────────────────
public class Section03Model
{
    [JsonPropertyName("primaryObjectives")]
    public List<ObjectiveBlock> PrimaryObjectives { get; set; } = new() { new ObjectiveBlock() };

    [JsonPropertyName("secondaryObjectives")]
    public List<ObjectiveBlock> SecondaryObjectives { get; set; } = new() { new ObjectiveBlock() };

    [JsonPropertyName("exploratoryObjectives")]
    public List<ObjectiveBlock> ExploratoryObjectives { get; set; } = new() { new ObjectiveBlock() };

    public bool IsEmpty() =>
        PrimaryObjectives.All(IsBlockEmpty) &&
        SecondaryObjectives.All(IsBlockEmpty) &&
        ExploratoryObjectives.All(IsBlockEmpty);

    private static bool IsBlockEmpty(ObjectiveBlock b) =>
        string.IsNullOrWhiteSpace(b.ObjectiveText) &&
        string.IsNullOrWhiteSpace(b.C70833) &&
        string.IsNullOrWhiteSpace(b.C49236) &&
        string.IsNullOrWhiteSpace(b.C25212) &&
        string.IsNullOrWhiteSpace(b.C188853) &&
        b.IceRows.All(r => string.IsNullOrWhiteSpace(r.C188856) && string.IsNullOrWhiteSpace(r.C188857));
}
