using System.Collections.Generic;
using Newtonsoft.Json;
using Nexmo.Api.Request;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Nexmo.Api
{
    public static class NumberInsight
    {
        public class NumberInsightBasicRequest
        {
            [JsonProperty(PropertyName = "number")]
            public string Number { get; set; }
            [JsonProperty(PropertyName = "country")]
            public string Country { get; set; }
        }

        public class NumberInsightBasicResponse
        {
            /// <summary>
            /// 	The status code and a description about your request.
            /// </summary>
            [JsonProperty(PropertyName = "status")]
            public string Status { get; set; }
            [JsonProperty(PropertyName = "status_message")]
            public string StatusMessage { get; set; }
            /// <summary>
            /// The unique identifier for your request. This is a alphanumeric string up to 40 characters.
            /// </summary>
            [JsonProperty(PropertyName = "request_id")]
            public string RequestId { get; set; }
            /// <summary>
            /// The number in your request in International format.
            /// </summary>
            [JsonProperty(PropertyName = "international_format_number")]
            public string InternationalFormatNumber { get; set; }
            /// <summary>
            /// Looked up Number in format used by the country the number belongs to.
            /// </summary>
            [JsonProperty(PropertyName = "national_format_number")]
            public string NationalFormatNumber { get; set; }
            /// <summary>
            /// 	Two character country code for number. This is in ISO 3166-1 alpha-2 format.
            /// </summary>
            [JsonProperty(PropertyName = "country_code")]
            public string CountryCode { get; set; }
            /// <summary>
            /// Two character country code for number. This is in ISO 3166-1 alpha-3 format.
            /// </summary>
            [JsonProperty(PropertyName = "country_code_iso3")]
            public string CountryCodeIso3 { get; set; }
            /// <summary>
            /// The full name of the country that number is registered in.
            /// </summary>
            [JsonProperty(PropertyName = "country_name")]
            public string CountryName { get; set; }
            /// <summary>
            /// The numeric prefix for the country that number is registered in.
            /// </summary>
            [JsonProperty(PropertyName = "country_prefix")]
            public string CountryPrefix { get; set; }
        }

        public class CarrierInfo
        {
            /// <summary>
            /// The MCCMNC for the carrier *number* is associated with.Unreal numbers are marked as 'unknown' and the request is rejected altogether if the number is impossible as per E.164 guidelines.
            /// </summary>
            [JsonProperty(PropertyName = "network_code")]
            public string NetworkCode { get; set; }
            /// <summary>
            /// The full name of the carrier that *number* is associated with.
            /// </summary>
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }
            /// <summary>
            /// The country that *number* is associated with. This is in ISO 3166-1 alpha-2 format.
            /// </summary>
            [JsonProperty(PropertyName = "country")]
            public string Country { get; set; }
            /// <summary>
            /// The type of network that *number* is associated with. For example mobile, landline, virtual, premium, toll-free.
            /// </summary>
            [JsonProperty(PropertyName = "network_type")]
            public string NetworkType { get; set; }
        }

        public class NumberInsightStandardResponse : NumberInsightBasicResponse
        {
            /// <summary>
            /// The amount in EUR charged to your account.
            /// </summary>
            [JsonProperty(PropertyName = "request_price")]
            public string RequestPrice { get; set; }
            /// <summary>
            /// Your account balance in EUR after this request.
            /// </summary>
            [JsonProperty(PropertyName = "remaining_balance")]
            public string RemainingBalance { get; set; }
            /// <summary>
            /// Information about the network number is currently connected to.
            /// </summary>
            [JsonProperty(PropertyName = "current_carrier")]
            public CarrierInfo CurrentCarrier { get; set; }
            /// <summary>
            /// Information about the network number was initially connected to
            /// </summary>
            [JsonProperty(PropertyName = "original_carrier")]
            public CarrierInfo OriginalCarrier { get; set; }
            [JsonProperty(PropertyName = "caller_name")]
            public string CallerName { get; set; }
            [JsonProperty(PropertyName = "first_name")]
            public string FirstName { get; set; }
            [JsonProperty(PropertyName = "last_name")]
            public string LastName { get; set; }
            [JsonProperty(PropertyName = "caller_type")]
            public string CallerType { get; set; }
            [JsonProperty(PropertyName = "ported")]
            public string PortedStatus { get; set; }
        }

        public class NumberInsightAdvancedRequest : NumberInsightBasicRequest
        {
            [JsonProperty(PropertyName = "cnam")]
            public bool Cnam { get; set; }
            [JsonProperty(PropertyName = "ip_address")]
            public string IpAddress { get; set; }
            [JsonProperty(PropertyName = "callback")]
            public string Callback { get; set; }
        }

        public class NumberInsightAdvancedResponse : NumberInsightStandardResponse
        {
            [JsonProperty(PropertyName = "lookup_outcome_message")]
            public string LookupOutcomeMessage { get; set; }
            [JsonProperty(PropertyName = "lookup_outcome")]
            public int LookupOutcome { get; set; }
            [JsonProperty(PropertyName = "valid_number")]
            public string NumberValidity { get; set; }
            [JsonProperty(PropertyName = "reachable")]
            public string NumberReachability { get; set; }
            [JsonProperty(PropertyName = "roaming")]
            public RoamingInformation RoamingInformation { get; set; }
        }

        public class NumberInsightRequest
        {
            public string Number { get; set; }
            public string Callback { get; set; }
        }

        public class NumberInsightRequestResponse
        {
            public string RequestId { get; set; }
            public string Number { get; set; }
            public string Status { get; set; }
            public string ErrorText { get; set; }
            public string RemainingBalance { get; set; }
            public string RequestPrice { get; set; }
        }

        public class NumberInsightResponse
        {
            public string RequestId { get; set; }
            public string Number { get; set; }
            public string Status { get; set; }
            public string StatusMessage { get; set; }
            public string NumberType { get; set; }
            public string CarrierNetworkCode { get; set; }
            public string CarrierNetworkName { get; set; }
            public string CarrierCountryCode { get; set; }
            public string Valid { get; set; }
            public string Ported { get; set; }
            public string Reachable { get; set; }
            public string Roaming { get; set; }
            public string RoamingCountryCode { get; set; }
            public string RoamingNetworkCode { get; set; }
        }

        /// <summary>
        /// Performs basic semantic checks on given phone number.
        /// </summary>
        /// <param name="request">NI request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static NumberInsightBasicResponse RequestBasic(NumberInsightBasicRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/ni/basic/json"), request, creds);

            return JsonConvert.DeserializeObject<NumberInsightBasicResponse>(response.JsonResponse);
        }

        /// <summary>
        /// Identifies the phone number type and, for mobile phone numbers, the network it is registered with.
        /// </summary>
        /// <param name="request">NI request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static NumberInsightStandardResponse RequestStandard(NumberInsightBasicRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/ni/standard/json"), request, creds);

            return JsonConvert.DeserializeObject<NumberInsightStandardResponse>(response.JsonResponse);
        }

        public static NumberInsightAdvancedResponse RequestAdvanced( NumberInsightAdvancedRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/ni/advanced/json"), request, creds);

            return JsonConvert.DeserializeObject<NumberInsightAdvancedResponse>(response.JsonResponse);
        }

        //public static NumberInsightAdvancedResponse RequestAdvancedAsync(NumberInsightAdvancedRequest request, Credentials creds = null)
        //{
        //    var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/ni/advanced/async/json"), request, creds);

        //    return JsonConvert.DeserializeObject<NumberInsightAdvancedResponse>(response.JsonResponse);
        //}

        /// <summary>
        /// Retrieve validity, roaming, and reachability information about a mobile phone number.
        /// </summary>
        /// <param name="request">NI advenced request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static NumberInsightRequestResponse Request(NumberInsightRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(NumberInsight), "/ni/json"), new Dictionary<string, string>
            {
                {"Number", request.Number},
                {"Callback", request.Callback}
            },
            creds);

            return JsonConvert.DeserializeObject<NumberInsightRequestResponse>(response.JsonResponse);
        }

        /// <summary>
        /// Deserializes a NumberInsight response JSON string
        /// </summary>
        /// <param name="json">NumberInsight response JSON string</param>
        /// <returns></returns>
        public static NumberInsightResponse Response(string json)
        {
            return JsonConvert.DeserializeObject<NumberInsightResponse>(json);
        }
    }
}