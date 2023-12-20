using System;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace Vonage.Conversations;

/// <summary>
///     Represents the Callback information.
/// </summary>
/// <param name="Url">The url.</param>
/// <param name="EventMask">The event mask.</param>
/// <param name="Parameters">The url parameters.</param>
/// <param name="Method">The Http Method.</param>
public record Callback(
    [property: JsonPropertyName("url")]
    [property: JsonPropertyOrder(0)]
    Uri Url,
    [property: JsonPropertyName("event_mask")]
    [property: JsonPropertyOrder(1)]
    string EventMask,
    [property: JsonPropertyName("params")]
    [property: JsonPropertyOrder(2)]
    CallbackParameters Parameters,
    [property: JsonPropertyName("method")]
    [property: JsonPropertyOrder(3)]
    HttpMethod Method);