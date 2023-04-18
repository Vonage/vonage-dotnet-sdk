using System;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Server.Serialization;

namespace Vonage.Server.Video.Signaling.SendSignals;

/// <summary>
///     Represents a request to send a signal to all participants.
/// </summary>
public readonly struct SendSignalsRequest : IVonageRequest, IHasApplicationId, IHasSessionId
{
    private SendSignalsRequest(Guid applicationId, string sessionId, SignalContent content)
    {
        this.ApplicationId = applicationId;
        this.SessionId = sessionId;
        this.Content = content;
    }

    /// <inheritdoc />
    public Guid ApplicationId { get; }

    /// <summary>
    ///     The signal content.
    /// </summary>
    public SignalContent Content { get; }

    /// <inheritdoc />
    public string SessionId { get; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/signal";

    /// <summary>
    ///     Parses the input into a SendSignalsRequest.
    /// </summary>
    /// <param name="applicationId">The Vonage application UUID.</param>
    /// <param name="sessionId">The Video session Id.</param>
    /// <param name="content"> The signal content.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<SendSignalsRequest> Parse(Guid applicationId, string sessionId, SignalContent content) =>
        Result<SendSignalsRequest>
            .FromSuccess(new SendSignalsRequest(applicationId, sessionId, content))
            .Bind(BuilderExtensions.VerifyApplicationId)
            .Bind(BuilderExtensions.VerifySessionId)
            .Bind(VerifyContentType)
            .Bind(VerifyContentData);

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.Build().SerializeObject(this.Content),
            Encoding.UTF8,
            "application/json");

    private static Result<SendSignalsRequest> VerifyContentData(SendSignalsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Content.Data, nameof(SignalContent.Data));

    private static Result<SendSignalsRequest> VerifyContentType(SendSignalsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Content.Type, nameof(SignalContent.Type));
}