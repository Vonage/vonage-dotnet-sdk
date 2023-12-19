﻿using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Conversations.GetConversations;

/// <inheritdoc />
public readonly struct GetConversationsRequest : IVonageRequest
{
    /// <summary>
    ///     The cursor to start returning results from. You are not expected to provide this manually, but to follow the url
    ///     provided in _links.next.href or _links.prev.href in the response which contains a cursor value.
    /// </summary>
    public Maybe<string> Cursor { get; }

    /// <summary>
    ///     Filter records that occurred before this point in time.
    /// </summary>
    public Maybe<DateTimeOffset> EndDate { get; internal init; }

    /// <summary>
    ///     Defines the data ordering.
    /// </summary>
    public FetchOrder Order { get; internal init; }

    /// <summary>
    ///     Number of results per page.
    /// </summary>
    public int PageSize { get; internal init; }

    /// <summary>
    ///     Filter records that occurred after this point in time.
    /// </summary>
    public Maybe<DateTimeOffset> StartDate { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForOptional Build() => new GetConversationsRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => throw new NotImplementedException();

    /// <inheritdoc />
    public string GetEndpointPath() => throw new NotImplementedException();
}