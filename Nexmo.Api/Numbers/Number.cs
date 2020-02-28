using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nexmo.Api.Numbers
{
    public class Number
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        
        [JsonProperty("msisdn")]
        public string Msisdn { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("features")]
        public IEnumerable<string> Features { get; set; }
        
        [JsonProperty("cost")]
        public decimal Cost { get; set; }
    }
}