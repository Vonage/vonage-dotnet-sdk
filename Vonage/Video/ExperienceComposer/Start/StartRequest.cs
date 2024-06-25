using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Serialization;
using Vonage.Server;

namespace Vonage.Video.ExperienceComposer.Start;

/// <inheritdoc />
public readonly struct StartRequest : IVonageRequest
{
    /// <summary>
    [JsonIgnore]
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    /// </summary>
    ///
    [JsonPropertyOrder(0)]
    public string SessionId { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonPropertyOrder(1)]
    public string Token { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonPropertyOrder(2)]
    public Uri Url { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonPropertyOrder(3)]
    public int MaxDuration { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonPropertyOrder(4)]
    public RenderResolution Resolution { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonPropertyOrder(5)]
    public StartProperties Properties { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

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