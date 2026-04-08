#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Represents the results from speech recognition during a multi-input NCCO action, including recognition hypotheses, errors, and timeout information.
/// </summary>
public class SpeechResult
{
    /// <summary>
    ///     An error message if a problem occurred during speech recognition. Not present when recognition succeeds.
    /// </summary>
    [JsonProperty("error")]
    [JsonPropertyName("error")]
    public string Error { get; set; }

    /// <summary>
    ///     The array of speech recognition hypotheses, ordered by confidence from highest to lowest.
    /// </summary>
    [JsonProperty("results")]
    [JsonPropertyName("results")]
    public SpeechRecognitionResult[] SpeechResults { get; set; }

    /// <summary>
    ///     The reason the speech input ended. Possible values: <c>end_on_silence_timeout</c> (caller stopped speaking), <c>max_duration</c> (maximum duration reached), or <c>start_timeout</c> (caller did not speak).
    /// </summary>
    [JsonProperty("timeout_reason")]
    [JsonPropertyName("timeout_reason")]
    public string TimeoutReason { get; set; }
}