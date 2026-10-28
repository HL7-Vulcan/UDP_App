using System.Text.Json.Serialization;

namespace UDP_App.Models;

public class Section04Model
{
    // 4.1 Description of Trial Design
    [JsonPropertyName("C147139")]  public string? C147139  { get; set; }  // Trial Design & Intervention Model (optional)
    [JsonPropertyName("C218726")]  public string? C218726  { get; set; }  // Trial Duration (optional)
    [JsonPropertyName("C219658")]  public string? C219658  { get; set; }  // Method of Assignment (optional)
    [JsonPropertyName("C218727")]  public string? C218727  { get; set; }  // Level and Method of Blinding (optional)
    [JsonPropertyName("C218728")]  public string? C218728  { get; set; }  // Additional Description (optional)
    [JsonPropertyName("C218729")]  public string? C218729  { get; set; }  // Stakeholder Input (optional)

    // 4.2 Rationale for Trial Design (alternate_1: either C142705 OR the subsections below)
    [JsonPropertyName("C142705")]  public string? C142705  { get; set; }  // Overall Rationale (alternate_1 top-level)
    [JsonPropertyName("C218730")]  public string? C218730  { get; set; }  // Rationale for Estimand(s)
    [JsonPropertyName("C215629")]  public string? C215629  { get; set; }  // Rationale for Intervention Model
    [JsonPropertyName("C218731")]  public string? C218731  { get; set; }  // Rationale for Control Type
    [JsonPropertyName("C218732")]  public string? C218732  { get; set; }  // Rationale for Trial Duration
    [JsonPropertyName("C218733")]  public string? C218733  { get; set; }  // Rationale for Adaptive/Novel Design
    [JsonPropertyName("C218734")]  public string? C218734  { get; set; }  // Rationale for Interim Analysis
    [JsonPropertyName("C218735")]  public string? C218735  { get; set; }  // Rationale for Other Aspects

    // 4.3 Trial Stopping Rules
    [JsonPropertyName("C142698")]  public string? C142698  { get; set; }  // Trial Stopping Rules (required)

    // 4.4 Start and End of Trial
    [JsonPropertyName("C218736")]  public string? C218736  { get; set; }  // Start of Trial (required)
    [JsonPropertyName("C218737")]  public string? C218737  { get; set; }  // End of Trial (required)
    [JsonPropertyName("C218738")]  public string? C218738  { get; set; }  // Access After End of Trial (required)
}
