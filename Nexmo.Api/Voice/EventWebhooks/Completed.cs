using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nexmo.Api.Voice.EventWebhooks
{
    public class Completed : CallStatusEvent
    {
        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }

        [JsonProperty("network")]
        public string Network { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        [JsonProperty("rate")]
        public string Rate { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }
    }
}
