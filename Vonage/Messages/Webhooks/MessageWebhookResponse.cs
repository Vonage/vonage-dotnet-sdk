using System;
using System.Text.Json.Serialization;

namespace Vonage.Messages.Webhooks;

/// <summary>
/// Represents a webhook response for Messages.
/// </summary>
public struct MessageWebhookResponse
{
    /// <summary>
    /// Channel specific metadata for Audio.
    /// </summary>
    public UrlDetails? Audio { get; set; }
    
    /// <summary>
    /// The channel the message came in on.
    /// </summary>
    public string Channel { get; set; }

    /// <summary>
    /// Client reference of up to 100 characters. The reference will be present in every message status.
    /// </summary>
    [JsonPropertyName("client_ref")] public string ClientReference { get; set; }

    /// <summary>
    /// This is only present for the Inbound Message where the user is quoting another message. It provides information about the quoted message and/or the product message being responded to.
    /// </summary>
    public ContextDetails? Context { get; set; }
    
    /// <summary>
    /// Channel specific metadata for File.
    /// </summary>
    public UrlDetails? File { get; set; }
    
    /// <summary>
    /// The phone number of the message sender in the E.164 format. Don't use a leading + or 00 when entering a phone number, start with the country code, for example, 447700900000. For SMS in certain localities alpha-numeric sender id's will work as well, see  <see href="https://developer.vonage.com/en/messaging/sms/guides/country-specific-features#country-specific-features">Global Messaging</see> for more details.
    /// </summary>
    public string From { get; set; }
    
    /// <summary>
    /// Channel specific metadata for Image.
    /// </summary>
    public UrlDetails? Image { get; set; }
    
    /// <summary>
    /// Channel specific metadata for Location.
    /// </summary>
    public LocationDetails? Location { get; set; }

    /// <summary>
    /// The type of message to send. You must provide text in this field.
    /// </summary>
    [JsonPropertyName("message_type")] public string MessageType { get; set; }

    /// <summary>
    /// The UUID of the message.
    /// </summary>
    [JsonPropertyName("message_uuid")] public Guid MessageUuid { get; set; }

    /// <summary>
    /// Channel specific metadata for Order.
    /// </summary>
    public OrderDetails? Order { get; set; }
    
    /// <summary>
    /// Represents the profile details.
    /// </summary>
    public ProfileDetails? Profile { get; set; }

    /// <summary>
    /// A message from the channel provider, which may contain a description, error codes or other information.
    /// </summary>
    [JsonPropertyName("provider_message")] public string ProviderMessage { get; set; }

    /// <summary>
    /// Channel specific metadata for Reply.
    /// </summary>
    public ReplyDetails? Reply { get; set; }
    
    /// <summary>
    /// Channel specific metadata for SMS.
    /// </summary>
    public SmsDetails? Sms { get; set; }
    
    /// <summary>
    /// Channel specific metadata for Sticker.
    /// </summary>
    public UrlDetails? Sticker { get; set; }
    
    /// <summary>
    /// The UTF-8 encoded text of the inbound message.
    /// </summary>
    public string Text { get; set; }
    
    /// <summary>
    /// The datetime of when the event occurred, in ISO 8601 format.
    /// </summary>
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    /// The phone number of the message recipient in the E.164 format. Don't use a leading + or 00 when entering a phone number, start with the country code, for example, 447700900000.
    /// </summary>
    public string To { get; set; }
    
    /// <summary>
    /// Represents details about message usage.
    /// </summary>
    public UsageDetails? Usage { get; set; }
    
    /// <summary>
    /// Channel specific metadata for Vcard.
    /// </summary>
    public UrlDetails? Vcard { get; set; }
    
    /// <summary>
    /// Channel specific metadata for Video.
    /// </summary>
    public UrlDetails? Video { get; set; }
}

/// <summary>
/// Represents details of an order.
/// </summary>
public struct OrderDetails
{
    /// <summary>
    /// The ID of the catalog containing the products in this order.
    /// </summary>
    [JsonPropertyName("catalog_id")] public string CatalogId { get; set; }

    /// <summary>
    /// The list of items.
    /// </summary>
    [JsonPropertyName("product_items")] public ProductItem[] ProductItems { get; set; }
}

/// <summary>
/// Represents an item.
/// </summary>
public struct ProductItem
{
    /// <summary>
    /// The currency code representing the currency for this specific item.
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// The unit price for this specific item.
    /// </summary>
    [JsonPropertyName("item_price")] public string ItemPrice { get; set; }

