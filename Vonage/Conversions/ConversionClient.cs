using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Conversions;

public class ConversionClient : IConversionClient
{
    private readonly Configuration configuration;
    public Credentials Credentials { get; set; }

    public ConversionClient(Credentials creds = null)
    {
        this.Credentials = creds;
        this.configuration = Configuration.Instance;
    }

    internal ConversionClient(Credentials credentials, Configuration configuration)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
    }

    public bool SmsConversion(ConversionRequest request, Credentials creds = null)
    {
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoPostRequestUrlContentFromObject<object>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/conversions/sms"),
            request
        );
        return true;
    }

    public async Task<bool> SmsConversionAsync(ConversionRequest request, Credentials creds = null)
    {
        await ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoPostRequestUrlContentFromObjectAsync<object>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/conversions/sms"),
                request
            );
        return true;
    }

    public bool VoiceConversion(ConversionRequest request, Credentials creds = null)
    {
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoPostRequestUrlContentFromObject<object>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/conversions/voice"),
            request
        );
        return true;
    }

    public async Task<bool> VoiceConversionAsync(ConversionRequest request, Credentials creds = null)
    {
        await ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoPostRequestUrlContentFromObjectAsync<object>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/conversions/voice"),
                request
            );
        return true;
    }

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;
}