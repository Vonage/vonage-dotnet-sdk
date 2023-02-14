using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Server.Serialization;

namespace Vonage.Server.Video.Sip.PlayToneIntoConnection;

/// <summary>
/// 
/// </summary>
public class PlayToneIntoConnectionRequest : IVonageRequest
{
    /// <summary>
    /// </summary>
    [JsonIgnore]
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonIgnore]
    public string ConnectionId { get; internal init; }

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
    public string GetEndpointPath() =>
        $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/connection/{this.ConnectionId}/play-dtmf";

    /// <summary>
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="sessionId"></param>
    /// <param name="connectionId"></param>
    /// <param name="digits"></param>
    /// <returns></returns>
    public static Result<PlayToneIntoConnectionRequest> Parse(Guid applicationId, string sessionId, string connectionId,
        string digits) =>
        Result<PlayToneIntoConnectionRequest>.FromSuccess(new PlayToneIntoConnectionRequest
            {
                SessionId = sessionId,
                Digits = digits,
                ApplicationId = applicationId,
                ConnectionId = connectionId,
            })
            .Bind(VerifyApplicationId)
            .Bind(VerifySessionId)
            .Bind(VerifyConnectionId)
            .Bind(VerifyDigits);

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.Build().SerializeObject(this), Encoding.UTF8, "application/json");

    private static Result<PlayToneIntoConnectionRequest> VerifyApplicationId(PlayToneIntoConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    private static Result<PlayToneIntoConnectionRequest> VerifyConnectionId(PlayToneIntoConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConnectionId, nameof(request.ConnectionId));

    private static Result<PlayToneIntoConnectionRequest> VerifyDigits(PlayToneIntoConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Digits, nameof(request.Digits));

    private static Result<PlayToneIntoConnectionRequest> VerifySessionId(PlayToneIntoConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));
}