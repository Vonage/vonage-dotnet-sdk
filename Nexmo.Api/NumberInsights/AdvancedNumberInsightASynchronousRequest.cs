using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    public class AdvancedNumberInsightAsynchronousRequest : AdvancedNumberInsightRequest
    {
        [JsonProperty("callback")]
        public string Callback { get; set; }
    }
}