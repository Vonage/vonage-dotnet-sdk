using Newtonsoft.Json;

namespace Vonage.NumberInsights
{
    public class BasicNumberInsightRequest
    {
        /// <summary>
        /// A single phone number that you need insight about in national or international format.
        /// </summary>
        [JsonProperty("number")]
        public string Number { get; set; }

        /// <summary>
        /// If a number does not have a country code or is uncertain, set the two-character country code. 
        /// This code must be in ISO 3166-1 alpha-2 format and in upper case. For example, GB or US. 
        /// If you set country and number is already in E.164 format, country must match the country code in number.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }
    }
}