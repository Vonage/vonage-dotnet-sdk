using System.Text.Json.Serialization;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.Messages.WhatsApp.ProductMessages.SingleItem;

/// <summary>
///     Represents the content for Single Item product messages.
/// </summary>
/// <param name="Body">The value of the body text.</param>
/// <param name="Footer">The value of the footer text.</param>
/// <param name="Action">Contains data about the product displayed in the message.</param>
public record SingleItemMessageContent(
    [property: JsonPropertyOrder(1)]
    [property: JsonConverter(typeof(MaybeJsonConverter<TextSection>))]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    Maybe<TextSection> Body,
    [property: JsonPropertyOrder(2)]
    [property: JsonConverter(typeof(MaybeJsonConverter<TextSection>))]
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    Maybe<TextSection> Footer,
    [property: JsonPropertyOrder(3)] SingleItemAction Action) : IProductMessageContent
{
    // [JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    /// <summary>
    ///     The content type.
    /// </summary>
    [JsonPropertyOrder(0)]
    public string Type => "product";
}

/// <summary>
///     Represents the action for a Single Item product message.
/// </summary>
/// <param name="CatalogId">The catalog Id.</param>
/// <param name="ProductRetailerId">The product retailer Id.</param>
public record SingleItemAction(
    [property: JsonPropertyOrder(0)] string CatalogId,
    [property: JsonPropertyOrder(1)] string ProductRetailerId);