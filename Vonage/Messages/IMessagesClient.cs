#region
using System.Threading.Tasks;
#endregion

namespace Vonage.Messages;

public interface IMessagesClient
{
    Task<MessagesResponse> SendAsync(IMessage message);

    /// <summary>
    ///     This endpoint lets you update the status of outbound and/or inbound messages for certain channels. For example, you
    ///     can revoke outbound messages or mark inbound messages as read.
    /// </summary>
    /// <param name="request">The request.</param>
    Task UpdateAsync(IUpdateMessageRequest request);
}