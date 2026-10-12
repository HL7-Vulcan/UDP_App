using System.Text.Json.Serialization;

namespace UDP_App.Models;

public class Section01Model
{
    // 1.1.1 Primary and Secondary Objectives and Estimands
    [JsonPropertyName("C218839")]  public string? C218839  { get; set; }
    // 1.1.2 Overall Design table instance id
    [JsonPropertyName("TABLE_X")]  public string? TABLE_X  { get; set; } = "DesignTable-01";
    // 1.2 Trial Schema
    [JsonPropertyName("C218720")]  public string? C218720  { get; set; }
    // 1.3 Schedule of Activities
    [JsonPropertyName("C132349")]  public string? C132349  { get; set; }

    // ── Design table (Section_01_Table_Template.fsh) ─────────────
    [JsonPropertyName("C98746")]   public string? C98746   { get; set; }  // Intervention Model
    [JsonPropertyName("C218703")]  public string? C218703  { get; set; }  // Population Type
    [JsonPropertyName("C49647")]   public string? C49647   { get; set; }  // Control Type
    [JsonPropertyName("C112038")]  public string? C112038  { get; set; }  // Condition
    [JsonPropertyName("C49693")]   public string? C49693   { get; set; }  // Minimum Age
    [JsonPropertyName("C49694")]   public string? C49694   { get; set; }  // Maximum Age
    [JsonPropertyName("C218475")]  public string? C218475  { get; set; }  // Intervention Assignment Method
    [JsonPropertyName("C223137")]  public string? C223137  { get; set; }  // Randomisation Type
    [JsonPropertyName("CC223138")] public string? CC223138 { get; set; }  // Other Assignment Method
    [JsonPropertyName("C223136")]  public string? C223136  { get; set; }  // Stratification Indicator
    [JsonPropertyName("C218704")]  public string? C218704  { get; set; }  // Site Distribution
    [JsonPropertyName("C218705")]  public string? C218705  { get; set; }  // Site Geographic Scope
    [JsonPropertyName("C218707")]  public string? C218707  { get; set; }  // Master Protocol Indicator
    [JsonPropertyName("C218706")]  public string? C218706  { get; set; }  // Adaptive Trial Design Indicator
    [JsonPropertyName("C98771")]   public string? C98771   { get; set; }  // Number of Arms
    [JsonPropertyName("C49658")]   public string? C49658   { get; set; }  // Trial Blind Schema
    [JsonPropertyName("C218709")]  public string? C218709  { get; set; }  // Blinded Roles
    [JsonPropertyName("C218710")]  public string? C218710  { get; set; }  // Target-Maximum
    [JsonPropertyName("C49692")]   public string? C49692   { get; set; }  // Number of Participants
    [JsonPropertyName("C218714")]  public string? C218714  { get; set; }  // Alt duration description
    [JsonPropertyName("C218715")]  public string? C218715  { get; set; }  // Total planned duration participation
    [JsonPropertyName("C218718")]  public string? C218718  { get; set; }  // Independent Committees
    [JsonPropertyName("C218719")]  public string? C218719  { get; set; }  // Other Committees

    // ── Comparator Product (Section_01_Comparator_Product.fsh) ───
    [JsonPropertyName("COMPARATOR_PRODUCT")] public string? COMPARATOR_PRODUCT { get; set; } = "ComparatorProduct-01";
    [JsonPropertyName("C218675")]  public string? C218675  { get; set; }  // Sponsor IP Code
    [JsonPropertyName("C97054")]   public string? C97054   { get; set; }  // Nonproprietary Name
}
