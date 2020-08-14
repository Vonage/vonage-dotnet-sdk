using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nexmo.Api.Voice.Nccos
{
    [System.Obsolete("The Nexmo.Api.Voice.Nccos.RecordAction class is obsolete. " +
           "References to it should be switched to the new Nexmo.Api.Voice.Nccos.RecordAction class.")]
    public class RecordAction : NccoAction
    {
        public enum AudioFormat
        {
            mp3=1,
            wav=2,
            ogg=3
        }

        /// <summary>
        /// Record the Call in a specific format.
        /// The default value is mp3, or wav when recording more than 2 channels.
        /// </summary>
        [JsonProperty("format")]
        [JsonConverter(typeof(StringEnumConverter))]
        public AudioFormat Format { get; set; }

        /// <summary>
        /// Record the sent and received audio in separate channels of a stereo 
        /// recording—set to conversation to enable this.
        /// </summary>
        [JsonProperty("split")]
        public string Split { get; set; }

        /// <summary>
        /// The number of channels to record (maximum 32). 
        /// If the number of participants exceeds channels any additional participants 
        /// will be added to the last channel in file. split conversation must also be enabled
        /// </summary>
        [JsonProperty("channels")]
        public uint Channels { get; set; }

        /// <summary>
        /// Stop recording after n seconds of silence. 
        /// Once the recording is stopped the recording data is sent to event_url. 
        /// The range of possible values is 3&lt;=endOnSilence&lt;=10.
        /// </summary>
        [JsonProperty("endOnSilence")]
        public string EndOnSilence { get; set; }

        /// <summary>
        /// Stop recording when a digit is pressed on the handset. Possible values are: *, # or any single digit e.g. 9
        /// </summary>
        [JsonProperty("endOnKey")]
        public string EndOnKey { get; set; }

        /// <summary>
        /// The maximum length of a recording in seconds. One the recording is stopped the recording 
        /// data is sent to event_url. The range of possible values 
        /// is between 3 seconds and 7200 seconds (2 hours)
        /// </summary>
        [JsonProperty("timeOut")]
        public string TimeOut { get; set; }

        /// <summary>
        /// Set to true to play a beep when a recording starts
        /// </summary>
        [JsonProperty("beepStart")]
        public string BeepStart { get; set; }

        /// <summary>
        /// The URL to the webhook endpoint that is called asynchronously when a recording is finished. 
        /// If the message recording is hosted by Nexmo, this webhook contains the URL you need to download 
        /// the recording and other meta data.
        /// </summary>
        [JsonProperty("eventUrl")]
        public string[] EventUrl { get; set; }

        /// <summary>
        /// The HTTP method used to make the request to eventUrl. The default value is POST.
        /// </summary>
        [JsonProperty("eventMethod")]
        public string EventMethod { get; set; }

        public RecordAction()
        {
            Action = ActionType.record;

        }
    }
}
