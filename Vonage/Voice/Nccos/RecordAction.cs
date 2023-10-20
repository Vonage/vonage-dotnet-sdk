﻿using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vonage.Voice.Nccos;

public class RecordAction : NccoAction
{
    [JsonProperty("action", Order = 0)] public override ActionType Action => ActionType.Record;

    /// <summary>
    /// Set to true to play a beep when a recording starts
    /// </summary>
    [JsonProperty("beepStart", Order = 8)]
    public bool BeepStart { get; set; }

    /// <summary>
    /// The number of channels to record (maximum 32). 
    /// If the number of participants exceeds channels any additional participants 
    /// will be added to the last channel in file. split conversation must also be enabled
    /// </summary>
    [JsonProperty("channels", Order = 3)]
    public uint? Channels { get; set; }

    /// <summary>
    /// Stop recording when a digit is pressed on the handset. Possible values are: *, # or any single digit e.g. 9
    /// </summary>
    [JsonProperty("endOnKey", Order = 5)]
    public string EndOnKey { get; set; }

    /// <summary>
    /// Stop recording after n seconds of silence. 
    /// Once the recording is stopped the recording data is sent to event_url. 
    /// The range of possible values is 3&lt;=endOnSilence&lt;=10.
    /// </summary>
    [JsonProperty("endOnSilence", Order = 4)]
    public string EndOnSilence { get; set; }

    /// <summary>
    /// The HTTP method used to make the request to eventUrl. The default value is POST.
    /// </summary>
    [JsonProperty("eventMethod", Order = 10)]
    public string EventMethod { get; set; }

    /// <summary>
    /// The URL to the webhook endpoint that is called asynchronously when a recording is finished. 
    /// If the message recording is hosted by Vonage, this webhook contains the URL you need to download 
    /// the recording and other meta data.
    /// </summary>
    [JsonProperty("eventUrl", Order = 9)]
    public string[] EventUrl { get; set; }

    /// <summary>
    /// Record the Call in a specific format.
    /// The default value is mp3, or wav when recording more than 2 channels.
    /// </summary>
    [JsonProperty("format", Order = 1)]
    public AudioFormat? Format { get; set; }

    /// <summary>
    /// Record the sent and received audio in separate channels of a stereo 
    /// recording—set to conversation to enable this.
    /// </summary>
    [JsonProperty("split", Order = 2)]
    public string Split { get; set; }

    /// <summary>
    /// The maximum length of a recording in seconds. One the recording is stopped the recording 
    /// data is sent to event_url. The range of possible values 
    /// is between 3 seconds and 7200 seconds (2 hours)
    /// </summary>
    [JsonProperty("timeOut", Order = 6)]
    public string TimeOut { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AudioFormat
    {
        [EnumMember(Value = "mp3")] Mp3 = 1,

        [EnumMember(Value = "wav")] Wav = 2,

        [EnumMember(Value = "ogg")] Ogg = 3,
    }
}