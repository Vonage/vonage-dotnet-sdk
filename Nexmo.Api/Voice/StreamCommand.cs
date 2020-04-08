using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Voice
{
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
    }
}
