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

        /// <summary>
        /// Send a SMS message.
        /// </summary>
        /// <param name="request">The SMS message request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <exception cref="NexmSmsResponseException">Thrown when the status of a message is non-zero or response is empty</exception>
        /// <returns></returns>
        public SendSmsResponse SendAnSms(SendSmsRequest request, Credentials creds = null)
        {
            var result = ApiRequest.DoPostRequestUrlContentFromObject<SendSmsResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sms/json"),
                request,
                creds ?? Credentials
            );

            ValidSmsResponse(result);
            return result;
        }

        private static void ValidSmsResponse(SendSmsResponse smsResponse)
        {
            if(smsResponse?.Messages == null)
            {
                throw new NexmoSmsResponseException("Encountered an Empty SMS response");
            }
            else if (smsResponse.Messages[0].Status != "0")
            {
                throw new NexmoSmsResponseException($"SMS Request Failed with status: {smsResponse.Messages[0].Status} and error message: {smsResponse.Messages[0].ErrorText}") { Response = smsResponse};
            }
        }
    }
}