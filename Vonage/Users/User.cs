using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Vonage.Common;

namespace Vonage.Users;

/// <summary>
///     Represents a PSTN (Public Switched Telephone Network) channel for voice communication.
/// </summary>
/// <param name="Number">The phone number associated with this PSTN channel.</param>
public record ChannelPstn([property: JsonPropertyName("number")] int Number);

/// <summary>
///     Represents a SIP (Session Initiation Protocol) channel for voice communication.
/// </summary>
/// <param name="Uri">The SIP URI endpoint address.</param>
/// <param name="Username">The username for SIP authentication.</param>
/// <param name="Password">The password for SIP authentication.</param>
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
///     Represents a VBC (Vonage Business Communications) channel.
/// </summary>
/// <param name="Extension">The VBC extension number.</param>
public record ChannelVbc([property: JsonPropertyName("extension")]
    string Extension);

/// <summary>
///     Represents a WebSocket channel for real-time audio streaming.
/// </summary>
/// <param name="Uri">The WebSocket endpoint URI.</param>
/// <param name="ContentType">The audio content type (e.g., "audio/l16;rate=16000").</param>
/// <param name="Headers">Custom headers to include in the WebSocket connection.</param>
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
///     Represents an SMS channel for text messaging.
/// </summary>
/// <param name="Number">The phone number associated with this SMS channel.</param>
public record ChannelSms([property: JsonPropertyName("number")] string Number);

/// <summary>
///     Represents an MMS channel for multimedia messaging.
/// </summary>
/// <param name="Number">The phone number associated with this MMS channel.</param>
public record ChannelMms([property: JsonPropertyName("number")] string Number);

/// <summary>
///     Represents a WhatsApp channel for messaging.
/// </summary>
/// <param name="Number">The phone number associated with this WhatsApp channel.</param>
public record ChannelWhatsApp([property: JsonPropertyName("number")] string Number);

/// <summary>
///     Represents a Viber channel for messaging.
/// </summary>
/// <param name="Number">The phone number associated with this Viber channel.</param>
public record ChannelViber([property: JsonPropertyName("number")] string Number);

/// <summary>
///     Represents a Facebook Messenger channel for messaging.
/// </summary>
/// <param name="Id">The Messenger user ID.</param>
public record ChannelMessenger([property: JsonPropertyName("id")] string Id);

/// <summary>
///     Represents the collection of communication channels associated with a user.
/// </summary>
/// <param name="Pstn">PSTN (landline/mobile) channels for the user.</param>
/// <param name="Sip">SIP channels for the user.</param>
/// <param name="Vbc">VBC (Vonage Business Communications) channels for the user.</param>
/// <param name="WebSocket">WebSocket channels for real-time audio streaming.</param>
/// <param name="Sms">SMS channels for the user.</param>
/// <param name="Mms">MMS channels for the user.</param>
/// <param name="WhatsApp">WhatsApp channels for the user.</param>
/// <param name="Viber">Viber channels for the user.</param>
/// <param name="Messenger">Facebook Messenger channels for the user.</param>
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
///     Represents a user in the Vonage platform with their profile and communication channels.
/// </summary>
/// <param name="Id">The unique identifier for the user.</param>
/// <param name="Name">The unique name for the user.</param>
/// <param name="DisplayName">A display name for the user. Does not need to be unique.</param>
/// <param name="ImageUrl">An image URL associated with the user's profile.</param>
/// <param name="Properties">Custom properties associated with the user.</param>
/// <param name="Channels">The communication channels configured for the user.</param>
/// <param name="Links">HAL links for related resources.</param>
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

/// <summary>
///     Represents custom properties that can be associated with a user.
/// </summary>
/// <param name="CustomData">A dictionary of custom key-value pairs for storing user-specific data.</param>
public record UserProperty(Dictionary<string, object> CustomData);