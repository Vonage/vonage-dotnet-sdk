using System.Text.Json.Serialization;

namespace Vonage.ApplicationsNew.Capabilities;

/// <summary>
///     Represents the webhook endpoints for the Messages capability.
///     Both webhooks are POST-only as mandated by the Messages API.
/// </summary>
public record MessagesWebhooks
{
    /// <summary>
    ///     The URL Vonage forwards inbound messages to.
    /// </summary>
    [JsonPropertyName("inbound_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PostOnlyWebhook InboundUrl { get; init; }

    /// <summary>
    ///     The URL Vonage sends message status updates (delivered, seen, etc.) to.
    /// </summary>
    [JsonPropertyName("status_url")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PostOnlyWebhook StatusUrl { get; init; }
}
