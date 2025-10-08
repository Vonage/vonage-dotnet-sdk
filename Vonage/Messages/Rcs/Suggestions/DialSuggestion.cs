#region
using System;
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs.Suggestions;

/// <summary>
///     A dial suggestion.
/// </summary>
/// <param name="Text">The text to display on the suggestion chip.</param>
/// <param name="PostbackData">
///     The data that will be sent back in the button.payload property of a button message via the
///     inbound message webhook when the user taps the suggestion chip.
/// </param>
/// <param name="PhoneNumber">The phone number to dial in E.164 format.</param>
public record DialSuggestion(string Text, string PostbackData, string PhoneNumber) : SuggestionBase
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "dial";

    /// <summary>
    ///     A URL to open if the device is unable to place a call.
    /// </summary>
    public Uri FallbackUrl { get; init; }

    /// <summary>
    ///     Returns a suggestion with a fallback url.
    /// </summary>
    /// <param name="url">A URL to open if the device is unable to place a call.</param>
    /// <returns>The suggestion.</returns>
    public DialSuggestion WithFallbackUrl(Uri url) => this with {FallbackUrl = url};
}