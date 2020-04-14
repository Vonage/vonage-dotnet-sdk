using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    public class BasicNumberInsightRequest
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}