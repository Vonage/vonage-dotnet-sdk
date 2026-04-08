#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Webhook event received when a call leg is transferred from one conversation to another.
/// </summary>
public class Transfer : Event
{
    /// <summary>
    ///     The UUID of the conversation that the call leg was originally in before the transfer.
    /// </summary>
    [JsonProperty("conversation_uuid_from")]
    [JsonPropertyName("conversation_uuid_from")]
    public string ConversationUuidFrom { get; set; }

    /// <summary>
    ///     The UUID of the conversation that the call leg was transferred to.
    /// </summary>
    [JsonProperty("conversation_uuid_to")]
    [JsonPropertyName("conversation_uuid_to")]
    public string ConversationUuidTo { get; set; }
}