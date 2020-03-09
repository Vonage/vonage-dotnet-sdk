using Newtonsoft.Json;

namespace Nexmo.Api.MessageSearch
{
    public class RejectionSearchResponse
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        
        [JsonProperty("items")]
        public Rejection[] Items { get; set; }
    }
}