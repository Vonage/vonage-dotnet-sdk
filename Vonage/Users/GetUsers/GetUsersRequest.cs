using System.Collections.Generic;
using System.Net.Http;
using EnumsNET;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Users.GetUsers;

/// <summary>
///     Represents a request to retrieve a paginated list of users with optional filtering and sorting options.
/// </summary>
public readonly struct GetUsersRequest : IVonageRequest
{
    /// <summary>
    ///     The pagination cursor for retrieving the next or previous page of results. This value is automatically extracted
    ///     from the _links.next.href or _links.prev.href in the response and should not be set manually.
    /// </summary>
    public Maybe<string> Cursor { get; internal init; }

    /// <summary>
    ///     Filters results to users matching this unique name. When specified, only users with an exact name match are returned.
    /// </summary>
    public Maybe<string> Name { get; internal init; }

    /// <summary>
    ///     The sort order for the results. Use <see cref="FetchOrder.Ascending"/> for oldest first or <see cref="FetchOrder.Descending"/> for newest first.
    /// </summary>
    public FetchOrder Order { get; internal init; }

    /// <summary>
    ///     The maximum number of user records to return per page. Defaults to 10 if not specified.
    /// </summary>
    public int PageSize { get; internal init; }

    /// <summary>
    ///     Initializes a builder for creating a GetUsersRequest with optional parameters.
    /// </summary>
    /// <returns>A builder instance for configuring the request.</returns>
    public static IBuilderForOptional Build() => new GetUsersRequestBuilder(Maybe<string>.None);

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => UriHelpers.BuildUri("/v1/users", this.GetQueryStringParameters());

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