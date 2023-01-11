using System;
using Newtonsoft.Json;

namespace Vonage.Messages.WhatsApp;

public class WhatsAppInboundMessage
{
    [JsonProperty("channel")] public MessagesChannel Channel { get; set; }

    [JsonProperty("client_ref")] public string ClientRef { get; set; }

    [JsonProperty("context")] public Context Context { get; set; }

    [JsonProperty("from")] public string From { get; set; }

    [JsonProperty("message_type")] public MessagesMessageType MessageType { get; set; }

    [JsonProperty("message_uuid")] public string MessageUuid { get; set; }

    [JsonProperty("profile")] public Profile Profile { get; set; }

    [JsonProperty("provider_message")] public string ProviderMessage { get; set; }

    [JsonProperty("text")] public string Text { get; set; }

    [JsonProperty("timestamp")] public DateTime Timestamp { get; set; }

    [JsonProperty("to")] public string To { get; set; }
}

public class Profile
{
    [JsonProperty("name")] public string Name { get; set; }
}

public class Context
{
    [JsonProperty("message_from")] public string MessageFrom { get; set; }

    [JsonProperty("message_uuid")] public string MessageUuid { get; set; }

    [JsonProperty("whatsapp_referred_product")]
    public WhatsAppReferredProduct WhatsAppReferredProduct { get; set; }
}

public class WhatsAppReferredProduct
{
    [JsonProperty("catalog_id")] public string CatalogId { get; set; }

    [JsonProperty("product_retailer_id")] public string ProductRetailerId { get; set; }
}