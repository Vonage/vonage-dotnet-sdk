using System.Collections.Generic;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api
{
    public static class NumberInsight
    {
        public class NumberInsightBasicRequest
        {
            public string number { get; set; }
            public string country { get; set; }
        }

        public class NumberInsightBasicResponse
        {
            /// <summary>
            /// 	The status code and a description about your request.
            /// </summary>
            public string status { get; set; }
            public string status_message { get; set; }
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
            /// 	Two character country code for number. This is in ISO 3166-1 alpha-2 format.
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
            /// Your account balance in EUR after this request.
            /// </summary>
            public string remaining_balance { get; set; }
            /// <summary>
            /// Information about the network number is currently connected to.
            /// </summary>
            public CarrierInfo current_carrier { get; set; }
            /// <summary>
            /// Information about the network number was initially connected to
            /// </summary>
            public CarrierInfo original_carrier { get; set; }
        }

        public class NumberInsightRequest
        {
            public string Number { get; set; }
            public string Callback { get; set; }
        }

        public class NumberInsightRequestResponse
        {
            public string request_id { get; set; }
            public string number { get; set; }
            public string status { get; set; }
            public string error_text { get; set; }
            public string remaining_balance { get; set; }
            public string request_price { get; set; }
        }

        public class NumberInsightResponse
        {
            public string request_id { get; set; }
            public string number { get; set; }
            public string status { get; set; }
            public string status_message { get; set; }
            public string number_type { get; set; }
            public string carrier_network_code { get; set; }
            public string carrier_network_name { get; set; }
            public string carrier_country_code { get; set; }
            public string valid { get; set; }
            public string ported { get; set; }
            public string reachable { get; set; }
            public string roaming { get; set; }
            public string roaming_country_code { get; set; }
            public string roaming_network_code { get; set; }
        }

        public static NumberInsightBasicResponse RequestBasic(NumberInsightBasicRequest request)
        {
            var jsonstring = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/number/format/json"), request);

            return JsonConvert.DeserializeObject<NumberInsightBasicResponse>(jsonstring);
        }

        public static NumberInsightStandardResponse RequestStandard(NumberInsightBasicRequest request)
        {
            var jsonstring = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/number/lookup/json"), request);

            return JsonConvert.DeserializeObject<NumberInsightStandardResponse>(jsonstring);
        }

        public static NumberInsightRequestResponse Request(NumberInsightRequest request)
        {
            var jsonstring = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(NumberInsight), "/ni/json"), new Dictionary<string, string>
            {
                {"number", request.Number},
                {"callback", request.Callback}
            });

            return JsonConvert.DeserializeObject<NumberInsightRequestResponse>(jsonstring);
        }

        public static NumberInsightResponse Response(string json)
        {
            return JsonConvert.DeserializeObject<NumberInsightResponse>(json);
        }
    }
}