using System.Text.Json.Serialization;

namespace UDP_App.Models;

/// <summary>A single coded option within a value set.</summary>
public class PicklistOption
{
    [JsonPropertyName("system")]  public string System  { get; set; } = "$NCIT";       // e.g. $NCIT, $SCT
    [JsonPropertyName("code")]    public string Code    { get; set; } = string.Empty;  // e.g. C49488
    [JsonPropertyName("display")] public string Display { get; set; } = string.Empty;  // e.g. Yes
    /// <summary>Full FSH token value stored in model fields: $SYSTEM#Code "Display"</summary>
    [JsonIgnore]
    public string FshValue => $"{System}#{Code} \"{Display}\"";
}

/// <summary>A named value set containing an ordered list of options.</summary>
public class ValueSet
{
    [JsonPropertyName("id")]          public string Id          { get; set; } = string.Empty;  // e.g. C217046
    [JsonPropertyName("name")]        public string Name        { get; set; } = string.Empty;  // e.g. Yes/No Indicator
    [JsonPropertyName("description")] public string Description { get; set; } = string.Empty;
    [JsonPropertyName("options")]     public List<PicklistOption> Options { get; set; } = new();
}
