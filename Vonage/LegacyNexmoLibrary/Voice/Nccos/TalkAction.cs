using Newtonsoft.Json;

namespace Nexmo.Api.Voice.Nccos
{
    [System.Obsolete("The Nexmo.Api.Voice.Nccos.TalkAction class is obsolete. " +
           "References to it should be switched to the new Nexmo.Api.Voice.Nccos.TalkAction class.")]
    public class TalkAction : NccoAction
    {
        /// <summary>
        /// A string of up to 1,500 characters (excluding SSML tags) containing the message to be 
        /// synthesized in the Call or Conversation. A single comma in text adds a short pause to the 
        /// synthesized speech. To add a longer pause a break tag needs to be used in SSML. 
        /// To use SSML tags, you must enclose the text in a speak element.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Set to true so this action is terminated when the user presses a button on the keypad. 
        /// Use this feature to enable users to choose an option without having to listen to the whole message 
        /// in your Interactive Voice Response (IVR ). If you set bargeIn to true on one more Stream actions then 
        /// the next non-stream action in the NCCO stack must be an input action. The default value is false.
        /// Once bargeIn is set to true it will stay true (even if bargeIn: false is set in a following action) 
        /// until an input action is encountered
        /// </summary>
        [JsonProperty("bargeIn")]
        public string BargeIn { get; set; }

        /// <summary>
        /// The number of times text is repeated before the Call is closed. The default value is 1. Set to 0 to loop infinitely.
        /// </summary>
        [JsonProperty("loop")]
        public string Loop { get; set; }

        /// <summary>
        /// The volume level that the speech is played. This can be any value between -1 to 1 with 0 being the default.
        /// </summary>
        [JsonProperty("level")]
        public string Level { get; set; }

        /// <summary>
        /// The name of the voice used to deliver text. You use the voiceName that has the correct language, 
        /// gender and accent for the message you are sending. 
        /// For example, the default voice kimberly is a female who speaks English with an 
        /// American accent (en-US). Possible values are listed in the Text-To-Speech guide.
        /// </summary>
        [JsonProperty("voiceName")]
        public string VoiceName { get; set; }

        public TalkAction()
        {
            Action = ActionType.talk;
        }
    }
}
