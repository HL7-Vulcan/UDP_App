using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using UDP_App.Models;

namespace UDP_App.Services;

/// <summary>
/// Queries a FHIR server to discover and list resources.
/// Uses basic authentication as configured in SettingsService.
/// </summary>
public class FhirBrowseService
{
    private readonly SettingsService _settings;
    private readonly IHttpClientFactory _httpClientFactory;

    public FhirBrowseService(SettingsService settings, IHttpClientFactory httpClientFactory)
    {
        _settings          = settings;
        _httpClientFactory = httpClientFactory;
    }

    // ── Public API ────────────────────────────────────────────────────────────

    /// <summary>
    /// Returns the list of resource types supported by the server (from its CapabilityStatement).
    /// </summary>
    public async Task<List<string>> GetResourceTypesAsync()
    {
        var client = BuildClient();
        var url    = $"{_settings.Settings.BaseUrl}/metadata?_summary=true";

        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return ParseResourceTypes(json);
    }

    /// <summary>
    /// Returns the total count of resources of the given type using _summary=count.
    /// Returns null if the request fails or the server doesn't support it.
    /// </summary>
    public async Task<int?> GetResourceCountAsync(string resourceType)
    {
        try
        {
            var client   = BuildClient();
            var url      = $"{_settings.Settings.BaseUrl}/{resourceType}?_summary=count";
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("total", out var total))
                return total.GetInt32();
        }
        catch { /* swallow */ }
        return null;
    }

    /// <summary>
    /// Fetches a single resource as a pretty-printed JSON string.
    /// </summary>
    public async Task<string?> GetResourceJsonAsync(string resourceType, string id)
    {
        try
        {
            var client   = BuildClient();
            var url      = $"{_settings.Settings.BaseUrl}/{resourceType}/{id}";
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;
            var raw = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(raw);
            return JsonSerializer.Serialize(doc.RootElement, new JsonSerializerOptions { WriteIndented = true });
        }
        catch { return null; }
    }

    /// <summary>
    /// Fetches a single resource as XML string.
    /// </summary>
    public async Task<string?> GetResourceXmlAsync(string resourceType, string id)
    {
        try
        {
            var client = BuildClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/fhir+xml"));
            var url      = $"{_settings.Settings.BaseUrl}/{resourceType}/{id}";
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadAsStringAsync();
        }
        catch { return null; }
    }

    /// <summary>
    /// Returns a page of resources of the given type.
    /// </summary>
    public async Task<(List<FhirResourceSummary> Resources, string? NextUrl)>
        GetResourcesAsync(string resourceType, string? pageUrl = null)
    {
        var client = BuildClient();
        var url    = pageUrl ?? $"{_settings.Settings.BaseUrl}/{resourceType}?_count=20&_summary=true&_sort=-_lastUpdated";

        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return ParseBundle(json, resourceType);
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    private HttpClient BuildClient()
    {
        var client  = _httpClientFactory.CreateClient();
        var s       = _settings.Settings;
        var token   = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{s.Username}:{s.Password}"));
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", token);
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/fhir+json"));
        return client;
    }

    private static List<string> ParseResourceTypes(string json)
    {
        var result = new List<string>();
        try
        {
            using var doc  = JsonDocument.Parse(json);
            var root       = doc.RootElement;
            if (!root.TryGetProperty("rest", out var rest)) return result;
            foreach (var restItem in rest.EnumerateArray())
            {
                if (!restItem.TryGetProperty("resource", out var resources)) continue;
                foreach (var res in resources.EnumerateArray())
                {
                    if (res.TryGetProperty("type", out var t))
                        result.Add(t.GetString() ?? string.Empty);
                }
            }
        }
        catch { /* return what we have */ }
        return result.Where(r => r.Length > 0).Distinct().OrderBy(r => r).ToList();
    }

    private static (List<FhirResourceSummary>, string?) ParseBundle(string json, string resourceType)
    {
        var resources = new List<FhirResourceSummary>();
        string? nextUrl = null;
        try
        {
            using var doc = JsonDocument.Parse(json);
            var root      = doc.RootElement;

            // Next page link
            if (root.TryGetProperty("link", out var links))
            {
                foreach (var link in links.EnumerateArray())
                {
                    if (link.TryGetProperty("relation", out var rel) &&
                        rel.GetString() == "next" &&
                        link.TryGetProperty("url", out var urlProp))
                    {
                        nextUrl = urlProp.GetString();
                    }
                }
            }

            // Entries
            if (!root.TryGetProperty("entry", out var entries)) return (resources, nextUrl);

            foreach (var entry in entries.EnumerateArray())
            {
                if (!entry.TryGetProperty("resource", out var res)) continue;

                var summary = new FhirResourceSummary
                {
                    ResourceType = res.TryGetProperty("resourceType", out var rt)
                                   ? rt.GetString() ?? resourceType : resourceType,
                    Id = res.TryGetProperty("id", out var id)
                         ? id.GetString() ?? string.Empty : string.Empty,
                };

                // Last updated from meta
                if (res.TryGetProperty("meta", out var meta) &&
                    meta.TryGetProperty("lastUpdated", out var lu))
                    summary.LastUpdated = lu.GetString();

                // Best-effort display text
                summary.Display = ExtractDisplay(res);

                resources.Add(summary);
            }
        }
        catch { /* return what we have */ }
        return (resources, nextUrl);
    }

    private static string? ExtractDisplay(JsonElement res)
    {
        // Try common display fields in priority order
        if (res.TryGetProperty("title", out var title) &&
            title.ValueKind == JsonValueKind.String)
            return title.GetString();

        if (res.TryGetProperty("name", out var name))
        {
            if (name.ValueKind == JsonValueKind.String) return name.GetString();
            // HumanName array (Patient)
            if (name.ValueKind == JsonValueKind.Array)
            {
                foreach (var n in name.EnumerateArray())
                {
                    var parts = new List<string>();
                    if (n.TryGetProperty("given", out var given))
                        foreach (var g in given.EnumerateArray())
                            parts.Add(g.GetString() ?? string.Empty);
                    if (n.TryGetProperty("family", out var family))
                        parts.Add(family.GetString() ?? string.Empty);
                    var text = string.Join(" ", parts.Where(p => p.Length > 0));
                    if (text.Length > 0) return text;
                }
            }
        }

        if (res.TryGetProperty("description", out var desc) &&
            desc.ValueKind == JsonValueKind.String)
            return desc.GetString();

        return null;
    }
}
