#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Webhook event received from a <c>notify</c> NCCO action. Carries a custom payload of the specified type.
/// </summary>
/// <typeparam name="T">The type of the custom payload included in the notification.</typeparam>
public class Notification<T> : EventBase
{
    /// <summary>
    ///     The unique identifier for the conversation associated with this notification.
    /// </summary>
    [JsonProperty("conversation_uuid")]
    [JsonPropertyName("conversation_uuid")]
    public string ConversationUuid { get; set; }

    /// <summary>
    ///     The custom payload specified in the <c>notify</c> NCCO action.
    /// </summary>
    [JsonProperty("payload")]
    [JsonPropertyName("payload")]
    public T Payload { get; set; }
}