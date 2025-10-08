#region
using System;
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs.Suggestions;

/// <summary>
///     A open URL suggestion.
/// </summary>
/// <param name="Text">The text to display on the suggestion chip.</param>
/// <param name="PostbackData">
///     The data that will be sent back in the button.payload property of a button message via the
///     inbound message webhook when the user taps the suggestion chip.
/// </param>
/// <param name="Url">The URL to open when the suggestion is tapped.</param>
public record OpenUrlSuggestion(string Text, string PostbackData, Uri Url) : SuggestionBase
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "open_url";

    /// <summary>
    ///     A short description of the URL for accessibility purposes.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Description { get; init; }

    /// <summary>
    ///     Returns a suggestion with a description.
    /// </summary>
    /// <param name="value">A short description of the URL for accessibility purposes.</param>
    /// <returns>The suggestion.</returns>
    public OpenUrlSuggestion WithDescription(string value) => this with {Description = value};
}