using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api.ClientMethods
{
    public class ShortCode
    {
        public Credentials Credentials { get; set; }
        public ShortCode(Credentials creds)
        {
            Credentials = creds;
        }

        /// <summary>
        /// Send a 2FA request.
        /// </summary>
        /// <param name="request">2FA request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public Api.SMS.SMSResponse RequestTwoFactorAuth(Api.ShortCode.TwoFactorAuthRequest request, Credentials creds = null)
        {
            if (!request.pin.HasValue)
            {
                request.pin = new Random().Next(0, 9999);
            }

            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Api.ShortCode), "/sc/us/2fa/json"), request, creds ?? Credentials);
            return JsonConvert.DeserializeObject<Api.SMS.SMSResponse>(json);
        }

        /// <summary>
        /// Send an Event Based Alerts request.
        /// </summary>
        /// <param name="request">Event Based Alerts request</param>
        /// <param name="customValues">Any custom parameters you need for template.</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public Api.SMS.SMSResponse RequestAlert(Api.ShortCode.AlertRequest request, Dictionary<string, string> customValues, Credentials creds = null)
        {
            var sb = ApiRequest.GetQueryStringBuilderFor(request, creds ?? Credentials);
            foreach (var key in customValues.Keys)
            {
                sb.AppendFormat("{0}={1}&", System.Net.WebUtility.UrlEncode(key), System.Net.WebUtility.UrlEncode(customValues[key]));
            }

            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Api.ShortCode), "/sc/us/alert/json?" + sb), creds);
            return JsonConvert.DeserializeObject<Api.SMS.SMSResponse>(json);
        }
    }
}
