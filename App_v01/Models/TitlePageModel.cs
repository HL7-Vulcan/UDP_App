using System.Text.Json.Serialization;

namespace UDP_App.Models;

public class TitlePageModel
{
    // ── Title Page ────────────────────────────────────────────────
    [JsonPropertyName("C181236")]  public string? C181236  { get; set; }  // Confidentiality Statement (optional)
    [JsonPropertyName("C132346")]  public string? C132346  { get; set; }  // Full Title (required)
    [JsonPropertyName("C94108")]   public string? C94108   { get; set; }  // Trial Acronym (optional)
    [JsonPropertyName("C132351")]  public string? C132351  { get; set; }  // Sponsor Protocol Identifier (required)
    [JsonPropertyName("C218672")]  public string? C218672  { get; set; }  // Original Protocol (required picklist)
    [JsonPropertyName("C181232")]  public string? C181232  { get; set; }  // Version Number (optional)
    [JsonPropertyName("C93813")]   public string? C93813   { get; set; }  // Version Date (optional date)
    [JsonPropertyName("C218477")]  public string? C218477  { get; set; }  // Amendment Identifier (conditional)
    [JsonPropertyName("C218673")]  public string? C218673  { get; set; }  // Amendment Scope (conditional picklist)
    [JsonPropertyName("C20108")]   public string? C20108   { get; set; }  // Country (conditional picklist)
    [JsonPropertyName("C218674")]  public string? C218674  { get; set; }  // Region (conditional picklist)
    [JsonPropertyName("C83081")]   public string? C83081   { get; set; }  // Site Identifier (conditional)
    [JsonPropertyName("C218675")]  public string? C218675  { get; set; }  // Sponsor IP Code (optional)
    [JsonPropertyName("C97054")]   public string? C97054   { get; set; }  // Nonproprietary Name (optional)
    [JsonPropertyName("C71898")]   public string? C71898   { get; set; }  // Proprietary Name (optional)
    [JsonPropertyName("C48281")]   public string? C48281   { get; set; }  // Trial Phase (required picklist)
    [JsonPropertyName("C94105")]   public string? C94105   { get; set; }  // Short Title (optional)
    [JsonPropertyName("C222495")]  public string? C222495  { get; set; }  // Sponsor Name (required)
    [JsonPropertyName("C218677")]  public string? C218677  { get; set; }  // Sponsor Legal Address (required)
    [JsonPropertyName("C218678")]  public string? C218678  { get; set; }  // Co-Sponsor Name (optional)
    [JsonPropertyName("C218679")]  public string? C218679  { get; set; }  // Co-Sponsor Legal Address (optional)
    [JsonPropertyName("C218680")]  public string? C218680  { get; set; }  // Local Sponsor Name (optional)
    [JsonPropertyName("C218681")]  public string? C218681  { get; set; }  // Local Sponsor Legal Address (optional)
    [JsonPropertyName("C218682")]  public string? C218682  { get; set; }  // Device Manufacturer Name (optional)
    [JsonPropertyName("C218683")]  public string? C218683  { get; set; }  // Device Manufacturer Address (optional)
    [JsonPropertyName("C218684")]  public string? C218684  { get; set; }  // EU CT Number (optional)
    [JsonPropertyName("C218685")]  public string? C218685  { get; set; }  // FDA IND Number (optional)
    [JsonPropertyName("C218686")]  public string? C218686  { get; set; }  // IDE Number (optional)
    [JsonPropertyName("C218687")]  public string? C218687  { get; set; }  // jRCT Number (optional)
    [JsonPropertyName("C172240")]  public string? C172240  { get; set; }  // NCT Number (optional)
    [JsonPropertyName("C218688")]  public string? C218688  { get; set; }  // NMPA IND Number (optional)
    [JsonPropertyName("C218689")]  public string? C218689  { get; set; }  // WHO/UTN Number (optional)
    [JsonPropertyName("C218690")]  public string? C218690  { get; set; }  // Other Regulatory Identifier (optional)
    [JsonPropertyName("C132352")]  public string? C132352  { get; set; }  // Sponsor Approval Date (conditional)
    [JsonPropertyName("C218484")]  public string? C218484  { get; set; }  // Signature URL (conditional)
    [JsonPropertyName("C222014")]  public string? C222014  { get; set; }  // Sponsor Signatory (optional)
    [JsonPropertyName("C222064")]  public string? C222064  { get; set; }  // Signatory Location (optional)
    [JsonPropertyName("C218693")]  public string? C218693  { get; set; }  // Medical Expert Contact (optional)
    [JsonPropertyName("C222063")]  public string? C222063  { get; set; }  // Medical Expert Location (optional)

    // ── Amendment Details ─────────────────────────────────────────
    [JsonPropertyName("C218694")]  public string? C218694  { get; set; }  // Amendment Status (conditional picklist)
    [JsonPropertyName("C218478")]  public string? C218478  { get; set; }  // Approx Enrolled (conditional)
    [JsonPropertyName("C218695")]  public string? C218695  { get; set; }  // Scope Enrollment Definition (conditional picklist)
    [JsonPropertyName("C218696")]  public string? C218696  { get; set; }  // Primary Reason (conditional picklist)
    [JsonPropertyName("C218697")]  public string? C218697  { get; set; }  // Secondary Reason (conditional picklist)
    [JsonPropertyName("C17649")]   public string? C17649   { get; set; }  // Other Reason Text (conditional)
    [JsonPropertyName("C42581")]   public string? C42581   { get; set; }  // Amendment Summary (conditional)
    [JsonPropertyName("C218698")]  public string? C218698  { get; set; }  // Substantial Impact Safety (conditional picklist)
    [JsonPropertyName("C218699")]  public string? C218699  { get; set; }  // Substantial Impact Safety Comment (conditional)
    [JsonPropertyName("C218700")]  public string? C218700  { get; set; }  // Substantial Impact Reliability (conditional picklist)
    [JsonPropertyName("C218701")]  public string? C218701  { get; set; }  // Substantial Impact Reliability Comment (conditional)

    // ── Overview of Changes (repeating rows) ─────────────────────
    [JsonPropertyName("ChgRows")]  public int     ChgRows  { get; set; } = 1;
    [JsonPropertyName("ChgData")]  public Dictionary<string, string> ChgData { get; set; } = new();
    // Per-row keys: C218483_r{n} = Description, C181233_r{n} = Rationale, C218479_r{n} = Section
}
