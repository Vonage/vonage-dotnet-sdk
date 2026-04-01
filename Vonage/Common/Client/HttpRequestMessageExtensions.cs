#region
using System.Net.Http;
using System.Net.Http.Headers;
#endregion

namespace Vonage.Common.Client;

/// <summary>
///     Extension methods for configuring <see cref="HttpRequestMessage" /> instances with Vonage-specific headers.
/// </summary>
internal static class HttpRequestMessageExtensions
{
    /// <summary>
    ///     Adds an authentication header to the HTTP request message.
    /// </summary>
    /// <param name="message">The HTTP request message to modify.</param>
    /// <param name="header">The authentication header value (typically Bearer or Basic).</param>
    /// <returns>The modified HTTP request message for method chaining.</returns>
    internal static HttpRequestMessage WithAuthenticationHeader(this HttpRequestMessage message,
        AuthenticationHeaderValue header)
    {
        message.Headers.Authorization = header;
        return message;
    }

    /// <summary>
    ///     Adds a User-Agent header to the HTTP request message.
    /// </summary>
    /// <param name="message">The HTTP request message to modify.</param>
    /// <param name="userAgent">The User-Agent string identifying the client application.</param>
    /// <returns>The modified HTTP request message for method chaining.</returns>
    internal static HttpRequestMessage WithUserAgent(this HttpRequestMessage message, string userAgent)
    {
        message.Headers.UserAgent.ParseAdd(userAgent);
        return message;
    }
}