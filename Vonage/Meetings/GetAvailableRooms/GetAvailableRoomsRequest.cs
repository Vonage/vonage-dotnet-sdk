using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;
using Vonage.Common.Client;

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
    private GetAvailableRoomsRequest(string startId, string endId)
    {
        this.StartId = startId;
        this.EndId = endId;
    }

    /// <summary>
    ///     The ID to end returning events at (excluding end_id itself).
    /// </summary>
    public string EndId { get; }

    /// <summary>
    ///     The ID to start returning events at.
    /// </summary>
    public string StartId { get; }

    /// <summary>
    ///     Build the request with default values.
    /// </summary>
    /// <returns>The request.</returns>
    public static GetAvailableRoomsRequest Build() => new(null, null);

    /// <summary>
    ///     Build the request with the specified values.
    /// </summary>
    /// <param name="startId">The ID to start returning events at.</param>
    /// <param name="endId">The ID to end returning events at (excluding end_id itself).</param>
    /// <returns>The request</returns>
    public static GetAvailableRoomsRequest Build(string startId, string endId) => new(startId, endId);

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, this.GetEndpointPath());
        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        return httpRequest;
    }

    /// <inheritdoc />
    public string GetEndpointPath() => QueryHelpers.AddQueryString(DefaultEndpoint, this.GetQueryStringParameters());

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string>();
        if (!string.IsNullOrWhiteSpace(this.StartId))
        {
            parameters.Add("start_id", this.StartId);
        }

        if (!string.IsNullOrWhiteSpace(this.EndId))
        {
            parameters.Add("end_id", this.EndId);
        }

        return parameters;
    }
}