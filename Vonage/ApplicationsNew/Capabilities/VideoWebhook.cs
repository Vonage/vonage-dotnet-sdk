using System.Text.Json.Serialization;

namespace Vonage.ApplicationsNew.Capabilities;

/// <summary>
///     Represents a Video capability webhook endpoint. Video webhooks use an active flag instead of an HTTP method.
/// </summary>
public record VideoWebhook(
    [property: JsonPropertyName("address")] string Address,
    [property: JsonPropertyName("active")]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    bool? Active = null);
