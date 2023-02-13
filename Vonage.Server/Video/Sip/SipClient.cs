using System;
using System.Net.Http;
using Vonage.Common;
using Vonage.Common.Client;

namespace Vonage.Server.Video.Sip;

/// <inheritdoc />
public class SipClient
{
    private readonly VonageHttpClient vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGeneration">Function used for generating a token.</param>
    /// <param name="userAgent">The user agent.</param>
    public SipClient(HttpClient httpClient, Func<string> tokenGeneration, string userAgent) =>
        this.vonageClient =
            new VonageHttpClient(httpClient, JsonSerializer.BuildWithSnakeCase(),
                new HttpClientOptions(tokenGeneration, userAgent));
}