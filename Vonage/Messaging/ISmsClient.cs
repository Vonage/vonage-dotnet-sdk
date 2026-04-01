using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Messaging;

/// <summary>
///     Provides methods for sending SMS messages using the Vonage SMS API.
/// </summary>
public interface ISmsClient
{
    /// <summary>
    ///     Sends an outbound SMS message from your Vonage account.
    /// </summary>
    /// <param name="request">The SMS request containing message details.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>A <see cref="SendSmsResponse"/> containing the message status and details.</returns>
    /// <exception cref="VonageSmsResponseException">Thrown when the SMS request fails or returns a non-zero status.</exception>
    /// <example>
    /// <code><![CDATA[
    /// var request = new SendSmsRequest
    /// {
    ///     From = "Vonage",
    ///     To = "447700900000",
    ///     Text = "Hello from Vonage!"
    /// };
    /// var response = await client.SmsClient.SendAnSmsAsync(request);
    /// Console.WriteLine($"Message ID: {response.Messages[0].MessageId}");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/blob/master/DotNetCliCodeSnippets/Messaging/SendSms.cs">Code snippet</seealso>
    Task<SendSmsResponse> SendAnSmsAsync(SendSmsRequest request, Credentials creds = null);

    /// <summary>
    ///     Sends an outbound SMS message from your Vonage account using simple parameters.
    /// </summary>
    /// <param name="from">The name or number the message should be sent from. Alphanumeric sender IDs are not supported in all countries.</param>
    /// <param name="to">The recipient phone number in E.164 format (e.g., 447700900000).</param>
    /// <param name="text">The body of the message. Use <see cref="SmsType.Unicode"/> for messages containing non-GSM characters.</param>
    /// <param name="type">The format of the message body. Defaults to <see cref="SmsType.Text"/>.</param>
    /// <param name="creds">Optional credentials to override the default client credentials.</param>
    /// <returns>A <see cref="SendSmsResponse"/> containing the message status and details.</returns>
    /// <exception cref="VonageSmsResponseException">Thrown when the SMS request fails or returns a non-zero status.</exception>
    /// <example>
    /// <code><![CDATA[
    /// var response = await client.SmsClient.SendAnSmsAsync(
    ///     from: "Vonage",
    ///     to: "447700900000",
    ///     text: "Hello from Vonage!"
    /// );
    /// Console.WriteLine($"Message sent: {response.Messages[0].MessageId}");
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/blob/master/DotNetCliCodeSnippets/Messaging/SendSmsWithUnicode.cs">Unicode SMS snippet</seealso>
    Task<SendSmsResponse> SendAnSmsAsync(string from, string to, string text, SmsType type = SmsType.Text,
        Credentials creds = null);
}