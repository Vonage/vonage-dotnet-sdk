using Newtonsoft.Json;
using System;

namespace Vonage.Voice.EventWebhooks;

[Obsolete("This item has been rendered obsolete due to the new multi-input functionality. Please add dtmf arguments to your input action and use the MultiInput object - see: https://developer.nexmo.com/voice/voice-api/ncco-reference#dtmf-input-settings")]
public class Input : Event
{
    /// <summary>
    /// The buttons pressed by the user
    /// </summary>
    [JsonProperty("dtmf")]
    public string Dtmf { get; set; }

    /// <summary>
    /// Whether the input action timed out: true if it did, false if not
    /// </summary>
    [JsonProperty("timed_out")]
    public bool TimedOut { get; set; }

    /// <summary>
    /// The unique identifier for this conversation
    /// </summary>
    [JsonProperty("conversation_uuid")]
    public string ConversationUuid { get; set; }

    /// <summary>
    /// The number the call came from
    /// </summary>
    [JsonProperty("from")]
    public string From { get; set; }

    /// <summary>
    /// The number the call was made to
    /// </summary>
    [JsonProperty("to")]
    public string To { get; set; }
}