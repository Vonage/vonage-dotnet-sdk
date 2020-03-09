using Nexmo.Api.Request;

namespace Nexmo.Api.Messaging
{
    public class SmsClient : ISmsClient
    {
        public Credentials Credentials { get; set; }

        public SmsClient(Credentials creds)
        {
            Credentials = creds;
        }
        public SendSmsResponse SendAnSms(SendSmsRequest request, Credentials creds = null)
        {
            return ApiRequest.DoPostRequestUrlContentFromObject<SendSmsResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sms/json"),
                request,
                creds ?? Credentials
            );
        }
    }
}