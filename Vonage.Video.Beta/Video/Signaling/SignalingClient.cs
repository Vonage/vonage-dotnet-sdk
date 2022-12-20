using System;
using System.Net.Http;

namespace Vonage.Video.Beta.Video.Signaling;

/// <inheritdoc />
public class SignalingClient : ISignalingClient
{
    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="httpClient">Http Client to used for further connections.</param>
    /// <param name="tokenGeneration">Function used for generating a token.</param>
    public SignalingClient(HttpClient httpClient, Func<string> tokenGeneration)
    {
    }
}