#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Webhook event received when an error occurs during a call. Contains the reason for the error and the associated conversation UUID.
/// </summary>
public class Error : EventBase
{
    /// <summary>
    ///     A human-readable description of the error that occurred.
    /// </summary>
    [JsonProperty("reason")]
    [JsonPropertyName("reason")]
    public string Reason { get; set; }

    /// <summary>
    ///     The unique identifier for the conversation associated with this error.
    /// </summary>
    [JsonProperty("conversation_uuid")]
    [JsonPropertyName("conversation_uuid")]
    public string ConversationUuid { get; set; }
}