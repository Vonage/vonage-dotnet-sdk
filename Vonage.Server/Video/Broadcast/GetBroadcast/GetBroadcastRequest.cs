using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;

namespace Vonage.Server.Video.Broadcast.GetBroadcast;

/// <summary>
///     Represents a request to retrieve a broadcast.
/// </summary>
public readonly struct GetBroadcastRequest : IVonageRequest, IHasApplicationId, IHasBroadcastId
{
    /// <inheritdoc />
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    public Guid BroadcastId { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new GetBroadcastRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/broadcast/{this.BroadcastId}";
}