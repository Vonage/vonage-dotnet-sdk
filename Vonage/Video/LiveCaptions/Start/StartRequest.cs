#region
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Serialization;
#endregion

namespace Vonage.Video.LiveCaptions.Start;

/// <inheritdoc />
public readonly struct StartRequest : IVonageRequest
{
    /// <summary>
    ///     A valid Vonage Video token with role set to Moderator.
    /// </summary>
    [JsonPropertyOrder(1)]
    public string Token { get; internal init; }

    /// <summary>
    ///     Vonage Application UUID
    /// </summary>
    [JsonIgnore]
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     The session ID of the Vonage Video session. The audio from Publishers publishing into this session will be used to
    ///     generate the captions.
    /// </summary>
    [JsonPropertyOrder(0)]
    public string SessionId { get; internal init; }

    /// <summary>
    ///     Whether to enable this to faster captioning at the cost of some degree of inaccuracies.
    /// </summary>
    [JsonPropertyOrder(4)]
    public bool PartialCaptions { get; internal init; }

    /// <summary>
    ///     The BCP-47 code for a spoken language used on this call. The default value is "en-US".
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonPropertyName("languageCode")]
    public string Language { get; internal init; }

    /// <summary>
    ///     The maximum duration for the audio captioning, in seconds. The default value is 14,400 seconds (4 hours), the
    ///     maximum duration allowed. The minimum value for maxDuration is 300 (300 seconds, or 5 minutes).
    /// </summary>
    [JsonPropertyOrder(3)]
    public int MaxDuration { get; internal init; }

    /// <summary>
    ///     A publicly reachable URL controlled by the customer and capable of generating the content to be rendered without
    ///     user intervention
    /// </summary>
    [JsonPropertyOrder(5)]
    [JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Uri> StatusCallbackUrl { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, this.GetEndpointPath())
        .WithContent(this.GetRequestContent())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v2/project/{this.ApplicationId}/captions";

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this), Encoding.UTF8,
            "application/json");

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new StartRequestBuilder();
}