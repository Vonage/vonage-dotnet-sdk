#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
/// </summary>
public class RcsCardRequest : RcsMessageBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.RCS;

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Card;

    /// <summary>
    ///     The card attachment.
    /// </summary>
    [JsonPropertyName("card")]
    [JsonPropertyOrder(9)]
    public CardAttachment Card { get; set; }
}