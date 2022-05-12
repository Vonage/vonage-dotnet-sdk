using Newtonsoft.Json;

namespace Vonage.Messages
{
    public class Attachment
    {
        /// <summary>
        /// The URL of the attachment.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}