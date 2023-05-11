using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Conversions;

public class ConversionClient : IConversionClient
{
    public Credentials Credentials { get; set; }

    public ConversionClient(Credentials creds = null)
    {
        this.Credentials = creds;
    }

    public bool SmsConversion(ConversionRequest request, Credentials creds = null)
    {
        new ApiRequest(creds ?? this.Credentials).DoPostRequestUrlContentFromObject<object>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/conversions/sms"),
            request
        );
        return true;
    }

    public async Task<bool> SmsConversionAsync(ConversionRequest request, Credentials creds = null)
    {
        await new ApiRequest(creds ?? this.Credentials).DoPostRequestUrlContentFromObjectAsync<object>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/conversions/sms"),
            request
        );
        return true;
    }

    public bool VoiceConversion(ConversionRequest request, Credentials creds = null)
    {
        new ApiRequest(creds ?? this.Credentials).DoPostRequestUrlContentFromObject<object>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/conversions/voice"),
            request
        );
        return true;
    }

    public async Task<bool> VoiceConversionAsync(ConversionRequest request, Credentials creds = null)
    {
        await new ApiRequest(creds ?? this.Credentials).DoPostRequestUrlContentFromObjectAsync<object>
        (
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/conversions/voice"),
            request
        );
        return true;
    }
}