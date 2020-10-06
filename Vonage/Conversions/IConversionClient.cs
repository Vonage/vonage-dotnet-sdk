using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Conversions
{
    public interface IConversionClient
    {
        /// <summary>
        /// Send a Conversion API request with information about the SMS message identified by message-id. 
        /// Vonage uses your conversion data and internal information about message-id to help improve our routing of messages in the future.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        Task<bool> SmsConversionAsync(ConversionRequest request, Credentials creds = null);
        /// <summary>
        /// Send a Conversion API request with information about the Call or Text-To-Speech identified by message-id. 
        /// Vonage uses your conversion data and internal information about message-id to help improve our routing of messages in the future.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        Task<bool> VoiceConversionAsync(ConversionRequest request, Credentials creds = null);
    }
}