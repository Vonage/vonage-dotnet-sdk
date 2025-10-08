#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs.Suggestions;

/// <summary>
///     A reply suggestion.
/// </summary>
/// <param name="Text">The text to display on the suggestion chip.</param>
/// <param name="PostbackData">
///     The data that will be sent back in the reply.id property of a reply message via the inbound
///     message webhook when the user taps the suggestion chip.
/// </param>
public record ReplySuggestion(string Text, string PostbackData) : SuggestionBase
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "reply";
}