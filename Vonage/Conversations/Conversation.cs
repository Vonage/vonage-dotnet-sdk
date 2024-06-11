using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.Conversations;

/// <summary>
///     Represents a conversation.
/// </summary>
/// <param name="Id">The unique identifier for this conversation</param>
/// <param name="Name">Your internal conversation name. Must be unique</param>
/// <param name="DisplayName">The public facing name of the conversation</param>
/// <param name="ImageUrl">An image URL that you associate with the conversation</param>
/// <param name="State">The state the conversation is in.</param>
/// <param name="SequenceNumber">The last Event ID in this conversation. This ID can be used to retrieve a specific event.</param>
/// <param name="Timestamp">Represents the conversation history</param>
/// <param name="Properties">Conversation properties</param>
/// <param name="Links">Additional links</param>
public record Conversation(
    string Id,
    string Name,
    [property: JsonConverter(typeof(MaybeJsonConverter<string>))]
    Maybe<string> DisplayName,
    [property: JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    Maybe<Uri> ImageUrl,
    string State,
    int SequenceNumber,
    Timestamp Timestamp,
    [property: JsonConverter(typeof(MaybeJsonConverter<Properties>))]
    Maybe<Properties> Properties,
    [property: JsonPropertyName("_links")] Links Links,
    [property: JsonPropertyName("_embedded")]
    [property: JsonConverter(typeof(MaybeJsonConverter<EmbeddedConversationData>))]
    Maybe<EmbeddedConversationData> Embedded);

/// <summary>
///     Represents the conversation history
/// </summary>
/// <param name="Created">The creation date</param>
/// <param name="Updated">The last update date</param>
/// <param name="Destroyed">The destroyed date</param>
public record Timestamp(
    DateTimeOffset Created,
    [property: JsonConverter(typeof(MaybeJsonConverter<DateTimeOffset>))]
    Maybe<DateTimeOffset> Updated,
    [property: JsonConverter(typeof(MaybeJsonConverter<DateTimeOffset>))]
    Maybe<DateTimeOffset> Destroyed);

/// <summary>
///     Represents the Conversation Properties.
/// </summary>
/// <param name="TimeToLive">Conversation time to live. After how many seconds an empty conversation is deleted.</param>
/// <param name="Type">The conversation type.</param>
/// <param name="CustomSortKey">The custom sort key.</param>
/// <param name="CustomData">The custom data.</param>
public record Properties(
    [property: JsonPropertyName("ttl")]
    [property: JsonPropertyOrder(0)]
    int TimeToLive,
    [property: JsonPropertyName("type")]
    [property: JsonPropertyOrder(1)]
    string Type,
    [property: JsonPropertyName("custom_sort_key")]
    [property: JsonPropertyOrder(2)]
    string CustomSortKey,
    [property: JsonPropertyName("custom_data")]
    [property: JsonPropertyOrder(3)]
    Dictionary<string, string> CustomData);

/// <summary>
///     Represents the conversation's embedded data.
/// </summary>
/// <param name="MemberId">The Member Id.</param>
/// <param name="MemberState">The state that the member is in.</param>
public record EmbeddedConversationData(
    [property: JsonPropertyName("id")]
    [property: JsonPropertyOrder(0)]
    string MemberId,
    [property: JsonPropertyName("state")]
    [property: JsonPropertyOrder(1)]
    [property: JsonConverter(typeof(EnumDescriptionJsonConverter<MemberState>))]
    MemberState MemberState);

/// <summary>
/// </summary>
public enum MemberState
{
    /// <summary>
    /// </summary>
    [Description("UNKNOWN")] Unknown,

    /// <summary>
    /// </summary>
    [Description("INVITED")] Invited,

    /// <summary>
    /// </summary>
    [Description("JOINED")] Joined,

    /// <summary>
    /// </summary>
    [Description("LEFT")] Left,
}