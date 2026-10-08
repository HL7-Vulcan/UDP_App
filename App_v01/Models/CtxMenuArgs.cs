namespace UDP_App.Models;

/// <summary>
/// Arguments passed from a child component up to the page when a field is right-clicked.
/// </summary>
public class CtxMenuArgs
{
    public string FieldId  { get; init; } = string.Empty;
    public string Guidance { get; init; } = string.Empty;
    public string X        { get; init; } = "0px";
    public string Y        { get; init; } = "0px";
}
