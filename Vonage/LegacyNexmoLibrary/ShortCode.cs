using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nexmo.Api.Request;
using System.Linq;

namespace Nexmo.Api
{
    [System.Obsolete("The Nexmo.Api.ShortCode class is obsolete.")]
    public static class ShortCode
    {
        public class TwoFactorAuthRequest
        {
            public string to { get; set; }
            public int? pin { get; set; }
            [JsonProperty("client-ref")]
            public string clientRef { get; set; }
        }

        public class AlertRequest
        {
            public string to { get; set; }
            [JsonProperty("status-report-req")]
            public string statusReportReq { get; set; }
            [JsonProperty("client-ref")]
            public string clientRef { get; set; }
            public int? template { get; set; }
            public string type { get; set; }
        }

        /// <summary>
        /// Send a 2FA request.
        /// </summary>
        /// <param name="request">2FA request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static SMS.SMSResponse RequestTwoFactorAuth(TwoFactorAuthRequest request, Credentials creds = null)
        {
            if (!request.pin.HasValue)
            {
                request.pin = new Random().Next(0, 9999);
            }
            var response = ApiRequest.DoGetRequestWithQueryParameters<SMS.SMSResponse>(ApiRequest.GetBaseUriFor(typeof(ShortCode), "/sc/us/2fa/json"), ApiRequest.AuthType.Query, request, creds);
            SMS.ValidateSmsResponse(response);            
            return response;
        }

        /// <summary>
        /// Send an Event Based Alerts request.
        /// </summary>
        /// <param name="request">Event Based Alerts request</param>
        /// <param name="customValues">Any custom parameters you need for template.</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static SMS.SMSResponse RequestAlert(AlertRequest request, Dictionary<string, string> customValues, Credentials creds = null)
        {
            var parameters = ApiRequest.GetParameters(request);
            foreach (var key in customValues.Keys)
            {
                parameters.Add(key, customValues[key]);
            }
            var response = ApiRequest.DoGetRequestWithQueryParameters<SMS.SMSResponse>(ApiRequest.GetBaseUriFor(typeof(ShortCode), "/sc/us/alert/json"), ApiRequest.AuthType.Query, parameters, creds);
            SMS.ValidateSmsResponse(response);
            return response;
        }
    }
}