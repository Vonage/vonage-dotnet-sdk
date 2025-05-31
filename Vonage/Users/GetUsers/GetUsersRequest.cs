#region
using System.Collections.Generic;
using System.Net.Http;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.ProactiveConnect;
#endregion

namespace Vonage.Users.GetUsers;

/// <inheritdoc />
public readonly struct GetUsersRequest : IVonageRequest
{
    /// <summary>
    ///     The cursor to start returning results from. You are not expected to provide this manually, but to follow the url
    ///     provided in _links.next.href or _links.prev.href in the response which contains a cursor value.
    /// </summary>
    public Maybe<string> Cursor { get; internal init; }

    /// <summary>
    ///     Unique name for a user
    /// </summary>
    public Maybe<string> Name { get; internal init; }

    /// <summary>
    ///     Defines the data ordering.
    /// </summary>
    public FetchOrder Order { get; internal init; }

    /// <summary>
    ///     Number of results per page.
    /// </summary>
    public int PageSize { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForOptional Build() => new GetUsersRequestBuilder(Maybe<string>.None);

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, UriHelpers.BuildUri("/v1/users", this.GetQueryStringParameters()))
        .Build();

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string>
        {
            {"page_size", this.PageSize.ToString()},
            {"order", this.Order.AsString(EnumFormat.Description)},
        };
        this.Name.IfSome(value => parameters.Add("name", value));
        this.Cursor.IfSome(value => parameters.Add("cursor", value));
        return parameters;
    }
}