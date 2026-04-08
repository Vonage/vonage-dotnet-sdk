using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Vonage.Voice;

/// <summary>
///     Represents a call endpoint used in call records and call creation. The populated properties depend on the endpoint type (phone, websocket, SIP, or VBC).
/// </summary>
public class CallEndpoint
{
    /// <summary>
    ///     The internet media type for WebSocket audio streaming. Supported values: "audio/l16;rate=8000" or "audio/l16;rate=16000". Only applicable for WebSocket endpoints.
    /// </summary>
    [JsonProperty("content-type", Order = 2)]
    public string ContentType { get; set; }

    /// <summary>
    ///     DTMF digits to send automatically when the call is answered. The * and # digits are respected. Use "p" to create 500ms pauses (e.g., "p*123#"). Only applicable for phone endpoints.
    /// </summary>
    [JsonProperty("dtmfAnswer")]
    public string DtmfAnswer { get; set; }

    /// <summary>
    ///     Custom metadata headers to include with the connection. For WebSocket endpoints, sent as HTTP headers during the upgrade. For SIP endpoints, sent as X-prefixed SIP INVITE headers.
    /// </summary>
    [JsonProperty("headers", Order = 3)]
    public object Headers { get; set; }

    /// <summary>
    ///     The phone number in E.164 format (7-15 digits, e.g., "14155550100"). Only applicable for phone endpoints.
    /// </summary>
    [JsonProperty("number")]
    public string Number { get; set; }

    /// <summary>
    ///     The endpoint type: "phone", "websocket", "sip", "app", or "vbc".
    /// </summary>
    [JsonProperty("type", Order = 0)]
    public string Type { get; set; }

    /// <summary>
    ///     The URI for WebSocket (e.g., "wss://example.com/socket") or SIP (e.g., "sip:user@sip.example.com") endpoints.
    /// </summary>
    [JsonProperty("uri", Order = 1)]
    public string Uri { get; set; }
}