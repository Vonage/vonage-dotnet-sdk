#region
using System;
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs.Suggestions;

/// <summary>
///     A create calendar event suggestion.
/// </summary>
/// <param name="Text">The text to display on the suggestion chip.</param>
/// <param name="PostbackData">
///     The data that will be sent back in the button.payload property of a button message via the
///     inbound message webhook when the user taps the suggestion chip.
/// </param>
/// <param name="StartTime">The start time of the calendar event.</param>
/// <param name="EndTime">The end time of the calendar event.</param>
/// <param name="Title">The title of the calendar event.</param>
public record CreateCalendarEventSuggestion(
    string Text,
    string PostbackData,
    [property: JsonIgnore] DateTime StartTime,
    [property: JsonIgnore] DateTime EndTime,
    string Title) : SuggestionBase
{
    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "create_calendar_event";

    /// <summary>
    ///     StartTime formatted to ISO 8601
    /// </summary>
    [JsonPropertyName("start_time")]
    public string FormattedStartTime => this.StartTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");

    /// <summary>
    ///     EndTime formatted to ISO 8601
    /// </summary>
    [JsonPropertyName("end_time")]
    public string FormattedEndTime => this.EndTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");

    /// <summary>
    ///     A URL to open if the device is unable to place a call.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Uri FallbackUrl { get; init; }

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
    public CreateCalendarEventSuggestion WithDescription(string value) => this with {Description = value};

    /// <summary>
    ///     Returns a suggestion with a fallback url.
    /// </summary>
    /// <param name="url">A URL to open if the device is unable to place a call.</param>
    /// <returns>The suggestion.</returns>
    public CreateCalendarEventSuggestion WithFallbackUrl(Uri url) => this with {FallbackUrl = url};
}