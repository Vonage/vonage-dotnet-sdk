using System.Text.Json.Serialization;

namespace Vonage.Common;

/// <summary>
///     Represents an Embedded JSON response with a custom content.
/// </summary>
/// <param name="Content">The embedded content.</param>
/// <typeparam name="T">Type of the content.</typeparam>
public record EmbeddedResponse<T>(
    [property: JsonPropertyName("_embedded")]
    T Content);