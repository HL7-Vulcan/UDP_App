using System.Text.Json.Serialization;

namespace UDP_App.Models;

public class Section11Model
{
    // 11.1 Regulatory and Ethical Considerations
    [JsonPropertyName("C218811")]  public string? C218811  { get; set; }  // Regulatory and Ethical Considerations (required)

    // 11.2 Trial Oversight
    [JsonPropertyName("C218812")]  public string? C218812  { get; set; }  // Trial Oversight (optional, alternative to 11.2.1/11.2.2)

    // 11.2.1 Investigator Responsibilities
    [JsonPropertyName("C218813")]  public string? C218813  { get; set; }  // Investigator Responsibilities (optional)

    // 11.2.2 Sponsor Responsibilities
    [JsonPropertyName("C218814")]  public string? C218814  { get; set; }  // Sponsor Responsibilities (optional)

    // 11.3 Informed Consent Process
    [JsonPropertyName("C184390")]  public string? C184390  { get; set; }  // Description of Informed Consent Process (required)
    [JsonPropertyName("C218815")]  public string? C218815  { get; set; }  // Description of Assent Process (optional)
    [JsonPropertyName("C218816")]  public string? C218816  { get; set; }  // Description of Emergency Consent Process (optional)

    // 11.3.1 Informed Consent for Rescreening
    [JsonPropertyName("C218817")]  public string? C218817  { get; set; }  // Informed Consent for Rescreening (optional)

    // 11.3.2 Informed Consent for Use of Remaining Samples
    [JsonPropertyName("C218818")]  public string? C218818  { get; set; }  // Informed Consent for Use of Remaining Samples in Exploratory Research (optional)

    // 11.4 Committees
    [JsonPropertyName("C218819")]  public string? C218819  { get; set; }  // Committees (required)

    // 11.5 Insurance and Indemnity
    [JsonPropertyName("C218820")]  public string? C218820  { get; set; }  // Insurance and Indemnity (required)

    // 11.6 Risk-Based Quality Management
    [JsonPropertyName("C218821")]  public string? C218821  { get; set; }  // Risk-Based Quality Management (required)

    // 11.7 Data Governance
    [JsonPropertyName("C218822")]  public string? C218822  { get; set; }  // Data Governance (required)

    // 11.8 Data Protection
    [JsonPropertyName("C218823")]  public string? C218823  { get; set; }  // Data Protection (required)

    // 11.9 Source Records
    [JsonPropertyName("C218824")]  public string? C218824  { get; set; }  // Source Records Introduction (required)
    [JsonPropertyName("C218825")]  public string? C218825  { get; set; }  // Investigator Expectations for Source Records (required)
    [JsonPropertyName("C218826")]  public string? C218826  { get; set; }  // Trial Monitor Expectations for Source Records (required)
    [JsonPropertyName("C222675")]  public string? C222675  { get; set; }  // Identification of Source Records (required)

    // 11.10 Protocol Deviations
    [JsonPropertyName("C218827")]  public string? C218827  { get; set; }  // Protocol Deviations (required)

    // 11.11 Early Site Closure
    [JsonPropertyName("C218828")]  public string? C218828  { get; set; }  // Decision Rights for Site Closure (required)
    [JsonPropertyName("C218829")]  public string? C218829  { get; set; }  // Criteria for Early Closure (required)
    [JsonPropertyName("C218830")]  public string? C218830  { get; set; }  // Responsibilities Following Early Site Closure (required)

    // 11.12 Data Dissemination
    [JsonPropertyName("C218831")]  public string? C218831  { get; set; }  // Data Dissemination (required)
}
