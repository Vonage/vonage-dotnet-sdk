using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Nexmo.Api.Messaging
{
    public class DeliveryReceipt
    {
        [JsonProperty("msisdn")]
        [FromQuery(Name = "msisdn")]
        public string Msisdn { get; set; }
        
        [JsonProperty("to")]
        [FromQuery(Name = "to")]
        public string To { get; set; }
        
        [JsonProperty("network-code")]
        [FromQuery(Name = "network-code")]
        public string NetworkCode { get; set; }
        
        [JsonProperty("messageId")]
        [FromQuery(Name = "messageId")]
        public string MessageId { get; set; }
        
        [JsonProperty("price")]
        [FromQuery(Name = "price")]
        public string Price { get; set; }
        
        [JsonProperty("status")]
        [FromQuery(Name = "status")]
        public string Status { get; set; }
        
        [JsonProperty("scts")]
        [FromQuery(Name = "scts")]
        public string Scts { get; set; }
        
        [JsonProperty("err-code")]
        [FromQuery(Name = "err-code")]
        public string ErrorCode { get; set; }
        
        [JsonProperty("message-timestamp")]
        [FromQuery(Name = "message-timestamp")]
        public string MessageTimestamp { get; set; }
    }
}