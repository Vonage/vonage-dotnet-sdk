#region
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Serialization;
using Vonage.Server;
#endregion

namespace Vonage.Video.ExperienceComposer.Start;

/// <inheritdoc />
public readonly struct StartRequest : IVonageRequest
{
    /// <summary>
    ///     The Vonage Application UUID.
    /// </summary>
    [JsonIgnore]
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     The Vonage Video session ID.
    /// </summary>
    [JsonPropertyOrder(0)]
    public string SessionId { get; internal init; }

    /// <summary>
    ///     A valid Vonage Video token with a Publisher role and (optionally) connection data to be associated with the output
    ///     stream.
    /// </summary>
    [JsonPropertyOrder(1)]
    public string Token { get; internal init; }

    /// <summary>
    ///     A publicly reachable URL controlled by the customer and capable of generating the content to be rendered without
    ///     user intervention. Must be between 15 and 2048 characters.
    /// </summary>
    [JsonPropertyOrder(2)]
    public Uri Url { get; internal init; }

    /// <summary>
    ///     The maximum time allowed for the Experience Composer, in seconds. The minimum value is 60 (1 minute), the maximum
    ///     is 36000 (10 hours), and the default is 7200 (2 hours).
    /// </summary>
    [JsonPropertyOrder(3)]
    public int MaxDuration { get; internal init; }

    /// <summary>
    ///     The resolution of the Experience Composer output.
    /// </summary>
    [JsonPropertyOrder(4)]
    public RenderResolution Resolution { get; internal init; }

    /// <summary>
    ///     The initial configuration of Publisher properties for the composed output stream.
    /// </summary>
    [JsonPropertyOrder(5)]
    public StartProperties Properties { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new StartRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, $"/v2/project/{this.ApplicationId}/render")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this), Encoding.UTF8,
            "application/json");
}

/// <summary>
///     Represents the initial Publisher properties for the composed output stream.
/// </summary>
/// <param name="Name">
///     The name of the composed output stream which is published to the session; between 1 and 200 characters.
/// </param>
public record StartProperties(string Name);