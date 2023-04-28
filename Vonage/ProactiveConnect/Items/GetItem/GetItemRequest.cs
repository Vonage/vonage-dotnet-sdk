using System;
using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.ProactiveConnect.Items.GetItem;

/// <summary>
///     Represents a request to retrieve an item.
/// </summary>
public readonly struct GetItemRequest : IVonageRequest
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
        .Initialize(HttpMethod.Get, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v0.1/bulk/lists/{this.ListId}/items/{this.ItemId}";
}