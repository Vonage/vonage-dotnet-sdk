using System;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
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
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     The signal content.
    /// </summary>
    public SignalContent Content { get; internal init; }

    /// <inheritdoc />
    public string SessionId { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new SendSignalsRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/signal";

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.Build().SerializeObject(this.Content),
            Encoding.UTF8,
            "application/json");
}