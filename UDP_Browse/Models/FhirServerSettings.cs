namespace UDP_Browse.Models;

public class FhirServerSettings
{
    public string BaseUrl    { get; set; } = string.Empty;
    public string Username   { get; set; } = string.Empty;
    public string Password   { get; set; } = string.Empty;
    public string VhewerUrl      { get; set; } = "https://vhewer.com";
    public string StudyOption    { get; set; } = "study";
    public string ExamplesFolder { get; set; } = "Examples";
}
