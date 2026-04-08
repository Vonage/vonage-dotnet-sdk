#region
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.EventWebhooks;

/// <summary>
///     Webhook event received when a call recording is completed and available for download.
/// </summary>
public class Record : Event
{
    /// <summary>
    ///     The timestamp when the recording started, in ISO 8601 format.
    /// </summary>
    [JsonProperty("start_time")]
    [JsonPropertyName("start_time")]
    public DateTime StartTime { get; set; }

    /// <summary>
    ///     The URL from which the recording can be downloaded. Pass this to <see cref="IVoiceClient.GetRecordingAsync"/> to retrieve the audio data.
    /// </summary>
    [JsonProperty("recording_url")]
    [JsonPropertyName("recording_url")]
    public string RecordingUrl { get; set; }

    /// <summary>
    ///     The size of the recording file in bytes.
    /// </summary>
    [JsonProperty("size")]
    [JsonPropertyName("size")]
    public uint Size { get; set; }

    /// <summary>
    ///     The unique identifier for this recording. Maps to the <c>recording_uuid</c> field in the webhook payload.
    /// </summary>
    [JsonProperty("recording_uuid")]
    [JsonPropertyName("recording_uuid")]
    public override string Uuid { get; set; }

    /// <summary>
    ///     The timestamp when the recording ended, in ISO 8601 format.
    /// </summary>
    [JsonProperty("end_time")]
    [JsonPropertyName("end_time")]
    public DateTime EndTime { get; set; }

    /// <summary>
    ///     The unique identifier for the conversation associated with this recording.
    /// </summary>
    [JsonProperty("conversation_uuid")]
    [JsonPropertyName("conversation_uuid")]
    public string ConversationUuid { get; set; }
}