using Newtonsoft.Json;

namespace Vonage.Voice
{
    public class DtmfCommand
    {
        /// <summary>
        /// The array of digits to send to the call
        /// </summary>
        [JsonProperty("digits")]
        public string Digits { get; set; }
    }
}
