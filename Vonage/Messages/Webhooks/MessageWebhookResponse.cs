#region
using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
#endregion

namespace Vonage.Messages.Webhooks;

/// <summary>
///     Represents a webhook response for Messages.
/// </summary>
public struct MessageWebhookResponse
{
    /// <summary>
    ///     Channel specific metadata for Audio.
    /// </summary>
    [JsonPropertyName("audio")]
    [JsonProperty("audio")]
    public UrlDetails? Audio { get; set; }

    /// <summary>
    ///     The channel the message came in on.
    /// </summary>
    [JsonPropertyName("channel")]
    [JsonProperty("channel")]
    public string Channel { get; set; }

    /// <summary>
    ///     Client reference of up to 100 characters. The reference will be present in every message status.
    /// </summary>
    [JsonPropertyName("client_ref")]
    [JsonProperty("client_ref")]
    public string ClientReference { get; set; }

    /// <summary>
    ///     This is only present for the Inbound Message where the user is quoting another message. It provides information
    ///     about the quoted message and/or the product message being responded to.
    /// </summary>
    [JsonPropertyName("context")]
    [JsonProperty("context")]
    public ContextDetails? Context { get; set; }

    /// <summary>
    ///     Channel specific metadata for File.
    /// </summary>
    [JsonPropertyName("file")]
    [JsonProperty("file")]
    public UrlDetails? File { get; set; }

    /// <summary>
    ///     The phone number of the message sender in the E.164 format. Don't use a leading + or 00 when entering a phone
    ///     number, start with the country code, for example, 447700900000. For SMS in certain localities alpha-numeric sender
    ///     id's will work as well, see
    ///     <see
    ///         href="https://developer.vonage.com/en/messaging/sms/guides/country-specific-features#country-specific-features">
    ///         Global
    ///         Messaging
    ///     </see>
    ///     for more details.
    /// </summary>
    [JsonPropertyName("from")]
    [JsonProperty("from")]
    public string From { get; set; }

    /// <summary>
    ///     Channel specific metadata for Image.
    /// </summary>
    [JsonPropertyName("image")]
    [JsonProperty("image")]
    public UrlDetails? Image { get; set; }

    /// <summary>
    ///     Channel specific metadata for Location.
    /// </summary>
    [JsonPropertyName("location")]
    [JsonProperty("location")]
    public LocationDetails? Location { get; set; }

    /// <summary>
    ///     The type of message to send. You must provide text in this field.
    /// </summary>
    [JsonPropertyName("message_type")]
    [JsonProperty("message_type")]
    public string MessageType { get; set; }

    /// <summary>
    ///     The UUID of the message.
    /// </summary>
    [JsonPropertyName("message_uuid")]
    [JsonProperty("message_uuid")]
    public Guid MessageUuid { get; set; }

    /// <summary>
    ///     Channel specific metadata for Order.
    /// </summary>
    [JsonPropertyName("order")]
    [JsonProperty("order")]
    public OrderDetails? Order { get; set; }

    /// <summary>
    ///     Represents the profile details.
    /// </summary>
    [JsonPropertyName("profile")]
    [JsonProperty("profile")]
    public ProfileDetails? Profile { get; set; }

    /// <summary>
    ///     A message from the channel provider, which may contain a description, error codes or other information.
    /// </summary>
    [JsonPropertyName("provider_message")]
    [JsonProperty("provider_message")]
    public string ProviderMessage { get; set; }

    /// <summary>
    ///     Channel specific metadata for Reply.
    /// </summary>
    [JsonPropertyName("reply")]
    [JsonProperty("reply")]
    public ReplyDetails? Reply { get; set; }

    /// <summary>
    ///     Channel specific metadata for SMS.
    /// </summary>
    [JsonPropertyName("sms")]
    [JsonProperty("sms")]
    public SmsDetails? Sms { get; set; }

    /// <summary>
    ///     Channel specific metadata for Sticker.
    /// </summary>
    [JsonPropertyName("sticker")]
    [JsonProperty("sticker")]
    public UrlDetails? Sticker { get; set; }

    /// <summary>
    ///     The UTF-8 encoded text of the inbound message.
    /// </summary>
    [JsonPropertyName("text")]
    [JsonProperty("text")]
    public string Text { get; set; }

    /// <summary>
    ///     The datetime of when the event occurred, in ISO 8601 format.
    /// </summary>
    [JsonPropertyName("timestamp")]
    [JsonProperty("timestamp")]
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    ///     The phone number of the message recipient in the E.164 format. Don't use a leading + or 00 when entering a phone
    ///     number, start with the country code, for example, 447700900000.
    /// </summary>
    [JsonPropertyName("to")]
    [JsonProperty("to")]
    public string To { get; set; }

    /// <summary>
    ///     Represents details about message usage.
    /// </summary>
    [JsonPropertyName("usage")]
    [JsonProperty("usage")]
    public UsageDetails? Usage { get; set; }

    /// <summary>
    ///     Channel specific metadata for Vcard.
    /// </summary>
    [JsonPropertyName("vcard")]
    [JsonProperty("vcard")]
    public UrlDetails? Vcard { get; set; }

    /// <summary>
    ///     Channel specific metadata for Video.
    /// </summary>
    [JsonPropertyName("video")]
    [JsonProperty("video")]
    public UrlDetails? Video { get; set; }

    /// <summary>
    /// </summary>
    [JsonPropertyName("origin")]
    [JsonProperty("origin")]
    public Origin Origin { get; set; }
}