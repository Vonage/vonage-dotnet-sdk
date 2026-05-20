#region
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs.Suggestions;

/// <summary>
///     Base class for all RCS suggestion types. Suggestions are interactive elements that appear as chips below messages.
/// </summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(DialSuggestion), "dial")]
[JsonDerivedType(typeof(ReplySuggestion), "reply")]
[JsonDerivedType(typeof(ViewLocationSuggestion), "view_location")]
[JsonDerivedType(typeof(ShareLocationSuggestion), "share_location")]
[JsonDerivedType(typeof(OpenUrlSuggestion), "open_url")]
[JsonDerivedType(typeof(OpenWebviewUrlSuggestion), "open_url_in_webview")]
[JsonDerivedType(typeof(CreateCalendarEventSuggestion), "create_calendar_event")]
public abstract record SuggestionBase
{
    private const int TextMaxLength = 25;

    /// <summary>
    ///     The data returned when the user taps the suggestion chip.
    /// </summary>
    public abstract string PostbackData { get; init; }

    /// <summary>
    ///     The text to display on the suggestion chip.
    /// </summary>
    public abstract string Text { get; init; }

    /// <summary>
    ///     The type for the suggestion object.
    /// </summary>
    public abstract string Type { get; }

    /// <summary>
    ///     Returns any validation errors for this suggestion.
    /// </summary>
    public virtual IEnumerable<string> GetErrors() => this.ValidateSuggestionText().Concat(this.ValidatePostbackData());

    private IEnumerable<string> ValidateSuggestionText()
    {
        if (string.IsNullOrEmpty(this.Text))
            yield return "Text must not be null or empty.";
        else if (this.Text.Length > TextMaxLength)
            yield return $"Text must not exceed {TextMaxLength} characters.";
    }

    private IEnumerable<string> ValidatePostbackData()
    {
        if (string.IsNullOrEmpty(this.PostbackData))
            yield return "PostbackData must not be null or empty.";
    }
}