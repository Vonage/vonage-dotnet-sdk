using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Vonage.Common;

namespace Vonage.Users;

/// <summary>
/// </summary>
/// <param name="Number"></param>
public record ChannelPstn([property: JsonPropertyName("number")] int Number);

/// <summary>
/// </summary>
/// <param name="Uri"></param>
/// <param name="Username"></param>
/// <param name="Password"></param>
public record ChannelSip([property: JsonPropertyName("uri")]
    [property: JsonPropertyOrder(0)]
    string Uri,
    [property: JsonPropertyName("username")]
    [property: JsonPropertyOrder(1)]
    string Username,
    [property: JsonPropertyName("password")]
    [property: JsonPropertyOrder(2)]
    string Password);

/// <summary>
/// </summary>
/// <param name="Extension"></param>
public record ChannelVbc([property: JsonPropertyName("extension")]
    string Extension);

/// <summary>
/// </summary>
/// <param name="Uri"></param>
/// <param name="ContentType"></param>
/// <param name="Headers"></param>
public record ChannelWebSocket([property: JsonPropertyName("uri")]
    [property: JsonPropertyOrder(0)]
    string Uri,
    [property: JsonPropertyName("content-type")]
    [property: JsonPropertyOrder(1)]
    string ContentType,
    [property: JsonPropertyName("headers")]
    [property: JsonPropertyOrder(2)]
    Dictionary<string, string> Headers);

/// <summary>
/// </summary>
/// <param name="Number"></param>
public record ChannelSms([property: JsonPropertyName("number")] string Number);

/// <summary>
/// </summary>
/// <param name="Number"></param>
public record ChannelMms([property: JsonPropertyName("number")] string Number);

/// <summary>
/// </summary>
/// <param name="Number"></param>
public record ChannelWhatsApp([property: JsonPropertyName("number")] string Number);

/// <summary>
/// </summary>
/// <param name="Number"></param>
public record ChannelViber([property: JsonPropertyName("number")] string Number);

/// <summary>
/// </summary>
/// <param name="Id"></param>
public record ChannelMessenger([property: JsonPropertyName("id")] string Id);

/// <summary>
/// </summary>
/// <param name="Pstn"></param>
/// <param name="Sip"></param>
/// <param name="Vbc"></param>
/// <param name="WebSocket"></param>
/// <param name="Sms"></param>
/// <param name="Mms"></param>
/// <param name="WhatsApp"></param>
/// <param name="Viber"></param>
/// <param name="Messenger"></param>
public record UserChannels(
    IEnumerable<ChannelPstn> Pstn,
    IEnumerable<ChannelSip> Sip,
    IEnumerable<ChannelVbc> Vbc,
    [property: JsonPropertyName("websocket")]
    IEnumerable<ChannelWebSocket> WebSocket,
    IEnumerable<ChannelSms> Sms,
    IEnumerable<ChannelMms> Mms,
    [property: JsonPropertyName("whatsapp")]
    IEnumerable<ChannelWhatsApp> WhatsApp,
    IEnumerable<ChannelViber> Viber,
    IEnumerable<ChannelMessenger> Messenger);

/// <summary>
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="DisplayName"></param>
/// <param name="ImageUrl"></param>
/// <param name="Properties"></param>
/// <param name="Channels"></param>
/// <param name="Links"></param>
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
    HalLinks Links);