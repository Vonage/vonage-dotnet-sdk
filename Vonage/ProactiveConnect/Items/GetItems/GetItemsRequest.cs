using System;
using System.Net.Http;
using EnumsNET;
using Vonage.Common.Client;

namespace Vonage.ProactiveConnect.Items.GetItems;

/// <summary>
///     Represents a request to retrieve items.
/// </summary>
public readonly struct GetItemsRequest : IVonageRequest
{
    /// <summary>
    ///     Unique identifier for a list.
    /// </summary>
    public Guid ListId { get; internal init; }

    /// <summary>
    ///     Defines the data ordering.
    /// </summary>
    public FetchOrder Order { get; internal init; }

    /// <summary>
    ///     Page of results to jump to.
    /// </summary>
    public int Page { get; internal init; }

    /// <summary>
    ///     Number of results per page.
    /// </summary>
    public int PageSize { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForListId Build() => new GetItemsRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/v0.1/bulk/lists/{this.ListId}/items?page={this.Page}&page_size={this.PageSize}&order={this.Order.AsString(EnumFormat.Description)}";
}