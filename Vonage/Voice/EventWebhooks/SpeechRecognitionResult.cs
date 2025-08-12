#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
/// </summary>
public class SpeechRecognitionResult
{
    /// <summary>
    /// Transcript text representing the words that the user spoke.
    /// </summary>
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string Text { get; set; }

    /// <summary>
    /// The confidence estimate between 0.0 and 1.0. A higher number indicates an estimated greater likelihood that the recognized words are correct.
    /// </summary>
    [JsonProperty("confidence")]
    [JsonPropertyName("confidence")]
    public string Confidence { get; set; }
}