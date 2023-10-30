using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Messaging;

public interface ISmsClient
{
    /// <summary>
    /// Send an outbound SMS from your Vonage account
    /// </summary>
    /// <param name="request"></param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<SendSmsResponse> SendAnSmsAsync(SendSmsRequest request, Credentials creds = null);

    /// <summary>
    /// Send an outbound SMS from your Vonage account
    /// </summary>
    /// <param name="from">The name or number the message should be sent from.</param>
    /// <param name="to">The number that the message should be sent to. Numbers are specified in E.164 format.</param>
    /// <param name="text">The body of the message being sent.</param>
    /// <param name="type">The format of the message body.</param>
    /// <param name="creds"></param>
    /// <returns></returns>
    Task<SendSmsResponse> SendAnSmsAsync(string from, string to, string text, SmsType type = SmsType.Text,
        Credentials creds = null);
}