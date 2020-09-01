using Nexmo.Api.Request;

namespace Nexmo.Api.Messaging
{
    [System.Obsolete("The Nexmo.Api.Messaging.ISmsClient interface is obsolete. " +
        "References to it should be switched to the new Vonage.Messaging.ISmsClient interface.")]
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