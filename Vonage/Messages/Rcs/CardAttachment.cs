#region
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using Vonage.Common.Serialization;
using Vonage.Messages.Rcs.Suggestions;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
///     Represents a card attachment
/// </summary>
/// <param name="Title">The title of the card.</param>
/// <param name="Text">The text to display on the card.</param>
/// <param name="MediaUrl">
///     The publicly accessible URL of the media attachment for the card.  Supported media include
///     images, videos, and PDFs.
/// </param>
public record CardAttachment(string Title, string Text, Uri MediaUrl)
{
    /// <summary>
    /// </summary>
    public enum Height
    {
        /// <summary>
        /// </summary>
        [Description("SHORT")] Short,

        /// <summary>
        /// </summary>
        [Description("MEDIUM")] Medium,

        /// <summary>
        /// </summary>
        [Description("TALL")] Tall,
    }

    /// <summary>
    ///     A short description of the media for accessibility purposes.
    /// </summary>
    public string MediaDescription { get; init; }

    /// <summary>
    ///     The height of the media element. If not specified, defaults to SHORT.
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionJsonConverter<Height>))]
    public Height? MediaHeight { get; init; }

    /// <summary>
    ///     The publicly accessible URL of the thumbnail image for the card.
    /// </summary>
    public Uri ThumbnailUrl { get; init; }

    /// <summary>
    ///     By default, media URLs are cached on the RCS platform for up to 60 days. Set this property to true to force the
    ///     platform to fetch a fresh copy of the media from the provided URL.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool MediaForceRefresh { get; init; }

    /// <summary>
    ///     An array of suggestion objects to include with the card. You can include up to 4 suggestions per card.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SuggestionBase[] Suggestions { get; init; }

    /// <summary>
    ///     Returns a card attachment with a new suggestion.
    /// </summary>
    /// <param name="suggestion">The suggestion</param>
    /// <returns>The card attachment</returns>
    public CardAttachment AppendSuggestion(SuggestionBase suggestion) =>
        this with
        {
            Suggestions = (this.Suggestions ?? new List<SuggestionBase>().ToArray()).Concat([suggestion]).ToArray(),
        };

    /// <summary>
    ///     Returns a card attachment with a forced media refresh.
    /// </summary>
    /// <returns>The card attachment</returns>
    public CardAttachment ForceMediaRefresh() => this with {MediaForceRefresh = true};

    /// <summary>
    ///     Returns a card attachment with a media description.
    /// </summary>
    /// <param name="value">The media description</param>
    /// <returns>The card attachment</returns>
    public CardAttachment WithMediaDescription(string value) => this with {MediaDescription = value};

    /// <summary>
    ///     Returns a card attachment with a media height.
    /// </summary>
    /// <param name="value">The media height</param>
    /// <returns>The card attachment</returns>
    public CardAttachment WithMediaHeight(Height value) => this with {MediaHeight = value};

    /// <summary>
    ///     Returns a card attachment with a thumbnail url.
    /// </summary>
    /// <param name="value">The thumbnail url.</param>
    /// <returns>The card attachment</returns>
    public CardAttachment WithThumbnailUrl(Uri value) => this with {ThumbnailUrl = value};
}