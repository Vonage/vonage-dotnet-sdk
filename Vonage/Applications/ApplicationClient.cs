using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Applications
{
    public class ApplicationClient : IApplicationClient
    {
        public Credentials Credentials { get; set; }
        public int? Timeout { get; private set; }
        public ApplicationClient(Credentials creds = null, int? timeout = null)
        {
            Credentials = creds;
            Timeout = timeout;
        }
        public Task<Application> CreateApplicaitonAsync(CreateApplicationRequest request, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContentAsync<Application>(
                "POST",
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
                request,
                ApiRequest.AuthType.Basic,
                creds ?? Credentials,
                timeout: Timeout
            );
        }

        public Task<ApplicationPage> ListApplicationsAsync(ListApplicationsRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<ApplicationPage>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
                ApiRequest.AuthType.Basic,
                request,
                creds ?? Credentials,
                timeout: Timeout
            );
        }

        public Task<Application> GetApplicationAsync(string id, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<Application>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
                ApiRequest.AuthType.Basic,
                credentials: creds ?? Credentials,
                timeout: Timeout
            );
        }

        public Task<Application> UpdateApplicationAsync(string id, CreateApplicationRequest request, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContentAsync<Application>(
                "PUT",
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
                request,
                ApiRequest.AuthType.Basic,
                creds ?? Credentials,
                timeout: Timeout
            );
        }

        public async Task<bool> DeleteApplicationAsync(string id, Credentials creds = null)
        {
            await ApiRequest.DoDeleteRequestWithUrlContentAsync(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
                null,
                ApiRequest.AuthType.Basic,
                creds ?? Credentials,
                timeout: Timeout
            );
            return true;
        }

        public Application CreateApplicaiton(CreateApplicationRequest request, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<Application>(
                "POST",
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
                request,
                ApiRequest.AuthType.Basic,
                creds ?? Credentials,
                timeout: Timeout
            );
        }

        public ApplicationPage ListApplications(ListApplicationsRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<ApplicationPage>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
                ApiRequest.AuthType.Basic,
                request,
                creds ?? Credentials,
                timeout: Timeout
            );
        }

        public Application GetApplication(string id, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParameters<Application>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
                ApiRequest.AuthType.Basic,
                credentials: creds ?? Credentials,
                timeout: Timeout
            );
        }

        public Application UpdateApplication(string id, CreateApplicationRequest request, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContent<Application>(
                "PUT",
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
                request,
                ApiRequest.AuthType.Basic,
                creds ?? Credentials,
                timeout: Timeout
            );
        }

        public bool DeleteApplication(string id, Credentials creds = null)
        {
            ApiRequest.DoDeleteRequestWithUrlContent(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
                null,
                ApiRequest.AuthType.Basic,
                creds ?? Credentials,
                timeout: Timeout
            );
            return true;
        }
    }
}