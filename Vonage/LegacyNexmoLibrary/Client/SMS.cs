using System;
using Nexmo.Api.Request;

namespace Nexmo.Api.ClientMethods
{
    [System.Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Vonage.VonageClient class")]
    public class SMS
    {
        public Credentials Credentials { get; set; }
        public SMS(Credentials creds)
        {
            Credentials = creds;
        }

        /// <summary>
        /// Send a SMS message.
        /// </summary>
        /// <param name="request">The SMS message request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        /// <exception cref="SmsResponseException">Throwns if the status received back from an SMS was non-zero</exception>
        public Api.SMS.SMSResponse Send(Api.SMS.SMSRequest request, Credentials creds = null)
        {
            return Api.SMS.Send(request, creds ?? Credentials);
        }
    }
}
