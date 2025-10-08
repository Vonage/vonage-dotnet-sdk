#region
using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
#endregion

namespace Vonage.Messages.Rcs.Suggestions;

/// <summary>
///     A open URL in webview suggestion.
/// </summary>
/// <param name="Text">The text to display on the suggestion chip.</param>
/// <param name="PostbackData">
///     The data that will be sent back in the button.payload property of a button message via the
///     inbound message webhook when the user taps the suggestion chip.
/// </param>
/// <param name="Url">The URL to open when the suggestion is tapped.</param>
public record OpenWebviewUrlSuggestion(string Text, string PostbackData, Uri Url) : SuggestionBase
{
    /// <summary>
    /// </summary>
    public enum ViewModeValue
    {
        /// <summary>
        /// </summary>
        [Description("FULL")] Full,

        /// <summary>
        /// </summary>
        [Description("TALL")] Tall,

        /// <summary>
        /// </summary>
        [Description("HALF")] Half,
    }

    /// <inheritdoc />
    [JsonIgnore]
    public override string Type => "open_url_in_webview";

    /// <summary>
    ///     A short description of the URL for accessibility purposes.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Description { get; init; }

    /// <summary>
    ///     The mode for displaying the URL in the webview window.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonConverter(typeof(EnumDescriptionJsonConverter<ViewModeValue>))]
    public ViewModeValue? ViewMode { get; init; }

    /// <summary>
    ///     Returns a suggestion with a description.
    /// </summary>
    /// <param name="value">A short description of the URL for accessibility purposes.</param>
    /// <returns>The suggestion.</returns>
    public OpenWebviewUrlSuggestion WithDescription(string value) => this with {Description = value};

    /// <summary>
    ///     Returns a suggestion with view mode.
    /// </summary>
    /// <param name="value">The mode for displaying the URL in the webview window.</param>
    /// <returns>The suggestion.</returns>
    public OpenWebviewUrlSuggestion WithViewMode(ViewModeValue value) => this with {ViewMode = value};
}