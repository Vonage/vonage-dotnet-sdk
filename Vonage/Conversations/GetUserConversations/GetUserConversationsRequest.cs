using System;
using System.ComponentModel;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Conversations.GetUserConversations;

/// <inheritdoc />
public readonly struct GetUserConversationsRequest : IVonageRequest
{
    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => throw new NotImplementedException();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v1/users/{this.UserId}/conversations";

    /// <summary>
    ///     The User ID.
    /// </summary>
    public string UserId { get; internal init; }

    /// <summary>
    ///     The cursor to start returning results from. You are not expected to provide this manually, but to follow the url
    ///     provided in _links.next.href or _links.prev.href in the response which contains a cursor value.
    /// </summary>
    public Maybe<string> Cursor { get; internal init; }

    /// <summary>
    ///     Defines the data ordering.
    /// </summary>
    public FetchOrder Order { get; internal init; }

    /// <summary>
    ///     Defines the column used for ordering.
    /// </summary>
    public string OrderBy { get; internal init; }

    /// <summary>
    ///     Number of results per page.
    /// </summary>
    public int PageSize { get; internal init; }

    /// <summary>
    ///     Filter records that occurred after this point in time.
    /// </summary>
    public Maybe<DateTimeOffset> StartDate { get; internal init; }

    /// <summary>
    ///     Defines the state of a conversation.
    /// </summary>
    public Maybe<State> State { get; internal init; }

    /// <summary>
    ///     Defines whether custom data should be included or not.
    /// </summary>
    public bool IncludeCustomData { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForUserId Build() => new GetUserConversationsRequestBuilder();
}

/// <summary>
///     Defines the state of a conversation.
/// </summary>
public enum State
{
    /// <summary>
    /// </summary>
    [Description("asc")] Invited,

    /// <summary>
    /// </summary>
    [Description("desc")] Joined,

    /// <summary>
    /// </summary>
    [Description("desc")] Left,
}