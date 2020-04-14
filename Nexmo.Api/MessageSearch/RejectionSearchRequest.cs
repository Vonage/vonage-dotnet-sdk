using Newtonsoft.Json;

namespace Nexmo.Api.MessageSearch
{
    public class RejectionSearchRequest
    {
        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }
    }
}