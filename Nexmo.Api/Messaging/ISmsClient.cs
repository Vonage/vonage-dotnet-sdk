using Nexmo.Api.Request;

namespace Nexmo.Api.Messaging
{
    public interface ISmsClient
    {
        /// <summary>
        /// Send an outbound SMS from your Nexmo account
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        SendSmsResponse SendAnSms(SendSmsRequest request, Credentials creds = null);
    }
}