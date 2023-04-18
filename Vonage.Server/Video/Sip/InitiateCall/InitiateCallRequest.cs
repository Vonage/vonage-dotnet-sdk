using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Server.Serialization;

namespace Vonage.Server.Video.Sip.InitiateCall;

/// <summary>
///     Represents a request to initiate an outbound Sip call.
/// </summary>
public readonly struct InitiateCallRequest : IVonageRequest, IHasApplicationId
{
    /// <inheritdoc />
    [JsonIgnore]
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     The OpenTok session ID for the SIP call to join.
    /// </summary>
    public string SessionId { get; internal init; }

    /// <summary>
    ///     The sip element.
    /// </summary>
    public SipElement Sip { get; internal init; }

    /// <summary>
    ///     The OpenTok token to be used for the participant being called. You can add token data to identify that the
    ///     participant is on a SIP endpoint or for other identifying data, such as phone numbers. (The OpenTok client
    ///     libraries include properties for inspecting the connection data for a client connected to a session.) See the Token
    ///     Creation developer guide.
    /// </summary>
    public string Token { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/dial";

    /// <summary>
    ///     Parses the input into a InitiateCallRequest.
    /// </summary>
    /// <param name="applicationId">Vonage Application UUID.</param>
    /// <param name="sessionId">The OpenTok session ID for the SIP call to join.</param>
    /// <param name="token">
    ///     The OpenTok token to be used for the participant being called. You can add token data to identify
    ///     that the participant is on a SIP endpoint or for other identifying data, such as phone numbers. (The OpenTok client
    ///     libraries include properties for inspecting the connection data for a client connected to a session.) See the Token
    ///     Creation developer guide.
    /// </param>
    /// <param name="element">The sip element.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<InitiateCallRequest> Parse(Guid applicationId, string sessionId, string token,
        SipElement element) =>
        Result<InitiateCallRequest>.FromSuccess(new InitiateCallRequest
            {
                ApplicationId = applicationId,
                SessionId = sessionId,
                Sip = element,
                Token = token,
            })
            .Bind(BuilderExtensions.VerifyApplicationId)
            .Bind(VerifySessionId)
            .Bind(VerifyToken);

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.Build().SerializeObject(this), Encoding.UTF8, "application/json");

    private static Result<InitiateCallRequest> VerifySessionId(InitiateCallRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

    private static Result<InitiateCallRequest> VerifyToken(InitiateCallRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Token, nameof(request.Token));
}