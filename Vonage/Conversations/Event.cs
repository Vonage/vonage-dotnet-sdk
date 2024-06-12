using System;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Vonage.Conversations;

public record Event(
    int Id,
    string Type,
    string From,
    JsonElement Body,
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
    JsonElement CustomData);

public record EmbeddedEventMember(string Id);