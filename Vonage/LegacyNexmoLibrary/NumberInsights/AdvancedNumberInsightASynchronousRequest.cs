using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    [System.Obsolete("The Nexmo.Api.NumberInsights.AdvancedNumberInsightAsynchronousRequest class is obsolete. " +
        "References to it should be switched to the new Vonage.NumberInsights.AdvancedNumberInsightAsynchronousRequest class.")]
    public class AdvancedNumberInsightAsynchronousRequest : AdvancedNumberInsightRequest
    {
        /// <summary>
        /// The callback URL
        /// </summary>
        [JsonProperty("callback")]
        public string Callback { get; set; }
    }
}