using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Conversations.GetConversations;

/// <inheritdoc />
public readonly struct GetConversationsRequest : IVonageRequest
{
    private const string ExpectedDateFormat = "yyyy-MM-ddTHH:mm:ssZ";

    /// <summary>
    ///     The cursor to start returning results from. You are not expected to provide this manually, but to follow the url
    ///     provided in _links.next.href or _links.prev.href in the response which contains a cursor value.
    /// </summary>
    public Maybe<string> Cursor { get; }

    /// <summary>
    ///     Filter records that occurred before this point in time.
    /// </summary>
    public Maybe<DateTimeOffset> EndDate { get; }

    /// <summary>
    ///     Defines the data ordering.
    /// </summary>
    public FetchOrder Order { get; }

    /// <summary>
    ///     Number of results per page.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    ///     Filter records that occurred after this point in time.
    /// </summary>
    public Maybe<DateTimeOffset> StartDate { get; }

    internal GetConversationsRequest(Maybe<string> cursor, Maybe<DateTimeOffset> endDate, FetchOrder order,
        int pageSize, Maybe<DateTimeOffset> startDate)
    {
        this.Cursor = cursor;
        this.EndDate = endDate;
        this.Order = order;
        this.PageSize = pageSize;
        this.StartDate = startDate;
    }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForOptional Build() => new GetConversationsRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => UriHelpers.BuildUri("/v1/conversations", this.GetQueryStringParameters());

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string>
        {
            {"page_size", this.PageSize.ToString()},
            {"order", this.Order.AsString(EnumFormat.Description)},
        };
        this.StartDate.IfSome(value =>
            parameters.Add("date_start", value.ToString(ExpectedDateFormat, CultureInfo.InvariantCulture)));
        this.EndDate.IfSome(value =>
            parameters.Add("date_end", value.ToString(ExpectedDateFormat, CultureInfo.InvariantCulture)));
        this.Cursor.IfSome(value => parameters.Add("cursor", value));
        return parameters;
    }
}