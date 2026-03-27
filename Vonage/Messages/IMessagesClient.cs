#region
using System.Threading.Tasks;
#endregion

namespace Vonage.Messages;

/// <summary>
///     Exposes methods for sending messages across multiple channels (SMS, MMS, WhatsApp, Messenger, Viber, RCS).
/// </summary>
public interface IMessagesClient
{
    /// <summary>
    ///     Sends a message through the specified channel.
    /// </summary>
    /// <param name="message">The message to send. Can be any implementation of <see cref="IMessage"/> such as <see cref="Sms.SmsRequest"/>, <see cref="WhatsApp.WhatsAppTextRequest"/>, etc.</param>
    /// <returns>A response containing the message UUID for tracking delivery status.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var message = new SmsRequest { To = "447700900000", From = "Vonage", Text = "Hello!" };
    /// var response = await client.SendAsync(message);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/main/SnippetSamples/Messages">More examples in the snippets repository</seealso>
    Task<MessagesResponse> SendAsync(IMessage message);

    /// <summary>
    ///     Updates the status of an outbound or inbound message for certain channels.
    ///     For example, you can revoke outbound messages or mark inbound messages as read.
    /// </summary>
    /// <param name="request">The update request containing the message UUID and the desired status change.</param>
    /// <example>
    /// <code><![CDATA[
    /// var request = new WhatsAppUpdateMessageRequest(messageUuid, UpdateStatus.Read);
    /// await client.UpdateAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/main/SnippetSamples/Messages">More examples in the snippets repository</seealso>
    Task UpdateAsync(IUpdateMessageRequest request);
}