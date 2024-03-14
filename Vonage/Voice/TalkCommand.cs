using Newtonsoft.Json;

namespace Vonage.Voice;

public class TalkCommand
{
    /// <summary>
    /// A UTF-8 and URL encoded string of up to 1500 characters containing
    /// the message to be synthesized in the Call or Conversation. Each
    /// comma in text adds a short pause to the synthesized speech.
    /// </summary>
    [JsonProperty("text")]
    public string Text { get; set; }

    /// <summary>
    /// Set to 0 to replay the audio file at stream_url when the stream ends. The default value is 1.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Ignore,
        PropertyName = "loop")]
    public int? Loop { get; set; }

    /// <summary>
    /// The volume level that the speech is played. This can be any value between -1 to 1 in 0.1 increments, with 0 being the default.
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; }

    /// <summary>
    /// The language (<see href="https://tools.ietf.org/html/bcp47">BCP-47</see>format) for the message you are sending. Default: en-US. Possible values are listed in the <see href="https://developer.nexmo.com/voice/voice-api/guides/text-to-speech#supported-languages">Text-To-Speech guide</see>.
    /// </summary>
    [JsonProperty("language")]
    public string Language { get; set; }

    /// <summary>
    /// The vocal style (vocal range, tessitura and timbre). Default: 0. Possible values are listed in the <see href="https://developer.nexmo.com/voice/voice-api/guides/text-to-speech#supported-languages">Text-To-Speech guide</see>.
    /// </summary>
    [JsonProperty("style")]
    public int? Style { get; set; }

    /// <summary>
    /// Set to `true` to use the premium version of the specified style if available, otherwise the standard version will be used. You can find more information about Premium Voices in the [Text-To-Speech guide](https://developer.vonage.com/en/voice/voice-api/guides/text-to-speech#premium-voices).
    /// </summary>
    public bool Premium { get; set; }
}