namespace Vonage.Common;

/// <summary>
///     Represents a request to be sent to Vonage's APIs.
/// </summary>
public interface IVonageRequest
{
    /// <summary>
    ///     Converts the request to a HttpRequest.
    /// </summary>
    /// <param name="token">The token.</param>
    /// <returns>The Http request.</returns>
    HttpRequestMessage BuildRequestMessage(string token);

    /// <summary>
    ///     Retrieves the endpoint's path.
    /// </summary>
    /// <returns>The endpoint's path.</returns>
    string GetEndpointPath();
}