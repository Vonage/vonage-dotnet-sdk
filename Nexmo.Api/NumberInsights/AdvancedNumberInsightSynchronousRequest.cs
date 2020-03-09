using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    public class AdvancedNumberInsightSynchronousRequest : StandardNumberInsightRequest
    {
        [JsonProperty("ip")]
        public string Ip { get; set; }
    }
}