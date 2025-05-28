#region
using System.Threading.Tasks;
#endregion

namespace Vonage.Messages;

/// <summary>
/// </summary>
public interface IMessagesClient
{
    /// <summary>
    ///     Send a Message
    /// </summary>
    /// <param name="message">The message</param>
    /// <returns>The message UUID</returns>
    Task<MessagesResponse> SendAsync(IMessage message);

    /// <summary>
    ///     This endpoint lets you update the status of outbound and/or inbound messages for certain channels. For example, you
    ///     can revoke outbound messages or mark inbound messages as read.
    /// </summary>
    /// <param name="request">The request.</param>
    Task UpdateAsync(IUpdateMessageRequest request);
}