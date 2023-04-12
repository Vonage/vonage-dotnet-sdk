using System;
using System.Text.Json.Serialization;

namespace Vonage.Messages.Webhooks;

public struct MessageWebhookResponse
{
    public string channel { get; set; }
    public Guid message_uuid { get; set; }
    public string to { get; set; }
    public string from { get; set; }
    public DateTimeOffset timestamp { get; set; }
    public string client_ref { get; set; }
    public string message_type { get; set; }
    public string text { get; set; }
    public SmsDetails? sms { get; set; }
    public UsageDetails? usage { get; set; }
    public ImageDetails? image { get; set; }
    public VcardDetails? vcard { get; set; }
    public AudioDetails? audio { get; set; }
    public VideoDetails? video { get; set; }
    public FileDetails? file { get; set; }
    public LocationDetails? location { get; set; }
    public ProfileDetails? profile { get; set; }
    public ContextDetails? context { get; set; }
    public ReplyDetails? reply { get; set; }
    public OrderDetails? order { get; set; }
    public StickerDetails? sticker { get; set; }
    public string provider_message { get; set; }
}

public struct StickerDetails
{
    public string  url { get; set; }
}

public struct OrderDetails
{
    public string catalog_id { get; set; }
    public ProductItem[] product_items { get; set; }
}

public struct ProductItem
{
    public string product_retailer_id { get; set; }
    public string quantity { get; set; }
    public string item_price { get; set; }
    public string currency { get; set; }
}

public struct LocationDetails
{
    public decimal lat { get; set; }
    [JsonPropertyName("long")]
    public decimal longitude { get; set; }
    public string name { get; set; }
    public string address { get; set; }
    
}

public struct ReplyDetails
{
    public string id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
}


public struct SmsDetails
{
    public string total_count { get; set; }
    public string num_messages { get; set; }
    public string keyword { get; set; }
}

public struct UsageDetails
{
    public string currency { get; set; }
    public string price { get; set; }
}

public struct ImageDetails
{
    public string url { get; set; }
}

public struct VcardDetails
{
    public string url { get; set; }
}

public struct AudioDetails
{
    public string url { get; set; }
}

public struct VideoDetails
{
    public string url { get; set; }
}

public struct FileDetails
{
    public string url { get; set; }
}

public struct ProfileDetails
{
    public string name { get; set; }
}

public struct ContextDetails
{
    public string message_uuid { get; set; }
    public string message_from { get; set; }
    public WhatsAppPreferredProduct whatsapp_referred_product { get; set; }
}

public struct WhatsAppPreferredProduct
{
    public string catalog_id { get; set; }
    public string product_retailer_id { get; set; }
}