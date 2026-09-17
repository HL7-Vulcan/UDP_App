using System.Text.Json.Serialization;

namespace Section_02.Models;

public class Section09Model
{
    // 9.1 Definitions
    [JsonPropertyName("C218476")] public string? C218476 { get; set; }
    [JsonPropertyName("C218781")] public string? C218781 { get; set; }
    [JsonPropertyName("C218782")] public string? C218782 { get; set; }
    [JsonPropertyName("C218783")] public string? C218783 { get; set; }

    // 9.2 Timing
    [JsonPropertyName("C218791")] public string? C218791 { get; set; }
    [JsonPropertyName("C218792")] public string? C218792 { get; set; }
    [JsonPropertyName("C25676")]  public string? C25676  { get; set; }
    [JsonPropertyName("C82552")]  public string? C82552  { get; set; }
    [JsonPropertyName("C218793")] public string? C218793 { get; set; }
    [JsonPropertyName("C218794")] public string? C218794 { get; set; }
    [JsonPropertyName("C218795")] public string? C218795 { get; set; }
    [JsonPropertyName("C218796")] public string? C218796 { get; set; }
    [JsonPropertyName("C217358")] public string? C217358 { get; set; }
    [JsonPropertyName("C218797")] public string? C218797 { get; set; }

    // 9.3 Pregnancy
    [JsonPropertyName("C218798")] public string? C218798 { get; set; }
    [JsonPropertyName("C218799")] public string? C218799 { get; set; }

    // 9.4 Special Safety
    [JsonPropertyName("C218800")] public string? C218800 { get; set; }

    // Dynamic reporting table rows
    [JsonPropertyName("RptRows")]            public int RptRows { get; set; } = 1;
    [JsonPropertyName("RptTableData")]       public Dictionary<string, string> RptTableData { get; set; } = new();
}
