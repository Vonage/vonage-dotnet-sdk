#region
using System.Text.Json.Serialization;
using Vonage.Messages.Rcs.Suggestions;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
/// </summary>
public class RcsCarouselRequest : RcsMessageBase
{
    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.RCS;

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Carousel;

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
    ///     The carousel attachment.
    /// </summary>
    [JsonPropertyOrder(9)]
    public CarouselAttachment Carousel { get; set; }

    /// <summary>
    ///     An array of suggestion objects to include with the card. You can include up to 11 suggestions per card.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyOrder(10)]
    public SuggestionBase[] Suggestions { get; set; }
}

/// <summary>
///     The carousel attachment.
/// </summary>
/// <param name="Cards">The list of cards.</param>
public record CarouselAttachment(
    [property: JsonPropertyName("cards")] params CardAttachment[] Cards);