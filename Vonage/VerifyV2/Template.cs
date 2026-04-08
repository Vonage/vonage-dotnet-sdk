#region
using System;
using System.Text.Json.Serialization;
using Vonage.Common;
#endregion

namespace Vonage.VerifyV2;

/// <summary>
///     Represents a custom verification message template. Templates allow you to customize the text content sent to users during verification.
/// </summary>
/// <param name="TemplateId">The unique identifier (UUID) of the template.</param>
/// <param name="Name">The reference name for the template. Must match the pattern ^[A-Za-z0-9_-]+$ and be unique within the account.</param>
/// <param name="IsDefault">Indicates whether this template is used as the default when no template_id is specified in verification requests.</param>
/// <param name="Links">HAL navigation links for accessing the template and its fragments.</param>
public record Template(
    [property: JsonPropertyName("template_id")]
    Guid TemplateId,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("is_default")]
    bool IsDefault,
    [property: JsonPropertyName("_links")] TemplateLinks Links);

/// <summary>
///     Represents HAL (Hypertext Application Language) navigation links for a template resource.
/// </summary>
/// <param name="Self">Link to retrieve this template.</param>
/// <param name="Fragments">Link to list all template fragments belonging to this template.</param>
public record TemplateLinks(
    [property: JsonPropertyName("self")] HalLink Self,
    [property: JsonPropertyName("fragments")]
    HalLink Fragments);