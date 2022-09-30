using System;
using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Messages
{
    public class MessagesClient : IMessagesClient
    {
        private readonly Credentials _credentials;
        public int? Timeout { get; private set; }

        public MessagesClient(Credentials credentials, int? timeout = null)
        {
            _credentials = credentials ?? throw new ArgumentNullException(nameof(credentials));
            Timeout = timeout;
        }

        public async Task<MessagesResponse> SendAsync(MessageRequestBase message)
        {
            var uri = ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v1/messages");

            var result = await ApiRequest.DoRequestWithJsonContentAsync<MessagesResponse>("POST", uri, message, ApiRequest.AuthType.Bearer, _credentials, timeout: Timeout);

            return result;
        }
    }
}
