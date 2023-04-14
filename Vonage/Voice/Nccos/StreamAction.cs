using Newtonsoft.Json;
using Vonage.Serialization;

namespace Vonage.Voice.Nccos;

public class StreamAction : NccoAction
{
    public override ActionType Action => ActionType.Stream;

    /// <summary>
    /// An array containing a single URL to an mp3 or wav (16-bit) audio file 
    /// to stream to the Call or Conversation.
    /// </summary>
    [JsonProperty("streamUrl")]
    public string[] StreamUrl { get; set; }

    /// <summary>
    /// Set the audio level of the stream in the range -1 &gt;=level&lt;=1 with a precision of 0.1. The default value is 0.
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; }

    /// <summary>
    /// Set to true so this action is terminated when the user presses a button on the keypad. 
    /// Use this feature to enable users to choose an option without having to listen to the whole message 
    /// in your Interactive Voice Response (IVR ). If you set bargeIn to true on one more Stream actions then 
    /// the next non-stream action in the NCCO stack must be an input action. The default value is false.
    /// Once bargeIn is set to true it will stay true (even if bargeIn: false is set in a following action) 
    /// until an input action is encountered
    /// </summary>
    [JsonProperty("bargeIn")]
    [JsonConverter(typeof(StringBoolConverter))]
    public bool BargeIn { get; set; }

    /// <summary>
    /// The number of times audio is repeated before the Call is closed. The default value is 1. Set to 0 to loop infinitely.
    /// </summary>
    [JsonProperty("loop")]
    public string Loop { get; set; }
}