using Nexmo.Api.Request;

namespace Nexmo.Api.ClientMethods
{
    public class Search
    {
        public Credentials Credentials { get; set; }
        public Search(Credentials creds)
        {
            Credentials = creds;
        }

        /// <summary>
        /// Search for information about a single message that you sent using SMS API.
        /// </summary>
        /// <param name="id">Nexmo message ID</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public Api.Search.Message GetMessage(string id, Credentials creds = null)
        {
            return Api.Search.GetMessage(id, creds ?? Credentials);
        }

        /// <summary>
        /// Search for information about the messages you sent using SMS API.
        /// </summary>
        /// <param name="request">Search request with numbers</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public Api.Search.Messages<Api.Search.Message> GetMessages(Api.Search.SearchRequest request, Credentials creds = null)
        {
            return Api.Search.GetMessages(request, creds ?? Credentials);
        }

        /// <summary>
        /// Search for messages that have been rejected by Nexmo. Messages rejected by carrier do not appear.
        /// </summary>
        /// <param name="request">Search request with numbers</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        public Api.Search.Messages<Api.Search.MessageBase> GetRejections(Api.Search.SearchRequest request, Credentials creds = null)
        {
            return Api.Search.GetRejections(request, creds ?? Credentials);
        }
    }
}
