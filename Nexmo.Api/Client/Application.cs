using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api.ClientMethods
{
    public class Application
    {
        public Credentials Credentials { get; set; }
        public Application(Credentials creds)
        {
            Credentials = creds;
        }

        /// <summary>
        /// Create a new application
        /// </summary>
        /// <param name="request">Application request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public ApplicationResponse Create(ApplicationRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Api.Application), "/v1/applications"), request, creds ?? Credentials);

            return JsonConvert.DeserializeObject<ApplicationResponse>(response.JsonResponse);
        }

        /// <summary>
        /// List all of the applications associated with this account
        /// </summary>
        /// <param name="PageSize">Set the number of items returned on each call to this endpoint. The default is 10 records.</param>
        /// <param name="PageIndex">Set the offset from the first page. The default value is 0, calls to this endpoint return a page of <page_size>. For example, set page_index to 3 to retrieve items 31 - 40 when page_size is the default value.</param>
        /// <param name="AppId">Optional id of specific application to retrieve</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public List<ApplicationResponse> List(int PageSize = 10, int PageIndex = 0, string AppId = "", Credentials creds = null)
        {
            if (!string.IsNullOrEmpty(AppId))
            {
                return new List<ApplicationResponse>
                {
                    JsonConvert.DeserializeObject<ApplicationResponse>(
                        ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Api.Application),
                                $"/v1/applications/{AppId}"),
                            // TODO: using this method sig allows us to have the api auth injected at the expense of opaque code here
                            new Dictionary<string, string>(),
                            creds ?? Credentials))
                };

            }

            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Api.Application), "/v1/applications"), new Dictionary<string, string>
                {
                    { "page_size", PageSize.ToString()},
                    { "page_index", PageIndex.ToString()}
                },
                creds ?? Credentials);
            var response = JsonConvert.DeserializeObject<ApplicationListResponse>(json);
            return response._embedded.applications;
        }

        /// <summary>
        /// Modify a single application
        /// </summary>
        /// <param name="request">Application request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public ApplicationResponse Update(ApplicationRequest request, Credentials creds = null)
        {
            var sb = ApiRequest.GetQueryStringBuilderFor(request);
            var response = ApiRequest.DoPutRequest(ApiRequest.GetBaseUriFor(typeof(Api.Application),
                $"/v1/applications/{request.id}?{sb}"), null, creds ?? Credentials);

            return JsonConvert.DeserializeObject<ApplicationResponse>(response.JsonResponse);
        }

        /// <summary>
        /// Delete a single application
        /// </summary>
        /// <param name="appId">The application id to delete</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public bool Delete(string appId, Credentials creds = null)
        {
            var sb = ApiRequest.GetQueryStringBuilderFor(new object());
            var response = ApiRequest.DoDeleteRequest(ApiRequest.GetBaseUriFor(typeof(Api.Application),
                $"/v1/applications/{appId}?{sb}"), null, creds ?? Credentials);

            return response.Status == HttpStatusCode.NoContent;
        }
    }
}