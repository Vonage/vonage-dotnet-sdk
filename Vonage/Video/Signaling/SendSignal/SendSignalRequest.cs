#region
using System;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Serialization;
#endregion

namespace Vonage.Video.Signaling.SendSignal;

/// <summary>
///     Represents a request to send a signal to specific participant.
/// </summary>
[Builder]
public readonly partial struct SendSignalRequest : IVonageRequest, IHasApplicationId, IHasSessionId, IHasConnectionId
{
    /// <summary>
    ///     The signal content.
    /// </summary>
    [Mandatory(3)]
    public SignalContent Content { get; internal init; }

    /// <inheritdoc />
    [Mandatory(0)]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [Mandatory(2)]
    public string ConnectionId { get; internal init; }

    /// <inheritdoc />
    [Mandatory(1)]
    public string SessionId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post,
                $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/connection/{this.ConnectionId}/signal")
            .WithContent(this.GetRequestContent())
            .Build();

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this.Content),
            Encoding.UTF8,
            "application/json");

    [ValidationRule]
    internal static Result<SendSignalRequest> VerifyApplicationId(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<SendSignalRequest> VerifyConnectionId(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConnectionId, nameof(request.ConnectionId));

    [ValidationRule]
    internal static Result<SendSignalRequest> VerifyContentData(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Content.Data, nameof(SignalContent.Data));

    [ValidationRule]
    internal static Result<SendSignalRequest> VerifyContentType(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Content.Type, nameof(SignalContent.Type));

    [ValidationRule]
    internal static Result<SendSignalRequest> VerifySessionId(SendSignalRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));
}