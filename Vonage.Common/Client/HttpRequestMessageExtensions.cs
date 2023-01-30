using System.Net.Http;
using System.Net.Http.Headers;

namespace Vonage.Common.Client;

internal static class HttpRequestMessageExtensions
{
    internal static HttpRequestMessage WithAuthorization(this HttpRequestMessage message, string token)
    {
        message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return message;
    }

    internal static HttpRequestMessage WithUserAgent(this HttpRequestMessage message, string userAgent)
    {
        message.Headers.UserAgent.ParseAdd(userAgent);
        return message;
    }
}