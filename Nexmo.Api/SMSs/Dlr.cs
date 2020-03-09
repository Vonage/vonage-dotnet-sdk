using Newtonsoft.Json;

namespace Nexmo.Api.Messaging
{
    public class Dlr
    {
        [JsonProperty("msisdn")]
        public string Msisdn { get; set; }
        
        [JsonProperty("to")]
        public string To { get; set; }
        
        [JsonProperty("network-code")]
        public string NetworkCode { get; set; }
        
        [JsonProperty("messageId")]
        public string MessageId { get; set; }
        
        [JsonProperty("price")]
        public string Price { get; set; }
        
        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("scts")]
        public string Scts { get; set; }
        
        [JsonProperty("err-code")]
        public string ErrorCode { get; set; }
        
        [JsonProperty("message-timestamp")]
        public string MessageTimestamp { get; set; }
    }
}