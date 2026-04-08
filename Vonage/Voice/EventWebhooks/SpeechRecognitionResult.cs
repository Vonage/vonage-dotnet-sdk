#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Represents a single speech recognition hypothesis with its transcript text and confidence score.
/// </summary>
public class SpeechRecognitionResult
{
    /// <summary>
    ///     The transcript text representing the words that the caller spoke.
    /// </summary>
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string Text { get; set; }

    /// <summary>
    ///     The confidence estimate between 0.0 and 1.0. A higher value indicates a greater likelihood that the recognized words are correct.
    /// </summary>
    [JsonProperty("confidence")]
    [JsonPropertyName("confidence")]
    public string Confidence { get; set; }
}