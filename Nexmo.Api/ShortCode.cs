using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api
{
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

            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(ShortCode), "/sc/us/2fa/json"), request, creds);
            return JsonConvert.DeserializeObject<SMS.SMSResponse>(json);
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
            var sb = ApiRequest.GetQueryStringBuilderFor(request, creds);
            foreach (var key in customValues.Keys)
            {
                sb.AppendFormat("{0}={1}&", System.Net.WebUtility.UrlEncode(key), System.Net.WebUtility.UrlEncode(customValues[key]));
            }

            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(ShortCode), "/sc/us/alert/json?" + sb), creds);
            return JsonConvert.DeserializeObject<SMS.SMSResponse>(json);
        }
    }
}