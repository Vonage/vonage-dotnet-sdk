using Newtonsoft.Json;

namespace Nexmo.Api.Voice
{
    [System.Obsolete("The Nexmo.Api.Voice.DtmfCommand class is obsolete. " +
           "References to it should be switched to the new Nexmo.Api.Voice.DtmfCommand class.")]
    public class DtmfCommand
    {
        /// <summary>
        /// The array of digits to send to the call
        /// </summary>
        [JsonProperty("digits")]
        public string Digits { get; set; }
    }
}
