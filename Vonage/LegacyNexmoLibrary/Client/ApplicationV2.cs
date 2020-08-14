using Nexmo.Api.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.ClientMethods
{
    [Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Vonage.VonageClient class")]
    public class ApplicationV2
    {
        public Credentials Credentials { get; set; }
        public ApplicationV2(Credentials credentials)
        {
            Credentials = credentials;
        }

        /// <summary>
        /// Create a new application
        /// </summary>
        /// <param name="request">Application request</param>
        /// <param name="credentials">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        [Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Vonage.VonageClient class")]
        public AppResponse Create(AppRequest request, Credentials credentials = null)
        {
            return Api.ApplicationV2.Create(request, credentials ?? Credentials);
        }

        /// <summary>
        /// Returns an application
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        [Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Vonage.VonageClient class")]
        public AppResponse Get(string appId, Credentials credentials = null)
        {
            return Api.ApplicationV2.Get(appId, credentials ?? Credentials);
        }

        /// <summary>
        /// List all of the applications associated with this account
        /// </summary>
        /// <param name="pageSize">Set the number of items returned on each call to this endpoint. The default is 10 records.</param>
        /// <param name="page">Set the offset from the first page. The default value is 0, calls to this endpoint return a page of page_size. For example, set page_index to 3 to retrieve items 31 - 40 when page_size is the default value.</param>
        /// <param name="AppId">Optional id of specific application to retrieve</param>
        /// <param name="credentials">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        [Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Vonage.VonageClient class")]
        public List<AppResponse> List(int pageSize = 10, int page = 0, Credentials credentials = null)
        {
            return Api.ApplicationV2.List(pageSize, page, credentials ?? Credentials);
        }

        /// <summary>
        /// Modify a single application
        /// </summary>
        /// <param name="request">Application request</param>
        /// <param name="credentials">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        [Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Vonage.VonageClient class")]
        public AppResponse Update(AppRequest request, Credentials credentials = null)
        {
            return Api.ApplicationV2.Update(request, credentials ?? Credentials);
        }

        /// <summary>
        /// Delete a single application
        /// </summary>
        /// <param name="appId">The application id to delete</param>
        /// <param name="credentials">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        /// <exception cref="NexmoHttpRequestException">thrown if an error is encountered when talking to the API</exception>
        [Obsolete("This item is rendered obsolete by version 5 - please use the new Interfaces provided by the Vonage.VonageClient class")]
        public bool Delete(string appId, Credentials credentials = null)
        {
            return Api.ApplicationV2.Delete(appId, credentials ?? Credentials);
        }
    }
}
