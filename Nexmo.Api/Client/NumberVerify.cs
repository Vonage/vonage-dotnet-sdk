using System.Collections.Generic;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api.ClientMethods
{
    public class NumberVerify
    {
        public Credentials Credentials { get; set; }
        public NumberVerify(Credentials creds)
        {
            Credentials = creds;
        }

        /// <summary>
        /// Number Verify: Generate and send a PIN to your user. You use the request_id in the response for the Verify Check.
        /// </summary>
        /// <param name="request">Verify request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public Api.NumberVerify.VerifyResponse Verify(Api.NumberVerify.VerifyRequest request, Credentials creds = null)
        {
            var jsonstring = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Api.NumberVerify), "/verify/json"), request, creds ?? Credentials);

            return JsonConvert.DeserializeObject<Api.NumberVerify.VerifyResponse>(jsonstring);
        }

        /// <summary>
        /// Number Verify: Confirm that the PIN you received from your user matches the one sent by Nexmo as a result of your Verify Request.
        /// </summary>
        /// <param name="request">Check request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public Api.NumberVerify.CheckResponse Check(Api.NumberVerify.CheckRequest request, Credentials creds = null)
        {
            var jsonstring = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Api.NumberVerify), "/verify/check/json"), new Dictionary<string, string>
                {
                    {"request_id", request.request_id},
                    {"code", request.code}
                },
                creds ?? Credentials);

            return JsonConvert.DeserializeObject<Api.NumberVerify.CheckResponse>(jsonstring);
        }

        /// <summary>
        /// Number Verify: Lookup the status of one or more requests.
        /// </summary>
        /// <param name="request">Search request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public Api.NumberVerify.SearchResponse Search(Api.NumberVerify.SearchRequest request, Credentials creds = null)
        {
            var jsonstring = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Api.NumberVerify), "/verify/search/json"), new Dictionary<string, string>()
                {
                    {"request_id", request.request_id},
                    {"request_ids", request.request_ids}
                },
                creds ?? Credentials);

            return JsonConvert.DeserializeObject<Api.NumberVerify.SearchResponse>(jsonstring);
        }

        /// <summary>
        /// Number Verify: Control the progress of your Verify Requests.
        /// </summary>
        /// <param name="request">Control request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public Api.NumberVerify.ControlResponse Control(Api.NumberVerify.ControlRequest request, Credentials creds = null)
        {
            var jsonstring = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Api.NumberVerify), "/verify/control/json"), request, creds ?? Credentials);

            return JsonConvert.DeserializeObject<Api.NumberVerify.ControlResponse>(jsonstring);
        }
    }
}
