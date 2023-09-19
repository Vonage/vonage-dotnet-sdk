using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Messaging;

public class SmsClient : ISmsClient
{
    private readonly Configuration configuration;
    public Credentials Credentials { get; set; }

    public SmsClient(Credentials creds = null)
    {
        this.Credentials = creds;
        this.configuration = Configuration.Instance;
    }

    public SmsClient(Credentials credentials, Configuration configuration)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
    }

    public SendSmsResponse SendAnSms(SendSmsRequest request, Credentials creds = null)
    {
        var result = ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoPostRequestUrlContentFromObject<SendSmsResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sms/json"),
                request
            );
        ValidSmsResponse(result);
        return result;
    }

    public SendSmsResponse SendAnSms(string from, string to, string text, SmsType type = SmsType.Text,
        Credentials creds = null) =>
        this.SendAnSms(new SendSmsRequest {From = from, To = to, Type = type, Text = text}, creds);

    /// <summary>
    ///     Send a SMS message.
    /// </summary>
    /// <param name="request">The SMS message request</param>
    /// <param name="creds">(Optional) Overridden credentials for only this request</param>
    /// <exception cref="VonageSmsResponseException">Thrown when the status of a message is non-zero or response is empty</exception>
    /// <returns></returns>
    public async Task<SendSmsResponse> SendAnSmsAsync(SendSmsRequest request, Credentials creds = null)
    {
        var result = await ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoPostRequestUrlContentFromObjectAsync<SendSmsResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/sms/json"),
                request
            );
        ValidSmsResponse(result);
        return result;
    }

    public Task<SendSmsResponse> SendAnSmsAsync(string from, string to, string text, SmsType type = SmsType.Text,
        Credentials creds = null) =>
        this.SendAnSmsAsync(new SendSmsRequest {From = from, To = to, Type = type, Text = text}, creds);

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;

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