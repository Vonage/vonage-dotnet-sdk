using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Server.Serialization;

namespace Vonage.Server.Video.Broadcast.AddStreamToBroadcast;

/// <inheritdoc />
public readonly struct AddStreamToBroadcastRequest : IVonageRequest
{
    /// <summary>
    ///     The Vonage Application UUID.
    /// </summary>
    [JsonIgnore]
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     The Id of the broadcast.
    /// </summary>
    [JsonIgnore]
    public string BroadcastId { get; internal init; }

    /// <summary>
    ///     Whether to include the stream's audio.
    /// </summary>
    [JsonPropertyOrder(1)]
    public bool HasAudio { get; internal init; }

    /// <summary>
    ///     Whether to include the stream's video.
    /// </summary>
    [JsonPropertyOrder(2)]
    public bool HasVideo { get; internal init; }

    /// <summary>
    ///     The Id of the stream to add.
    /// </summary>
    [JsonPropertyName("addStream")]
    [JsonPropertyOrder(0)]
    public Guid StreamId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(new HttpMethod("PATCH"), this.GetEndpointPath())
            .WithContent(this.GetRequestContent())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/broadcast/{this.BroadcastId}";

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.Build().SerializeObject(this), Encoding.UTF8, "application/json");
}