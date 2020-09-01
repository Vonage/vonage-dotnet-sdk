using Newtonsoft.Json;
using System;

namespace Nexmo.Api.Voice.EventWebhooks
{
    [System.Obsolete("The Nexmo.Api.Voice.EventWebhooks.Answered class is obsolete. " +
        "References to it should be switched to the new Vonage.Voice.EventWebhooks.Answered class.")]
    public class Answered : CallStatusEvent
    {
        /// <summary>
        /// call start time
        /// </summary>
        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// cost rate for the call
        /// </summary>
        [JsonProperty("rate")]
        public string Rate { get; set; }
        
        /// <summary>
        /// Network the call came from
        /// </summary>
        [JsonProperty("network")]
        public string Network { get; set; }
    }
}
