﻿using System;
using System.Text.Json.Serialization;
using Vonage.Common;

namespace Vonage.Conversations;

/// <summary>
///     Represents a member.
/// </summary>
/// <param name="Id">The member Id.</param>
/// <param name="ConversationId">The conversation Id.</param>
/// <param name="State">The state that the member is in. Possible values are INVITED, JOINED, LEFT, or UNKNOWN.</param>
/// <param name="KnockingId"></param>
/// <param name="InvitedBy"></param>
/// <param name="Channel"></param>
/// <param name="Embedded"></param>
/// <param name="Timestamp">The member timestamps.</param>
/// <param name="Initiator"></param>
/// <param name="Media"></param>
/// <param name="Links"></param>
public record Member(
    string Id,
    string ConversationId,
    string State,
    string KnockingId,
    string InvitedBy,
    MemberChannel Channel,
    [property: JsonPropertyName("_embedded")]
    MemberEmbedded Embedded,
    MemberTimestamp Timestamp,
    MemberInitiator Initiator,
    MemberMedia Media,
    [property: JsonPropertyName("_links")] HalLink Links
);

/// <summary>
///     Represents a channel.
/// </summary>
/// <param name="Type">The channel type.</param>
/// <param name="From">Which channel types this member accepts messages from (if not set, all are accepted)</param>
/// <param name="To">Settings which control who this member can send messages to.</param>
public record MemberChannel(string Type, MemberChannelFrom From, MemberChannelTo To);

/// <summary>
///     Represents the source channel.
/// </summary>
/// <param name="Type">
///     Comma separated list, can include channel types app, phone, sms, mms, whatsapp , viber, or
///     messenger.
/// </param>
public record MemberChannelFrom(string Type);

/// <summary>
///     Represents the destination channel.
/// </summary>
/// <param name="Type">The channel type.</param>
/// <param name="User">The user ID of the member that this member can send messages to.</param>
/// <param name="Number">The phone number of the member that this member can send messages to.</param>
/// <param name="Id">The Id.</param>
public record MemberChannelTo(string Type, string User, string Number, string Id);

/// <summary>
///     Represents the member timestamps.
/// </summary>
/// <param name="Invited">The time that the member was invited to a conversation</param>
/// <param name="Joined">The time that the member joined the conversation</param>
/// <param name="Left">The time that the member left the conversation</param>
public record MemberTimestamp(DateTimeOffset Invited, DateTimeOffset Joined, DateTimeOffset Left);

/// <summary>
///     Represents which user initiated the conversation.
/// </summary>
/// <param name="Joined">When invited by Admin JWT.</param>
/// <param name="Invited">When invited by User.</param>
public record MemberInitiator(MemberInitiatorJoined Joined, MemberInitiatorInvited Invited);

/// <summary>
///     Represents a member invited by User.
/// </summary>
/// <param name="IsSystem">false if the user was invited by a user.</param>
/// <param name="UserId">The user Id.</param>
/// <param name="MemberId">The member Id.</param>
public record MemberInitiatorJoined(bool IsSystem, string UserId, string MemberId);

/// <summary>
///     Represents a member invited by Admin JWT.
/// </summary>
/// <param name="IsSystem">true if the user was invited by an admin JWT.</param>
public record MemberInitiatorInvited(bool IsSystem);

/// <summary>
///     Represents details about the current media setting states.
/// </summary>
/// <param name="AudioSettings">Audio settings</param>
/// <param name="Audio">Indicates whether an audio connection is possible.</param>
public record MemberMedia(MemberMediaSettings AudioSettings, bool Audio);

/// <summary>
///     Represents audio settings.
/// </summary>
/// <param name="Enabled">Is audio currently enabled.</param>
/// <param name="EarMuffed">Is audio currently earmuffed.</param>
/// <param name="Muted">Is audio currently muted.</param>
public record MemberMediaSettings(
    bool Enabled,
    [property: JsonPropertyName("earmuffed")]
    bool EarMuffed,
    bool Muted);

/// <summary>
///     Represents the embedded data.
/// </summary>
/// <param name="User">The user.</param>
public record MemberEmbedded(MemberEmbeddedUser User);

/// <summary>
///     Represents a user.
/// </summary>
/// <param name="Id">The user Id.</param>
/// <param name="Name">The unique user name.</param>
/// <param name="DisplayName">The display name.</param>
/// <param name="Links">Link to the user.</param>
public record MemberEmbeddedUser(
    string Id,
    string Name,
    string DisplayName,
    [property: JsonPropertyName("_links")] HalLinks<HalLink> Links);