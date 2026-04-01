#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;
#endregion

namespace Vonage.Messaging;

/// <summary>
///     Implementation of <see cref="ISmsClient"/> for sending SMS messages via the Vonage SMS API.
/// </summary>
public class SmsClient : ISmsClient
{
    private readonly Configuration configuration;
    private readonly ITimeProvider timeProvider = new TimeProvider();

    /// <summary>
    ///     Initializes a new instance of the <see cref="SmsClient"/> class.
    /// </summary>
    /// <param name="creds">Optional credentials to use for API requests.</param>
    public SmsClient(Credentials creds = null)
    {
        this.Credentials = creds;
        this.configuration = Configuration.Instance;
    }

    internal SmsClient(Credentials credentials, Configuration configuration, ITimeProvider timeProvider)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
        this.timeProvider = timeProvider;
    }

    /// <summary>
    ///     Gets or sets the credentials used to authenticate API requests.
    /// </summary>
    public Credentials Credentials { get; set; }

    /// <inheritdoc/>
    public async Task<SendSmsResponse> SendAnSmsAsync(SendSmsRequest request, Credentials creds = null)
    {
        var result = await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoPostRequestUrlContentFromObjectAsync<SendSmsResponse>(
                this.configuration.BuildUri(ApiRequest.UriType.Rest, "/sms/json"),
                request
            ).ConfigureAwait(false);
        ValidSmsResponse(result);
        return result;
    }

    /// <inheritdoc/>
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