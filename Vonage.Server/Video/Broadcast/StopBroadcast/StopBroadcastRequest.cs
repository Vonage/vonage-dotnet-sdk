using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;

namespace Vonage.Server.Video.Broadcast.StopBroadcast;

/// <summary>
///     Represents a request to stop a broadcast.
/// </summary>
public readonly struct StopBroadcastRequest : IVonageRequest, IHasApplicationId
{
    /// <inheritdoc />
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     The Id of the broadcast.
    /// </summary>
    public Guid BroadcastId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/broadcast/{this.BroadcastId}/stop";
}