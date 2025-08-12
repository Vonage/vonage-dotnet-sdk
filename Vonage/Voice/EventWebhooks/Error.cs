#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
/// </summary>
public class Error : EventBase
{
    /// <summary>
    /// Information about the nature of the error
    /// </summary>
    [JsonProperty("reason")]
    [JsonPropertyName("reason")]
    public string Reason { get; set; }

    /// <summary>
    /// The unique identifier for this conversation
    /// </summary>
    [JsonProperty("conversation_uuid")]
    [JsonPropertyName("conversation_uuid")]
    public string ConversationUuid { get; set; }
}