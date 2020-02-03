using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api
{
    public static class NumberInsight
    {
        private const string BASIC = "basic";
        private const string STANDARD = "standard";
        private const string ADVANCED = "advanced";
        public class NumberInsightRequest
        {
            /// <summary>
            /// Required. A single phone number that you need insight about in national or international format. The number may include any or all of the following: white space, -, +, (, ).
            /// </summary>
            [JsonProperty(PropertyName = "number")]
            public string Number { get; set; }
            /// <summary>
            /// Optional. If a number does not have a country code or is uncertain, set the two-character country code. This code must be in ISO 3166-1 alpha-2 format and in upper case. For example, GB or US. If you set country and number is already in E.164  format, country must match the country code in number.
            /// </summary>
            [JsonProperty(PropertyName = "country")]
            public string Country { get; set; }
            /// <summary>
            /// Optional. Indicates if the name of the person who owns the phone number should be looked up and returned in the response. Set to true to receive phone number owner name in the response. This features is available for US numbers only and incurs an additional charge. Default value is false.
            /// </summary>
            [JsonProperty(PropertyName = "cnam")]
            public string CallerIDName { get; set; }
            /// <summary>
            /// Optional. The IP address of the user. If supplied, we will compare this to the country the user's phone is located in and return an error if it does not match.
            /// </summary>
            [JsonProperty(PropertyName = "ip")]
            public string IPAddress { get; set; }
        }

        public class NumberInsightAsyncRequest : NumberInsightRequest
        {
            /// <summary>
            /// Webhook URL used to send NI response to
            /// </summary>
            [JsonProperty(PropertyName = "callback")]
            public string Callback { get; set; }
        }

        public class NumberInsightBasicResponse
        {
            /// <summary>
            /// The status code and a description about your request. When status is 0 or 1,status_message is returned. For all other values,error_text. 
            /// </summary>
            [JsonProperty(PropertyName = "status")]
            public string Status { get; set; }
            [JsonProperty(PropertyName = "status_message")]
            public string StatusMessage { get; set; }
            [JsonProperty(PropertyName = "error_text")]
            public string ErrorText { get; set; }

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
            /// Two character country code for number. This is in ISO 3166-1 alpha-2 format.
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
            /// If there is an internal lookup error, the refund_price will reflect the lookup price. If cnam is requested for a non-US number the refund_price will reflect the cnam price. If both of these conditions occur, refund_price is the sum of the lookup price and cnam price.
            /// </summary>
            [JsonProperty(PropertyName = "refund_price")]
            public string RefundPrice { get; set; }
            /// <summary>
            /// Your account balance in EUR after this request.
            /// </summary>
            [JsonProperty(PropertyName = "remaining_balance")]
            public string RemainingBalance { get; set; }
            /// <summary>
            /// If the user has changed carrier for number. Possible values are: unknown, ported, not_ported, assumed_not_ported, assumed_ported. The assumed status means that the information supplier has replied to the request but has not said explicitly that the number is ported.
            /// </summary>
            [JsonProperty(PropertyName = "ported")]
            public string PortedStatus { get; set; }
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
            /// <summary>
            /// Full name of the person who owns the phone number.unknown if this information is not available. This parameter is only present if cnam had a value of true within the request.
            /// </summary>
            [JsonProperty(PropertyName = "caller_name")]
            public string CallerName { get; set; }
            /// <summary>
            /// First name of the person who owns the phone number if the owner is an individual. This parameter is only present if cnam had a value of true within the request.
            /// </summary>
            [JsonProperty(PropertyName = "first_name")]
            public string FirstName { get; set; }
            /// <summary>
            /// Last name of the person who owns the phone number if the owner is an individual. This parameter is only present if cnam had a value of true within the request.
            /// </summary>
            [JsonProperty(PropertyName = "last_name")]
            public string LastName { get; set; }
            /// <summary>
            /// The value will be business if the owner of a phone number is a business. If the owner is an individual the value will be consumer. The value will be unknown if this information is not available. This parameter is only present if cnam had a value of true within the request.
            /// </summary>
            [JsonProperty(PropertyName = "caller_type")]
            public string CallerType { get; set; }
        }

        public class NumberInsightAdvancedResponse : NumberInsightStandardResponse
        {
            /// <summary>
            /// Shows if all information about a phone number has been returned.
            /// </summary>
            [JsonProperty(PropertyName = "lookup_outcome")]
            public int LookupOutcome { get; set; }
            [JsonProperty(PropertyName = "lookup_outcome_message")]
            public string LookupOutcomeMessage { get; set; }

            /// <summary>
            /// Does number exist. Possible values are unknown, valid, not_valid. This is applicable to mobile numbers only.
            /// </summary>
            [JsonProperty(PropertyName = "valid_number")]
            public string NumberValidity { get; set; }
            /// <summary>
            /// Can you call number now. Possible values are: unknown, reachable, undeliverable, absent, bad_number, blacklisted. This is applicable to mobile numbers only.
            /// </summary>
            [JsonProperty(PropertyName = "reachable")]
            public string NumberReachability { get; set; }
            /// <summary>
            /// Information about the roaming status for number. Possible values. This is applicable to mobile numbers only.
            /// </summary>
            [JsonProperty(PropertyName = "roaming")]
            public Roaming RoamingInformation { get; set; }
            /// <summary>
            /// The ip address you specified in the request. This field is blank if you did not specify ip.
            /// </summary>
            public string ip { get; set; }
            /// <summary>
            /// Warning levels for ip: unknown or no_warning
            /// </summary>
            public string ip_warnings { get; set; }
            /// <summary>
            /// The match status between ip and number. Possible values are. Country Level or Mismatch. This value is only returned if you set ip in the request.
            /// </summary>
            public string ip_match_level { get; set; }
            /// <summary>
            /// The country that ip is allocated to. This value is only returned if you set ip in the request.
            /// </summary>
            public string ip_country { get; set; }
        }

    public class Roaming
    {
        public string status { get; set; }
        public string roaming_country_code { get; set; }
        public string roaming_network_code { get; set; }
        public string roaming_network_name { get; set; }
    }

    public class NumberInsightAsyncRequestResponse
        {
            public string request_id { get; set; }
            public string number { get; set; }
            public string status { get; set; }
            public string error_text { get; set; }
            public string remaining_balance { get; set; }
            public string request_price { get; set; }
        }

        /// <summary>
        /// Performs basic semantic checks on given phone number.
        /// </summary>
        /// <param name="request">NI request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static NumberInsightBasicResponse RequestBasic(NumberInsightRequest request, Credentials creds = null)
        {
            return SendSynchronousInsightRequest<NumberInsightBasicResponse>(request, BASIC, creds);
        }

        /// <summary>
        /// Identifies the phone number type and, for mobile phone numbers, the network it is registered with.
        /// </summary>
        /// <param name="request">NI standard request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static NumberInsightStandardResponse RequestStandard(NumberInsightRequest request, Credentials creds = null)
        {
            return SendSynchronousInsightRequest<NumberInsightStandardResponse>(request, STANDARD, creds);
        }

        /// <summary>
        /// Retrieve validity, roaming, and reachability information about a mobile phone number.
        /// </summary>
        /// <param name="request">NI advenced request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NumberInishghtRequestException">Thrown when response holds a bad status</exception>
        /// <returns></returns>
        public static NumberInsightAdvancedResponse RequestAdvanced(NumberInsightRequest request, Credentials creds = null)
        {
            return SendSynchronousInsightRequest<NumberInsightAdvancedResponse>(request, ADVANCED, creds);
        }

        public static T SendSynchronousInsightRequest<T>(NumberInsightRequest request, string level, Credentials creds = null) where T : NumberInsightBasicResponse
        {
            var response = ApiRequest.DoPostRequest<T>(ApiRequest.GetBaseUriFor(typeof(NumberVerify), $"/ni/{level}/json"), request, creds);
            if (response?.Status != "0")
            {
                throw new NumberInsightResponseException($"Number Inisght Request failed with status of: {response?.Status} and with an error message of {response?.StatusMessage}");
            }
            return response;
        }

        /// <summary>
        /// Retrieve validity, roaming, and reachability information about a mobile phone number via a webhook
        /// </summary>
        /// <param name="request">NI advenced request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static NumberInsightAsyncRequestResponse RequestAsync(NumberInsightAsyncRequest request, Credentials creds = null)
        {
            var parameters = new Dictionary<string, string>
            {
                {"number", request.Number},
                {"callback", request.Callback}
            };

            if (!string.IsNullOrEmpty(request.Country))
            {
                parameters.Add("country", request.Country);
            }
            if (!string.IsNullOrEmpty(request.CallerIDName))
            {
                parameters.Add("cnam", request.CallerIDName);
            }
            if (!string.IsNullOrEmpty(request.IPAddress))
            {
                parameters.Add("ip", request.IPAddress);
            }

            return ApiRequest.DoPostRequest<NumberInsightAsyncRequestResponse>(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/ni/advanced/async/json"), parameters, creds);
        }

        public class NumberInsightResponseException : Exception
        {
            public NumberInsightResponseException(string message) : base(message) { }
        }
    }
}