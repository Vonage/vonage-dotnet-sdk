using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vonage.Messages.WhatsApp
{
    public class MessageTemplate
    {
        /// <summary>
        /// The name of the template. For WhatsApp use your WhatsApp namespace (available via Facebook Business Manager), followed by a colon : and the name of the template to use.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The parameters are an array of strings. The first value being {{1}} in the template.
        /// </summary>
        [JsonProperty("parameters")]
        public List<string> Parameters { get; set; }
    }
}