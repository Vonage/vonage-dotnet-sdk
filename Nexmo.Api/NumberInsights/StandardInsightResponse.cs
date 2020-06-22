using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nexmo.Api.NumberInsights
{
    public class StandardInsightResponse : BasicInsightResponse
    {
        /// <summary>
        /// The amount in EUR charged to your account.
        /// </summary>
        [JsonProperty("request_price")]
        public string RequestPrice { get; set; }

        /// <summary>
        /// If there is an internal lookup error, the refund_price will reflect the lookup price. 
        /// If cnam is requested for a non-US number the refund_price will reflect the cnam price. 
        /// If both of these conditions occur, refund_price is the sum of the lookup price and cnam price.
        /// </summary>
        [JsonProperty("refund_price")]
        public string RefundPrice { get; set; }

        /// <summary>
        /// Your account balance in EUR after this request. Not returned with Number Insight Advanced Async API.
        /// </summary>
        [JsonProperty("remaining_balance")]
        public string RemainingBalance { get; set; }

        /// <summary>
        /// Information about the network number is currently connected to.
        /// </summary>
        [JsonProperty("current_carrier")]
        public Carrier CurrentCarrier { get; set; }

        /// <summary>
        /// Information about the network number is currently connected to.
        /// </summary>
        [JsonProperty("original_carrier")]
        public Carrier OriginalCarrier { get; set; }

        /// <summary>
        /// If the user has changed carrier for number. 
        /// The assumed status means that the information supplier has 
        /// replied to the request but has not said explicitly that the number is ported.
        /// </summary>
        [JsonProperty("ported")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PortedStatus Ported { get; set; }

        /// <summary>
        /// Information about the roaming status for number. This is applicable to mobile numbers only.
        /// </summary>
        [JsonProperty("roaming")]
        public Roaming Roaming { get; set; }

        /// <summary>
        /// Information about the network number is currently connected to.
        /// </summary>
        [JsonProperty("caller_identity")]
        public CallerId CallerIdentity { get; set; }

        /// <summary>
        /// Full name of the person or business who owns the phone number. 
        /// Unknown if this information is not available. 
        /// This parameter is only present if cnam had a value of true within the request.
        /// </summary>
        [JsonProperty("caller_name")]
        public string CallerName { get; set; }

        /// <summary>
        /// Last name of the person who owns the phone number if the owner is an individual. 
        /// This parameter is only present if cnam had a value of true within the request.
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// First name of the person who owns the phone number if the owner is an individual. 
        /// This parameter is only present if cnam had a value of true within the request.
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// The value will be business if the owner of a phone number is a business. 
        /// If the owner is an individual the value will be consumer. 
        /// The value will be unknown if this information is not available. 
        /// This parameter is only present if cnam had a value of true within the request.
        /// </summary>
        [JsonProperty("caller_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CallerType CallerType { get; set; }
    }
}