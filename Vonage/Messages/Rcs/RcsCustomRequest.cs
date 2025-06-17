#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
/// </summary>
public class RcsCustomRequest : MessageRequestBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.RCS;

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Custom;

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
    ///     A custom payload. The schema of a custom object can vary widely.
    /// </summary>
    [JsonPropertyName("custom")]
    [JsonPropertyOrder(9)]
    public object Custom { get; set; }
}