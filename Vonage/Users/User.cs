using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Vonage.Common;

namespace Vonage.Users;

public record CustomData([property: JsonPropertyName("custom_key")]
    string CustomKey);

public record ChannelPSTN([property: JsonPropertyName("number")] int Number);

public record ChannelSIP([property: JsonPropertyName("uri")]
    [property: JsonPropertyOrder(0)]
    string Uri,
    [property: JsonPropertyName("username")]
    [property: JsonPropertyOrder(1)]
    string Username,
    [property: JsonPropertyName("password")]
    [property: JsonPropertyOrder(2)]
    string Password);

public record ChannelVBC([property: JsonPropertyName("extension")]
    string Extension);

public record ChannelWebSocket([property: JsonPropertyName("uri")]
    [property: JsonPropertyOrder(0)]
    string Uri,
    [property: JsonPropertyName("content-type")]
    [property: JsonPropertyOrder(1)]
    string ContentType,
    [property: JsonPropertyName("headers")]
    [property: JsonPropertyOrder(2)]
    Dictionary<string, string> Headers);

public record ChannelSMS([property: JsonPropertyName("number")] string Number);
public record ChannelMMS([property: JsonPropertyName("number")] string Number);
public record ChannelWhatsApp([property: JsonPropertyName("number")] string Number);
public record ChannelViber([property: JsonPropertyName("number")] string Number);
public record ChannelMessenger([property: JsonPropertyName("id")] string Id);

public record UserChannels(
    IEnumerable<ChannelPSTN> Pstn,
    IEnumerable<ChannelSIP> Sip,
    IEnumerable<ChannelVBC> Vbc,
    [property: JsonPropertyName("websocket")]
    IEnumerable<ChannelWebSocket> WebSocket,
    IEnumerable<ChannelSMS> Sms,
    IEnumerable<ChannelMMS> Mms,
    [property: JsonPropertyName("whatsapp")]
    IEnumerable<ChannelWhatsApp> WhatsApp,
    IEnumerable<ChannelViber> Viber,
    IEnumerable<ChannelMessenger> Messenger);

public record User([property: JsonPropertyName("id")]
    [property: JsonPropertyOrder(0)]
    string Id,
    [property: JsonPropertyName("name")]
    [property: JsonPropertyOrder(1)]
    string Name,
    [property: JsonPropertyName("display_name")]
    [property: JsonPropertyOrder(2)]
    string DisplayName,
    [property: JsonPropertyName("image_url")]
    [property: JsonPropertyOrder(3)]
    Uri ImageUrl,
    [property: JsonPropertyName("properties")]
    [property: JsonPropertyOrder(4)]
    Dictionary<string, object> Properties,
    [property: JsonPropertyName("channels")]
    [property: JsonPropertyOrder(5)]
    UserChannels Channels,
    [property: JsonPropertyName("_links")]
    [property: JsonPropertyOrder(6)]
    HalLinks Links)
{
};