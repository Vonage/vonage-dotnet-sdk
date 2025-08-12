#region
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
/// </summary>
public class MultiInput : Event
{
    /// <summary>
    /// Result of Dtmf input
    /// </summary>
    [JsonProperty("dtmf")]
    [JsonPropertyName("dtmf")]
    public DtmfResult Dtmf { get; set; }

    /// <summary>
    /// Result of the speech recognition
    /// </summary>
    [JsonProperty("speech")]
    [JsonPropertyName("speech")]
    public SpeechResult Speech { get; set; }

    /// <summary>
    ///     The buttons pressed by the user
    /// </summary>
    /// <summary>
    ///     Whether the input action timed out: true if it did, false if not
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    [Obsolete("Use the timeout information on either DTMF or Speech instead.")]
    public bool TimedOut { get; set; }

    /// <summary>
    ///     The unique identifier for this conversation
    /// </summary>
    [JsonProperty("conversation_uuid")]
    [JsonPropertyName("conversation_uuid")]
    public string ConversationUuid { get; set; }

    /// <summary>
    ///     The number the call came from
    /// </summary>
    [JsonProperty("from")]
    [JsonPropertyName("from")]
    public string From { get; set; }

    /// <summary>
    ///     The number the call was made to
    /// </summary>
    [JsonProperty("to")]
    [JsonPropertyName("to")]
    public string To { get; set; }
}