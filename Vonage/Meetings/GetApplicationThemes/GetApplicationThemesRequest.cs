using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.Meetings.GetApplicationThemes;

/// <summary>
///     Represents a request to retrieve application themes.
/// </summary>
public struct GetApplicationThemesRequest : IVonageRequest
{
    /// <summary>
    ///     The default request.
    /// </summary>
    public static GetApplicationThemesRequest Default => new();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token) =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .WithAuthorizationToken(token)
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/beta/meetings/themes";
}