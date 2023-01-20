using System.Net.Http;
using System.Net.Http.Headers;
using Vonage.Common.Client;

namespace Vonage.Meetings.GetDialNumbers;

/// <summary>
///     Represents a request to retrieve dial-in numbers.
/// </summary>
public readonly struct GetDialNumbersRequest : IVonageRequest
{
    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage(string token)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, this.GetEndpointPath());
        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return httpRequest;
    }

    /// <inheritdoc />
    public string GetEndpointPath() => "/beta/meetings/dial-in-numbers";
}