using System.Text.Json.Serialization;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.Messages.WhatsApp.ProductMessages.MultipleItems;

/// <summary>
///     Represents the content for Multiple Item product messages.
/// </summary>
/// <param name="Header">The value of the header text.</param>
/// <param name="Body">The value of the body text.</param>
/// <param name="Footer">The value of the footer text.</param>
/// <param name="Action">Contains data about the product displayed in the message.</param>
public record MultipleItemsMessageContent(
    [property: JsonPropertyOrder(1)] TextSection Header,
    [property: JsonPropertyOrder(2)] TextSection Body,
    [property: JsonPropertyOrder(3)]
    [property: JsonConverter(typeof(MaybeJsonConverter<TextSection>))]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    Maybe<TextSection> Footer,
    [property: JsonPropertyOrder(4)] MultipleItemsAction Action) : IProductMessageContent
{
    /// <summary>
    ///     The content type.
    /// </summary>
    [JsonPropertyOrder(0)]
    public string Type => "product_list";
}

/// <summary>
///     Represents the action for a Multiple Item product message.
/// </summary>
/// <param name="CatalogId">The catalog Id.</param>
/// <param name="Sections">The section.</param>
public record MultipleItemsAction(
    [property: JsonPropertyOrder(0)] string CatalogId,
    [property: JsonPropertyOrder(1)] params Section[] Sections);

/// <summary>
///     Represents a section of items grouped by title.
/// </summary>
/// <param name="Title">The title of the sections.</param>
/// <param name="ProductItems">The product items of the section.</param>
public record Section(
    [property: JsonPropertyOrder(0)] string Title,
    [property: JsonPropertyOrder(1)] params ProductItem[] ProductItems);

/// <summary>
///     Represents a product item.
/// </summary>
/// <param name="ProductRetailerId">The product retailer Id.</param>
public record ProductItem(string ProductRetailerId);