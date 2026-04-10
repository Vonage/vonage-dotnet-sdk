#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Video.Sip.InitiateCall;
using Vonage.Video.Sip.PlayToneIntoCall;
using Vonage.Video.Sip.PlayToneIntoConnection;
#endregion

namespace Vonage.Video.Sip;

/// <summary>
///     Represents a client for handling SIP calls.
/// </summary>
public class SipClient
{
    private readonly VonageHttpClient<StandardApiError> vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    internal SipClient(VonageHttpClientConfiguration configuration) => this.vonageClient =
        new VonageHttpClient<StandardApiError>(configuration, JsonSerializerBuilder.BuildWithCamelCase());

    /// <summary>
    ///     Connects your SIP platform to an Vonage Video session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success with the response if the operation succeeds, Failure it if fails.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = InitiateCallRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithSessionId(sessionId)
    ///     .WithToken(token)
    ///     .WithSipUri(new Uri("sip:user@sip.example.com"))
    ///     .Create();
    /// var result = await client.VideoClient.SipClient.InitiateCallAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<InitiateCallResponse>> InitiateCallAsync(Result<InitiateCallRequest> request) =>
        this.vonageClient.SendWithResponseAsync<InitiateCallRequest, InitiateCallResponse>(request);

    /// <summary>
    ///     Sends DTMF digits to all participants in an Vonage Video call.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = PlayToneIntoCallRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithSessionId(sessionId)
    ///     .WithDigits("1234")
    ///     .Create();
    /// var result = await client.VideoClient.SipClient.PlayToneIntoCallAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<Unit>> PlayToneIntoCallAsync(Result<PlayToneIntoCallRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <summary>
    ///     Sends DTMF digits to a single participant in an Vonage Video call.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = PlayToneIntoConnectionRequest.Build()
    ///     .WithApplicationId(applicationId)
    ///     .WithSessionId(sessionId)
    ///     .WithConnectionId(connectionId)
    ///     .WithDigits("1234")
    ///     .Create();
    /// var result = await client.VideoClient.SipClient.PlayToneIntoConnectionAsync(request);
    /// ]]></code>
    /// </example>
    public Task<Result<Unit>> PlayToneIntoConnectionAsync(Result<PlayToneIntoConnectionRequest> request) =>
        this.vonageClient.SendAsync(request);
}