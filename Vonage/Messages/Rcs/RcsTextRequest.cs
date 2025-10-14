#region
using System.Text.Json.Serialization;
using Vonage.Messages.Rcs.Suggestions;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
/// </summary>
public class RcsTextRequest : RcsMessageBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.RCS;

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Text;

    /// <summary>
    ///     The text of the message to send. Limited to 3072 characters, including unicode.
    /// </summary>
    [JsonPropertyName("text")]
    [JsonPropertyOrder(9)]
    public string Text { get; set; }

    /// <summary>
    ///     An array of suggestion objects to include with the card. You can include up to 11 suggestions per card.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(10)]
    public SuggestionBase[] Suggestions { get; set; }
}