    /// <summary>
    /// The ID of the specific product being ordered.


    /// </summary>
    [JsonPropertyName("product_retailer_id")]
    public string ProductRetailerId { get; set; }

    /// <summary>
    /// The quantity ordered for this specific item.
    /// </summary>
    public string Quantity { get; set; }
}

/// <summary>
/// Represents the location details.
/// </summary>
public struct LocationDetails
{
    /// <summary>
    /// Address of the location. Only displayed if name is present.
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// Latitude of the location.
    /// </summary>
    [JsonPropertyName("lat")] public decimal Latitude { get; set; }

    /// <summary>
    /// Longitude of the location.
    /// </summary>
    [JsonPropertyName("long")] public decimal Longitude { get; set; }

    /// <summary>
    /// Name of the location.
    /// </summary>
    public string Name { get; set; }
}

/// <summary>
/// Represents a Reply details.
/// </summary>
public struct ReplyDetails
{
    /// <summary>
    /// A description that may be added to the interactive options presented (available only on interactive lists).
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// An identifier to help identify the exact interactive message response.
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    /// The title displayed on the interactive option chosen.
    /// </summary>
    public string Title { get; set; }
}

/// <summary>
/// Channel specific metadata for SMS.
/// </summary>
public struct SmsDetails
{
    /// <summary>
    /// The first word of the message sent to uppercase.
    /// </summary>
    public string Keyword { get; set; }

    /// <summary>
    /// The number of inbound SMS messages concatenated together to comprise this message. SMS messages are 160 characters, if an inbound message exceeds that size they are concatenated together to forma single message. This number indicates how many messages formed this webhook.
    /// </summary>
    [JsonPropertyName("num_messages")] public string MessagesCount { get; set; }

    /// <summary>
    /// The number of inbound SMS messages concatenated together to comprise this message. SMS messages are 160 characters, if an inbound message exceeds that size they are concatenated together to forma single message. This number indicates how many messages formed this webhook.
    /// </summary>
    [JsonPropertyName("total_count")] public string TotalCount { get; set; }
}

/// <summary>
/// Represents details about the message usage.
/// </summary>
public struct UsageDetails
{
    /// <summary>
    /// The charge currency in ISO 4217 format.
    /// </summary>
    public string Currency { get; set; }
    
    /// <summary>
    /// The charge amount as a stringified number.
    /// </summary>
    public string Price { get; set; }
}

/// <summary>
/// Represents a details with an accessible URL.
/// </summary>
public struct UrlDetails
{
    /// <summary>
    /// The publicly accessible URL of the attachment.
    /// </summary>
    public string Url { get; set; }
}

/// <summary>
/// Represents the profile details.
/// </summary>
public struct ProfileDetails
{
    /// <summary>
    /// The WhatsApp number's displayed profile name.
    /// </summary>
    public string Name { get; set; }
}

/// <summary>
/// This is only present for the Inbound Message where the user is quoting another message. It provides information about the quoted message and/or the product message being responded to.
/// </summary>
public struct ContextDetails
{
    /// <summary>
    /// The phone number of the original sender of the message being quoted in the E.164 format.
    /// </summary>
    [JsonPropertyName("message_from")] public string MessageFrom { get; set; }

    /// <summary>
    /// The UUID of the message being quoted.
    /// </summary>
    [JsonPropertyName("message_uuid")] public string MessageUuid { get; set; }

    /// <summary>
    /// An object containing details of a product from a product message being quoted or replied to using the 'Message Business' option.
    /// </summary>
    [JsonPropertyName("whatsapp_referred_product")]
    public WhatsAppPreferredProduct ReferredProduct { get; set; }
}

/// <summary>
/// An object containing details of a product from a product message being quoted or replied to using the 'Message Business' option.
/// </summary>
public struct WhatsAppPreferredProduct
{
    /// <summary>
    /// The ID of the catalog associated with the product from the product message being quoted or replied to using the 'Message Business' option.
    /// </summary>
    [JsonPropertyName("catalog_id")] public string CatalogId { get; set; }

    /// <summary>
    /// The ID of the product from the product message being quoted or replied to using the 'Message Business' option.
    /// </summary>
    [JsonPropertyName("product_retailer_id")]
    public string ProductRetailerId { get; set; }
}