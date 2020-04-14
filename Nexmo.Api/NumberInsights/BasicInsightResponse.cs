using Newtonsoft.Json;

namespace Nexmo.Api.NumberInsights
{
    public class BasicInsightResponse
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("status_message")]
        public string StatusMessage { get; set; }

        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("international_format_number")]
        public string InternationalFormatNumber { get; set; }

        [JsonProperty("national_format_number")]
        public string NationalFormatNumber { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country_code_iso3")]
        public string CountryCodeIso3 { get; set; }
        
        [JsonProperty("country_name")] 
        public string CountryName { get; set; }
        
        [JsonProperty("country_prefix")] 
        public string CountryPrefix { get; set; }
    }
}