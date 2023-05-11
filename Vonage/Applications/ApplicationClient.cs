using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Applications;

public class ApplicationClient : IApplicationClient
{
    public Credentials Credentials { get; set; }

    public ApplicationClient(Credentials creds = null) => this.Credentials = creds;

    public Application CreateApplicaiton(CreateApplicationRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoRequestWithJsonContent<Application>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
            request,
            AuthType.Basic
        );

    public Task<Application> CreateApplicaitonAsync(CreateApplicationRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoRequestWithJsonContentAsync<Application>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
            request,
            AuthType.Basic
        );

    public bool DeleteApplication(string id, Credentials creds = null)
    {
        new ApiRequest(creds ?? this.Credentials).DoDeleteRequestWithUrlContent(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
            null,
            AuthType.Basic
        );
        return true;
    }

    public async Task<bool> DeleteApplicationAsync(string id, Credentials creds = null)
    {
        await new ApiRequest(creds ?? this.Credentials).DoDeleteRequestWithUrlContentAsync(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
            null,
            AuthType.Basic
        );
        return true;
    }

    public Application GetApplication(string id, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParameters<Application>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
            AuthType.Basic
        );

    public Task<Application> GetApplicationAsync(string id, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParametersAsync<Application>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
            AuthType.Basic
        );

    public ApplicationPage ListApplications(ListApplicationsRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParameters<ApplicationPage>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
            AuthType.Basic,
            request
        );

    public Task<ApplicationPage> ListApplicationsAsync(ListApplicationsRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoGetRequestWithQueryParametersAsync<ApplicationPage>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
            AuthType.Basic,
            request
        );

    public Application UpdateApplication(string id, CreateApplicationRequest request, Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoRequestWithJsonContent<Application>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
            request,
            AuthType.Basic
        );

    public Task<Application> UpdateApplicationAsync(string id, CreateApplicationRequest request,
        Credentials creds = null) =>
        new ApiRequest(creds ?? this.Credentials).DoRequestWithJsonContentAsync<Application>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
            request,
            AuthType.Basic
        );
}