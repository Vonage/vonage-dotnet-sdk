using System.Text.Json.Serialization;

namespace Vonage.Messages.WhatsApp.ProductMessages;

/// <summary>
///     Represents a product message. Product messages provide a way for businesses to showcase and share products and
///     services with customers via a WhatsApp chat, and for those customers to browse items, add them to a cart, and
///     submit an order without leaving the chat.
/// </summary>
/// <param name="Details">Contains the details of the message.</param>
/// <typeparam name="T">Type of the content.</typeparam>
public record ProductMessage<T>([property: JsonPropertyOrder(1)]
    [property: JsonPropertyName("interactive")]
    T Details) where T : IProductMessageContent
{
    /// <summary>
    ///     The type of the product message.
    /// </summary>
    [JsonPropertyOrder(0)]
    public string Type => "interactive";
}