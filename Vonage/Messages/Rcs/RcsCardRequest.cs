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
/// </summary>
public class RcsCardRequest : RcsMessageBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.RCS;

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Card;

    /// <summary>
    ///     The duration in seconds the delivery of a message will be attempted. By default, Vonage attempts delivery for 72
    ///     hours, however the maximum effective value depends on the operator and is typically 24 - 48 hours. We recommend
    ///     this value should be kept at its default or at least 30 minutes.
    /// </summary>
    [JsonPropertyName("ttl")]
    [JsonPropertyOrder(8)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int TimeToLive { get; set; }

    /// <summary>
    ///     The card attachment.
    /// </summary>
    [JsonPropertyName("card")]
    [JsonPropertyOrder(9)]
    public CardAttachment Card { get; set; }
}

/// <summary>
/// </summary>
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
    /// </summary>
    public string MediaDescription { get; init; }

    /// <summary>
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionJsonConverter<Height>))]
    public Height? MediaHeight { get; init; }

    /// <summary>
    /// </summary>
    public Uri ThumbnailUrl { get; init; }

    /// <summary>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool MediaForceRefresh { get; init; }

    /// <summary>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SuggestionBase[] Suggestions { get; init; }

    public CardAttachment AppendSuggestion(SuggestionBase suggestion) =>
        this with
        {
            Suggestions = (this.Suggestions ?? new List<SuggestionBase>().ToArray()).Concat([suggestion]).ToArray(),
        };

    public CardAttachment ForceMediaRefresh() => this with {MediaForceRefresh = true};

    public CardAttachment WithMediaDescription(string value) => this with {MediaDescription = value};

    public CardAttachment WithMediaHeight(Height value) => this with {MediaHeight = value};

    public CardAttachment WithThumbnailUrl(Uri value) => this with {ThumbnailUrl = value};
}