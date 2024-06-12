using System.Collections.Generic;
using System.Net.Http;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Conversations.GetEvents;

/// <inheritdoc />
public readonly struct GetEventsRequest : IVonageRequest
{
    /// <summary>
    ///     The conversation Id.
    /// </summary>
    public string ConversationId { get; internal init; }

    /// <summary>
    ///     The cursor to start returning results from. You are not expected to provide this manually, but to follow the url
    ///     provided in _links.next.href or _links.prev.href in the response which contains a cursor value.
    /// </summary>
    public Maybe<string> Cursor { get; internal init; }

    /// <summary>
    ///     The ID to end returning events at
    /// </summary>
    public Maybe<string> EndId { get; internal init; }

    /// <summary>
    ///     Defines the data ordering.
    /// </summary>
    public FetchOrder Order { get; internal init; }

    /// <summary>
    ///     Number of results per page.
    /// </summary>
    public int PageSize { get; internal init; }

    /// <summary>
    ///     The ID to start returning events at
    /// </summary>
    public Maybe<string> StartId { get; internal init; }

    /// <summary>
    ///     The type of event to search for. Does not currently support custom events
    /// </summary>
    public Maybe<string> EventType { get; internal init; }

    /// <summary>
    ///     Exclude deleted events from the response
    /// </summary>
    public bool ExcludeDeletedEvents { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => UriHelpers.BuildUri($"/v1/conversations/{this.ConversationId}/events",
        this.GetQueryStringParameters());

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string>
        {
            {"page_size", this.PageSize.ToString()},
            {"order", this.Order.AsString(EnumFormat.Description)},
            {"exclude_deleted_events", this.ExcludeDeletedEvents.ToString().ToLowerInvariant()},
        };
        this.Cursor.IfSome(value => parameters.Add("cursor", value));
        this.StartId.IfSome(value => parameters.Add("start_id", value));
        this.EndId.IfSome(value => parameters.Add("end_id", value));
        this.EventType.IfSome(value => parameters.Add("event_type", value));
        return parameters;
    }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForConversationId Build() => new GetEventsRequestBuilder(Maybe<string>.None);
}