using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api
{
    public static class NumberInsight
    {
        public class NumberInsightRequest
        {
            /// <summary>
            /// Required. A single phone number that you need insight about in national or international format. The number may include any or all of the following: white space, -, +, (, ).
            /// </summary>
            public string number { get; set; }
            /// <summary>
            /// Optional. If a number does not have a country code or is uncertain, set the two-character country code. This code must be in ISO 3166-1 alpha-2 format and in upper case. For example, GB or US. If you set country and number is already in E.164  format, country must match the country code in number.
            /// </summary>
            public string country { get; set; }
            /// <summary>
            /// Optional. Indicates if the name of the person who owns the phone number should be looked up and returned in the response. Set to true to receive phone number owner name in the response. This features is available for US numbers only and incurs an additional charge. Default value is false.
            /// </summary>
            public string cnam { get; set; }
            /// <summary>
            /// Optional. The IP address of the user. If supplied, we will compare this to the country the user's phone is located in and return an error if it does not match.
            /// </summary>
            public string ip { get; set; }
        }

        public class NumberInsightAsyncRequest : NumberInsightRequest
        {
            /// <summary>
            /// Webhook URL used to send NI response to
            /// </summary>
            public string callback { get; set; }
        }

        public class NumberInsightBasicResponse
        {
            /// <summary>
            /// The status code and a description about your request. When status is 0 or 1,status_message is returned. For all other values,error_text. 
            /// </summary>
            public string status { get; set; }
            public string status_message { get; set; }
            public string error_text { get; set; }

            /// <summary>
            /// The unique identifier for your request. This is a alphanumeric string up to 40 characters.
            /// </summary>
            public string request_id { get; set; }
            /// <summary>
            /// The number in your request in International format.
            /// </summary>
            public string international_format_number { get; set; }
            /// <summary>
            /// Looked up Number in format used by the country the number belongs to.
            /// </summary>
            public string national_format_number { get; set; }
            /// <summary>
            /// Two character country code for number. This is in ISO 3166-1 alpha-2 format.
            /// </summary>
            public string country_code { get; set; }
            /// <summary>
            /// Two character country code for number. This is in ISO 3166-1 alpha-3 format.
            /// </summary>
            public string country_code_iso3 { get; set; }
            /// <summary>
            /// The full name of the country that number is registered in.
            /// </summary>
            public string country_name { get; set; }
            /// <summary>
            /// The numeric prefix for the country that number is registered in.
            /// </summary>
            public string country_prefix { get; set; }
        }

        public class CarrierInfo
        {
            /// <summary>
            /// The MCCMNC for the carrier *number* is associated with.Unreal numbers are marked as 'unknown' and the request is rejected altogether if the number is impossible as per E.164 guidelines.
            /// </summary>
            public string network_code { get; set; }
            /// <summary>
            /// The full name of the carrier that *number* is associated with.
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// The country that *number* is associated with. This is in ISO 3166-1 alpha-2 format.
            /// </summary>
            public string country { get; set; }
            /// <summary>
            /// The type of network that *number* is associated with. For example mobile, landline, virtual, premium, toll-free.
            /// </summary>
            public string network_type { get; set; }
        }

        public class NumberInsightStandardResponse : NumberInsightBasicResponse
        {
            /// <summary>
            /// The amount in EUR charged to your account.
            /// </summary>
            public string request_price { get; set; }
            /// <summary>
            /// If there is an internal lookup error, the refund_price will reflect the lookup price. If cnam is requested for a non-US number the refund_price will reflect the cnam price. If both of these conditions occur, refund_price is the sum of the lookup price and cnam price.
            /// </summary>
            public string refund_price { get; set; }
            /// <summary>
            /// Your account balance in EUR after this request.
            /// </summary>
            public string remaining_balance { get; set; }
            /// <summary>
            /// If the user has changed carrier for number. Possible values are: unknown, ported, not_ported, assumed_not_ported, assumed_ported. The assumed status means that the information supplier has replied to the request but has not said explicitly that the number is ported.
            /// </summary>
            public string ported { get; set; }
            /// <summary>
            /// Information about the network number is currently connected to.
            /// </summary>
            public CarrierInfo current_carrier { get; set; }
            /// <summary>
            /// Information about the network number was initially connected to
            /// </summary>
            public CarrierInfo original_carrier { get; set; }
            /// <summary>
            /// Full name of the person who owns the phone number.unknown if this information is not available. This parameter is only present if cnam had a value of true within the request.
            /// </summary>
            public string caller_name { get; set; }
            /// <summary>
            /// First name of the person who owns the phone number if the owner is an individual. This parameter is only present if cnam had a value of true within the request.
            /// </summary>
            public string first_name { get; set; }
            /// <summary>
            /// Last name of the person who owns the phone number if the owner is an individual. This parameter is only present if cnam had a value of true within the request.
            /// </summary>
            public string last_name { get; set; }
            /// <summary>
            /// The value will be business if the owner of a phone number is a business. If the owner is an individual the value will be consumer. The value will be unknown if this information is not available. This parameter is only present if cnam had a value of true within the request.
            /// </summary>
            public string caller_type { get; set; }
        }

        public class NumberInsightAdvancedResponse : NumberInsightStandardResponse
        {
            /// <summary>
            /// Shows if all information about a phone number has been returned.
            /// </summary>
            public string lookup_outcome { get; set; }
            public string lookup_outcome_message { get; set; }

            /// <summary>
            /// Does number exist. Possible values are unknown, valid, not_valid. This is applicable to mobile numbers only.
            /// </summary>
            public string valid_number { get; set; }
            /// <summary>
            /// Can you call number now. Possible values are: unknown, reachable, undeliverable, absent, bad_number, blacklisted. This is applicable to mobile numbers only.
            /// </summary>
            public string reachable { get; set; }
            /// <summary>
            /// Information about the roaming status for number. Possible values. This is applicable to mobile numbers only.
            /// </summary>
            public string roaming { get; set; }
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
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/ni/basic/json"), request, creds);

            return JsonConvert.DeserializeObject<NumberInsightBasicResponse>(response.JsonResponse);
        }

        /// <summary>
        /// Identifies the phone number type and, for mobile phone numbers, the network it is registered with.
        /// </summary>
        /// <param name="request">NI standard request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static NumberInsightStandardResponse RequestStandard(NumberInsightRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/ni/standard/json"), request, creds);

            return JsonConvert.DeserializeObject<NumberInsightStandardResponse>(response.JsonResponse);
        }

        /// <summary>
        /// Retrieve validity, roaming, and reachability information about a mobile phone number.
        /// </summary>
        /// <param name="request">NI advenced request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static NumberInsightAdvancedResponse RequestAdvanced(NumberInsightRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/ni/advanced/json"), request, creds);

            return JsonConvert.DeserializeObject<NumberInsightAdvancedResponse>(response.JsonResponse);
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
                {"number", request.number},
                {"callback", request.callback}
            };

            if (!string.IsNullOrEmpty(request.country))
            {
                parameters.Add("country", request.country);
            }
            if (!string.IsNullOrEmpty(request.cnam))
            {
                parameters.Add("cnam", request.cnam);
            }
            if (!string.IsNullOrEmpty(request.ip))
            {
                parameters.Add("ip", request.cnam);
            }

            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/ni/advanced/async/json"), parameters, creds);

            return JsonConvert.DeserializeObject<NumberInsightAsyncRequestResponse>(response.JsonResponse);
        }   

        /// <summary>
        /// Deserializes a NumberInsight response JSON string
        /// </summary>
        /// <param name="json">NumberInsight response JSON string</param>
        /// <returns></returns>
        public static NumberInsightAdvancedResponse Response(string json)
        {
            return JsonConvert.DeserializeObject<NumberInsightAdvancedResponse>(json);
        }
    }
}