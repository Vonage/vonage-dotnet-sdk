#region
using System;
using System.Collections.Generic;
using System.Linq;
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
/// <param name="Description">A short description of the URL for accessibility purposes.</param>
public record OpenUrlSuggestion(string Text, string PostbackData, Uri Url, string Description) : SuggestionBase
{
    private const int DescriptionMaxLength = 500;

    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "open_url";

    /// <inheritdoc />
    public override IEnumerable<string> GetErrors() =>
        base.GetErrors()
            .Concat(this.ValidateUrl())
            .Concat(this.ValidateDescription());

    private IEnumerable<string> ValidateUrl()
    {
        if (this.Url == null)
            yield return "Url must not be null.";
    }

    private IEnumerable<string> ValidateDescription()
    {
        if (string.IsNullOrEmpty(this.Description))
            yield return "Description must not be null or empty.";
        else if (this.Description.Length > DescriptionMaxLength)
            yield return $"Description must not exceed {DescriptionMaxLength} characters.";
    }
}