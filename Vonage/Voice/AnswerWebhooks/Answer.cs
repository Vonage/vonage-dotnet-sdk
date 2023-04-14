using Newtonsoft.Json;
using Vonage.Voice.EventWebhooks;

namespace Vonage.Voice.AnswerWebhooks;

public class Answer : EventBase
{
    /// <summary>
    /// The number the call came from (this could be your Vonage number if the call is started programmatically)
    /// </summary>
    [JsonProperty("to")]
    public string To { get; set; }

    /// <summary>
    /// The call the number is to (this could be a Vonage number or another phone number)
    /// </summary>
    [JsonProperty("from")]
    public string From { get; set; }

    /// <summary>
    /// A unique identifier for this call
    /// </summary>
    [JsonProperty("uuid")]
    public string Uuid { get; set; }

    /// <summary>
    /// A unique identifier for this conversation
    /// </summary>
    [JsonProperty("conversation_uuid")]
    public string ConversationUuid { get; set; }

}