#region
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Vonage.Messages.Rcs.Suggestions;
#endregion

namespace Vonage.Messages.Rcs;

/// <summary>
///     Represents a carousel message request to be sent via RCS. Carousels display multiple rich cards in a horizontally scrollable format.
/// </summary>
public class RcsCarouselRequest : RcsMessageBase
{
    private const int CardsMaxItems = 10;
    private const int SuggestionsMaxItems = 11;

    /// <inheritdoc />
    public override MessagesChannel Channel => MessagesChannel.RCS;

    /// <inheritdoc />
    public override IEnumerable<string> GetErrors() =>
        base.GetErrors()
            .Concat(this.ValidateCarouselPresence())
            .Concat(this.ValidateCardsCount())
            .Concat(this.Carousel?.Cards?.SelectMany(c => c.GetErrors()) ?? Enumerable.Empty<string>())
            .Concat(this.ValidateSuggestionsCount())
            .Concat(this.Suggestions?.SelectMany(s => s.GetErrors()) ?? Enumerable.Empty<string>());

    private IEnumerable<string> ValidateCarouselPresence()
    {
        if (this.Carousel == null)
            yield return "Carousel must not be null.";
    }

    private IEnumerable<string> ValidateCardsCount()
    {
        if (this.Carousel?.Cards is {Length: > CardsMaxItems})
            yield return $"Carousel must contain at most {CardsMaxItems} cards.";
    }

    private IEnumerable<string> ValidateSuggestionsCount()
    {
        if (this.Suggestions?.Length > SuggestionsMaxItems)
            yield return $"Suggestions must contain at most {SuggestionsMaxItems} items.";
    }

    /// <inheritdoc />
    public override MessagesMessageType MessageType => MessagesMessageType.Carousel;

    /// <summary>
    ///     The carousel attachment containing multiple rich cards.
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