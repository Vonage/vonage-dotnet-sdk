#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
/// </summary>
public class RcsTextRequest : MessageRequestBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.RCS;

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Text;

    /// <summary>
    ///     The duration in seconds the delivery of a message will be attempted. By default Vonage attempts delivery for 72
    ///     hours, however the maximum effective value depends on the operator and is typically 24 - 48 hours. We recommend
    ///     this value should be kept at its default or at least 30 minutes.
    /// </summary>
    [JsonPropertyName("ttl")]
    [JsonPropertyOrder(8)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]

    public int TimeToLive { get; set; }

    /// <summary>
    ///     The text of the message to send. Limited to 3072 characters, including unicode.
    /// </summary>
    [JsonPropertyName("text")]
    [JsonPropertyOrder(9)]
    public string Text { get; set; }
}