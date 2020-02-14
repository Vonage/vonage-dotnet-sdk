using System.Collections.Generic;
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
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        /// <exception cref="SmsResponseException">Throwns if the status received back from an SMS was non-zero</exception>
        public Api.SMS.SMSResponse RequestTwoFactorAuth(Api.ShortCode.TwoFactorAuthRequest request, Credentials creds = null)
        {
            return Api.ShortCode.RequestTwoFactorAuth(request, creds ?? Credentials);
        }

        /// <summary>
        /// Send an Event Based Alerts request.
        /// </summary>
        /// <param name="request">Event Based Alerts request</param>
        /// <param name="customValues">Any custom parameters you need for template.</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        /// <exception cref="SmsResponseException">Throwns if the status received back from an SMS was non-zero</exception>
        public Api.SMS.SMSResponse RequestAlert(Api.ShortCode.AlertRequest request, Dictionary<string, string> customValues, Credentials creds = null)
        {
            return Api.ShortCode.RequestAlert(request, customValues, creds ?? Credentials);
        }
    }
}