using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    [System.Obsolete("The Nexmo.Api.NumberInsights.BasicInsightResponse class is obsolete. " +
        "References to it should be switched to the new Vonage.NumberInsights.BasicInsightResponse class.")]
    public class BasicInsightResponse : NumberInsightResponseBase
    {
        /// <summary>
        /// The status description of your request.
        /// </summary>
        [JsonProperty("status_message")]
        public string StatusMessage { get; set; }

        /// <summary>
        /// The number in your request in international format.
        /// </summary>
        [JsonProperty("international_format_number")]
        public string InternationalFormatNumber { get; set; }

        /// <summary>
        /// The number in your request in the format used by the country the number belongs to.
        /// </summary>
        [JsonProperty("national_format_number")]
        public string NationalFormatNumber { get; set; }

        /// <summary>
        /// Two character country code for number. This is in ISO 3166-1 alpha-2 format.
        /// </summary>
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Three character country code for number. This is in ISO 3166-1 alpha-3 format.
        /// </summary>
        [JsonProperty("country_code_iso3")]
        public string CountryCodeIso3 { get; set; }

        /// <summary>
        /// The full name of the country that number is registered in.
        /// </summary>
        [JsonProperty("country_name")] 
        public string CountryName { get; set; }

        /// <summary>
        /// The numeric prefix for the country that number is registered in.
        /// </summary>
        [JsonProperty("country_prefix")] 
        public string CountryPrefix { get; set; }
    }
}