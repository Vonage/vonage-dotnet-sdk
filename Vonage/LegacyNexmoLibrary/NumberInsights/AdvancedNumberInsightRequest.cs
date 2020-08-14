using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    [System.Obsolete("The Nexmo.Api.NumberInsights.AdvancedNumberInsightRequest class is obsolete. " +
        "References to it should be switched to the new Vonage.NumberInsights.AdvancedNumberInsightRequest class.")]
    public class AdvancedNumberInsightRequest : StandardNumberInsightRequest
    {
        /// <summary>
        /// This parameter is deprecated as we are no longer able to retrieve reliable IP data globally from carriers.
        /// </summary>
        [JsonProperty("ip")]
        public string Ip { get; set; }
    }
}