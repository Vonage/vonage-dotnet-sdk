#region
using System;
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs.Suggestions;

/// <summary>
///     A view location suggestion.
/// </summary>
/// <param name="Text">The text to display on the suggestion chip.</param>
/// <param name="PostbackData">
///     The data that will be sent back in the button.payload property of a button message via the
///     inbound message webhook when the user taps the suggestion chip.
/// </param>
/// <param name="PhoneNumber">The phone number to dial in E.164 format.</param>
/// <param name="Latitude">The latitude of the location to view.</param>
/// <param name="Longitude">The longitude of the location to view.</param>
/// <param name="PinLabel">A label to display on the location pin.</param>
public record ViewLocationSuggestion(
    string Text,
    string PostbackData,
    string Latitude,
    string Longitude,
    string PinLabel) : SuggestionBase
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "view_location";

    /// <summary>
    ///     A URL to open if the device is unable to display a map.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Uri FallbackUrl { get; init; }

    /// <summary>
    ///     Returns a suggestion with a fallback url.
    /// </summary>
    /// <param name="url">A URL to open if the device is unable to display a map.</param>
    /// <returns>The suggestion.</returns>
    public ViewLocationSuggestion WithFallbackUrl(Uri url) => this with {FallbackUrl = url};
}