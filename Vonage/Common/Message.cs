#region
using Newtonsoft.Json;
#endregion

namespace Vonage.Common;

/// <summary>
///     Represents an SMS message received via webhook or retrieved from the API.
/// </summary>
/// <remarks>
///     This class is used for deserializing webhook payloads and API responses containing message data.
/// </remarks>
public class Message
{
    /// <summary>
    ///     Gets or sets the message type (e.g., "text", "unicode").
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }

    /// <summary>
    ///     Gets or sets the unique identifier for the message.
    /// </summary>
    [JsonProperty("message-id")]
    public string MessageId { get; set; }

    /// <summary>
    ///     Gets or sets the Vonage account ID that sent or received the message.
    /// </summary>
    [JsonProperty("account-id")]
    public string AccountId { get; set; }

    /// <summary>
    ///     Gets or sets the timestamp when the message was received, in ISO 8601 format.
    /// </summary>
    [JsonProperty("date-received")]
    public string DateReceived { get; set; }

    /// <summary>
    ///     Gets or sets the network code of the carrier that delivered the message.
    /// </summary>
    [JsonProperty("network")]
    public string Network { get; set; }

    /// <summary>
    ///     Gets or sets the sender's phone number or alphanumeric sender ID.
    /// </summary>
    [JsonProperty("from")]
    public string From { get; set; }

    /// <summary>
    ///     Gets or sets the recipient's phone number in E.164 format.
    /// </summary>
    [JsonProperty("to")]
    public string To { get; set; }

    /// <summary>
    ///     Gets or sets the message content.
    /// </summary>
    [JsonProperty("body")]
    public string Body { get; set; }

    /// <summary>
    ///     Gets or sets the cost of the message in EUR.
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    ///     Gets or sets the timestamp when message processing completed, in ISO 8601 format.
    /// </summary>
    [JsonProperty("date-closed")]
    public string DateClosed { get; set; }

    /// <summary>
    ///     Gets or sets the time taken to deliver the message, in milliseconds.
    /// </summary>
    [JsonProperty("latency")]
    public decimal Latency { get; set; }

    /// <summary>
    ///     Gets or sets the client reference that was set when the message was sent.
    /// </summary>
    [JsonProperty("client-ref")]
    public string ClientRef { get; set; }

    /// <summary>
    ///     Gets or sets the delivery status of the message (e.g., "delivered", "failed").
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }

    /// <summary>
    ///     Gets or sets the error code if the message failed. See Vonage documentation for error codes.
    /// </summary>
    [JsonProperty("error-code")]
    public string ErrorCode { get; set; }

    /// <summary>
    ///     Gets or sets the human-readable description of the error code.
    /// </summary>
    [JsonProperty("error-code-label")]
    public string ErrorCodeLabel { get; set; }

    /// <summary>
    ///     Gets or sets the final delivery status of the message.
    /// </summary>
    [JsonProperty("final-status")]
    public string FinalStatus { get; set; }
}