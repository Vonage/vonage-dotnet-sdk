using System.Collections.Generic;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api
{
    public static class Number
    {
        public class SearchRequest
        {
            /// <summary>
            /// Required. Country code. Ex: CA
            /// </summary>
            public string country { get; set; }
            /// <summary>
            /// Optional. A matching pattern. Ex: 888
            /// </summary>
            public string pattern { get; set; }
            /// <summary>
            /// Optional. Strategy for matching pattern. Expected values: 0 "starts with" (default), 1 "anywhere", 2 "ends with".
            /// </summary>
            public string search_pattern { get; set; }
            /// <summary>
            /// Optional. Available features are SMS and VOICE, use a comma-separated values. Ex: SMS,VOICE
            /// </summary>
            public string features { get; set; }
            /// <summary>
            /// Optional. Page index (>0, default 1). Ex: 2
            /// </summary>
            public string index { get; set; }
            /// <summary>
            /// Optional. Page size (max 100, default 10). Ex: 25
            /// </summary>
            public string size { get; set; }
        }

        public class NumberUpdateCommand
        {
            /// <summary>
            /// Required. The two character country code in ISO 3166-1 alpha-2 format.
            /// </summary>
            public string country { get; set; }
            /// <summary>
            /// Required. An available inbound virtual number. For example, 447700900000.
            /// </summary>
            public string msisdn { get; set; }
            /// <summary>
            /// Optional. An URL encoded URI to the webhook endpoint endpoint that handles inbound messages. Your webhook endpoint must be active before you make this request, Nexmo makes a GET request to your endpoint and checks that it returns a 200 OK response. Set to empty string to clear.
            /// </summary>
            public string moHttpUrl { get; set; }
            /// <summary>
            /// Optional. The associated system type for your SMPP client. For example inbound.
            /// </summary>
            public string moSmppSysType { get; set; }
            /// <summary>
            /// Optional. The voice webhook type. Possible values are sip, tel, vxml (VoiceXML) or app
            /// </summary>
            public string voiceCallbackType { get; set; }
            /// <summary>
            /// Required if voiceCallbackType is specified. A URI for your voiceCallbackType or an Application ID
            /// </summary>
            public string voiceCallbackValue { get; set; }
            /// <summary>
            /// Optional. Nexmo sends a request to this webhook endpoint when a call ends.
            /// </summary>
            public string voiceStatusCallback { get; set; }
        }

        public class SearchResult
        {
            public string country { get; set; }
            public string msisdn { get; set; }
            public string type { get; set; }
            public IEnumerable<string> features { get; set; }
            public decimal cost { get; set; }
        }

        public class SearchResults
        {
            public int count { get; set; }
            public IEnumerable<SearchResult> numbers { get; set; }
        }

        /// <summary>
        /// Retrieve the list of virtual numbers available for a specific country.
        /// </summary>
        /// <param name="request">Search filter</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static SearchResults Search(SearchRequest request, Credentials creds = null)
        {
            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Number), "/number/search/"), request, creds);
            return JsonConvert.DeserializeObject<SearchResults>(json);
        }

        /// <summary>
        /// Rent a specific virtual number.
        /// </summary>
        /// <param name="country">ISO 3166-1 alpha-2 country code</param>
        /// <param name="number">Number to rent</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static ResponseBase Buy(string country, string number, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Number), "/number/buy"), new Dictionary<string, string>
            {
                {"country", country},
                {"msisdn", number}
            },
            creds);

            return JsonConvert.DeserializeObject<ResponseBase>(response.JsonResponse);
        }

        /// <summary>
        /// Change the webhook endpoints associated with a rented virtual number or associate a virtual number with an Application.
        /// </summary>
        /// <param name="cmd">Update request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static ResponseBase Update(NumberUpdateCommand cmd, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Number), "/number/update"), cmd, creds);

            return JsonConvert.DeserializeObject<ResponseBase>(response.JsonResponse);
        }

        /// <summary>
        /// Cancel your rental of a specific virtual number.
        /// </summary>
        /// <param name="country">ISO 3166-1 alpha-2 country code</param>
        /// <param name="number">The number to cancel</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static ResponseBase Cancel(string country, string number, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Number), "/number/cancel"), new Dictionary<string, string>
            {
                {"country", country},
                {"msisdn", number}
            },
            creds);

            return JsonConvert.DeserializeObject<ResponseBase>(response.JsonResponse);
        }
    }
}