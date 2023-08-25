using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Meetings.GetRooms;

/// <summary>
///     Represents a request to retrieve all rooms.
/// </summary>
public readonly struct GetRoomsRequest : IVonageRequest
{
    private const string DefaultEndpoint = "/v1/meetings/rooms";

    /// <summary>
    ///     The ID to end returning events at (excluding end_id itself).
    /// </summary>
    public Maybe<int> EndId { get; init; }

    /// <summary>
    ///     The maximum number of rooms in the current page.
    /// </summary>
    public Maybe<int> PageSize { get; init; }

    /// <summary>
    ///     The ID to start returning events at.
    /// </summary>
    public Maybe<int> StartId { get; init; }

    /// <summary>
    ///     Build the request with default values.
    /// </summary>
    /// <returns>The request.</returns>
    public static IOptionalBuilder Build() => new GetRoomsRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => QueryHelpers.AddQueryString(DefaultEndpoint, this.GetQueryStringParameters());

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string>();
        this.StartId.Map(value => value.ToString()).IfSome(value => parameters.Add("start_id", value));
        this.EndId.Map(value => value.ToString()).IfSome(value => parameters.Add("end_id", value));
        this.PageSize.Map(value => value.ToString()).IfSome(value => parameters.Add("page_size", value));
        return parameters;
    }
}