#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
/// </summary>
/// <typeparam name="T"></typeparam>
public class Notification<T> : EventBase
{
    /// <summary>
    /// A unique identifier for this conversation
    /// </summary>
    [JsonProperty("conversation_uuid")]
    [JsonPropertyName("conversation_uuid")]
    public string ConversationUuid { get; set; }

    /// <summary>
    /// Custom payload of for the notification action
    /// </summary>
    [JsonProperty("payload")]
    [JsonPropertyName("payload")]
    public T Payload { get; set; }
}