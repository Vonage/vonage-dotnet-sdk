#region
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice;

/// <summary>
///     Represents a command to play text-to-speech into an active call.
/// </summary>
public class TalkCommand
{
    /// <summary>
    ///     The text to synthesize and play into the call. Up to 1500 UTF-8 characters. Each comma adds a short pause to the synthesized speech.
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; }

    /// <summary>
    ///     The number of times to repeat the text. Set to 0 for infinite looping. Default is 1.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Ignore,
        PropertyName = "loop")]
    public int? Loop { get; set; }

    /// <summary>
    ///     The volume level for the speech, from -1 to 1 in 0.1 increments. Default is 0.
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; }

    /// <summary>
    ///     The language for the text-to-speech in BCP-47 format (e.g., "en-US", "fr-FR"). Default is "en-US".
    /// </summary>
    [JsonProperty("language")]
    public string Language { get; set; }

    /// <summary>
    ///     The vocal style index (vocal range, tessitura, and timbre). Default is 0. Available styles vary by language.
    /// </summary>
    [JsonProperty("style")]
    public int? Style { get; set; }

    /// <summary>
    ///     Set to <c>true</c> to use the premium version of the specified voice style if available; otherwise the standard version is used.
    /// </summary>
    [JsonProperty("premium")]
    public bool Premium { get; set; }
}