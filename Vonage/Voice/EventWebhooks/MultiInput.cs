using Newtonsoft.Json;

namespace Vonage.Voice.EventWebhooks;

public class MultiInput : Event
{
    /// <summary>
    /// Result of Dtmf input
    /// </summary>
    [JsonProperty("dtmf")]
    public DtmfResult Dtmf { get; set; }

    /// <summary>
    /// Result of the speech recognition
    /// </summary>
    [JsonProperty("speech")]
    public SpeechResult Speech { get; set; }

    /// <summary>
    ///     The buttons pressed by the user
    /// </summary>
    /// <summary>
    ///     Whether the input action timed out: true if it did, false if not
    /// </summary>
    [JsonProperty("timed_out")]
    public bool TimedOut { get; set; }

    /// <summary>
    ///     The unique identifier for this conversation
    /// </summary>
    [JsonProperty("conversation_uuid")]
    public string ConversationUuid { get; set; }

    /// <summary>
    ///     The number the call came from
    /// </summary>
    [JsonProperty("from")]
    public string From { get; set; }

    /// <summary>
    ///     The number the call was made to
    /// </summary>
    [JsonProperty("to")]
    public string To { get; set; }
}