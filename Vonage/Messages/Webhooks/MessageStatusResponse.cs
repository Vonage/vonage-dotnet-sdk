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
    ///     The UUID of the message.
    /// </summary>
    [JsonPropertyName("message_uuid")]
    [JsonProperty("message_uuid")]
    public string MessageId { get; set; }

    /// <summary>
    ///     The sender of the message.
    /// </summary>
    [JsonPropertyName("from")]
    [JsonProperty("from")]
    public string From { get; set; }

    /// <summary>
    ///     The recipient of the message.
    /// </summary>
    [JsonPropertyName("to")]
    [JsonProperty("to")]
    public string To { get; set; }

    /// <summary>
    ///     The datetime of when the status event occurred, in ISO 8601 format.
    /// </summary>
    [JsonPropertyName("timestamp")]
    [JsonProperty("timestamp")]
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    ///     The status of the message (e.g., submitted, delivered, read, rejected, undeliverable).
    /// </summary>
    [JsonPropertyName("status")]
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    ///     Client reference that was provided when sending the message.
    /// </summary>
    [JsonPropertyName("client_ref")]
    [JsonProperty("client_ref")]
    public string ClientReference { get; set; }

    /// <summary>
    ///     The channel through which the message was sent.
    /// </summary>
    [JsonPropertyName("channel")]
    [JsonProperty("channel")]
    public string Channel { get; set; }

    /// <summary>
    ///     Error details if the message failed.
    /// </summary>
    [JsonPropertyName("error")]
    [JsonProperty("error")]
    public StatusError Error { get; set; }

    /// <summary>
    ///     Workflow information if the message was part of a failover workflow.
    /// </summary>
    [JsonPropertyName("workflow")]
    [JsonProperty("workflow")]
    public StatusWorkflow Workflow { get; set; }

    /// <summary>
    ///     Billing and usage information for the message.
    /// </summary>
    [JsonPropertyName("usage")]
    [JsonProperty("usage")]
    public StatusUsage Usage { get; set; }

    /// <summary>
    ///     Destination network information.
    /// </summary>
    [JsonPropertyName("destination")]
    [JsonProperty("destination")]
    public StatusDestination Destination { get; set; }

    /// <summary>
    ///     SMS-specific status details.
    /// </summary>
    [JsonPropertyName("sms")]
    [JsonProperty("sms")]
    public StatusSms Sms { get; set; }

    /// <summary>
    ///     WhatsApp-specific status details including pricing and conversation information.
    /// </summary>
    [JsonPropertyName("whatsapp")]
    [JsonProperty("whatsapp")]
    public StatusWhatsApp WhatsApp { get; set; }
}