#region
using Newtonsoft.Json;
#endregion

namespace Vonage.Voice;

/// <summary>
///     Represents a command to stream an audio file into an active call.
/// </summary>
public class StreamCommand
{
    /// <summary>
    ///     An array containing the URL of the audio file to stream. Must be a single-channel 16-bit WAV at 8kHz or 16kHz, or an MP3 file.
    /// </summary>
    [JsonProperty("stream_url")]
    public string[] StreamUrl { get; set; }

    /// <summary>
    ///     The number of times to replay the audio file. Set to 0 for infinite looping. Default is 1.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Ignore,
        PropertyName = "loop")]
    public int? Loop { get; set; }

    /// <summary>
    ///     The audio volume level, from -1 to 1 in 0.1 increments. Default is 0.
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; }
}