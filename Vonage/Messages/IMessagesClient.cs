using System.Threading.Tasks;

namespace Vonage.Messages
{
    public interface IMessagesClient
    {
        Task<MessagesResponse> SendAsync(MessageRequestBase message);
    }
}