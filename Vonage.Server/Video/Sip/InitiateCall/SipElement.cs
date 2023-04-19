using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.Server.Video.Sip.InitiateCall;

/// <summary>
///     Represents a SIP element.
/// </summary>
public readonly struct SipElement
{
    /// <summary>
    ///     Contains the username and password to be used in the the SIP INVITE request for HTTP digest
    ///     authentication, if it is required by your SIP platform.
    /// </summary>
    [JsonPropertyOrder(0)]
    [JsonPropertyName("auth")]
    [JsonConverter(typeof(MaybeJsonConverter<SipAuthentication>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<SipAuthentication> Authentication { get; internal init; }

    /// <summary>
    ///     The number or string that will be sent to the final SIP number as the caller. It must be a string in the form of
    ///     from@example.com, where from can be a string or a number. If from is set to a number (for example,
    ///     "14155550101@example.com"), it will show up as the incoming number on PSTN phones. If from is undefined or set to a
    ///     string (for example, "joe@example.com"),  +00000000 will show up as the incoming number on PSTN phones.
    /// </summary>
    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> From { get; internal init; }

    /// <summary>
    ///     Indicates whether the media must be transmitted encrypted. The default value is false.
    /// </summary>
    [JsonPropertyOrder(4)]
    [JsonPropertyName("secure")]
    public bool HasEncryptedMedia { get; internal init; }

    /// <summary>
    ///     Indicates whether the SIP end point observes force mute moderation. Also, with observeForceMute set to true, the
    ///     caller can press"*6" to unmute and mute the published audio. For the "*6" mute toggle to work, the SIP caller must
    ///     negotiate RFC2833 DTMFs (RFC2833/RFC4733 digits). The mute toggle is not supported with SIP INFO or in-band DTMFs.
    ///     A message (in English) is played to the caller when the caller mutes and unmutes, or when the SIP client is muted
    ///     through a force mute action. The default is false.
    /// </summary>
    [JsonPropertyOrder(3)]
    [JsonPropertyName("observeForceMute")]
    public bool HasForceMute { get; internal init; }

    /// <summary>
    ///     Indicates whether the SIP call will include video. With video included, the SIP client's video is included in the
    ///     OpenTok stream that is sent to the OpenTok session. The SIP client will receive a single composed video of the
    ///     published streams in the OpenTok session. The default value is false.
    /// </summary>
    [JsonPropertyOrder(5)]
    [JsonPropertyName("video")]
    public bool HasVideo { get; internal init; }

    /// <summary>
    ///     Defines custom headers to be added to the SIP INVITE request initiated fromOpenTok to your SIP platform.
    /// </summary>
    [JsonPropertyOrder(2)]
    [JsonConverter(typeof(MaybeJsonConverter<Dictionary<string, string>>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<Dictionary<string, string>> Headers { get; internal init; }

    /// <summary>
    ///     The SIP URI to be used as destination of the SIP call initiated from OpenTok to your SIP platform. If the SIP uri
    ///     contains a transport=tls header, the negotiation between Vonage and the SIP endpoint will be done securely. Note
    ///     that this will only apply to the negotiation itself,and not to the transmission of audio. If you also audio
    ///     transmission to be encrypted, set the secure property to true.
    /// </summary>
    [JsonPropertyOrder(6)]
    public Uri Uri { get; internal init; }

    /// <summary>
    /// Represents the authentication for the SIP call.
    /// </summary>
    /// <param name="Username">Contains the username to be used in the the SIP INVITE request for HTTP digest authentication, if it is required by your SIP platform.</param>
    /// <param name="Password">Contains the password to be used in the the SIP INVITE request for HTTP digest authentication, if it is required by your SIP platform.</param>
    public record SipAuthentication(string Username, string Password);
}