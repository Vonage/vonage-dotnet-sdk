using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Applications
{
    public class ApplicationClient : IApplicationClient
    {
        public Credentials Credentials { get; set; }
        public ApplicationClient(Credentials creds = null)
        {
            Credentials = creds;
        }
        public Task<Application> CreateApplicaitonAsync(CreateApplicationRequest request, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContentAsync<Application>(
                "POST",
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
                request,
                ApiRequest.AuthType.Basic,
                creds ?? Credentials
            );
        }

        public Task<ApplicationPage> ListApplicationsAsync(ListApplicationsRequest request, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<ApplicationPage>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
                ApiRequest.AuthType.Basic,
                request,
                creds ?? Credentials
            );
        }

        public Task<Application> GetApplicationAsync(string id, Credentials creds = null)
        {
            return ApiRequest.DoGetRequestWithQueryParametersAsync<Application>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
                ApiRequest.AuthType.Basic,
                credentials: creds ?? Credentials
            );
        }

        public Task<Application> UpdateApplicationAsync(string id, CreateApplicationRequest request, Credentials creds = null)
        {
            return ApiRequest.DoRequestWithJsonContentAsync<Application>(
                "PUT",
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
                request,
                ApiRequest.AuthType.Basic,
                creds ?? Credentials
            );
        }

        public async Task<bool> DeleteApplicationAsync(string id, Credentials creds = null)
        {
            await ApiRequest.DoDeleteRequestWithUrlContentAsync(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
                null,
                ApiRequest.AuthType.Basic,
                creds ?? Credentials
            );
            return true;
        }

        public Application CreateApplicaiton(CreateApplicationRequest request, Credentials creds = null)
        {
            return CreateApplicaitonAsync(request, creds).GetAwaiter().GetResult();
        }

        public ApplicationPage ListApplications(ListApplicationsRequest request, Credentials creds = null)
        {
            return ListApplicationsAsync(request, creds).GetAwaiter().GetResult();
        }

        public Application GetApplication(string id, Credentials creds = null)
        {
            return GetApplicationAsync(id, creds).GetAwaiter().GetResult();
        }

        public Application UpdateApplication(string id, CreateApplicationRequest request, Credentials creds = null)
        {
            return UpdateApplicationAsync(id, request, creds).GetAwaiter().GetResult();
        }

        public bool DeleteApplication(string id, Credentials creds = null)
        {
            return DeleteApplicationAsync(id, creds).GetAwaiter().GetResult();
        }
    }
}