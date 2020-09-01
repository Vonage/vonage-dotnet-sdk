using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    [System.Obsolete("The Nexmo.Api.NumberInsights.StandardNumberInsightRequest class is obsolete. " +
        "References to it should be switched to the new Vonage.NumberInsights.StandardNumberInsightRequest class.")]
    public class StandardNumberInsightRequest : BasicNumberInsightRequest
    {
        /// <summary>
        /// Indicates if the name of the person who owns the phone number should be looked up and returned in the response. 
        /// Set to true to receive phone number owner name in the response. 
        /// This features is available for US numbers only and incurs an additional charge.
        /// </summary>
        [JsonProperty("cnam")]
        public bool? Cnam { get; set; }
    }
}