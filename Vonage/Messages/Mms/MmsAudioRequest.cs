﻿#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Mms;

public class MmsAudioRequest : MessageRequestBase
{
    [JsonPropertyOrder(8)] public CaptionedAttachment Audio { get; set; }

    public override MessagesChannel Channel => MessagesChannel.MMS;

    public override MessagesMessageType MessageType => MessagesMessageType.Audio;

    /// <summary>
    ///     Time-To-Live (how long a message should exist before it is delivered successfully) in seconds. If a message is not
    ///     delivered successfully within the TTL time, the message is considered expired and will be rejected if TTL is
    ///     supported.
    /// </summary>
    [JsonPropertyName("ttl")]
    [JsonPropertyOrder(9)]
    public int? TimeToLive { get; set; }
}