using System;
using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.Server.Video.Broadcast.AddStreamToBroadcast;

/// <inheritdoc />
public readonly struct AddStreamToBroadcastRequest : IVonageRequest
{
    /// <summary>
    ///     The Vonage Application UUID.
    /// </summary>
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     The Id of the broadcast.
    /// </summary>
    public string BroadcastId { get; internal init; }

    /// <summary>
    ///     Whether to include the stream's audio.
    /// </summary>
    public bool HasAudio { get; internal init; }

    /// <summary>
    ///     Whether to include the stream's video.
    /// </summary>
    public bool HasVideo { get; internal init; }

    /// <summary>
    ///     The Id of the stream to add.
    /// </summary>
    public Guid StreamId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(new HttpMethod("PATCH"), this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/broadcast/{this.BroadcastId}";
}