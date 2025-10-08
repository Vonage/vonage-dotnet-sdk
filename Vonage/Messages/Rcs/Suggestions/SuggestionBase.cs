#region
using System.Text.Json.Serialization;
#endregion

namespace Vonage.Messages.Rcs.Suggestions;

/// <summary>
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
    /// <summary>
    ///     The type for the suggestion object.
    /// </summary>
    public abstract string Type { get; }
}