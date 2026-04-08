#region
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Webhook event received when a multi-input NCCO action completes, containing both DTMF and speech recognition results.
/// </summary>
public class MultiInput : Event
{
    /// <summary>
    ///     The DTMF input result collected from the caller during the input action.
    /// </summary>
    [JsonProperty("dtmf")]
    [JsonPropertyName("dtmf")]
    public DtmfResult Dtmf { get; set; }

    /// <summary>
    ///     The speech recognition result from the caller's voice input.
    /// </summary>
    [JsonProperty("speech")]
    [JsonPropertyName("speech")]
    public SpeechResult Speech { get; set; }

    /// <summary>
    ///     Whether the input action timed out before the caller provided input. Use the timeout information on <see cref="Dtmf"/> or <see cref="Speech"/> instead.
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