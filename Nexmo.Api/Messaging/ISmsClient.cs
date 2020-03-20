using Nexmo.Api.Request;

namespace Nexmo.Api.Messaging
{
    public interface ISmsClient
    {
        SendSmsResponse SendAnSms(SendSmsRequest request, Credentials creds = null);
    }
}