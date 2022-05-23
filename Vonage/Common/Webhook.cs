using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vonage.Common
{
    public class Webhook
    {
        [JsonProperty("http_method")]
        public string Method { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public enum Type
        {
            [EnumMember(Value = "answer_url")]
            AnswerUrl = 1,

            [EnumMember(Value = "event_url")]
            EventUrl = 2,

            [EnumMember(Value = "inbound_url")]
            InboundUrl = 3,

            [EnumMember(Value = "status_url")]
            StatusUrl = 4,

            [EnumMember(Value = "fallback_answer_url")]
            FallbackAnswerUrl = 5,

            [EnumMember(Value = "Unknown")]
            Unknown = 6
        }

    }
}