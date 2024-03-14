using Newtonsoft.Json;

namespace Vonage.Voice.Nccos;

public class TalkAction : NccoAction
{
    [JsonProperty("action", Order = 0)] public override ActionType Action => ActionType.Talk;

    /// <summary>
    /// Set to true so this action is terminated when the user presses a button on the keypad. 
    /// Use this feature to enable users to choose an option without having to listen to the whole message 
    /// in your Interactive Voice Response (IVR ). If you set bargeIn to true on one more Stream actions then 
    /// the next non-stream action in the NCCO stack must be an input action. The default value is false.
    /// Once bargeIn is set to true it will stay true (even if bargeIn: false is set in a following action) 
    /// until an input action is encountered
    /// </summary>
    [JsonProperty("bargeIn", Order = 2)]
    public bool BargeIn { get; set; }

    /// <summary>
    /// The language (<see href="https://tools.ietf.org/html/bcp47">BCP-47</see>format) for the message you are sending. Default: en-US. Possible values are listed in the <see href="https://developer.nexmo.com/voice/voice-api/guides/text-to-speech#supported-languages">Text-To-Speech guide</see>.
    /// </summary>
    [JsonProperty("language", Order = 6)]
    public string Language { get; set; }

    /// <summary>
    /// The volume level that the speech is played. This can be any value between -1 to 1 with 0 being the default.
    /// </summary>
    [JsonProperty("level", Order = 4)]
    public string Level { get; set; }

    /// <summary>
    /// The number of times text is repeated before the Call is closed. The default value is 1. Set to 0 to loop infinitely.
    /// </summary>
    [JsonProperty("loop", Order = 3)]
    public string Loop { get; set; }

    /// <summary>
    ///     Set to true to use the premium version of the specified style if available, otherwise the standard version will be
    ///     used. The default value is false.
    /// </summary>
    [JsonProperty("premium", Order = 8)]
    public bool Premium { get; set; }

    /// <summary>
    /// The vocal style (vocal range, tessitura and timbre). Default: 0. Possible values are listed in the <see href="https://developer.nexmo.com/voice/voice-api/guides/text-to-speech#supported-languages">Text-To-Speech guide</see>.
    /// </summary>
    [JsonProperty("style", Order = 7)]
    public int? Style { get; set; }

    /// <summary>
    /// A string of up to 1,500 characters (excluding SSML tags) containing the message to be 
    /// synthesized in the Call or Conversation. A single comma in text adds a short pause to the 
    /// synthesized speech. To add a longer pause a break tag needs to be used in SSML. 
    /// To use SSML tags, you must enclose the text in a speak element.
    /// </summary>
    [JsonProperty("text", Order = 1)]
    public string Text { get; set; }
}