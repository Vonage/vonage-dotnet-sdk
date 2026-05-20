#region
using System;
using System.Collections.Generic;
using System.Linq;
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
/// <param name="Description">A short description of the URL for accessibility purposes.</param>
public record CreateCalendarEventSuggestion(
    string Text,
    string PostbackData,
    [property: JsonIgnore] DateTime StartTime,
    [property: JsonIgnore] DateTime EndTime,
    string Title,
    string Description) : SuggestionBase
{
    private const int TitleMaxLength = 100;
    private const int DescriptionMaxLength = 500;

    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "create_calendar_event";

    /// <inheritdoc />
    public override IEnumerable<string> GetErrors() =>
        base.GetErrors()
            .Concat(this.ValidateTitle())
            .Concat(this.ValidateDescription());

    private IEnumerable<string> ValidateTitle()
    {
        if (string.IsNullOrEmpty(this.Title))
            yield return "Title must not be null or empty.";
        else if (this.Title.Length > TitleMaxLength)
            yield return $"Title must not exceed {TitleMaxLength} characters.";
    }

    private IEnumerable<string> ValidateDescription()
    {
        if (string.IsNullOrEmpty(this.Description))
            yield return "Description must not be null or empty.";
        else if (this.Description.Length > DescriptionMaxLength)
            yield return $"Description must not exceed {DescriptionMaxLength} characters.";
    }

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
    public Uri FallbackUrl { get; private init; }

    /// <summary>
    ///     Returns a suggestion with a fallback url.
    /// </summary>
    /// <param name="url">A URL to open if the device is unable to place a call.</param>
    /// <returns>The suggestion.</returns>
    public CreateCalendarEventSuggestion WithFallbackUrl(Uri url) => this with {FallbackUrl = url};
}