using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
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

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new PlayToneIntoConnectionRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/connection/{this.ConnectionId}/play-dtmf";

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.Build().SerializeObject(this), Encoding.UTF8, "application/json");
}