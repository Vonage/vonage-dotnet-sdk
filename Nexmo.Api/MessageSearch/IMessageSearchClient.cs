using Nexmo.Api.Request;
using Nexmo.Api.Common;
namespace Nexmo.Api.MessageSearch
{
    public interface IMessageSearchClient
    {
        Message SearchMessage(MessageSearchRequest request, Credentials creds = null);

        MessagesSearchResponse SearchMessages(MessagesSearchRequest request, Credentials creds = null);

        RejectionSearchResponse SearchRejections(RejectionSearchRequest request, Credentials creds = null);
    }
}