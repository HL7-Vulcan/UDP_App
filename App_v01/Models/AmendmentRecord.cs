using System.Text.Json.Serialization;

namespace UDP_App.Models;

/// <summary>
/// A named amendment set stored in the session. Contains the amendment-specific
/// fields extracted from TitlePageModel plus identifying metadata.
/// </summary>
public class AmendmentRecord
{
    [JsonPropertyName("id")]          public string Id          { get; set; } = Guid.NewGuid().ToString("N")[..8];
    [JsonPropertyName("name")]        public string Name        { get; set; } = "Amendment";
    [JsonPropertyName("createdAt")]   public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [JsonPropertyName("updatedAt")]   public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // ── Amendment Status ─────────────────────────────────────────────
    [JsonPropertyName("C218694")]  public string? C218694  { get; set; }  // Amendment Status

    // ── Version / Identifier / Scope (rows 6-9 from Title Page) ─────
    [JsonPropertyName("C181232")]  public string? C181232  { get; set; }  // Version Number
    [JsonPropertyName("C93813")]   public string? C93813   { get; set; }  // Version Date
    [JsonPropertyName("C218477")]  public string? C218477  { get; set; }  // Amendment Identifier
    [JsonPropertyName("C218673")]  public string? C218673  { get; set; }  // Amendment Scope (Global/Not Global)
    [JsonPropertyName("C20108")]   public string? C20108   { get; set; }  // Country
    [JsonPropertyName("C218674")]  public string? C218674  { get; set; }  // Region
    [JsonPropertyName("C83081")]   public string? C83081   { get; set; }  // Site Identifier

    // ── Amendment Details ────────────────────────────────────────────
    [JsonPropertyName("C218478")]  public string? C218478  { get; set; }  // Approx Enrolled
    [JsonPropertyName("C218695")]  public string? C218695  { get; set; }  // Scope Enrollment Definition
    [JsonPropertyName("C218874")]  public string? C218874  { get; set; }  // Number Impacted
    [JsonPropertyName("C218696")]  public string? C218696  { get; set; }  // Primary Reason
    [JsonPropertyName("C218697")]  public string? C218697  { get; set; }  // Secondary Reason (semi-colon joined)
    [JsonPropertyName("C17649")]   public string? C17649   { get; set; }  // Other Primary Reason Text
    [JsonPropertyName("C17649_2")] public string? C17649_2 { get; set; }  // Other Secondary Reason Text
    [JsonPropertyName("C42581")]   public string? C42581   { get; set; }  // Amendment Summary
    [JsonPropertyName("C218698")]  public string? C218698  { get; set; }  // Substantial Impact Safety
    [JsonPropertyName("C218699")]  public string? C218699  { get; set; }  // Substantial Impact Safety Comment
    [JsonPropertyName("C218700")]  public string? C218700  { get; set; }  // Substantial Impact Reliability
    [JsonPropertyName("C218701")]  public string? C218701  { get; set; }  // Substantial Impact Reliability Comment

    // ── Overview of Changes (repeating rows) ─────────────────────────
    [JsonPropertyName("ChgRows")]  public int     ChgRows  { get; set; } = 1;
    [JsonPropertyName("ChgData")]  public Dictionary<string, string> ChgData { get; set; } = new();
    // Per-row keys: C218483_r{n} = Description, C181233_r{n} = Rationale, C218479_r{n} = Section
}
