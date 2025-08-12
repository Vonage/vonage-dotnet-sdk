#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
/// </summary>
public class Transfer : Event
{
    /// <summary>
    /// The conversation ID that the leg was originally in
    /// </summary>
    [JsonProperty("conversation_uuid_from")]
    [JsonPropertyName("conversation_uuid_from")]
    public string ConversationUuidFrom { get; set; }

    /// <summary>
    /// The conversation ID that the leg was transferred to
    /// </summary>
    [JsonProperty("conversation_uuid_to")]
    [JsonPropertyName("conversation_uuid_to")]
    public string ConversationUuidTo { get; set; }
}