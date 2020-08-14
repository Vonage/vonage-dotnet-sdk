using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Nexmo.Api.Redaction
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RedactionType
    {
        [EnumMember(Value = "inbound")]
        Inbound,
        [EnumMember(Value = "outbound")]
        Outbound
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum RedactionProduct
    {
        [EnumMember(Value = "sms")]
        Sms,
        [EnumMember(Value = "voice")]
        Voice,
        [EnumMember(Value = "number-insight")]
        NumberInsight,
        [EnumMember(Value = "verify")]
        Verify,
        [EnumMember(Value = "verify-sdk")]
        VerifySdk,
        [EnumMember(Value = "messages")]
        Messages

    }
    [System.Obsolete("The Nexmo.Api.Redaction.RedactRequest class is obsolete. " +
        "References to it should be switched to the new Vonage.Redaction.RedactRequest class.")]
    public class RedactRequest
    {
        /// <summary>
        /// The transaction ID to redact
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Product name that the ID provided relates to
        /// </summary>
        [JsonProperty("product")]
        public RedactionProduct? Product { get; set; }

        /// <summary>
        /// Required if redacting SMS data
        /// </summary>
        [JsonProperty("type")]
        public RedactionType? Type { get; set; }
    }
}