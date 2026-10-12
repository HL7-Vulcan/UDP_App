namespace UDP_App.Models;

/// <summary>
/// A lightweight summary of a single FHIR resource returned from the server.
/// </summary>
public class FhirResourceSummary
{
    public string ResourceType { get; set; } = string.Empty;
    public string Id           { get; set; } = string.Empty;
    public string? LastUpdated { get; set; }
    public string? Display     { get; set; }
}
