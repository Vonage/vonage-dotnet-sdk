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
///     Represents a request to play a tone for all participants of a session.
/// </summary>
public class PlayToneIntoCallRequest : IVonageRequest
{
    /// <summary>
    ///     Vonage Application UUID.
    /// </summary>
    [JsonIgnore]
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     The string of DTMF digits to send. This can include 0-9, '*', '#', and 'p'. A p indicates a pause of 500ms (if you
    ///     need to add a delay in sending the digits).
    /// </summary>
    public string Digits { get; internal init; }

    /// <summary>
    ///     Video session ID.
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
    ///     Parses the input into a PlayToneIntoCallRequest.
    /// </summary>
    /// <param name="applicationId">Vonage Application UUID.</param>
    /// <param name="sessionId">Video session ID.</param>
    /// <param name="digits">
    ///     The string of DTMF digits to send. This can include 0-9, '*', '#', and 'p'. A p indicates a pause
    ///     of 500ms (if you need to add a delay in sending the digits).
    /// </param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
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