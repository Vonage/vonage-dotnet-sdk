using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Meetings.GetAvailableRooms;

/// <summary>
///     Represents a request to retrieve all available rooms.
/// </summary>
public readonly struct GetAvailableRoomsRequest : IVonageRequest
{
    private const string DefaultEndpoint = "/beta/meetings/rooms";

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="startId">The ID to start returning events at.</param>
    /// <param name="endId">The ID to end returning events at (excluding end_id itself).</param>
    private GetAvailableRoomsRequest(Maybe<string> startId, Maybe<string> endId)
    {
        this.StartId = startId;
        this.EndId = endId;
    }

    /// <summary>
    ///     The ID to end returning events at (excluding end_id itself).
    /// </summary>
    public Maybe<string> EndId { get; }

    /// <summary>
    ///     The ID to start returning events at.
    /// </summary>
    public Maybe<string> StartId { get; }

    /// <summary>
    ///     Build the request with default values.
    /// </summary>
    /// <returns>The request.</returns>
    public static GetAvailableRoomsRequest Build() => new(Maybe<string>.None, Maybe<string>.None);

    /// <summary>
    ///     Build the request with the specified values.
    /// </summary>
    /// <param name="startId">The ID to start returning events at.</param>
    /// <param name="endId">The ID to end returning events at (excluding end_id itself).</param>
    /// <returns>The request</returns>
    public static GetAvailableRoomsRequest Build(string startId, string endId) =>
        new(startId ?? Maybe<string>.None, endId ?? Maybe<string>.None);

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
        this.StartId.Bind(VerifyIfNotEmpty).IfSome(value => parameters.Add("start_id", value));
        this.EndId.Bind(VerifyIfNotEmpty).IfSome(value => parameters.Add("end_id", value));
        return parameters;
    }

    private static Maybe<string> VerifyIfNotEmpty(string value) =>
        string.IsNullOrWhiteSpace(value) ? Maybe<string>.None : value;
}