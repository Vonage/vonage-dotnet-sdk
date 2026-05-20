#region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
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
    private static readonly Regex PhoneNumberPattern = new Regex(@"^\+?[1-9]\d{1,14}$");

    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "dial";

    /// <inheritdoc />
    public override IEnumerable<string> GetErrors() =>
        base.GetErrors().Concat(this.ValidatePhoneNumber());

    private IEnumerable<string> ValidatePhoneNumber()
    {
        if (string.IsNullOrEmpty(this.PhoneNumber))
            yield return "PhoneNumber must not be null or empty.";
        else if (!PhoneNumberPattern.IsMatch(this.PhoneNumber))
            yield return "PhoneNumber must be in E.164 format.";
    }

    /// <summary>
    ///     A URL to open if the device is unable to place a call.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Uri FallbackUrl { get; init; }

    /// <summary>
    ///     Returns a suggestion with a fallback url.
    /// </summary>
    /// <param name="url">A URL to open if the device is unable to place a call.</param>
    /// <returns>The suggestion.</returns>
    public DialSuggestion WithFallbackUrl(Uri url) => this with {FallbackUrl = url};
}