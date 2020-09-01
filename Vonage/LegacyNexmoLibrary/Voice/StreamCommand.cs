using Newtonsoft.Json;

namespace Nexmo.Api.Voice
{
    [System.Obsolete("The Nexmo.Api.Voice.StreamCommand class is obsolete. " +
           "References to it should be switched to the new Nexmo.Api.Voice.StreamCommand class.")]
    public class StreamCommand
    {
        /// <summary>
        /// An array of URLs pointing to the .mp3 or .wav audio file to stream.
        /// </summary>
        [JsonProperty("stream_url")]
        public string[] StreamUrl { get; set; }
        
        /// <summary>
        /// Set to 0 to replay the audio file at stream_url when the stream ends.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Ignore, PropertyName ="loop")]
        public int? Loop { get; set; }

        /// <summary>
        /// Set the audio level of the stream in the range -1 &gt;= level &lt;= 1 with a precision of 0.1. The default value is 0.
        /// </summary>
        public string Level { get; set; }
    }
}
