using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Messages
{
    public class MessagesClient : IMessagesClient
    {
        private readonly Credentials _credentials;

        public MessagesClient(Credentials credentials)
        {
            _credentials = credentials;
        }

        public async Task<MessagesResponse> SendAsync(MessageRequestBase message)
        {
            var uri = ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v1/messages");
            var result = await ApiRequest.DoRequestWithJsonContentAsync<MessagesResponse>("POST", uri, message,
                ApiRequest.AuthType.Bearer, _credentials);
            return result;
        }
    }
}