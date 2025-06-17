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

namespace Vonage.Video.Signaling.SendSignals;

/// <summary>
///     Represents a request to send a signal to all participants.
/// </summary>
[Builder]
public readonly partial struct SendSignalsRequest : IVonageRequest, IHasApplicationId, IHasSessionId
{
    /// <summary>
    ///     The signal content.
    /// </summary>
    [Mandatory(2)]
    public SignalContent Content { get; internal init; }

    /// <inheritdoc />
    [Mandatory(0)]
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    [Mandatory(1)]
    public string SessionId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/signal")
            .WithContent(this.GetRequestContent())
            .Build();

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this.Content),
            Encoding.UTF8,
            "application/json");

    [ValidationRule]
    internal static Result<SendSignalsRequest> VerifyApplicationId(SendSignalsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApplicationId, nameof(request.ApplicationId));

    [ValidationRule]
    internal static Result<SendSignalsRequest> VerifyContentData(SendSignalsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Content.Data, nameof(SignalContent.Data));

    [ValidationRule]
    internal static Result<SendSignalsRequest> VerifyContentType(SendSignalsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Content.Type, nameof(SignalContent.Type));

    [ValidationRule]
    internal static Result<SendSignalsRequest> VerifySessionId(SendSignalsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));
}