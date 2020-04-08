using Nexmo.Api.Request;

namespace Nexmo.Api.Conversions
{
    public interface IConversionClient
    {
        bool SmsConversion(ConversionRequest request, Credentials creds = null);
        bool VoiceConversion(ConversionRequest request, Credentials creds = null);
    }
}