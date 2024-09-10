#region
using System.Text.Json.Serialization;
using Vonage.Common;
#endregion

namespace Vonage.VerifyV2;

/// <summary>
///     References a template.
/// </summary>
/// <param name="TemplateId">The id.</param>
/// <param name="Name">The reference name.</param>
/// <param name="IsDefault">Indicates whether it is the default template</param>
/// <param name="Links">Navigation links.</param>
public record Template(
    [property: JsonPropertyName("template_id")]
    string TemplateId,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("is_default")]
    bool IsDefault,
    [property: JsonPropertyName("_links")] TemplateLinks Links);

/// <summary>
///     Represents navigation links for a template.
/// </summary>
/// <param name="Self">Link to this template</param>
/// <param name="Fragments">Link to fragments for this template</param>
public record TemplateLinks(
    [property: JsonPropertyName("self")] HalLink Self,
    [property: JsonPropertyName("fragments")]
    HalLink Fragments);