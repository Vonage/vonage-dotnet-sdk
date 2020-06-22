using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Voice
{
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
    }
}
