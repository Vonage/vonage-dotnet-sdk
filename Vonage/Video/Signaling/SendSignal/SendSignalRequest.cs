using System;
using System.Net.Http;
using System.Text;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Serialization;

namespace Vonage.Video.Signaling.SendSignal;

/// <summary>
///     Represents a request to send a signal to specific participant.
/// </summary>
public readonly struct SendSignalRequest : IVonageRequest, IHasApplicationId, IHasSessionId, IHasConnectionId
{
    /// <inheritdoc />
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    public string ConnectionId { get; internal init; }

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
    public static IBuilderForApplicationId Build() => new SendSignalRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/connection/{this.ConnectionId}/signal";

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this.Content),
            Encoding.UTF8,
            "application/json");
}