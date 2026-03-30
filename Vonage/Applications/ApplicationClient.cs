#region
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Request;
#endregion

namespace Vonage.Applications;

/// <summary>
///     Client for managing Vonage applications. Implements the Vonage Application API v2.
/// </summary>
public class ApplicationClient : IApplicationClient
{
    private readonly Configuration configuration;
    private readonly ITimeProvider timeProvider = new TimeProvider();

    /// <summary>
    ///     Initializes a new instance of the <see cref="ApplicationClient" /> class.
    /// </summary>
    /// <param name="creds">Optional credentials for authenticating API requests.</param>
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

    /// <summary>
    ///     The credentials used to authenticate API requests.
    /// </summary>
    public Credentials Credentials { get; set; }

    /// <inheritdoc />
    public Task<Application> CreateApplicationAsync(CreateApplicationRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<Application>(
                HttpMethod.Post,
                this.configuration.BuildUri(ApiRequest.UriType.Api, "/v2/applications"),
                request,
                AuthType.Basic
            );

    /// <inheritdoc/>
    public async Task<bool> DeleteApplicationAsync(string id, Credentials creds = null)
    {
        await ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoDeleteRequestWithUrlContentAsync(
                this.configuration.BuildUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
                null,
                AuthType.Basic
            ).ConfigureAwait(false);
        return true;
    }

    /// <inheritdoc/>
    public Task<Application> GetApplicationAsync(string id, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<Application>(
                this.configuration.BuildUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
                AuthType.Basic
            );

    /// <inheritdoc/>
    public Task<ApplicationPage> ListApplicationsAsync(ListApplicationsRequest request, Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoGetRequestWithQueryParametersAsync<ApplicationPage>(
                this.configuration.BuildUri(ApiRequest.UriType.Api, "/v2/applications"),
                AuthType.Basic,
                request
            );

    /// <inheritdoc/>
    public Task<Application> UpdateApplicationAsync(string id, CreateApplicationRequest request,
        Credentials creds = null) =>
        ApiRequest.Build(this.GetCredentials(creds), this.configuration, this.timeProvider)
            .DoRequestWithJsonContentAsync<Application>(
                HttpMethod.Put,
                this.configuration.BuildUri(ApiRequest.UriType.Api, $"/v2/applications/{id}"),
                request,
                AuthType.Basic
            );

    private Credentials GetCredentials(Credentials overridenCredentials) => overridenCredentials ?? this.Credentials;
}