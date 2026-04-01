#region
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Vonage.Cryptography;
using JsonConverter = Newtonsoft.Json.JsonConverter;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
#endregion

namespace Vonage.Messaging;

/// <summary>
///     Represents an inbound SMS message received via webhook from the Vonage SMS API.
///     Configure your webhook URL in the Vonage Dashboard to receive these messages.
/// </summary>
public class InboundSms : ISignable
{
    /// <summary>
    ///     The Vonage API key that received this message.
    /// </summary>
    [JsonProperty("api-key")]
    [JsonPropertyName("api-key")]
    public string ApiKey { get; set; }

    /// <summary>
    ///     Indicates whether this message is part of a concatenated (multi-part) SMS.
    /// </summary>
    [JsonProperty("concat")]
    [JsonPropertyName("concat")]
    [Newtonsoft.Json.JsonConverter(typeof(StringBoolConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(StringToBoolConverter))]
    public bool Concat { get; set; }

    /// <summary>
    ///     The part number of this message within a concatenated SMS (1-indexed).
    /// </summary>
    [JsonProperty("concat-part")]
    [JsonPropertyName("concat-part")]
    public string ConcatPart { get; set; }

    /// <summary>
    ///     The reference identifier linking all parts of a concatenated SMS.
    /// </summary>
    [JsonProperty("concat-ref")]
    [JsonPropertyName("concat-ref")]
    public string ConcatRef { get; set; }

    /// <summary>
    ///     The total number of parts in a concatenated SMS.
    /// </summary>
    [JsonProperty("concat-total")]
    [JsonPropertyName("concat-total")]
    public string ConcatTotal { get; set; }

    /// <summary>
    ///     The content of the message for binary messages (hex-encoded).
    /// </summary>
    [JsonProperty("data")]
    [JsonPropertyName("data")]
    public string Data { get; set; }

    /// <summary>
    ///     The first word of the message text, typically used for keyword-based routing.
    /// </summary>
    [JsonProperty("keyword")]
    [JsonPropertyName("keyword")]
    public string Keyword { get; set; }

    /// <summary>
    ///     The unique identifier for this message.
    /// </summary>
    [JsonProperty("messageId")]
    [JsonPropertyName("messageId")]
    public string MessageId { get; set; }

    /// <summary>
    ///     The timestamp when the message was received, in format: <c>yyyy-MM-dd HH:mm:ss</c>.
    /// </summary>
    [JsonProperty("message-timestamp")]
    [JsonPropertyName("message-timestamp")]
    public string MessageTimestamp { get; set; }

    /// <summary>
    ///     The phone number of the sender in E.164 format.
    /// </summary>
    [JsonProperty("msisdn")]
    [JsonPropertyName("msisdn")]
    public string Msisdn { get; set; }

    /// <summary>
    ///     A random string used for signature validation. Only present if signatures are enabled.
    /// </summary>
    [JsonProperty("nonce")]
    [JsonPropertyName("nonce")]
    public string Nonce { get; set; }

    /// <summary>
    ///     The text content of the message.
    /// </summary>
    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string Text { get; set; }

    /// <summary>
    ///     A Unix timestamp used for signature validation. Only present if signatures are enabled.
    /// </summary>
    [JsonProperty("timestamp")]
    [JsonPropertyName("timestamp")]
    public string Timestamp { get; set; }

    /// <summary>
    ///     The Vonage virtual number that received the message.
    /// </summary>
    [JsonProperty("to")]
    [JsonPropertyName("to")]
    public string To { get; set; }

    /// <summary>
    ///     The message type: <c>text</c>, <c>unicode</c>, or <c>binary</c>.
    /// </summary>
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    ///     The User Data Header for binary messages (hex-encoded).
    /// </summary>
    [JsonProperty("udh")]
    [JsonPropertyName("udh")]
    public string Udh { get; set; }

    /// <summary>
    ///     Validate the webhook signature against a secret.
    /// </summary>
    /// <param name="signatureSecret">The secret.</param>
    /// <param name="method">The encryption method.</param>
    /// <returns>Whether the signature has been validated or not.</returns>
    public bool ValidateSignature(string signatureSecret, SmsSignatureGenerator.Method method) =>
        SignatureValidation.ValidateSignature(this, signatureSecret, method);

    [JsonProperty("sig")]
    [JsonPropertyName("sig")]
    public string Sig { get; set; }
}

internal class StringBoolConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(bool);

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
        JsonSerializer serializer) =>
        bool.TryParse(reader.Value?.ToString(), out var boolValue) && boolValue;

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (bool.TryParse(value?.ToString(), out var boolValue))
        {
            writer.WriteValue(boolValue.ToString().ToLowerInvariant());
        }
        else
        {
            writer.WriteNull();
        }
    }
}

internal class StringToBoolConverter : System.Text.Json.Serialization.JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        bool.TryParse(reader.GetString(), out var boolValue) && boolValue;

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.ToString().ToLowerInvariant());
}