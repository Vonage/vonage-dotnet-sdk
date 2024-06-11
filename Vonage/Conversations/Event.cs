using System;
using System.Text.Json.Serialization;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Vonage.Conversations;

public record Event(
    int Id,
    string Type,
    string From,
    EventBodyBase Body,
    DateTimeOffset Timestamp,
    [property: JsonPropertyName("_embedded")]
    EmbeddedEventData Embedded,
    [property: JsonPropertyName("_links")] Links Links);

public record EmbeddedEventData(
    [property: JsonPropertyName("from_user")]
    EmbeddedEventUser User,
    [property: JsonPropertyName("from_member")]
    EmbeddedEventMember Member);

public record EmbeddedEventUser(
    string Id,
    string Name,
    [property: JsonPropertyName("display_name")]
    string DisplayName,
    [property: JsonPropertyName("image_url")]
    string ImageUrl,
    object CustomData);

public record EmbeddedEventMember(string Id);

[JsonPolymorphic(TypeDiscriminatorPropertyName = "message_type")]
[JsonDerivedType(typeof(EventBodyBase))]
[JsonDerivedType(typeof(EventBodyTextMessage), "text")]
[JsonDerivedType(typeof(EventBodyImageMessage), "image")]
[JsonDerivedType(typeof(EventBodyAudioMessage), "audio")]
[JsonDerivedType(typeof(EventBodyVideoMessage), "video")]
[JsonDerivedType(typeof(EventBodyFileMessage), "file")]
[JsonDerivedType(typeof(EventBodyTemplateMessage), "template")]
[JsonDerivedType(typeof(EventBodyCustomMessage), "custom")]
[JsonDerivedType(typeof(EventBodyVcardMessage), "vcard")]
[JsonDerivedType(typeof(EventBodyLocationMessage), "location")]
public record EventBodyBase;

public record EventBodyTextMessage(string Text) : EventBodyBase;
public record EventBodyImageMessage(EventBodyImageUrl Image) : EventBodyBase;
public record EventBodyImageUrl(string Url);
public record EventBodyAudioMessage(EventBodyAudioUrl Audio) : EventBodyBase;
public record EventBodyAudioUrl(string Url);
public record EventBodyVideoMessage(EventBodyVideoUrl Video) : EventBodyBase;
public record EventBodyVideoUrl(string Url);
public record EventBodyFileMessage(EventBodyFileUrl File) : EventBodyBase;
public record EventBodyFileUrl(string Url);

public record EventBodyTemplateMessage(string Name, object[] Parameters, EventBodyTemplateWhatsApp WhatsApp)
    : EventBodyBase;

public record EventBodyTemplateWhatsApp(string Policy, string Locale);
public record EventBodyCustomMessage(object Custom) : EventBodyBase;
public record EventBodyVcardMessage(EventBodyVcardUrl Vcard, EventBodyImageUrl Image) : EventBodyBase;
public record EventBodyVcardUrl(string Url);
public record EventBodyLocationMessage(EventBodyLocation File) : EventBodyBase;
public record EventBodyLocation(string Longitude, string Latitude, string Name, string Address);