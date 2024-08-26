#region
using System;
using Newtonsoft.Json;
using Vonage.Cryptography;
#endregion

namespace Vonage.Messaging;

public class InboundSms : ISignable
{
    [JsonProperty("api-key")] public string ApiKey { get; set; }

    [JsonProperty("concat")]
    [JsonConverter(typeof(StringBoolConverter))]
    public bool Concat { get; set; }

    [JsonProperty("concat-part")] public string ConcatPart { get; set; }

    [JsonProperty("concat-ref")] public string ConcatRef { get; set; }

    [JsonProperty("concat-total")] public string ConcatTotal { get; set; }

    [JsonProperty("data")] public string Data { get; set; }

    [JsonProperty("keyword")] public string Keyword { get; set; }

    [JsonProperty("messageId")] public string MessageId { get; set; }

    [JsonProperty("message-timestamp")] public string MessageTimestamp { get; set; }

    [JsonProperty("msisdn")] public string Msisdn { get; set; }

    [JsonProperty("nonce")] public string Nonce { get; set; }

    [JsonProperty("text")] public string Text { get; set; }

    [JsonProperty("timestamp")] public string Timestamp { get; set; }

    [JsonProperty("to")] public string To { get; set; }

    [JsonProperty("type")] public string Type { get; set; }

    [JsonProperty("udh")] public string Udh { get; set; }

    [JsonProperty("sig")] public string Sig { get; set; }

    /// <summary>
    ///     Validate the webhook signature against a secret.
    /// </summary>
    /// <param name="signatureSecret">The secret.</param>
    /// <param name="method">The encryption method.</param>
    /// <returns>Whether the signature has been validated or not.</returns>
    public bool ValidateSignature(string signatureSecret, SmsSignatureGenerator.Method method) =>
        SignatureValidation.ValidateSignature(this, signatureSecret, method);
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