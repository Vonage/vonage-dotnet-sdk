using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Server.Serialization;

namespace Vonage.Server.Video.Broadcast.AddStreamToBroadcast;

/// <summary>
///     Represents a request to add a stream to a broadcast.
/// </summary>
public readonly struct AddStreamToBroadcastRequest : IVonageRequest, IHasApplicationId, IHasStreamId
{
    /// <inheritdoc />
    [JsonIgnore]
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     The Id of the broadcast.
    /// </summary>
    [JsonIgnore]
    public Guid BroadcastId { get; internal init; }

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

    /// <inheritdoc />
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
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/broadcast/{this.BroadcastId}/streams";

    private StringContent GetRequestContent() =>
        new(JsonSerializerBuilder.Build().SerializeObject(this), Encoding.UTF8, "application/json");
}