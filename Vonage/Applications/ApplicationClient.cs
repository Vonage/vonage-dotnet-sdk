using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Request;

namespace Vonage.Applications;

public class ApplicationClient : IApplicationClient
{
    private readonly Configuration configuration;
    public Credentials Credentials { get; set; }

    public ApplicationClient(Credentials creds = null)
    {
        this.Credentials = creds;
        this.configuration = Configuration.Instance;
    }

    internal ApplicationClient(Credentials credentials, Configuration configuration)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
    }

    public Application CreateApplicaiton(CreateApplicationRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoRequestWithJsonContent<Application>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
            request,
            AuthType.Basic
        );

    public Task<Application> CreateApplicaitonAsync(CreateApplicationRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoRequestWithJsonContentAsync<Application>(
            HttpMethod.Post,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
            request,
            AuthType.Basic
        );

    public bool DeleteApplication(string id, Credentials creds = null)
    {
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoDeleteRequestWithUrlContent(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
            null,
            AuthType.Basic
        );
        return true;
    }

    public async Task<bool> DeleteApplicationAsync(string id, Credentials creds = null)
    {
        await ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoDeleteRequestWithUrlContentAsync(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
            null,
            AuthType.Basic
        );
        return true;
    }

    public Application GetApplication(string id, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoGetRequestWithQueryParameters<Application>(
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
            AuthType.Basic
        );

    public Task<Application> GetApplicationAsync(string id, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoGetRequestWithQueryParametersAsync<Application>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
                AuthType.Basic
            );

    public ApplicationPage ListApplications(ListApplicationsRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoGetRequestWithQueryParameters<ApplicationPage>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
                AuthType.Basic,
                request
            );

    public Task<ApplicationPage> ListApplicationsAsync(ListApplicationsRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration)
            .DoGetRequestWithQueryParametersAsync<ApplicationPage>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, "/v2/applications"),
                AuthType.Basic,
                request
            );

    public Application UpdateApplication(string id, CreateApplicationRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoRequestWithJsonContent<Application>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
            request,
            AuthType.Basic
        );

    public Task<Application> UpdateApplicationAsync(string id, CreateApplicationRequest request,
        Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration).DoRequestWithJsonContentAsync<Application>(
            HttpMethod.Put,
            ApiRequest.GetBaseUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
            request,
            AuthType.Basic
        );

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;
}