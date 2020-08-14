using System;
using Newtonsoft.Json;

namespace Nexmo.Api.Voice.EventWebhooks
{
    [System.Obsolete("The Nexmo.Api.Voice.EventWebhooks.Record class is obsolete. " +
        "References to it should be switched to the new Vonage.Voice.EventWebhooks.Record class.")]
    public class Record : Event
    {
        /// <summary>
        /// Timestamp (ISO 8601 format) of the start time of the call
        /// </summary>
        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Where to download the recording from
        /// </summary>
        [JsonProperty("recording_url")]
        public string RecordingUrl { get; set; }

        /// <summary>
        /// The size of the recording file (in bytes)
        /// </summary>
        [JsonProperty("size")]
        public uint Size { get; set; }

        /// <summary>
        /// A unique identifier for this recording
        /// </summary>
        [JsonProperty("recording_uuid")]
        public override string Uuid { get; set; }

        /// <summary>
        /// Timestamp (ISO 8601 format) of the end time of the call
        /// </summary>
        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// The unique identifier for this conversation
        /// </summary>
        [JsonProperty("conversation_uuid")]
        public string ConversationUuid { get; set; }
    }
}
