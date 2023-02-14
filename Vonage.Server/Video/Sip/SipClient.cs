using System;
using System.Net.Http;
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Server.Video.Sip.InitiateCall;
using Vonage.Server.Video.Sip.PlayToneIntoCall;

namespace Vonage.Server.Video.Sip;


/// <summary>
/// Represents a client for handling SIP calls.
/// </summary>
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
            new VonageHttpClient(httpClient, JsonSerializer.BuildWithCamelCase(),
                new HttpClientOptions(tokenGeneration, userAgent));

    /// <summary>
    /// Connects your SIP platform to an OpenTok session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The call response.</returns>
    public Task<Result<InitiateCallResponse>> InitiateCallAsync(Result<InitiateCallRequest> request) =>
        this.vonageClient.SendWithResponseAsync<InitiateCallRequest, InitiateCallResponse>(request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public Task<Result<Unit>> PlayToneIntoCallAsync(Result<PlayToneIntoCallRequest> request) =>
        this.vonageClient.SendAsync(request);
}