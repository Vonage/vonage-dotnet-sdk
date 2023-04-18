using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;
using Vonage.Server.Serialization;

namespace Vonage.Server.Video.Broadcast.RemoveStreamFromBroadcast;

/// <summary>
///     Represents a request to remove a stream from a broadcast.
/// </summary>
public readonly struct RemoveStreamFromBroadcastRequest : IVonageRequest, IHasApplicationId, IHasStreamId
{
    /// <inheritdoc />
    [JsonIgnore]
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     The Id of the broadcast.
    /// </summary>
    [JsonIgnore]
    public Guid BroadcastId { get; internal init; }

    /// <inheritdoc />
    [JsonPropertyName("removeStream")]
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