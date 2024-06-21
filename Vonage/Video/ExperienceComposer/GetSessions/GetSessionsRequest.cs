using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Client.Builders;

namespace Vonage.Video.ExperienceComposer.GetSessions;

/// <summary>
///     Represents a request to retrieve sessions.
/// </summary>
public readonly struct GetSessionsRequest : IVonageRequest, IHasApplicationId
{
    /// <inheritdoc />
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     Set a count query parameter to limit the number of experience composers to be returned. The default number of
    ///     archives returned is 50 (or fewer, if there are fewer than 50 archives). The default is 50 and the maximum is 1000
    /// </summary>
    public int Count { get; internal init; }

    /// <summary>
    ///     Set an offset query parameters to specify the index offset of the first experience composer. 0 is offset of the
    ///     most recently started archive (excluding deleted archive). 1 is the offset of the experience composer that started
    ///     prior to the most recent composer. The default value is 0.
    /// </summary>
    public int Offset { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() =>
        VonageRequestBuilder
            .Initialize(HttpMethod.Get, this.GetEndpointPath())
            .Build();

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new GetSessionsRequestBuilder();

    /// <inheritdoc />
    public string GetEndpointPath() =>
        $"/v2/project/{this.ApplicationId}/render?offset={this.Offset}&count={this.Count}";
}