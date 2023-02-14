using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Server.Serialization;

namespace Vonage.Server.Video.Sip.InitiateCall;

/// <summary>
///     Represents a request to initiate an outbound Sip call.
/// </summary>
public readonly struct InitiateCallRequest : IVonageRequest
{
    /// <summary>
    /// </summary>
    [JsonIgnore]
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    /// </summary>
    public string SessionId { get; internal init; }

    /// <summary>
    /// </summary>
    public SipElement Sip { get; internal init; }

    /// <summary>
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
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="sessionId"></param>
    /// <param name="token"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    public static Result<InitiateCallRequest> Parse(Guid applicationId, string sessionId, string token,
        SipElement element) =>
        Result<InitiateCallRequest>.FromSuccess(new InitiateCallRequest
            {
                ApplicationId = applicationId,
                SessionId = sessionId,
                Sip = element,
                Token = token,
            })
            .Bind(VerifyApplicationId)
            .Bind(VerifySessionId)
            .Bind(VerifyToken);

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.Build().SerializeObject(this), Encoding.UTF8, "application/json");

    private static Result<InitiateCallRequest> VerifyApplicationId(InitiateCallRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<InitiateCallRequest> VerifySessionId(InitiateCallRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

    private static Result<InitiateCallRequest> VerifyToken(InitiateCallRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Token, nameof(request.Token));
}