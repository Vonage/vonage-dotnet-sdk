using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;

namespace Vonage.Server.Video.Sessions.GetStreams;

/// <summary>
///     Represents a request to retrieve streams.
/// </summary>
public readonly struct GetStreamsRequest : IVonageRequest, IHasApplicationId, IHasSessionId
{
    /// <inheritdoc />
    public Guid ApplicationId { get; internal init; }

    /// <inheritdoc />
    public string SessionId { get; internal init; }

    /// <summary>
    /// Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new GetStreamsRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/session/{this.SessionId}/stream";
}