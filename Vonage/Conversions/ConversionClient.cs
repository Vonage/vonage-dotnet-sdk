using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Conversions
{
    public class ConversionClient : IConversionClient
    {
        public Credentials Credentials { get; set; }
        public int? Timeout { get; private set; }

        public ConversionClient(Credentials creds = null, int? timeout = null)
        {
            Credentials = creds;
            Timeout = timeout;
        }
        public async Task<bool> SmsConversionAsync(ConversionRequest request, Credentials creds = null)
        {
            await ApiRequest.DoPostRequestUrlContentFromObjectAsync<object>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/conversions/sms"),
                request,
                creds ?? Credentials,
                timeout: Timeout
            );
            return true;
        }

        public async Task<bool> VoiceConversionAsync(ConversionRequest request, Credentials creds = null)
        {
            await ApiRequest.DoPostRequestUrlContentFromObjectAsync<object>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/conversions/voice"),
                request,
                creds ?? Credentials,
                timeout: Timeout
            );
            return true;
        }

        public bool SmsConversion(ConversionRequest request, Credentials creds = null)
        {
            ApiRequest.DoPostRequestUrlContentFromObject<object>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/conversions/sms"),
                request,
                creds ?? Credentials,
                timeout: Timeout
            );
            return true;
        }

        public bool VoiceConversion(ConversionRequest request, Credentials creds = null)
        {
            ApiRequest.DoPostRequestUrlContentFromObject<object>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/conversions/voice"),
                request,
                creds ?? Credentials,
                timeout: Timeout
            );
            return true;
        }
    }
}