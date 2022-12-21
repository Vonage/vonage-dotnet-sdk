using System.Net.Http;

namespace Vonage.Video.Beta.Video;

/// <summary>
///     Represents a request for Video features.
/// </summary>
public interface IVideoRequest
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