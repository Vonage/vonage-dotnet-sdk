using Newtonsoft.Json;
using System;

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
    /// The name of the voice used to deliver text. You use the voice_name
    /// that has the correct language, gender and accent for the message
    /// you are sending. For example, the default voice kimberley is a
    /// female who speaks English with an American accent (en-US).
    /// Possible values for voice_name are listed at https://docs.nexmo.com/voice/voice-api/api-reference#talk_put
    /// </summary>
    [JsonProperty("voice_name")]
    [Obsolete("This parameter has been made obsolete by the language and style fields. Please see https://developer.nexmo.com/voice/voice-api/guides/text-to-speech#locale for more details")]
    public string VoiceName { get; set; }

    /// <summary>
    /// Set to 0 to replay the audio file at stream_url when the stream ends. The default value is 1.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Ignore, PropertyName ="loop")]
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
}