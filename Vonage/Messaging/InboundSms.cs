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
/// </summary>
public class InboundSms : ISignable
{
    [JsonProperty("api-key")]
    [JsonPropertyName("api-key")]
    public string ApiKey { get; set; }

    [JsonProperty("concat")]
    [JsonPropertyName("concat")]
    [Newtonsoft.Json.JsonConverter(typeof(StringBoolConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(StringToBoolConverter))]
    public bool Concat { get; set; }

    [JsonProperty("concat-part")]
    [JsonPropertyName("concat-part")]
    public string ConcatPart { get; set; }

    [JsonProperty("concat-ref")]
    [JsonPropertyName("concat-ref")]
    public string ConcatRef { get; set; }

    [JsonProperty("concat-total")]
    [JsonPropertyName("concat-total")]
    public string ConcatTotal { get; set; }

    [JsonProperty("data")]
    [JsonPropertyName("data")]
    public string Data { get; set; }

    [JsonProperty("keyword")]
    [JsonPropertyName("keyword")]
    public string Keyword { get; set; }

    [JsonProperty("messageId")]
    [JsonPropertyName("messageId")]
    public string MessageId { get; set; }

    [JsonProperty("message-timestamp")]
    [JsonPropertyName("message-timestamp")]
    public string MessageTimestamp { get; set; }

    [JsonProperty("msisdn")]
    [JsonPropertyName("msisdn")]
    public string Msisdn { get; set; }

    [JsonProperty("nonce")]
    [JsonPropertyName("nonce")]
    public string Nonce { get; set; }

    [JsonProperty("text")]
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonProperty("timestamp")]
    [JsonPropertyName("timestamp")]
    public string Timestamp { get; set; }

    [JsonProperty("to")]
    [JsonPropertyName("to")]
    public string To { get; set; }

    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public string Type { get; set; }

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