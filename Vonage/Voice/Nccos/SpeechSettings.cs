#region
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
#endregion

namespace Vonage.Voice.Nccos;

public class SpeechSettings
{
    /// <summary>
    ///     The unique ID of the Call leg for the user to capture the speech of.
    /// </summary>
    [JsonProperty("uuid", Order = 0)]
    public string[] Uuid { get; set; }

    /// <summary>
    ///     Controls how long the system will wait after user stops speaking to decide the input is completed. The default
    ///     value is 2 (seconds). The range of possible values is between 1 second and 10 seconds.
    /// </summary>
    [JsonProperty("endOnSilence", Order = 1)]
    public int? EndOnSilence { get; set; }

    /// <summary>
    ///     Expected language of the user's speech. Format: BCP-47. Default: en-US see list of supported languages:
    ///     https://developer.nexmo.com/voice/voice-api/guides/asr#language
    /// </summary>
    [JsonProperty("language", Order = 2)]
    public string Language { get; set; }

    /// <summary>
    ///     Array of hints (strings) to improve recognition quality if certain words are expected from the user.
    /// </summary>
    [JsonProperty("context", Order = 3)]
    public string[] Context { get; set; }

    /// <summary>
    ///     Controls how long the system will wait for the user to start speaking. The range of possible values is between 1
    ///     second and 10 seconds.
    /// </summary>
    [JsonProperty("startTimeout", Order = 4)]
    public int? StartTimeout { get; set; }

    /// <summary>
    ///     Controls maximum speech duration (from the moment user starts speaking). The default value is 60 (seconds). The
    ///     range of possible values is between 1 and 60 seconds.
    /// </summary>
    [JsonProperty("maxDuration", Order = 5)]
    public int? MaxDuration { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty("provider", Order = 6)]
    [JsonConverter(typeof(StringEnumConverter))]
    public SpeechProvider? Provider { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty("providerOptions", Order = 7)]
    public SpeechProviderOptions? ProviderOptions { get; set; }
}

/// <summary>
/// </summary>
public enum SpeechProvider
{
    /// <summary>
    /// </summary>
    [EnumMember(Value = "deepgram")] Deepgram,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "google")] Google,
}

/// <summary>
/// </summary>
public struct SpeechProviderOptions
{
    /// <summary>
    /// </summary>
    [JsonProperty("model", Order = 0)]
    public string Model { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty("dictation", Order = 1)]
    public bool Dictation { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty("filler_words", Order = 2)]
    public bool FillerWords { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty("interim_results", Order = 3)]
    public bool InterimResults { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty("keywords", Order = 4)]
    public string[] Keywords { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty("language", Order = 5)]
    public string Language { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty("numerals", Order = 6)]
    public bool Numerals { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty("profanity_filter", Order = 7)]
    public bool ProfanityFilter { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty("punctuate", Order = 8)]
    public bool Punctuate { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty("redact", Order = 9)]
    public bool Redact { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty("smart_format", Order = 10)]
    public bool SmartFormat { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty("endpointing", Order = 11)]
    public int Endpointing { get; set; }

    /// <summary>
    /// </summary>
    [JsonProperty("utterance_end", Order = 12)]
    public int UtteranceEnd { get; set; }
}