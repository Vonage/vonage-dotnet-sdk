using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vonage.Voice;

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