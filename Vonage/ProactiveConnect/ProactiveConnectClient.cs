using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.ProactiveConnect.Lists.GetLists;

namespace Vonage.ProactiveConnect;

internal class ProactiveConnectClient : IProactiveConnectClient
{
    private readonly VonageHttpClient vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    internal ProactiveConnectClient(VonageHttpClientConfiguration configuration) =>
        this.vonageClient = new VonageHttpClient(configuration, JsonSerializer.BuildWithSnakeCase());

    /// <inheritdoc />
    public Task<Result<GetListsResponse>> GetListsAsync(Result<GetListsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetListsRequest, GetListsResponse>(request);
}