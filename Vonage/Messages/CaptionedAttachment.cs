using Newtonsoft.Json;

namespace Vonage.Messages
{
    public class CaptionedAttachment
    {
        /// <summary>
        /// The URL of the attachment.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Additional text to accompany the attachment.
        /// </summary>
        [JsonProperty("caption")]
        public string Caption { get; set; }
    }
}