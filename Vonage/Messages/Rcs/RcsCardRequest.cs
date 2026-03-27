#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
///     Represents a rich card message request to be sent via RCS. Rich cards combine media, text, and interactive suggestions.
/// </summary>
public class RcsCardRequest : RcsMessageBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.RCS;

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Card;

    /// <summary>
    ///     The rich card attachment containing title, text, media, and optional suggestions.
    /// </summary>
    [JsonPropertyName("card")]
    [JsonPropertyOrder(9)]
    public CardAttachment Card { get; set; }
}