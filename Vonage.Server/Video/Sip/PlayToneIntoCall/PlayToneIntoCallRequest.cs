using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Server.Serialization;

namespace Vonage.Server.Video.Sip.PlayToneIntoCall;

/// <summary>
/// </summary>
public class PlayToneIntoCallRequest : IVonageRequest
{
    /// <summary>
    /// </summary>
    [JsonIgnore]
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    /// </summary>
    public string Digits { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonIgnore]
    public string SessionId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/play-dtmf";

    /// <summary>
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="sessionId"></param>
    /// <param name="digits"></param>
    /// <returns></returns>
    public static Result<PlayToneIntoCallRequest> Parse(Guid applicationId, string sessionId, string digits) =>
        Result<PlayToneIntoCallRequest>.FromSuccess(new PlayToneIntoCallRequest
            {
                SessionId = sessionId,
                Digits = digits, ApplicationId = applicationId,
            })
            .Bind(VerifyApplicationId)
            .Bind(VerifySessionId)
            .Bind(VerifyDigits);

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.Build().SerializeObject(this), Encoding.UTF8, "application/json");

    private static Result<PlayToneIntoCallRequest> VerifyApplicationId(PlayToneIntoCallRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<PlayToneIntoCallRequest> VerifyDigits(PlayToneIntoCallRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Digits, nameof(request.Digits));

    private static Result<PlayToneIntoCallRequest> VerifySessionId(PlayToneIntoCallRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));
}