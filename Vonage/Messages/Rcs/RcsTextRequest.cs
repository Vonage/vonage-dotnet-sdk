#region
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Vonage.Messages.Rcs.Suggestions;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
///     Represents a text message request to be sent via RCS (Rich Communication Services).
/// </summary>
public class RcsTextRequest : RcsMessageBase
{
    private const int SuggestionsMaxItems = 11;

    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.RCS;

    /// <inheritdoc />
    public override IEnumerable<string> GetErrors() =>
        base.GetErrors()
            .Concat(this.ValidateText())
            .Concat(this.ValidateSuggestionsCount())
            .Concat(this.Suggestions?.SelectMany(s => s.GetErrors()) ?? Enumerable.Empty<string>());

    private IEnumerable<string> ValidateText()
    {
        if (string.IsNullOrEmpty(this.Text))
            yield return "Text must not be null or empty.";
    }

    private IEnumerable<string> ValidateSuggestionsCount()
    {
        if (this.Suggestions?.Length > SuggestionsMaxItems)
            yield return $"Suggestions must contain at most {SuggestionsMaxItems} items.";
    }

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