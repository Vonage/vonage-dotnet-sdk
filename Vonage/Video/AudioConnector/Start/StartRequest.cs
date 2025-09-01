#region
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Serialization;
#endregion

namespace Vonage.Video.AudioConnector.Start;

/// <inheritdoc />
public readonly struct StartRequest : IVonageRequest
{
    /// <summary>
    /// </summary>
    [JsonPropertyOrder(1)]
    public string Token { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonIgnore]
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonPropertyOrder(0)]
    public string SessionId { get; internal init; }

    /// <summary>
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonPropertyName("websocket")]
    public WebSocket WebSocket { get; internal init; }

    /// <summary>
    ///     Initializes a builder.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForApplicationId Build() => new StartRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, $"/v2/project/{this.ApplicationId}/connect")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithCamelCase().SerializeObject(this), Encoding.UTF8,
            "application/json");
}

/// <summary>
///     Represents a websocket configuration.
/// </summary>
/// <param name="Uri">A publicly reachable WebSocket URI to be used for the destination of the audio stream</param>
/// <param name="Streams">
///     An array of stream IDs for the Vonage Video streams you want to include in the WebSocket audio.
///     If you omit this property, all streams in the session will be included.
/// </param>
/// <param name="Headers">
///     An object of key-value pairs of headers to be sent to your WebSocket server with each message,
///     with a maximum length of 512 bytes.
/// </param>
/// <param name="AudioRate">A number representing the audio sampling rate in Hz.</param>
/// <param name="Bidirectional">Enables bidirectional audio on the websocket..</param>
public record WebSocket(
    Uri Uri,
    string[] Streams,
    Dictionary<string, string> Headers,
    SupportedAudioRates AudioRate,
    bool Bidirectional);

/// <summary>
///     A number representing the audio sampling rate in Hz.
/// </summary>
public enum SupportedAudioRates
{
    /// <summary>
    ///     8000Hz
    /// </summary>
    AUDIO_RATE_8000Hz = 8000,

    /// <summary>
    ///     16000Hz
    /// </summary>
    AUDIO_RATE_16000Hz = 16000,
}