using System.Text.Json.Serialization;

namespace Vonage.ApplicationsNew.Capabilities;

/// <summary>
///     Represents the Messages capability configuration for an application.
/// </summary>
public record MessagesCapability
{
    /// <summary>
    ///     The Messages API version. Defaults to v1 if not set.
    /// </summary>
    [JsonPropertyName("version")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Version { get; init; }

    /// <summary>
    ///     Webhook endpoints for Messages events.
    /// </summary>
    [JsonPropertyName("webhooks")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MessagesWebhooks Webhooks { get; init; }
}
