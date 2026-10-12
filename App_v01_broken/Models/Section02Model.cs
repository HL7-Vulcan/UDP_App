using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace UDP_App.Models;

public class Section02Model
{
    // 2.1 Purpose of Trial
    [JsonPropertyName("C146997")]
    public string? C146997 { get; set; }

    // 2.2.1 Risk Summary - Trial Intervention
    [JsonPropertyName("C218721")]
    public string? C218721 { get; set; }

    // 2.2.1 Risk Summary - Trial Procedures
    [JsonPropertyName("C218722")]
    public string? C218722 { get; set; }

    // 2.2.1 Risk Summary - Other
    [JsonPropertyName("C218723")]
    public string? C218723 { get; set; }

    // 2.2.2 Benefit Summary
    [JsonPropertyName("C218724")]
    public string? C218724 { get; set; }

    // 2.2.3 Overall Risk-Benefit Assessment
    [JsonPropertyName("C218725")]
    public string? C218725 { get; set; }

    public bool IsEmpty() =>
        string.IsNullOrWhiteSpace(C146997) &&
        string.IsNullOrWhiteSpace(C218721) &&
        string.IsNullOrWhiteSpace(C218722) &&
        string.IsNullOrWhiteSpace(C218723) &&
        string.IsNullOrWhiteSpace(C218724) &&
        string.IsNullOrWhiteSpace(C218725);
}
