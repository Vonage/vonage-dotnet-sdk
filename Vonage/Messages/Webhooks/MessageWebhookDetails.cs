#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Messages.Webhooks;

/// <summary>
///     Represents details of an order.
/// </summary>
public struct OrderDetails
{
    /// <summary>
    ///     The ID of the catalog containing the products in this order.
    /// </summary>
    [JsonPropertyName("catalog_id")]
    [JsonProperty("catalog_id")]
    public string CatalogId { get; set; }

    /// <summary>
    ///     The list of items.
    /// </summary>
    [JsonPropertyName("product_items")]
    [JsonProperty("product_items")]
    public ProductItem[] ProductItems { get; set; }
}

/// <summary>
///     Represents an item.
/// </summary>
public struct ProductItem
{
    /// <summary>
    ///     The currency code representing the currency for this specific item.
    /// </summary>
    [JsonPropertyName("currency")]
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    ///     The unit price for this specific item.
    /// </summary>
    [JsonPropertyName("item_price")]
    [JsonProperty("item_price")]
    public string ItemPrice { get; set; }

    /// <summary>
    ///     The ID of the specific product being ordered.
    /// </summary>
    [JsonPropertyName("product_retailer_id")]
    [JsonProperty("product_retailer_id")]
    public string ProductRetailerId { get; set; }

    /// <summary>
    ///     The quantity ordered for this specific item.
    /// </summary>
    [JsonPropertyName("quantity")]
    [JsonProperty("quantity")]
    public string Quantity { get; set; }
}

/// <summary>
///     Represents the location details.
/// </summary>
public struct LocationDetails
{
    /// <summary>
    ///     Address of the location. Only displayed if name is present.
    /// </summary>
    [JsonPropertyName("address")]
    [JsonProperty("address")]
    public string Address { get; set; }

    /// <summary>
    ///     Latitude of the location.
    /// </summary>
    [JsonPropertyName("lat")]
    [JsonProperty("lat")]
    public decimal Latitude { get; set; }

    /// <summary>
    ///     Longitude of the location.
    /// </summary>
    [JsonPropertyName("long")]
    [JsonProperty("long")]
    public decimal Longitude { get; set; }

    /// <summary>
    ///     Name of the location.
    /// </summary>
    [JsonPropertyName("name")]
    [JsonProperty("name")]
    public string Name { get; set; }
}

/// <summary>
///     Represents a Reply details.
/// </summary>
public struct ReplyDetails
{
    /// <summary>
    ///     A description that may be added to the interactive options presented (available only on interactive lists).
    /// </summary>
    [JsonPropertyName("description")]
    [JsonProperty("description")]
    public string Description { get; set; }

    /// <summary>
    ///     An identifier to help identify the exact interactive message response.
    /// </summary>
    [JsonPropertyName("id")]
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    ///     The title displayed on the interactive option chosen.
    /// </summary>
    [JsonPropertyName("title")]
    [JsonProperty("title")]
    public string Title { get; set; }
}

/// <summary>
///     Channel specific metadata for SMS.
/// </summary>
public struct SmsDetails
{
    /// <summary>
    ///     The first word of the message sent to uppercase.
    /// </summary>
    [JsonPropertyName("keyword")]
    [JsonProperty("keyword")]
    public string Keyword { get; set; }

    /// <summary>
    ///     The number of inbound SMS messages concatenated together to comprise this message. SMS messages are 160 characters,
    ///     if an inbound message exceeds that size they are concatenated together to forma single message. This number
    ///     indicates how many messages formed this webhook.
    /// </summary>
    [JsonPropertyName("num_messages")]
    [JsonProperty("num_messages")]
    public string MessagesCount { get; set; }

    /// <summary>
    ///     The number of inbound SMS messages concatenated together to comprise this message. SMS messages are 160 characters,
    ///     if an inbound message exceeds that size they are concatenated together to forma single message. This number
    ///     indicates how many messages formed this webhook.
    /// </summary>
    [JsonPropertyName("total_count")]
    [JsonProperty("total_count")]
    public string TotalCount { get; set; }
}

/// <summary>
///     Represents details about the message usage.
/// </summary>
public struct UsageDetails
{
    /// <summary>
    ///     The charge currency in ISO 4217 format.
    /// </summary>
    [JsonPropertyName("currency")]
    [JsonProperty("currency")]
    public string Currency { get; set; }

    /// <summary>
    ///     The charge amount as a stringified number.
    /// </summary>
    [JsonPropertyName("price")]
    [JsonProperty("price")]
    public string Price { get; set; }
}

/// <summary>
///     Represents a details with an accessible URL.
/// </summary>
public struct UrlDetails
{
    /// <summary>
    ///     The publicly accessible URL of the attachment.
    /// </summary>
    [JsonPropertyName("url")]
    [JsonProperty("url")]
    public string Url { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("name")]
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("caption")]
    [JsonProperty("caption")]
    public string Caption { get; set; }
}

/// <summary>
///     Represents the profile details.
/// </summary>
public struct ProfileDetails
{
    /// <summary>
    ///     The WhatsApp number's displayed profile name.
    /// </summary>
    [JsonPropertyName("name")]
    [JsonProperty("name")]
    public string Name { get; set; }
}

/// <summary>
///     This is only present for the Inbound Message where the user is quoting another message. It provides information
///     about the quoted message and/or the product message being responded to.
/// </summary>
public struct ContextDetails
{
    /// <summary>
    ///     The phone number of the original sender of the message being quoted in the E.164 format.
    /// </summary>
    [JsonPropertyName("message_from")]
    [JsonProperty("message_from")]
    public string MessageFrom { get; set; }

    /// <summary>
    ///     The UUID of the message being quoted.
    /// </summary>
    [JsonPropertyName("message_uuid")]
    [JsonProperty("message_uuid")]
    public string MessageUuid { get; set; }

    /// <summary>
    ///     An object containing details of a product from a product message being quoted or replied to using the 'Message
    ///     Business' option.
    /// </summary>
    [JsonPropertyName("whatsapp_referred_product")]
    [JsonProperty("whatsapp_referred_product")]
    public WhatsAppReferredProduct ReferredProduct { get; set; }
}

/// <summary>
///     An object containing details of a product from a product message being quoted or replied to using the 'Message
///     Business' option.
/// </summary>
public struct WhatsAppReferredProduct
{
    /// <summary>
    ///     The ID of the catalog associated with the product from the product message being quoted or replied to using the
    ///     'Message Business' option.
    /// </summary>
    [JsonPropertyName("catalog_id")]
    [JsonProperty("catalog_id")]
    public string CatalogId { get; set; }

    /// <summary>
    ///     The ID of the product from the product message being quoted or replied to using the 'Message Business' option.
    /// </summary>
    [JsonPropertyName("product_retailer_id")]
    [JsonProperty("product_retailer_id")]
    public string ProductRetailerId { get; set; }
}

/// <summary>
/// </summary>
/// <param name="NetworkCode"></param>
public record Origin(
    [property: JsonPropertyName("network_code")]
    [property: JsonProperty("network_code")]
    string NetworkCode);