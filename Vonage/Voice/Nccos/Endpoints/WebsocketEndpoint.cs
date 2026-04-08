#region
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice.Nccos.Endpoints;

/// <summary>
///     Represents a WebSocket endpoint for streaming audio to and from a WebSocket server.
/// </summary>
public class WebsocketEndpoint : Endpoint
{
    public WebsocketEndpoint() => this.Type = EndpointType.Websocket;

    /// <summary>
    ///     The internet media type for the audio being streamed. Must be <c>audio/l16;rate=16000</c> or <c>audio/l16;rate=8000</c>.
    /// </summary>
    [JsonProperty("content-type", Order = 1)]
    public string ContentType { get; set; }

    /// <summary>
    ///     An object containing custom metadata to include as headers in the WebSocket connection request.
    /// </summary>
    [JsonProperty("headers", Order = 2)]
    public object Headers { get; set; }

    /// <summary>
    ///     The URI of the WebSocket server to stream audio to, e.g. <c>wss://example.com/socket</c>.
    /// </summary>
    [JsonProperty("uri", Order = 0)]
    public string Uri { get; set; }

    /// <summary>
    ///     Optional configuration defining how the Authorization HTTP header is set in the WebSocket opening handshake.
    /// </summary>
    [JsonProperty("authorization", Order = 3)]
    public WebsocketAuthorization Authorization { get; set; }
}

/// <summary>
///     Optional configuration defining how the Authorization HTTP header is set in the WebSocket opening handshake.
/// </summary>
/// <param name="Type">
///     Defines the authorization mode: * vonage – the Voice API includes the same JWT used for signed
///     webhooks in the Authorization header ("Bearer
///     <JWT>"). * custom – a developer-supplied Authorization header value is sent verbatim.
/// </param>
/// <param name="Value">
///     Required only when authorization.type = "custom". The raw header value to include, e.g. "Bearer
///     abc123" or "ApiKey X9Z...". Ignored when authorization.type = "vonage".
/// </param>
public record WebsocketAuthorization(
    [property: JsonProperty("type", Order = 0)]
    string Type,
    [property: JsonProperty("value", Order = 1)]
    string Value);