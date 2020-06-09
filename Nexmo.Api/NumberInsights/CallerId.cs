using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nexmo.Api.NumberInsights
{
    public class CallerId
    {
        /// <summary>
        /// The value will be business if the owner of a phone number is a business. 
        /// If the owner is an individual the value will be consumer. 
        /// The value will be unknown if this information is not available. 
        /// This parameter is only present if cnam had a value of true within the request.
        /// </summary>
        [JsonProperty("caller_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CallerType CallerType { get; set; }

        /// <summary>
        /// Full name of the person or business who owns the phone number. 
        /// Unknown if this information is not available. 
        /// This parameter is only present if cnam had a value of true within the request.
        /// </summary>
        [JsonProperty("caller_name")]
        public string CallerName { get; set; }

        /// <summary>
        /// First name of the person who owns the phone number if the owner is an individual. 
        /// This parameter is only present if cnam had a value of true within the request.
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the person who owns the phone number if the owner is an individual. 
        /// This parameter is only present if cnam had a value of true within the request.
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}