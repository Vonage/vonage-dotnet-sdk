using System.Collections.Generic;
using System.Net.Http;
using EnumsNET;
using Microsoft.AspNetCore.WebUtilities;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.ProactiveConnect;

namespace Vonage.Users.GetUsers;

/// <inheritdoc />
public readonly struct GetUsersRequest : IVonageRequest
{
    /// <summary>
    ///     The cursor to start returning results from. You are not expected to provide this manually, but to follow the url
    ///     provided in _links.next.href or _links.prev.href in the response which contains a cursor value.
    /// </summary>
    public Maybe<string> Cursor { get; }

    /// <summary>
    ///     Unique name for a user
    /// </summary>
    public Maybe<string> Name { get; }

    /// <summary>
    ///     Defines the data ordering.
    /// </summary>
    public FetchOrder Order { get; }

    /// <summary>
    ///     Number of results per page.
    /// </summary>
    public int PageSize { get; }

    internal GetUsersRequest(Maybe<string> cursor, Maybe<string> name, FetchOrder order, int pageSize)
    {
        this.Cursor = cursor;
        this.Name = name;
        this.Order = order;
        this.PageSize = pageSize;
    }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForOptional Build() => new GetUsersRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => QueryHelpers.AddQueryString("/v1/users",
        this.GetQueryStringParameters());

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string>
        {
            {"page_size", this.PageSize.ToString()},
            {"order", this.Order.AsString(EnumFormat.Description)},
        };
        this.Name.IfSome(value => parameters.Add("name", value));
        return parameters;
    }
}