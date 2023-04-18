﻿using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Server.Serialization;

namespace Vonage.Server.Video.Sip.PlayToneIntoConnection;

/// <summary>
///     Represents a request to play a tone for a specific participant of a session.
/// </summary>
public class PlayToneIntoConnectionRequest : IVonageRequest, IHasApplicationId, IHasSessionId, IHasConnectionId
{
    /// <inheritdoc />
    [JsonIgnore]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [JsonIgnore]
    public string ConnectionId { get; internal init; }

    /// <summary>
    ///     The string of DTMF digits to send. This can include 0-9, '*', '#', and 'p'. A p indicates a pause of 500ms (if you
    ///     need to add a delay in sending the digits).
    /// </summary>
    public string Digits { get; internal init; }

    /// <inheritdoc />
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
    ///     Parses the input into a PlayToneIntoConnectionRequest.
    /// </summary>
    /// <param name="applicationId">Vonage Application UUID.</param>
    /// <param name="sessionId">Video session ID.</param>
    /// <param name="connectionId">Specific publisher connection ID.</param>
    /// <param name="digits">
    ///     The string of DTMF digits to send. This can include 0-9, '*', '#', and 'p'. A p indicates a pause
    ///     of 500ms (if you need to add a delay in sending the digits).
    /// </param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<PlayToneIntoConnectionRequest> Parse(Guid applicationId, string sessionId, string connectionId,
        string digits) =>
        Result<PlayToneIntoConnectionRequest>.FromSuccess(new PlayToneIntoConnectionRequest
            {
                SessionId = sessionId,
                Digits = digits,
                ApplicationId = applicationId,
                ConnectionId = connectionId,
            })
            .Bind(BuilderExtensions.VerifyApplicationId)
            .Bind(BuilderExtensions.VerifySessionId)
            .Bind(BuilderExtensions.VerifyConnectionId)
            .Bind(VerifyDigits);

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.Build().SerializeObject(this), Encoding.UTF8, "application/json");

    private static Result<PlayToneIntoConnectionRequest> VerifyDigits(PlayToneIntoConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Digits, nameof(request.Digits));
}