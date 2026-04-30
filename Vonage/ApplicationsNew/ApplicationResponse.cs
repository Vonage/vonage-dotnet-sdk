using System.Text.Json.Serialization;

namespace Vonage.ApplicationsNew;

/// <summary>
///     Represents a Vonage application returned from the API.
/// </summary>
public record ApplicationResponse(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("capabilities")] ApplicationCapabilities Capabilities,
    [property: JsonPropertyName("privacy")] ApplicationPrivacy Privacy,
    [property: JsonPropertyName("keys")] ApplicationResponseKeys Keys);
