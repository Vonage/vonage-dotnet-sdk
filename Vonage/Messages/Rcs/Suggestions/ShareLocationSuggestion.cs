#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs.Suggestions;

/// <summary>
///     A share location suggestion.
/// </summary>
/// <param name="Text">The text to display on the suggestion chip.</param>
/// <param name="PostbackData">
///     The data that will be sent back in the button.payload property of a button message via the
///     inbound message webhook when the user taps the suggestion chip.
/// </param>
public record ShareLocationSuggestion(string Text, string PostbackData) : SuggestionBase
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "share_location";
}