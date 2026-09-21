using System.Text.Json.Serialization;

namespace UDP_App.Models;

public class Section06Model
{
    // 6 overview + table heading
    [JsonPropertyName("C218747")] public string? C218747 { get; set; }

    // 6.1
    [JsonPropertyName("C218751")] public string? C218751 { get; set; }
    // 6.2
    [JsonPropertyName("C218752")] public string? C218752 { get; set; }
    // 6.3
    [JsonPropertyName("C218753")] public string? C218753 { get; set; }
    // 6.4
    [JsonPropertyName("C218754")] public string? C218754 { get; set; }
    // 6.5
    [JsonPropertyName("C218755")] public string? C218755 { get; set; }
    // 6.6.1
    [JsonPropertyName("C176274")] public string? C176274 { get; set; }
    // 6.6.2
    [JsonPropertyName("C115525")] public string? C115525 { get; set; }
    // 6.6.3
    [JsonPropertyName("C176267")] public string? C176267 { get; set; }
    // 6.7.1
    [JsonPropertyName("C218756")] public string? C218756 { get; set; }
    // 6.7.2 conditional
    [JsonPropertyName("C25196")]  public string? C25196  { get; set; }
    // 6.7.3 conditional
    [JsonPropertyName("C189349")] public string? C189349 { get; set; }
    // 6.7.4 conditional
    [JsonPropertyName("C218757")] public string? C218757 { get; set; }
    // 6.8
    [JsonPropertyName("C218758")] public string? C218758 { get; set; }
    // 6.9
    [JsonPropertyName("C218759")] public string? C218759 { get; set; }
    // 6.9.1 conditional
    [JsonPropertyName("C222329")] public string? C222329 { get; set; }
    // 6.9.2 conditional
    [JsonPropertyName("C222330")] public string? C222330 { get; set; }
    // 6.9.3 conditional
    [JsonPropertyName("C218761")] public string? C218761 { get; set; }
    // 6.10
    [JsonPropertyName("C53630")]  public string? C53630  { get; set; }
    // 6.10.1 conditional
    [JsonPropertyName("C218762")] public string? C218762 { get; set; }
    // 6.10.2 conditional
    [JsonPropertyName("C218763")] public string? C218763 { get; set; }

    // Intervention table
    [JsonPropertyName("TblRows")]    public int TblRows { get; set; } = 1;
    [JsonPropertyName("TblData")]    public Dictionary<string, string> TblData { get; set; } = new();
}
