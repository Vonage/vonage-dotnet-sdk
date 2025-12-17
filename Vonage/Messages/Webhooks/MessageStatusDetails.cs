#region
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Messages.Webhooks;

/// <summary>
/// </summary>
/// <param name="Type"></param>
/// <param name="Title"></param>
/// <param name="Detail"></param>
/// <param name="Instance"></param>
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
/// </summary>
/// <param name="Id"></param>
/// <param name="ItemsNumber"></param>
/// <param name="ItemsTotal"></param>
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
/// </summary>
/// <param name="Currency"></param>
/// <param name="Price"></param>
public record StatusUsage(
    [property: JsonPropertyName("currency")]
    [property: JsonProperty("currency")]
    string Currency,
    [property: JsonPropertyName("price")]
    [property: JsonProperty("price")]
    string Price);

/// <summary>
/// </summary>
/// <param name="NetworkCode"></param>
public record StatusDestination(
    [property: JsonPropertyName("network_code")]
    [property: JsonProperty("network_code")]
    string NetworkCode);

/// <summary>
/// </summary>
/// <param name="Count"></param>
public record StatusSms(
    [property: JsonPropertyName("count_total")]
    [property: JsonProperty("count_total")]
    string Count);

/// <summary>
/// </summary>
/// <param name="Pricing"></param>
/// <param name="Conversation"></param>
public record StatusWhatsApp(
    [property: JsonPropertyName("pricing")]
    [property: JsonProperty("pricing")]
    StatusWhatsAppPricing Pricing,
    [property: JsonPropertyName("conversation")]
    [property: JsonProperty("conversation")]
    StatusWhatsAppConversation Conversation
);

/// <summary>
/// </summary>
/// <param name="Type"></param>
/// <param name="PricingModel"></param>
/// <param name="Category"></param>
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
/// </summary>
/// <param name="Id"></param>
/// <param name="Origin"></param>
public record StatusWhatsAppConversation(
    [property: JsonPropertyName("id")]
    [property: JsonProperty("id")]
    string Id,
    [property: JsonPropertyName("origin")]
    [property: JsonProperty("origin")]
    StatusWhatsAppConversationOrigin Origin);

/// <summary>
/// </summary>
/// <param name="Type"></param>
public record StatusWhatsAppConversationOrigin(
    [property: JsonPropertyName("type")]
    [property: JsonProperty("type")]
    string Type);