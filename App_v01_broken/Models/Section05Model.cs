using System.Text.Json.Serialization;

namespace UDP_App.Models;

public class Section05Model
{
    // 5.1 Description of Trial Population and Rationale
    [JsonPropertyName("C218739")] public string? C218739 { get; set; }

    // 5.2 Inclusion Criteria (narrative text)
    [JsonPropertyName("C25532")]  public string? C25532  { get; set; }

    // 5.3 Exclusion Criteria
    [JsonPropertyName("C25370")]  public string? C25370  { get; set; }

    // 5.4.1 Definitions Related to Childbearing Potential
    [JsonPropertyName("C218740")] public string? C218740 { get; set; }

    // 5.4.2 Contraception Requirements
    [JsonPropertyName("C218741")] public string? C218741 { get; set; }

    // 5.5 Lifestyle Restrictions
    [JsonPropertyName("C218742")] public string? C218742 { get; set; }

    // 5.5.1 Meals and Dietary Restrictions
    [JsonPropertyName("C218743")] public string? C218743 { get; set; }

    // 5.5.2 Caffeine, Alcohol, Tobacco, and Other Restrictions
    [JsonPropertyName("C218744")] public string? C218744 { get; set; }

    // 5.5.3 Physical Activity Restrictions
    [JsonPropertyName("C218745")] public string? C218745 { get; set; }

    // 5.5.4 Other Activity Restrictions
    [JsonPropertyName("C218746")] public string? C218746 { get; set; }

    // 5.6.1 Screen Failure
    [JsonPropertyName("C49628")]  public string? C49628  { get; set; }

    // 5.6.2 Rescreening
    [JsonPropertyName("C179373")] public string? C179373 { get; set; }

    // Inclusion group table (5.2)
    // Row-level fields stored as: name_r{N}, title_r{N}, description_r{N},
    //   type_r{N}, membership_r{N}, combinationMethod_r{N}
    // Characteristic-level fields stored as: char_code_r{N}_c{M}, char_low_r{N}_c{M},
    //   char_high_r{N}_c{M}, char_exclude_r{N}_c{M}, char_desc_r{N}_c{M}
    // Characteristic count per row: CharCount_r{N}
    [JsonPropertyName("InclRows")]    public int InclRows { get; set; } = 1;
    [JsonPropertyName("InclData")]    public Dictionary<string, string> InclData { get; set; } = new();

    // Exclusion group table (5.3) — same structure as InclData
    [JsonPropertyName("ExclRows")]    public int ExclRows { get; set; } = 1;
    [JsonPropertyName("ExclData")]    public Dictionary<string, string> ExclData { get; set; } = new();
}
