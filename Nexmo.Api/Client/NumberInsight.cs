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
        public Api.NumberInsight.NumberInsightBasicResponse RequestBasic(Api.NumberInsight.NumberInsightRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Api.NumberVerify), "/ni/basic/json"), request, creds ?? Credentials);

            return JsonConvert.DeserializeObject<Api.NumberInsight.NumberInsightBasicResponse>(response.JsonResponse);
        }

        /// <summary>
        /// Identifies the phone number type and, for mobile phone numbers, the network it is registered with.
        /// </summary>
        /// <param name="request">NI standard request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public Api.NumberInsight.NumberInsightStandardResponse RequestStandard(Api.NumberInsight.NumberInsightRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Api.NumberVerify), "/ni/standard/json"), request, creds ?? Credentials);

            return JsonConvert.DeserializeObject<Api.NumberInsight.NumberInsightStandardResponse>(response.JsonResponse);
        }

        /// <summary>
        /// Retrieve validity, roaming, and reachability information about a mobile phone number.
        /// </summary>
        /// <param name="request">NI advenced request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public Api.NumberInsight.NumberInsightAdvancedResponse RequestAdvanced(Api.NumberInsight.NumberInsightRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/ni/advanced/json"),
                request, creds ?? Credentials);

            return JsonConvert.DeserializeObject<Api.NumberInsight.NumberInsightAdvancedResponse>(response.JsonResponse);
        }

        /// <summary>
        /// Retrieve validity, roaming, and reachability information about a mobile phone number via a webhook
        /// </summary>
        /// <param name="request">NI advenced request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public Api.NumberInsight.NumberInsightAsyncRequestResponse RequestAsync(Api.NumberInsight.NumberInsightAsyncRequest request, Credentials creds = null)
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

            var response = ApiRequest.DoPostRequest(
                ApiRequest.GetBaseUriFor(typeof(NumberVerify), "/ni/advanced/async/json"), parameters,
                creds ?? Credentials);

            return JsonConvert.DeserializeObject<Api.NumberInsight.NumberInsightAsyncRequestResponse>(response.JsonResponse);
        }

        /// <summary>
        /// Deserializes a NumberInsight response JSON string
        /// </summary>
        /// <param name="json">NumberInsight response JSON string</param>
        /// <returns></returns>
        public Api.NumberInsight.NumberInsightAdvancedResponse Response(string json)
        {
            return JsonConvert.DeserializeObject<Api.NumberInsight.NumberInsightAdvancedResponse>(json);
        }
    }
}