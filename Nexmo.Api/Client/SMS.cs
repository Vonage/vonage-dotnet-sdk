using System.Net;
using Nexmo.Api.Request;

namespace Nexmo.Api.ClientMethods
{
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
        public Api.SMS.SMSResponse Send(Api.SMS.SMSRequest request, Credentials creds = null)
        {
            return Api.SMS.Send(request, out HttpStatusCode statusCode, creds ?? Credentials);
        }

        /// <summary>
        /// Send a SMS message.
        /// </summary>
        /// <param name="request">The SMS message request</param>
        /// <param name="statusCode">The HttpStatusCode returned from the request.</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public Api.SMS.SMSResponse Send(Api.SMS.SMSRequest request, out HttpStatusCode statusCode, Credentials creds = null)
        {
            return Api.SMS.Send(request, out statusCode, creds ?? Credentials);
        }
    }
}
