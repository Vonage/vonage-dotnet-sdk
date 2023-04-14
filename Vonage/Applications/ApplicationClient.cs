using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Applications;

public class ApplicationClient : IApplicationClient
{
    public Credentials Credentials { get; set; }
    public ApplicationClient(Credentials creds = null)
    {
        this.Credentials = creds;
    }
    public Task<Application> CreateApplicaitonAsync(CreateApplicationRequest request, Credentials creds = null)
    {
        return ApiRequest.DoRequestWithJsonContentAsync<Application>(
            "POST",
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
            request,
            AuthType.Basic,
            creds ?? this.Credentials
        );
    }

    public Task<ApplicationPage> ListApplicationsAsync(ListApplicationsRequest request, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParametersAsync<ApplicationPage>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
            AuthType.Basic,
            request,
            creds ?? this.Credentials
        );
    }

    public Task<Application> GetApplicationAsync(string id, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParametersAsync<Application>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
            AuthType.Basic,
            credentials: creds ?? this.Credentials
        );
    }

    public Task<Application> UpdateApplicationAsync(string id, CreateApplicationRequest request, Credentials creds = null)
    {
        return ApiRequest.DoRequestWithJsonContentAsync<Application>(
            "PUT",
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
            request,
            AuthType.Basic,
            creds ?? this.Credentials
        );
    }

    public async Task<bool> DeleteApplicationAsync(string id, Credentials creds = null)
    {
        await ApiRequest.DoDeleteRequestWithUrlContentAsync(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
            null,
            AuthType.Basic,
            creds ?? this.Credentials
        );
        return true;
    }

    public Application CreateApplicaiton(CreateApplicationRequest request, Credentials creds = null)
    {
        return ApiRequest.DoRequestWithJsonContent<Application>(
            "POST",
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
            request,
            AuthType.Basic,
            creds ?? this.Credentials
        );
    }

    public ApplicationPage ListApplications(ListApplicationsRequest request, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParameters<ApplicationPage>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
            AuthType.Basic,
            request,
            creds ?? this.Credentials
        );
    }

    public Application GetApplication(string id, Credentials creds = null)
    {
        return ApiRequest.DoGetRequestWithQueryParameters<Application>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
            AuthType.Basic,
            credentials: creds ?? this.Credentials
        );
    }

    public Application UpdateApplication(string id, CreateApplicationRequest request, Credentials creds = null)
    {
        return ApiRequest.DoRequestWithJsonContent<Application>(
            "PUT",
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
            request,
            AuthType.Basic,
            creds ?? this.Credentials
        );
    }

    public bool DeleteApplication(string id, Credentials creds = null)
    {
        ApiRequest.DoDeleteRequestWithUrlContent(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
            null,
            AuthType.Basic,
            creds ?? this.Credentials
        );
        return true;
    }
}