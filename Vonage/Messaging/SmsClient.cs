using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Messaging;

public class SmsClient : ISmsClient
{
    public Credentials Credentials { get; set; }

    public SmsClient(Credentials creds = null)
    {
        this.Credentials = creds;
    }

    public SendSmsResponse SendAnSms(SendSmsRequest request, Credentials creds = null)
    {
        var result = new ApiRequest(creds ?? this.Credentials).DoPostRequestUrlContentFromObject<SendSmsResponse>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sms/json"),
            request
        );
        ValidSmsResponse(result);
        return result;
    }

    public SendSmsResponse SendAnSms(string from, string to, string text, SmsType type = SmsType.Text,
        Credentials creds = null)
    {
        return this.SendAnSms(new SendSmsRequest {From = from, To = to, Type = type, Text = text}, creds);
    }

    /// <summary>
    /// Send a SMS message.
    /// </summary>
    /// <param name="request">The SMS message request</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request</param>
    /// <exception cref="VonageSmsResponseException">Thrown when the status of a message is non-zero or response is empty</exception>
    /// <returns></returns>
    public async Task<SendSmsResponse> SendAnSmsAsync(SendSmsRequest request, Credentials creds = null)
    {
        var result = await new ApiRequest(creds ?? this.Credentials)
            .DoPostRequestUrlContentFromObjectAsync<SendSmsResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sms/json"),
                request
            );
        ValidSmsResponse(result);
        return result;
    }

    public Task<SendSmsResponse> SendAnSmsAsync(string from, string to, string text, SmsType type = SmsType.Text,
        Credentials creds = null)
    {
        return this.SendAnSmsAsync(new SendSmsRequest {From = from, To = to, Type = type, Text = text}, creds);
    }

    private static void ValidSmsResponse(SendSmsResponse smsResponse)
    {
        if (smsResponse?.Messages == null)
        {
            throw new VonageSmsResponseException("Encountered an Empty SMS response");
        }

        if (smsResponse.Messages[0].Status != "0")
        {
            throw new VonageSmsResponseException(
                    $"SMS Request Failed with status: {smsResponse.Messages[0].Status} and error message: {smsResponse.Messages[0].ErrorText}")
                {Response = smsResponse};
        }
    }
}