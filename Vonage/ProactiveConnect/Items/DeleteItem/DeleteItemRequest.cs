using System;
using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.ProactiveConnect.Items.DeleteItem;

/// <summary>
///     Represents a request to delete an item.
/// </summary>
public readonly struct DeleteItemRequest : IVonageRequest
{
    /// <summary>
    ///     Unique identifier for the item.
    /// </summary>
    public Guid ItemId { get; internal init; }

    /// <summary>
    ///     Unique identifier for the list.
    /// </summary>
    public Guid ListId { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForListId Build() => new DeleteItemRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Delete, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v0.1/bulk/lists/{this.ListId}/items/{this.ItemId}";
}