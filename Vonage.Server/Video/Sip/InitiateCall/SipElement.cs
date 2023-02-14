using System.Text.Json.Serialization;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.Server.Video.Sip.InitiateCall;

/// <summary>
///     Represents a SIP element.
/// </summary>
public readonly struct SipElement
{
    [JsonPropertyOrder(0)]
    [JsonPropertyName("auth")]
    [JsonConverter(typeof(MaybeJsonConverter<SipAuthentication>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<SipAuthentication> Authentication { get; internal init; }

    [JsonPropertyOrder(1)]
    [JsonConverter(typeof(MaybeJsonConverter<string>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<string> From { get; internal init; }

    [JsonPropertyOrder(4)]
    [JsonPropertyName("secure")]
    public bool HasEncryptedMedia { get; internal init; }

    [JsonPropertyOrder(3)]
    [JsonPropertyName("observeForceMute")]
    public bool HasForceMute { get; internal init; }

    [JsonPropertyOrder(5)]
    [JsonPropertyName("video")]
    public bool HasVideo { get; internal init; }

    [JsonPropertyOrder(2)]
    [JsonConverter(typeof(MaybeJsonConverter<SipHeader>))]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Maybe<SipHeader> Headers { get; internal init; }

    [JsonPropertyOrder(6)] public string Uri { get; internal init; }

    public readonly struct SipAuthentication
    {
        public string Password { get; }
        public string Username { get; }

        public SipAuthentication(string password, string username)
        {
            this.Password = password;
            this.Username = username;
        }
    }

    public readonly struct SipHeader
    {
        [JsonPropertyName("headerKey")]
        [JsonConverter(typeof(MaybeJsonConverter<string>))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Maybe<string> CustomHeaderKey { get; }

        public SipHeader(Maybe<string> customHeaderKey) => this.CustomHeaderKey = customHeaderKey;
    }
}