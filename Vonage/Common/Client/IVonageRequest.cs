using System.Net.Http;

namespace Vonage.Common.Client;

/// <summary>
///     Represents a request to be sent to Vonage's APIs.
/// </summary>
public interface IVonageRequest
{
    /// <summary>
    ///     Converts the request to a HttpRequest.
    /// </summary>
    /// <returns>The Http request.</returns>
    HttpRequestMessage BuildRequestMessage();
}