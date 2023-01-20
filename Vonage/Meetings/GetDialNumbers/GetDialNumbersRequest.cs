using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.Meetings.GetDialNumbers;

/// <summary>
///     Represents a request to retrieve dial-in numbers.
/// </summary>
public readonly struct GetDialNumbersRequest : IVonageRequest
{
    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token) =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .WithAuthorizationToken(token)
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/beta/meetings/dial-in-numbers";
}