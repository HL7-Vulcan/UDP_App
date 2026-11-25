using System.Text.Json.Serialization;

namespace UDP_App.Models;

public class Section10Model
{
    // ── Top-level fields ────────────────────────────────────────────────────
    [JsonPropertyName("C164387")]  public string? C164387  { get; set; }  // General Considerations (required)
    [JsonPropertyName("C218801")]  public string? C218801  { get; set; }  // Analysis Sets (required)
    [JsonPropertyName("C218802")]  public string? C218802  { get; set; }  // Primary Efficacy Analysis Method (required)
    [JsonPropertyName("C218807")]  public string? C218807  { get; set; }  // Multiple Comparisons/Multiplicity (required)
    [JsonPropertyName("C218808")]  public string? C218808  { get; set; }  // Additional Analysis (required)
    [JsonPropertyName("C218809")]  public string? C218809  { get; set; }  // Safety Analysis (required)
    [JsonPropertyName("C142582")]  public string? C142582  { get; set; }  // Interim Analysis (required)
    [JsonPropertyName("C218810")]  public string? C218810  { get; set; }  // Sample Size (required)
    [JsonPropertyName("C115467")]  public string? C115467  { get; set; }  // Additional Statistical Considerations (required)

    // ── Primary objective repeating table ──────────────────────────────────
    // Keys: C218482_p{r}, C218803_p{r}, C218804_p{r}, C218480_p{r}, C218481_p{r}
    [JsonPropertyName("PriRows")]    public int PriRows    { get; set; } = 1;
    [JsonPropertyName("PriData")]    public Dictionary<string, string> PriData    { get; set; } = new();

    // ── Secondary objective repeating table ────────────────────────────────
    // Keys: C218482_s{r}, C218805_s{r}, C218806_s{r}, C218480_s{r}, C218481_s{r}
    [JsonPropertyName("SecRows")]    public int SecRows    { get; set; } = 1;
    [JsonPropertyName("SecData")]    public Dictionary<string, string> SecData    { get; set; } = new();
}
