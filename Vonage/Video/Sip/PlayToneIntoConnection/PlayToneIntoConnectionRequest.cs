#region
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Serialization;
#endregion

namespace Vonage.Video.Sip.PlayToneIntoConnection;

/// <summary>
///     Represents a request to play a tone for a specific participant of a session.
/// </summary>
[Builder]
public readonly partial struct PlayToneIntoConnectionRequest : IVonageRequest, IHasApplicationId, IHasSessionId,
    IHasConnectionId
{
    /// <summary>
    ///     The string of DTMF digits to send. This can include 0-9, '*', '#', and 'p'. A p indicates a pause of 500ms (if you
    ///     need to add a delay in sending the digits).
    /// </summary>
    [Mandatory(3, nameof(VerifyDigits))]
    public string Digits { get; internal init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Mandatory(0, nameof(VerifyApplicationId))]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Mandatory(2, nameof(VerifyConnectionId))]
    public string ConnectionId { get; internal init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Mandatory(1, nameof(VerifySessionId))]
    public string SessionId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post,
                $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/connection/{this.ConnectionId}/play-dtmf")
            .WithContent(this.GetRequestContent())
            .Build();

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    internal static Result<PlayToneIntoConnectionRequest> VerifyApplicationId(PlayToneIntoConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    internal static Result<PlayToneIntoConnectionRequest> VerifyConnectionId(PlayToneIntoConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConnectionId, nameof(request.ConnectionId));

    internal static Result<PlayToneIntoConnectionRequest> VerifyDigits(PlayToneIntoConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Digits, nameof(request.Digits));

    internal static Result<PlayToneIntoConnectionRequest> VerifySessionId(PlayToneIntoConnectionRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));
}