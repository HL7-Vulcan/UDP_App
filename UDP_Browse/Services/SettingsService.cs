using UDP_Browse.Models;

namespace UDP_Browse.Services;

/// <summary>
/// Holds the active FHIR server settings for the session.
/// Initialised from appsettings.json; overridden from the Setup page.
/// </summary>
public class SettingsService
{
    public FhirServerSettings Settings { get; private set; }

    public SettingsService(IConfiguration configuration)
    {
        Settings = configuration.GetSection("FhirServer").Get<FhirServerSettings>()
                   ?? new FhirServerSettings();
    }

    public void Update(FhirServerSettings updated)
    {
        Settings = new FhirServerSettings
        {
            BaseUrl   = updated.BaseUrl.TrimEnd('/'),
            Username  = updated.Username,
            Password  = updated.Password,
            VhewerUrl   = updated.VhewerUrl.TrimEnd('/'),
            StudyOption    = updated.StudyOption.Trim(),
            ExamplesFolder = updated.ExamplesFolder.Trim(),
        };
    }
}
