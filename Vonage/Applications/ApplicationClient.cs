using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;

namespace Vonage.Applications;

public class ApplicationClient : IApplicationClient
{
    private readonly Configuration configuration;
    private readonly ITimeProvider timeProvider = new TimeProvider();

    public ApplicationClient(Credentials creds = null)
    {
        this.Credentials = creds;
        this.configuration = Configuration.Instance;
    }

    internal ApplicationClient(Credentials credentials, Configuration configuration, ITimeProvider timeProvider)
    {
        this.Credentials = credentials;
        this.configuration = configuration;
        this.timeProvider = timeProvider;
    }

    public Credentials Credentials { get; set; }

    /// <inheritdoc/>
    [Obsolete("Favor asynchronous version instead.")]
    public Application CreateApplicaiton(CreateApplicationRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContent<Application>(
                HttpMethod.Post,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/v2/applications"),
                request,
                AuthType.Basic
            );

    /// <inheritdoc/>
    [Obsolete("Favor typo-free method instead.")]
    public Task<Application> CreateApplicaitonAsync(CreateApplicationRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<Application>(
                HttpMethod.Post,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/v2/applications"),
                request,
                AuthType.Basic
            );

    /// <inheritdoc />
    public Task<Application> CreateApplicationAsync(CreateApplicationRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<Application>(
                HttpMethod.Post,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/v2/applications"),
                request,
                AuthType.Basic
            );

    /// <inheritdoc/>
    [Obsolete("Favor asynchronous version instead.")]
    public bool DeleteApplication(string id, Credentials creds = null)
    {
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoDeleteRequestWithUrlContent(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, $"/v2/applications/{id}"),
                null,
                AuthType.Basic
            );
        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteApplicationAsync(string id, Credentials creds = null)
    {
        await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoDeleteRequestWithUrlContentAsync(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, $"/v2/applications/{id}"),
                null,
                AuthType.Basic
            );
        return true;
    }

    /// <inheritdoc/>
    [Obsolete("Favor asynchronous version instead.")]
    public Application GetApplication(string id, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParameters<Application>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, $"/v2/applications/{id}"),
                AuthType.Basic
            );

    /// <inheritdoc/>
    public Task<Application> GetApplicationAsync(string id, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<Application>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, $"/v2/applications/{id}"),
                AuthType.Basic
            );

    /// <inheritdoc/>
    [Obsolete("Favor asynchronous version instead.")]
    public ApplicationPage ListApplications(ListApplicationsRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParameters<ApplicationPage>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/v2/applications"),
                AuthType.Basic,
                request
            );

    /// <inheritdoc/>
    public Task<ApplicationPage> ListApplicationsAsync(ListApplicationsRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<ApplicationPage>(
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, "/v2/applications"),
                AuthType.Basic,
                request
            );

    /// <inheritdoc/>
    [Obsolete("Favor asynchronous version instead.")]
    public Application UpdateApplication(string id, CreateApplicationRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContent<Application>(
                HttpMethod.Put,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, $"/v2/applications/{id}"),
                request,
                AuthType.Basic
            );

    /// <inheritdoc/>
    public Task<Application> UpdateApplicationAsync(string id, CreateApplicationRequest request,
        Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<Application>(
                HttpMethod.Put,
                ApiRequest.GetBaseUri(ApiRequest.UriType.Api, this.configuration, $"/v2/applications/{id}"),
                request,
                AuthType.Basic
            );

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;
}