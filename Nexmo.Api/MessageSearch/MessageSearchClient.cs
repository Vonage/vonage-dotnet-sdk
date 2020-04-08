using Nexmo.Api.Request;
using Nexmo.Api.Common;
namespace Nexmo.Api.MessageSearch
{
    public class MessageSearchClient : IMessageSearchClient
    {
        public Credentials Credentials { get; set; }

        public MessageSearchClient(Credentials credentials)
        {
            Credentials = credentials;
        }
        public Message SearchMessage(MessageSearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithUrlContent<Message>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/search/message"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
        }

        public MessagesSearchResponse SearchMessages(MessagesSearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithUrlContent<MessagesSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/search/messages"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
        }

        public RejectionSearchResponse SearchRejections(RejectionSearchRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithUrlContent<RejectionSearchResponse>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Rest, "/search/rejections"),
                ApiRequest.AuthType.Query,
                request,
                creds ?? Credentials
            );
        }
    }
}