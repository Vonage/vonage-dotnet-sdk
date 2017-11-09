using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Nexmo.Api.Request;

namespace Nexmo.Api
{
    public class ApplicationRequest
    {
        /// <summary>
        /// Optional. Used when updating an existing application.
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// The name of your application. Required.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// The Nexmo product or products that you access with this application. Possible values are:
        ///   voice
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// The URL where your webhook delivers the Nexmo Call Control Object that governs this call.As soon as your user answers a call Platform makes a request to answer_url.Use answer_method to manage the HTTP method.
        /// </summary>
        public string answer_url { get; set; }
        /// <summary>
        /// The HTTP method used to make the request to answer_url. The default value is GET.
        /// </summary>
        public string answer_method { get; set; }
        /// <summary>
        /// Required. Nexmo sends event information asynchronously to this URL when status changes. The values sent to this URL are:
        ///   started
        ///   ringing
        ///   answered
        ///   busy
        ///   failed
        ///   timeout
        ///   cancelled
        ///   machine - Platform detected an answering machine
        ///   complete
        /// </summary>
        public string event_url { get; set; }
        /// <summary>
        /// The HTTP method used to send event information to event_url. The default value is POST.
        /// </summary>
        public string event_method { get; set; }
    }

    public class KeyPair
    {
        /// <summary>
        /// The public key used to validate the jwt in a https://en.wikipedia.org/wiki/JSON_Web_Token.
        /// </summary>
        public string private_key { get; set; }
        /// <summary>
        /// The private key you use to generate the JSON Web Token (JWT) that authenticates your requests to Voice API.
        /// </summary>
        public string public_key { get; set; }
    }

    public class VoiceHooks
    {
        public Webhook[] webhooks { get; set; }
    }

    public class Webhook
    {
        public string endpoint { get; set; }
        public string endpoint_type { get; set; }
        public string http_method { get; set; }
    }

    public class ApplicationResponse
    {
        /// <summary>
        /// The name of your application
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// The Nexmo product or products that you access with this application. Currently only voice is supported.
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// The ID allocated to your application by Nexmo.
        /// </summary>
        public string id { get; set; }
        public KeyPair keys { get; set; }
        public VoiceHooks voice { get; set; }
        public HALLinks _links { get; set; }
    }

    public class ApplicationList
    {
        public List<ApplicationResponse> applications { get; set; }
    }

    public class ApplicationListResponse : PaginatedResponse<ApplicationList>
    {}

    public class Application
    {
        /// <summary>
        /// Create a new application
        /// </summary>
        /// <param name="request">Application request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static ApplicationResponse Create(ApplicationRequest request, Credentials creds = null)
        {
            var response = ApiRequest.DoPostRequest(ApiRequest.GetBaseUriFor(typeof(Application), "/v1/applications"), request, creds);

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
        public static List<ApplicationResponse> List(int PageSize = 10, int PageIndex = 0, string AppId = "", Credentials creds = null)
        {
            if (!string.IsNullOrEmpty(AppId))
            {
                return new List<ApplicationResponse>
                {
                    JsonConvert.DeserializeObject<ApplicationResponse>(
                        ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Application),
                        $"/v1/applications/{AppId}"),
                        // TODO: using this method sig allows us to have the api auth injected at the expense of opaque code here
                        new Dictionary<string, string>(),
                        creds))
                };

            }

            var json = ApiRequest.DoRequest(ApiRequest.GetBaseUriFor(typeof(Application), "/v1/applications"), new Dictionary<string, string>
            {
                { "page_size", PageSize.ToString()},
                { "page_index", PageIndex.ToString()}
            },
            creds);
            var response = JsonConvert.DeserializeObject<ApplicationListResponse>(json);
            return response._embedded.applications;
        }

        /// <summary>
        /// Modify a single application
        /// </summary>
        /// <param name="request">Application request</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static ApplicationResponse Update(ApplicationRequest request, Credentials creds = null)
        {
            var sb = ApiRequest.GetQueryStringBuilderFor(request);
            var response = ApiRequest.DoPutRequest(ApiRequest.GetBaseUriFor(typeof(Application),
                $"/v1/applications/{request.id}?{sb}"), null, creds);

            return JsonConvert.DeserializeObject<ApplicationResponse>(response.JsonResponse);
        }

        /// <summary>
        /// Delete a single application
        /// </summary>
        /// <param name="appId">The application id to delete</param>
        /// <param name="creds">(Optional) Overridden credentials for only this request</param>
        /// <returns></returns>
        public static bool Delete(string appId, Credentials creds = null)
        {
            var sb = ApiRequest.GetQueryStringBuilderFor(new object());
            var response = ApiRequest.DoDeleteRequest(ApiRequest.GetBaseUriFor(typeof(Application),
                $"/v1/applications/{appId}?{sb}"), null, creds);

            return response.Status == HttpStatusCode.NoContent;
        }
    }
}