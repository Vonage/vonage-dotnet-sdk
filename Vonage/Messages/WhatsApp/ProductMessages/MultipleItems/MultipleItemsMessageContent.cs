using System.Text.Json.Serialization;

namespace Vonage.Messages.WhatsApp.ProductMessages.MultipleItems;

/// <summary>
/// </summary>
/// <param name="Header"></param>
/// <param name="Body"></param>
/// <param name="Footer"></param>
/// <param name="Action"></param>
public record MultipleItemsMessageContent(
    [property: JsonPropertyOrder(1)] TextSection Header,
    [property: JsonPropertyOrder(2)] TextSection Body,
    [property: JsonPropertyOrder(3)] TextSection Footer,
    [property: JsonPropertyOrder(4)] ActionMultipleItems Action) : IProductMessageContent
{
    /// <summary>
    /// </summary>
    [JsonPropertyOrder(0)]
    public string Type => "product_list";
}

/// <summary>
/// </summary>
/// <param name="CatalogId"></param>
/// <param name="Sections"></param>
public record ActionMultipleItems(
    [property: JsonPropertyOrder(0)] string CatalogId,
    [property: JsonPropertyOrder(1)] params Section[] Sections);

/// <summary>
/// </summary>
/// <param name="Title"></param>
/// <param name="ProductItems"></param>
public record Section(
    [property: JsonPropertyOrder(0)] string Title,
    [property: JsonPropertyOrder(1)] params ProductItem[] ProductItems);

/// <summary>
/// </summary>
/// <param name="ProductRetailerId"></param>
public record ProductItem(string ProductRetailerId);