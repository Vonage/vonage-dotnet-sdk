using Newtonsoft.Json;
using System;

namespace Nexmo.Api.Voice.EventWebhooks
{
    public class Answered : CallStatusEvent
    {
        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        [JsonProperty("rate")]
        public string Rate { get; set; }
        
        [JsonProperty("network")]
        public string Network { get; set; }
    }
}
