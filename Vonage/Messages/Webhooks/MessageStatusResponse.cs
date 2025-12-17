#region
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Messages.Webhooks;

/// <summary>
///     Represents a webhook status response for Messages.
/// </summary>
public class MessageStatusResponse
{
    /// <summary>
    /// </summary>
    [JsonPropertyName("message_uuid")]
    [JsonProperty("message_uuid")]
    public string MessageId { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("from")]
    [JsonProperty("from")]
    public string From { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("to")]
    [JsonProperty("to")]
    public string To { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("timestamp")]
    [JsonProperty("timestamp")]
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("status")]
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("client_ref")]
    [JsonProperty("client_ref")]
    public string ClientReference { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("channel")]
    [JsonProperty("channel")]
    public string Channel { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("error")]
    [JsonProperty("error")]
    public StatusError Error { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("workflow")]
    [JsonProperty("workflow")]
    public StatusWorkflow Workflow { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("usage")]
    [JsonProperty("usage")]
    public StatusUsage Usage { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("destination")]
    [JsonProperty("destination")]
    public StatusDestination Destination { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("sms")]
    [JsonProperty("sms")]
    public StatusSms Sms { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("whatsapp")]
    [JsonProperty("whatsapp")]
    public StatusWhatsApp WhatsApp { get; set; }
}