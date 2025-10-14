#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
/// </summary>
public class RcsCustomRequest : RcsMessageBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.RCS;

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Custom;

    /// <summary>
    ///     A custom payload. The schema of a custom object can vary widely.
    /// </summary>
    [JsonPropertyName("custom")]
    [JsonPropertyOrder(9)]
    public object Custom { get; set; }
}