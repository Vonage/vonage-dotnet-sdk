#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Webhook event delivered when a transcription becomes available for a recording.
/// </summary>
public class TranscriptionWebhook : EventBase
{
    /// <summary>
    ///     The unique identifier for the conversation.
    /// </summary>
    [JsonProperty("conversation_uuid")]
    [JsonPropertyName("conversation_uuid")]
    public string ConversationUuid { get; set; }

    /// <summary>
    ///     The unique identifier for the recording associated with this transcription.
    /// </summary>
    [JsonProperty("recording_uuid")]
    [JsonPropertyName("recording_uuid")]
    public string RecordingUuid { get; set; }

    /// <summary>
    ///     The status of the transcription. Will be <c>transcribed</c> when the transcription is ready.
    /// </summary>
    [JsonProperty("status")]
    [JsonPropertyName("status")]
    public string Status { get; set; }

    /// <summary>
    ///     The URL from which the transcription result can be downloaded.
    ///     Pass this value to <c>IVoiceClient.GetTranscriptionAsync</c> to retrieve the full transcription.
    /// </summary>
    [JsonProperty("transcription_url")]
    [JsonPropertyName("transcription_url")]
    public string TranscriptionUrl { get; set; }

    /// <summary>
    ///     The type of event. Always <c>record</c> for transcription webhooks.
    /// </summary>
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; }
}

