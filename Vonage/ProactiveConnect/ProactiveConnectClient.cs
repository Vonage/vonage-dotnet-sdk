#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.ProactiveConnect.Events.GetEvents;
using Vonage.ProactiveConnect.Items;
using Vonage.ProactiveConnect.Items.CreateItem;
using Vonage.ProactiveConnect.Items.DeleteItem;
using Vonage.ProactiveConnect.Items.ExtractItems;
using Vonage.ProactiveConnect.Items.GetItem;
using Vonage.ProactiveConnect.Items.GetItems;
using Vonage.ProactiveConnect.Items.ImportItems;
using Vonage.ProactiveConnect.Items.UpdateItem;
using Vonage.ProactiveConnect.Lists;
using Vonage.ProactiveConnect.Lists.ClearList;
using Vonage.ProactiveConnect.Lists.CreateList;
using Vonage.ProactiveConnect.Lists.DeleteList;
using Vonage.ProactiveConnect.Lists.GetList;
using Vonage.ProactiveConnect.Lists.GetLists;
using Vonage.ProactiveConnect.Lists.ReplaceItems;
using Vonage.ProactiveConnect.Lists.UpdateList;
using Vonage.Serialization;
#endregion

namespace Vonage.ProactiveConnect;

internal class ProactiveConnectClient : IProactiveConnectClient
{
    private readonly VonageHttpClient<StandardApiError> vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    internal ProactiveConnectClient(VonageHttpClientConfiguration configuration) =>
        this.vonageClient =
            new VonageHttpClient<StandardApiError>(configuration, JsonSerializerBuilder.BuildWithSnakeCase());

    /// <inheritdoc />
    public Task<Result<Unit>> ClearListAsync(Result<ClearListRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <inheritdoc />
    public Task<Result<ListItem>> CreateItemAsync(Result<CreateItemRequest> request) =>
        this.vonageClient.SendWithResponseAsync<CreateItemRequest, ListItem>(request);

    /// <inheritdoc />
    public Task<Result<List>> CreateListAsync(Result<CreateListRequest> request) =>
        this.vonageClient.SendWithResponseAsync<CreateListRequest, List>(request);

    /// <inheritdoc />
    public Task<Result<Unit>> DeleteItemAsync(Result<DeleteItemRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <inheritdoc />
    public Task<Result<Unit>> DeleteListAsync(Result<DeleteListRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <inheritdoc />
    public Task<Result<string>> ExtractItemsAsync(Result<ExtractItemsRequest> request) =>
        this.vonageClient.SendWithRawResponseAsync(request);

    /// <inheritdoc />
    public Task<Result<PaginationResult<EmbeddedEvents>>> GetEventsAsync(Result<GetEventsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetEventsRequest, PaginationResult<EmbeddedEvents>>(request);

    /// <inheritdoc />
    public Task<Result<ListItem>> GetItemAsync(Result<GetItemRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetItemRequest, ListItem>(request);

    /// <inheritdoc />
    public Task<Result<PaginationResult<EmbeddedItems>>> GetItemsAsync(Result<GetItemsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetItemsRequest, PaginationResult<EmbeddedItems>>(request);

    /// <inheritdoc />
    public Task<Result<List>> GetListAsync(Result<GetListRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetListRequest, List>(request);

    /// <inheritdoc />
    public Task<Result<PaginationResult<EmbeddedLists>>> GetListsAsync(Result<GetListsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetListsRequest, PaginationResult<EmbeddedLists>>(request);

    /// <inheritdoc />
    public Task<Result<ImportItemsResponse>> ImportItemsAsync(Result<ImportItemsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<ImportItemsRequest, ImportItemsResponse>(request);

    /// <inheritdoc />
    public Task<Result<Unit>> ReplaceItemsAsync(Result<ReplaceItemsRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <inheritdoc />
    public Task<Result<ListItem>> UpdateItemAsync(Result<UpdateItemRequest> request) =>
        this.vonageClient.SendWithResponseAsync<UpdateItemRequest, ListItem>(request);

    /// <inheritdoc />
    public Task<Result<List>> UpdateListAsync(Result<UpdateListRequest> request) =>
        this.vonageClient.SendWithResponseAsync<UpdateListRequest, List>(request);
}