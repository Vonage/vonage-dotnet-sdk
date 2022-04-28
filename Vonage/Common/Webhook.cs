using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Vonage.Common
{
    public class Webhook
    {
        [JsonProperty("http_method")]
        public string Method { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
        
        public enum Type
        {
            [EnumMember(Value = "answer_url")]
            answer_url = 1,

            [EnumMember(Value = "event_url")]
            event_url = 2,

            [EnumMember(Value = "inbound_url")]
            inbound_url = 3,

            [EnumMember(Value = "status_url")]
            status_url = 4,

            [EnumMember(Value = "fallback_answer_url")]
            fallback_answer_url = 5,

            [EnumMember(Value = "Unknown")]
            Unknown = 6
        }

    }
}