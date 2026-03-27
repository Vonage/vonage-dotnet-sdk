#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Messages.Webhooks;

/// <summary>
///     Represents error details when a message fails to be delivered.
/// </summary>
/// <param name="Type">A URI reference that identifies the error type.</param>
/// <param name="Title">A short human-readable summary of the error.</param>
/// <param name="Detail">A human-readable explanation of the error.</param>
/// <param name="Instance">A URI reference that identifies the specific occurrence of the error.</param>
public record StatusError(
    [property: JsonPropertyName("type")]
    [property: JsonProperty("type")]
    string Type,
    [property: JsonPropertyName("title")]
    [property: JsonProperty("title")]
    string Title,
    [property: JsonPropertyName("detail")]
    [property: JsonProperty("detail")]
    string Detail,
    [property: JsonPropertyName("instance")]
    [property: JsonProperty("instance")]
    string Instance);

/// <summary>
///     Represents workflow information for failover message sequences.
/// </summary>
/// <param name="Id">The unique identifier of the workflow.</param>
/// <param name="ItemsNumber">The current item number in the workflow sequence.</param>
/// <param name="ItemsTotal">The total number of items in the workflow.</param>
public record StatusWorkflow(
    [property: JsonPropertyName("workflow_id")]
    [property: JsonProperty("workflow_id")]
    string Id,
    [property: JsonPropertyName("items_number")]
    [property: JsonProperty("items_number")]
    string ItemsNumber,
    [property: JsonPropertyName("items_total")]
    [property: JsonProperty("items_total")]
    string ItemsTotal);

/// <summary>
///     Represents billing and usage information for a message.
/// </summary>
/// <param name="Currency">The currency code in ISO 4217 format.</param>
/// <param name="Price">The price charged for the message.</param>
public record StatusUsage(
    [property: JsonPropertyName("currency")]
    [property: JsonProperty("currency")]
    string Currency,
    [property: JsonPropertyName("price")]
    [property: JsonProperty("price")]
    string Price);

/// <summary>
///     Represents destination network information.
/// </summary>
/// <param name="NetworkCode">The network code of the destination carrier.</param>
public record StatusDestination(
    [property: JsonPropertyName("network_code")]
    [property: JsonProperty("network_code")]
    string NetworkCode);

/// <summary>
///     Represents SMS-specific status details.
/// </summary>
/// <param name="Count">The total number of SMS segments used.</param>
public record StatusSms(
    [property: JsonPropertyName("count_total")]
    [property: JsonProperty("count_total")]
    string Count);

/// <summary>
///     Represents WhatsApp-specific status details.
/// </summary>
/// <param name="Pricing">WhatsApp pricing information for the message.</param>
/// <param name="Conversation">WhatsApp conversation details.</param>
public record StatusWhatsApp(
    [property: JsonPropertyName("pricing")]
    [property: JsonProperty("pricing")]
    StatusWhatsAppPricing Pricing,
    [property: JsonPropertyName("conversation")]
    [property: JsonProperty("conversation")]
    StatusWhatsAppConversation Conversation
);

/// <summary>
///     Represents WhatsApp pricing information.
/// </summary>
/// <param name="Type">The pricing type.</param>
/// <param name="PricingModel">The pricing model used.</param>
/// <param name="Category">The message category for pricing purposes.</param>
public record StatusWhatsAppPricing(
    [property: JsonPropertyName("type")]
    [property: JsonProperty("type")]
    string Type,
    [property: JsonPropertyName("pricing_model")]
    [property: JsonProperty("pricing_model")]
    string PricingModel,
    [property: JsonPropertyName("category")]
    [property: JsonProperty("category")]
    string Category);

/// <summary>
///     Represents WhatsApp conversation details.
/// </summary>
/// <param name="Id">The unique identifier of the conversation.</param>
/// <param name="Origin">Information about who initiated the conversation.</param>
public record StatusWhatsAppConversation(
    [property: JsonPropertyName("id")]
    [property: JsonProperty("id")]
    string Id,
    [property: JsonPropertyName("origin")]
    [property: JsonProperty("origin")]
    StatusWhatsAppConversationOrigin Origin);

/// <summary>
///     Represents the origin of a WhatsApp conversation.
/// </summary>
/// <param name="Type">The conversation type indicating who initiated the conversation (e.g., business_initiated, user_initiated).</param>
public record StatusWhatsAppConversationOrigin(
    [property: JsonPropertyName("type")]
    [property: JsonProperty("type")]
    string Type);