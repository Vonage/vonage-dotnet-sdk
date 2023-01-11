using System;
using Newtonsoft.Json;

namespace Vonage.Messages.WhatsApp;

public class WhatsAppDeliveryReceipt
{
    [JsonProperty("channel")] public MessagesChannel Channel { get; set; }

    [JsonProperty("client_ref")] public string ClientRef { get; set; }

    [JsonProperty("error")] public Error Error { get; set; }

    [JsonProperty("from")] public string From { get; set; }

    [JsonProperty("message_uuid")] public string MessageUuid { get; set; }

    [JsonProperty("status")] public MessagesDlrStatus Status { get; set; }

    [JsonProperty("timestamp")] public DateTime Timestamp { get; set; }

    [JsonProperty("to")] public string To { get; set; }

    [JsonProperty("usage")] public Usage Usage { get; set; }

    [JsonProperty("whatsapp")] public WhatsApp WhatsApp { get; set; }
}

public enum MessagesDlrStatus
{
    Submitted,
    Delivered,
    Rejected,
    Undeliverable,
    Read,
}

public class Error
{
    [JsonProperty("detail")] public string Detail { get; set; }

    [JsonProperty("instance")] public string Instance { get; set; }

    [JsonProperty("title")] public string Title { get; set; }

    [JsonProperty("type")] public string Type { get; set; }
}

public class Conversation
{
    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("origin")] public Origin Origin { get; set; }
}

public class Origin
{
    [JsonProperty("type")] public string Type { get; set; }
}

public class Usage
{
    [JsonProperty("currency")] public string Currency { get; set; }

    [JsonProperty("price")] public string Price { get; set; }
}

public class WhatsApp
{
    [JsonProperty("conversation")] public Conversation Conversation { get; set; }
}