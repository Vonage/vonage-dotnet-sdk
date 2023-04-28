using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.ProactiveConnect.Events.GetEvents;

/// <summary>
///     Represents a request to retrieve all events.
/// </summary>
public readonly struct GetEventsRequest : IVonageRequest
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
    public static IBuilderForPage Build() => new GetEventsRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v.01/bulk/events?page={this.Page}&page_size={this.PageSize}";
}