using System.Collections.Generic;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api.ClientMethods
{
    public class NumberInsight
    {
        public Credentials Credentials { get; set; }
        public NumberInsight(Credentials creds)
        {
            Credentials = creds;
        }

        /// <summary>
        /// Performs basic semantic checks on given phone number.
        /// </summary>
        /// <param name="request">NI request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public Api.NumberInsight.NumberInsightBasicResponse RequestBasic(Api.NumberInsight.NumberInsightBasicRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Api.NumberVerify), "/number/format/json"), request, creds ?? Credentials);

            return JsonConvert.DeserializeObject<Api.NumberInsight.NumberInsightBasicResponse>(response.JsonResponse);
        }

        /// <summary>
        /// Identifies the phone number type and, for mobile phone numbers, the network it is registered with.
        /// </summary>
        /// <param name="request">NI request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public Api.NumberInsight.NumberInsightStandardResponse RequestStandard(Api.NumberInsight.NumberInsightBasicRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Api.NumberVerify), "/number/lookup/json"), request, creds ?? Credentials);

            return JsonConvert.DeserializeObject<Api.NumberInsight.NumberInsightStandardResponse>(response.JsonResponse);
        }

        /// <summary>
        /// Retrieve validity, roaming, and reachability information about a mobile phone number.
        /// </summary>
        /// <param name="request">NI advenced request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public Api.NumberInsight.NumberInsightRequestResponse Request(Api.NumberInsight.NumberInsightRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Api.NumberInsight), "/ni/json"), new Dictionary<string, string>
                {
                    {"number", request.Number},
                    {"callback", request.Callback}
                },
                creds ?? Credentials);

            return JsonConvert.DeserializeObject<Api.NumberInsight.NumberInsightRequestResponse>(response.JsonResponse);
        }

        /// <summary>
        /// Deserializes a NumberInsight response JSON string
        /// </summary>
        /// <param name="json">NumberInsight response JSON string</param>
        /// <returns></returns>
        public Api.NumberInsight.NumberInsightResponse Response(string json)
        {
            return JsonConvert.DeserializeObject<Api.NumberInsight.NumberInsightResponse>(json);
        }
    }
}
