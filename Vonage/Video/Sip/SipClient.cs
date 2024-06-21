using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Video.Sip.InitiateCall;
using Vonage.Video.Sip.PlayToneIntoCall;
using Vonage.Video.Sip.PlayToneIntoConnection;

namespace Vonage.Video.Sip;

/// <summary>
///     Represents a client for handling SIP calls.
/// </summary>
public class SipClient
{
    private readonly VonageHttpClient vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    internal SipClient(VonageHttpClientConfiguration configuration) => this.vonageClient =
        new VonageHttpClient(configuration, JsonSerializerBuilder.BuildWithCamelCase());

    /// <summary>
    ///     Connects your SIP platform to an OpenTok session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success with the response if the operation succeeds, Failure it if fails.</returns>
    public Task<Result<InitiateCallResponse>> InitiateCallAsync(Result<InitiateCallRequest> request) =>
        this.vonageClient.SendWithResponseAsync<InitiateCallRequest, InitiateCallResponse>(request);

    /// <summary>
    ///     Sends DTMF digits to all participants in an OpenTok call.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public Task<Result<Unit>> PlayToneIntoCallAsync(Result<PlayToneIntoCallRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <summary>
    ///     Sends DTMF digits to a single participant in an OpenTok call.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    public Task<Result<Unit>> PlayToneIntoConnectionAsync(Result<PlayToneIntoConnectionRequest> request) =>
        this.vonageClient.SendAsync(request);
}