using System.Collections.Generic;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api
{
    public static class NumberVerify
    {
        public class VerifyRequest
        {
            public string number { get; set; }
            public string brand { get; set; }
            public string country { get; set; }
            public string sender_id { get; set; }
            public string code_length { get; set; }
            public string lg { get; set; }
            public string require_type { get; set; }
            public string pin_expiry { get; set; }
            public string next_event_wait { get; set; }
        }

        public class VerifyResponse
        {
            public string request_id { get; set; }
            public string status { get; set; }
            public string error_text { get; set; }
        }

        public static VerifyResponse Verify(VerifyRequest request, Credentials creds = null)
        {
            var jsonstring = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/verify/json"), request, creds);

            return JsonConvert.DeserializeObject<VerifyResponse>(jsonstring);
        }

        public class CheckRequest
        {
            public string request_id { get; set; }
            public string code { get; set; }
        }

        public class CheckResponse
        {
            public string event_id { get; set; }
            public string status { get; set; }
            public string price { get; set; }
            public string currency { get; set; }
            public string error_text { get; set; }
        }

        public static CheckResponse Check(CheckRequest request, Credentials creds = null)
        {
            var jsonstring = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/verify/check/json"), new Dictionary<string, string>
            {
                {"request_id", request.request_id},
                {"code", request.code}
            },
            creds);

            return JsonConvert.DeserializeObject<CheckResponse>(jsonstring);
        }

        public class SearchRequest
        {
            public string request_id { get; set; }
            public string request_ids { get; set; }
        }

        public class SearchResponse
        {
            public string request_id { get; set; }
            public string account_id { get; set; }
            public string number { get; set; }
            public string sender_id { get; set; }
            public string date_submitted { get; set; }
            public string date_finalized { get; set; }
            public string first_event_date { get; set; }
            public string last_event_date { get; set; }
            public string status { get; set; }
            public string price { get; set; }
            public string currency { get; set; }
            public string error_text { get; set; }
            public CheckObj[] checks { get; set; }
        }

        public class CheckObj
        {
            public string date_received { get; set; }
            public string code { get; set; }
            public string status { get; set; }
            public string ip_address { get; set; }
        }

        public static SearchResponse Search(SearchRequest request, Credentials creds = null)
        {
            var jsonstring = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/verify/search/json"), new Dictionary<string, string>()
            {
                {"request_id", request.request_id},
                {"request_ids", request.request_ids}
            },
            creds);

            return JsonConvert.DeserializeObject<SearchResponse>(jsonstring);
        }

        public class ControlRequest
        {
            /// <summary>
            /// The request_id you received in the Verify Request Response
            /// </summary>
            public string request_id { get; set; }
            /// <summary>
            /// Change the command workflow. Supported values are:
            ///   cancel - stop the request
            ///   trigger_next_event - advance the request to the next part of the process.
            /// </summary>
            public string cmd { get; set; }
        }

        public class ControlResponse
        {
            /// <summary>
            /// The Verify Control Response code) that explains how your request proceeded
            /// </summary>
            public string status { get; set; }
            /// <summary>
            /// The cmd you sent in the request.
            /// </summary>
            public string command { get; set; }
        }

        public static ControlResponse Control(ControlRequest request, Credentials creds = null)
        {
            var jsonstring = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/verify/control/json"), request, creds);

            return JsonConvert.DeserializeObject<ControlResponse>(jsonstring);
        }
}
}
