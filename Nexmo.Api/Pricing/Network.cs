using Newtonsoft.Json;

namespace Nexmo.Api.Pricing
{
    public class Network
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("price")]
        public string Price { get; set; }
        
        [JsonProperty("currency")] 
        public string Currency { get; set; }
        
        [JsonProperty("mcc")] 
        public string Mcc { get; set; }
        
        [JsonProperty("mnc")]
        public string Mnc { get; set; }
        
        [JsonProperty("networkCode")]
        public string NetworkCode { get; set; }
        
        [JsonProperty("networkName")]
        public string NetworkName { get; set; }
    }
}