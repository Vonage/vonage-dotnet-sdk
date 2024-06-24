using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Server;

namespace Vonage.Video.ExperienceComposer.Start;

/// <inheritdoc />
public struct StartRequest : IVonageRequest
{
    /// <summary>
    /// </summary>
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    /// </summary>
    public string SessionId { get; internal init; }

    /// <summary>
    /// </summary>
    public string Token { get; internal init; }

    /// <summary>
    /// </summary>
    public Uri Url { get; internal init; }

    /// <summary>
    /// </summary>
    public int MaxDuration { get; internal init; }

    /// <summary>
    /// </summary>
    public RenderResolution Resolution { get; internal init; }

    /// <summary>
    /// </summary>
    public StartProperties Properties { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => throw new NotImplementedException();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/render";

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new StartRequestBuilder();
}

/// <summary>
/// </summary>
/// <param name="Name"></param>
public record StartProperties(string Name);