using Newtonsoft.Json;

namespace Vonage.Messages.WhatsApp
{
    public class MessageWhatsApp
    {
        /// <summary>
        /// Policy for resolving what language template to use. Please note that WhatsApp deprecated the fallback policy in January of 2020,
        /// all requests carrying a fallback policy will be rejected with a 400 error. As of right now the only valid choice is deterministic
        /// </summary>
        [JsonProperty("policy")]
        public string Policy { get; set; }

        /// <summary>
        /// The BCP 47 language of the template. Vonage will translate the BCP 47 format to the WhatsApp equivalent. For examples en-GB will be auto-translate to en_GB.
        /// </summary>
        [JsonProperty("locale")]
        public string Locale { get; set; }
    }
}