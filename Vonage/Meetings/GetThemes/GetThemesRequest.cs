using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.Meetings.GetThemes;

/// <summary>
///     Represents a request to retrieve all themes.
/// </summary>
public struct GetThemesRequest : IVonageRequest
{
    /// <summary>
    ///     The default request.
    /// </summary>
    public static GetThemesRequest Default => new();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token) =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .WithAuthorizationToken(token)
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/beta/meetings/themes";
}