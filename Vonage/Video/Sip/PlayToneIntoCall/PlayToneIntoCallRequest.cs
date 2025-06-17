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

namespace Vonage.Video.Sip.PlayToneIntoCall;

/// <summary>
///     Represents a request to play a tone for all participants of a session.
/// </summary>
[Builder]
public readonly partial struct PlayToneIntoCallRequest : IVonageRequest, IHasApplicationId, IHasSessionId
{
    /// <summary>
    ///     The string of DTMF digits to send. This can include 0-9, '*', '#', and 'p'. A p indicates a pause of 500ms (if you
    ///     need to add a delay in sending the digits).
    /// </summary>
    [Mandatory(2)]
    public string Digits { get; internal init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Mandatory(0)]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [JsonIgnore]
    [Mandatory(1)]
    public string SessionId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/play-dtmf";

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    [ValidationRule]
    internal static Result<PlayToneIntoCallRequest> VerifyApplicationId(PlayToneIntoCallRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<PlayToneIntoCallRequest> VerifyDigits(PlayToneIntoCallRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Digits, nameof(request.Digits));

    [ValidationRule]
    internal static Result<PlayToneIntoCallRequest> VerifySessionId(PlayToneIntoCallRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));
}