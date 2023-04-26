using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.ProactiveConnect.Lists.GetLists;

/// <summary>
///     Represents a request to retrieve all lists.
/// </summary>
public readonly struct GetListsRequest : IVonageRequest
{
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
    public static IBuilderForPage Build() => new GetListsRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v0.1/bulk/lists?page={this.Page}&page_size={this.PageSize}";
}