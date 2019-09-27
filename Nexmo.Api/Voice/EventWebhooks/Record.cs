using System;
using Newtonsoft.Json;

namespace Nexmo.Api.Voice.EventWebhooks
{
    public class Record : Event
    {
        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        [JsonProperty("recording_url")]
        public string RecordingUrl { get; set; }

        [JsonProperty("size")]
        public uint Size { get; set; }

        [JsonProperty("recording_uuid")]
        public override string Uuid { get; set; }

        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }

        [JsonProperty("conversation_uuid")]
        public string ConversationUuid { get; set; }
    }
}
