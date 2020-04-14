using Newtonsoft.Json;

namespace Nexmo.Api.Conversions
{
    public class ConversionRequest
    {
        [JsonProperty("message-id")]
        public string MessageId { get; set; }

        [JsonProperty("delivered")]
        public string Delivered { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }
    }
}