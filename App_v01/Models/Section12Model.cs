using System.Text.Json.Serialization;

namespace UDP_App.Models;

public class Section12Model
{
    // 12.1 Clinical Laboratory Tests
    [JsonPropertyName("C25294")]   public string? C25294   { get; set; }  // Clinical Laboratory Tests

    // 12.2 Country/Region-Specific Differences (repeating rows)
    [JsonPropertyName("CrRows")]   public int     CrRows   { get; set; } = 0;
    [JsonPropertyName("CrData")]   public Dictionary<string, string> CrData { get; set; } = new();
    // Per-row keys: C218832_r{n} = Identifier, C218833_r{n} = Requirements, C218834_r{n} = Clarifications

    // 12.3 Prior Protocol Amendment(s)
    // Amendment rows (one row per prior amendment + Original Protocol)
    [JsonPropertyName("AmendRows")] public int    AmendRows { get; set; } = 1;
    [JsonPropertyName("AmendData")] public Dictionary<string, string> AmendData { get; set; } = new();
    // Per-row keys: C218477_r{n} = Identifier, C132352_r{n} = Approval Date, C218695_r{n} = Enrollment, C218695e_r{n} = Enrollment Description
    // Row 0 is always "Original Protocol" (identifier fixed, date + enrollment editable)

    // Alternate location for overview of changes (optional)
    [JsonPropertyName("C218836")] public string? C218836 { get; set; }

    // Change description rows per amendment (keyed by amendment row index)
    [JsonPropertyName("ChgRows")] public Dictionary<string, int> ChgRows { get; set; } = new();
    [JsonPropertyName("ChgData")] public Dictionary<string, string> ChgData { get; set; } = new();
    // Per-change keys: C218483_a{n}_r{m} = Description, C181233_a{n}_r{m} = Rationale, C218479_a{n}_r{m} = Section
}
