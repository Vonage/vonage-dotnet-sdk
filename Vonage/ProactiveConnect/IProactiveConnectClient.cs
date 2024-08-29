#region
using System;
using System.Threading.Tasks;
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
#endregion

namespace Vonage.ProactiveConnect;

/// <summary>
///     Exposes Proactive Connect features.
/// </summary>
public interface IProactiveConnectClient
{
    /// <summary>
    ///     Clears a list.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    [Obsolete("Proactive Connect API is being sunset. It will be removed from the SDK on the next major version.")]
    Task<Result<Unit>> ClearListAsync(Result<ClearListRequest> request);

    /// <summary>
    ///     Creates an item.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    [Obsolete("Proactive Connect API is being sunset. It will be removed from the SDK on the next major version.")]
    Task<Result<ListItem>> CreateItemAsync(Result<CreateItemRequest> request);

    /// <summary>
    ///     Creates a list.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    [Obsolete("Proactive Connect API is being sunset. It will be removed from the SDK on the next major version.")]
    Task<Result<List>> CreateListAsync(Result<CreateListRequest> request);

    /// <summary>
    ///     Deletes an item.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    [Obsolete("Proactive Connect API is being sunset. It will be removed from the SDK on the next major version.")]
    Task<Result<Unit>> DeleteItemAsync(Result<DeleteItemRequest> request);

    /// <summary>
    ///     Deletes a list.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    [Obsolete("Proactive Connect API is being sunset. It will be removed from the SDK on the next major version.")]
    Task<Result<Unit>> DeleteListAsync(Result<DeleteListRequest> request);

    /// <summary>
    ///     Extracts list items as a CSV format.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    [Obsolete("Proactive Connect API is being sunset. It will be removed from the SDK on the next major version.")]
    Task<Result<string>> ExtractItemsAsync(Result<ExtractItemsRequest> request);

    /// <summary>
    ///     Retrieves all events.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    [Obsolete("Proactive Connect API is being sunset. It will be removed from the SDK on the next major version.")]
    Task<Result<PaginationResult<EmbeddedEvents>>> GetEventsAsync(Result<GetEventsRequest> request);

    /// <summary>
    ///     Retrieves an item.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    [Obsolete("Proactive Connect API is being sunset. It will be removed from the SDK on the next major version.")]
    Task<Result<ListItem>> GetItemAsync(Result<GetItemRequest> request);

    /// <summary>
    ///     Retrieves all items of a list.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    [Obsolete("Proactive Connect API is being sunset. It will be removed from the SDK on the next major version.")]
    Task<Result<PaginationResult<EmbeddedItems>>> GetItemsAsync(Result<GetItemsRequest> request);

    /// <summary>
    ///     Retrieves a single list.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    [Obsolete("Proactive Connect API is being sunset. It will be removed from the SDK on the next major version.")]
    Task<Result<List>> GetListAsync(Result<GetListRequest> request);

    /// <summary>
    ///     Retrieves all lists.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    [Obsolete("Proactive Connect API is being sunset. It will be removed from the SDK on the next major version.")]
    Task<Result<PaginationResult<EmbeddedLists>>> GetListsAsync(Result<GetListsRequest> request);

    /// <summary>
    ///     Imports list items from a CSV file.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    [Obsolete("Proactive Connect API is being sunset. It will be removed from the SDK on the next major version.")]
    Task<Result<ImportItemsResponse>> ImportItemsAsync(Result<ImportItemsRequest> request);

    /// <summary>
    ///     Fetches and replaces all items from datasource.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    [Obsolete("Proactive Connect API is being sunset. It will be removed from the SDK on the next major version.")]
    Task<Result<Unit>> ReplaceItemsAsync(Result<ReplaceItemsRequest> request);

    /// <summary>
    ///     Updates an item.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    [Obsolete("Proactive Connect API is being sunset. It will be removed from the SDK on the next major version.")]
    Task<Result<ListItem>> UpdateItemAsync(Result<UpdateItemRequest> request);

    /// <summary>
    ///     Updates a list.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    [Obsolete("Proactive Connect API is being sunset. It will be removed from the SDK on the next major version.")]
    Task<Result<List>> UpdateListAsync(Result<UpdateListRequest> request);
}