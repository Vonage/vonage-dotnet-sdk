#region
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;
using Vonage.Serialization;
#endregion

namespace Vonage.Video.AudioConnector.Start;

/// <inheritdoc />
public readonly struct StartRequest : IVonageRequest
{
    /// <summary>
    ///     A valid Vonage Video token for the Audio Connector connection to the Vonage Video session.
    /// </summary>
    [JsonPropertyOrder(1)]
    public string Token { get; internal init; }

    /// <summary>
    ///     The Vonage Application UUID.
    /// </summary>
    [JsonIgnore]
    public Guid ApplicationId { get; internal init; }

    /// <summary>
    ///     The Vonage Video session ID that includes the Vonage Video streams you want to include in the WebSocket stream.
    /// </summary>
    [JsonPropertyOrder(0)]
    public string SessionId { get; internal init; }

    /// <summary>
    ///     The WebSocket configuration for the audio stream destination.
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
    bool Bidirectional)
{
    /// <summary>
    ///     Configures how audio is serialized on the WebSocket wire. By default, audio is sent as raw binary PCM 16-bit
    ///     frames.
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<AudioTransport>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<AudioTransport> AudioTransport { get; init; }
}

/// <summary>
///     Defines the serialization format for audio on the WebSocket wire.
/// </summary>
public enum AudioTransportType
{
    /// <summary>
    ///     Raw PCM16 binary frames (the default).
    /// </summary>
    [Description("binary")]
    Binary,

    /// <summary>
    ///     JSON-wrapped audio frames. Requires <see cref="AudioTransport.Encoding"/> to be set to 'base64'.
    /// </summary>
    [Description("json")]
    Json,
}

/// <summary>
///     Represents the configuration for how audio is serialized on the WebSocket wire.
/// </summary>
public record AudioTransport
{
    /// <summary>
    ///     Serialization format: 'binary' (raw PCM16, the default) or 'json'.
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionJsonConverter<AudioTransportType>))]
    public AudioTransportType Transport { get; init; }

    /// <summary>
    ///     Encoding format. Required when <see cref="Transport"/> is <see cref="AudioTransportType.Json"/>. Set to
    ///     'base64'.
    /// </summary>
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> Encoding { get; init; }

    /// <summary>
    ///     The JSON key for the outbound audio data. Defaults to 'audio'.
    /// </summary>
    [JsonPropertyName("audio_field")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> AudioField { get; init; }

    /// <summary>
    ///     The JSON key for inbound audio data when bidirectional is enabled. Defaults to the same value as
    ///     <see cref="AudioField"/>.
    /// </summary>
    [JsonPropertyName("receive_audio_field")]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> ReceiveAudioField { get; init; }

    /// <summary>
    ///     Extra key-value pairs included in every outbound JSON audio message.
    /// </summary>
    [JsonPropertyName("static_fields")]
    [JsonConverter(typeof(MaybeJsonConverter<Dictionary<string, string>>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Dictionary<string, string>> StaticFields { get; init; }
}

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
    
    /// <summary>
    ///     24000Hz
    /// </summary>
    AUDIO_RATE_24000Hz = 24000,
}