using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.ProactiveConnect.Lists;
using Vonage.ProactiveConnect.Lists.ClearList;
using Vonage.ProactiveConnect.Lists.CreateList;
using Vonage.ProactiveConnect.Lists.DeleteList;
using Vonage.ProactiveConnect.Lists.GetList;
using Vonage.ProactiveConnect.Lists.GetLists;
using Vonage.ProactiveConnect.Lists.ReplaceItems;

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
    public Task<Result<Unit>> ClearListAsync(Result<ClearListRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <inheritdoc />
    public Task<Result<List>> CreateListAsync(Result<CreateListRequest> request) =>
        this.vonageClient.SendWithResponseAsync<CreateListRequest, List>(request);

    /// <inheritdoc />
    public Task<Result<List>> DeleteListAsync(Result<DeleteListRequest> request) =>
        this.vonageClient.SendWithResponseAsync<DeleteListRequest, List>(request);

    /// <inheritdoc />
    public Task<Result<List>> GetListAsync(Result<GetListRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetListRequest, List>(request);

    /// <inheritdoc />
    public Task<Result<GetListsResponse>> GetListsAsync(Result<GetListsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetListsRequest, GetListsResponse>(request);

    /// <inheritdoc />
    public Task<Result<Unit>> ReplaceItemsAsync(Result<ReplaceItemsRequest> request) =>
        this.vonageClient.SendAsync(request);
}