#region
using System.Collections.Generic;
using System.Linq;
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
    public override IEnumerable<string> GetErrors() =>
        base.GetErrors()
            .Concat(this.ValidateCardPresence())
            .Concat(this.Card?.GetErrors() ?? Enumerable.Empty<string>())
            .Concat(this.ValidateCardOrientation());

    private IEnumerable<string> ValidateCardPresence()
    {
        if (this.Card == null)
            yield return "Card must not be null.";
    }

    private IEnumerable<string> ValidateCardOrientation()
    {
        if (this.Rcs is {CardOrientation: RcsCardOrientation.Horizontal, ImageAlignment: null})
            yield return "ImageAlignment is required when CardOrientation is Horizontal.";
    }

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Card;

    /// <summary>
    ///     The rich card attachment containing title, text, media, and optional suggestions.
    /// </summary>
    [JsonPropertyName("card")]
    [JsonPropertyOrder(9)]
    public CardAttachment Card { get; set; }
}