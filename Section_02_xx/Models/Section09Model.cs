using System.Text.Json.Serialization;

namespace Section_02.Models;

public class Section09Model
{
    // 9.1 Definitions
    [JsonPropertyName("field_9_01_C218476")] public string? field_9_01_C218476 { get; set; }
    [JsonPropertyName("field_9_02_C218781")] public string? field_9_02_C218781 { get; set; }
    [JsonPropertyName("field_9_03_C218782")] public string? field_9_03_C218782 { get; set; }
    [JsonPropertyName("field_9_04_C218783")] public string? field_9_04_C218783 { get; set; }

    // 9.2 Timing
    [JsonPropertyName("field_9_12_C218791")] public string? field_9_12_C218791 { get; set; }
    [JsonPropertyName("field_9_13_C218792")] public string? field_9_13_C218792 { get; set; }
    [JsonPropertyName("field_9_14_C25676")]  public string? field_9_14_C25676  { get; set; }
    [JsonPropertyName("field_9_15_C82552")]  public string? field_9_15_C82552  { get; set; }
    [JsonPropertyName("field_9_16_C218793")] public string? field_9_16_C218793 { get; set; }
    [JsonPropertyName("field_9_17_C218794")] public string? field_9_17_C218794 { get; set; }
    [JsonPropertyName("field_9_18_C218795")] public string? field_9_18_C218795 { get; set; }
    [JsonPropertyName("field_9_19_C218796")] public string? field_9_19_C218796 { get; set; }
    [JsonPropertyName("field_9_20_C217358")] public string? field_9_20_C217358 { get; set; }
    [JsonPropertyName("field_9_21_C218797")] public string? field_9_21_C218797 { get; set; }

    // 9.3 Pregnancy
    [JsonPropertyName("field_9_22_C218798")] public string? field_9_22_C218798 { get; set; }
    [JsonPropertyName("field_9_23_C218799")] public string? field_9_23_C218799 { get; set; }

    // 9.4 Special Safety
    [JsonPropertyName("field_9_24_C218800")] public string? field_9_24_C218800 { get; set; }

    // Dynamic reporting table rows
    [JsonPropertyName("RptRows")]            public int RptRows { get; set; } = 1;
    [JsonPropertyName("RptTableData")]       public Dictionary<string, string> RptTableData { get; set; } = new();
}
