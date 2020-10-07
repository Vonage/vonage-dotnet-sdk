using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Conversions
{
    public class ConversionClient : IConversionClient
    {
        public Credentials Credentials { get; set; }

        public ConversionClient(Credentials creds = null)
        {
            Credentials = creds;
        }
        public async Task<bool> SmsConversionAsync(ConversionRequest request, Credentials creds = null)
        {
            await ApiRequest.DoPostRequestUrlContentFromObjectAsync<object>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/conversions/sms"),
                request,
                creds??Credentials
            );
            return true;
        }

        public async Task<bool> VoiceConversionAsync(ConversionRequest request, Credentials creds = null)
        {
            await ApiRequest.DoPostRequestUrlContentFromObjectAsync<object>
            (
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/conversions/voice"),
                request,
                creds??Credentials
            );
            return true;
        }

        public bool SmsConversion(ConversionRequest request, Credentials creds = null)
        {
            return SmsConversionAsync(request, creds).GetAwaiter().GetResult();
        }

        public bool VoiceConversion(ConversionRequest request, Credentials creds = null)
        {
            return VoiceConversionAsync(request, creds).GetAwaiter().GetResult();
        }
    }
}