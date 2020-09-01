using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    [System.Obsolete("The Nexmo.Api.NumberInsights.NumberInsightResponseBase class is obsolete. " +
        "References to it should be switched to the new Vonage.NumberInsights.NumberInsightResponseBase class.")]
    public class NumberInsightResponseBase
    {
        /// <summary>
        /// Status of the Number Insight Request
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// The unique identifier for your request. This is a alphanumeric string up to 40 characters.
        /// </summary>
        [JsonProperty("request_id")]
        public string RequestId { get; set; }
    }
}
