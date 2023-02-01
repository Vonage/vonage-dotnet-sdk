using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Meetings.GetRoomsByTheme;

/// <summary>
///     Represents a request to get all rooms associated with a theme.
/// </summary>
public readonly struct GetRoomsByThemeRequest : IVonageRequest
{
    /// <summary>
    ///     The ID to end returning events at (excluding end_id itself).
    /// </summary>
    public Maybe<string> EndId { get; }

    /// <summary>
    ///     The ID to start returning events at.
    /// </summary>
    public Maybe<string> StartId { get; }

    /// <summary>
    /// </summary>
    public string ThemeId { get; }

    internal GetRoomsByThemeRequest(string themeId, Maybe<string> startId, Maybe<string> endId)
    {
        this.ThemeId = themeId;
        this.StartId = startId;
        this.EndId = endId;
    }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => QueryHelpers.AddQueryString($"/beta/meetings/themes/{this.ThemeId}/rooms",
        this.GetQueryStringParameters());

    private Dictionary<string, string> GetQueryStringParameters()
    {
        var parameters = new Dictionary<string, string>();
        this.StartId.IfSome(value => parameters.Add("start_id", value));
        this.EndId.IfSome(value => parameters.Add("end_id", value));
        return parameters;
    }
}