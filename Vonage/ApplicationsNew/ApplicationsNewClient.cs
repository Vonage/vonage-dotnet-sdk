using System.Threading.Tasks;
using Vonage.ApplicationsNew.CreateApplication;
using Vonage.ApplicationsNew.DeleteApplication;
using Vonage.ApplicationsNew.GetApplication;
using Vonage.ApplicationsNew.ListApplications;
using Vonage.ApplicationsNew.UpdateApplication;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;

namespace Vonage.ApplicationsNew;

/// <inheritdoc />
internal class ApplicationsNewClient : IApplicationsNewClient
{
    private readonly VonageHttpClient<StandardApiError> vonageClient;

    /// <summary>
    ///     Creates a new Application API client.
    /// </summary>
    /// <param name="configuration">The HTTP client configuration.</param>
    public ApplicationsNewClient(VonageHttpClientConfiguration configuration) =>
        this.vonageClient =
            new VonageHttpClient<StandardApiError>(configuration, JsonSerializerBuilder.BuildWithSnakeCase());

    /// <inheritdoc />
    public Task<Result<ListApplicationsResponse>> ListApplicationsAsync(Result<ListApplicationsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<ListApplicationsRequest, ListApplicationsResponse>(request);

    /// <inheritdoc />
    public Task<Result<ApplicationResponse>> CreateApplicationAsync(Result<CreateApplicationRequest> request) =>
        this.vonageClient.SendWithResponseAsync<CreateApplicationRequest, ApplicationResponse>(request);

    /// <inheritdoc />
    public Task<Result<ApplicationResponse>> GetApplicationAsync(Result<GetApplicationRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetApplicationRequest, ApplicationResponse>(request);

    /// <inheritdoc />
    public Task<Result<ApplicationResponse>> UpdateApplicationAsync(Result<UpdateApplicationRequest> request) =>
        this.vonageClient.SendWithResponseAsync<UpdateApplicationRequest, ApplicationResponse>(request);

    /// <inheritdoc />
    public Task<Result<Unit>> DeleteApplicationAsync(Result<DeleteApplicationRequest> request) =>
        this.vonageClient.SendAsync(request);
}
