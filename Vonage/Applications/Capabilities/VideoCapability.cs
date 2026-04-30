using System.Text.Json.Serialization;

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents the Video capability configuration for an application.
/// </summary>
public record VideoCapability
{
    /// <summary>
    ///     Webhook endpoints for Video events.
    /// </summary>
    [JsonPropertyName("webhooks")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoWebhooks Webhooks { get; init; }
}
