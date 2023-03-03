using System.Threading.Tasks;

namespace Vonage.Messages;

public interface IMessagesClient
{
    Task<MessagesResponse> SendAsync(IMessage message);
}