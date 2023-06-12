using System.Net.Http;
using System.Net.Http.Headers;

namespace Vonage.Common.Client;

internal static class HttpRequestMessageExtensions
{
    internal static HttpRequestMessage WithAuthenticationHeader(this HttpRequestMessage message,
        AuthenticationHeaderValue header)
    {
        message.Headers.Authorization = header;
        return message;
    }

    internal static HttpRequestMessage WithUserAgent(this HttpRequestMessage message, string userAgent)
    {
        message.Headers.UserAgent.ParseAdd(userAgent);
        return message;
    }
}