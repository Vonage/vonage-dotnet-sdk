using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.Meetings.GetDialNumbers;

/// <summary>
///     Represents a request to retrieve dial-in numbers.
/// </summary>
public readonly struct GetDialNumbersRequest : IVonageRequest
{
    /// <summary>
    ///     The default request.
    /// </summary>
    public static GetDialNumbersRequest Default => new();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => "/meetings/dial-in-numbers";
}