using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;

namespace Vonage.Server.Video.Broadcast.StopBroadcast;

/// <summary>
///     Represents a request to stop a broadcast.
/// </summary>
public readonly struct StopBroadcastRequest : IVonageRequest, IHasApplicationId, IHasBroadcastId
{
    /// <inheritdoc />
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    public Guid BroadcastId { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new StopBroadcastRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Post, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/broadcast/{this.BroadcastId}/stop";
}