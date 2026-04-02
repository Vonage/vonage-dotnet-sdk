using Newtonsoft.Json;

namespace Vonage.Numbers;

/// <summary>
///     Represents a virtual phone number that is owned by your Vonage account,
///     including its webhook configuration for messages and voice.
/// </summary>
public class OwnedNumber : Number
{
    /// <inheritdoc cref="Number.MoHttpUrl"/>
    [JsonProperty("moHttpUrl")]
    public new string MoHttpUrl { get; set; }

    /// <inheritdoc cref="Number.MessagesCallbackType"/>
    [JsonProperty("messagesCallbackType")]
    public new string MessagesCallbackType { get; set; }

    /// <inheritdoc cref="Number.MessagesCallbackValue"/>
    [JsonProperty("messagesCallbackValue")]
    public new string MessagesCallbackValue { get; set; }

    /// <inheritdoc cref="Number.VoiceCallbackType"/>
    [JsonProperty("voiceCallbackType")]
    public new string VoiceCallbackType { get; set; }

    /// <inheritdoc cref="Number.VoiceCallbackValue"/>
    [JsonProperty("voiceCallbackValue")]
    public new string VoiceCallbackValue { get; set; }
}