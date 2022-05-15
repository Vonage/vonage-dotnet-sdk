using System.ComponentModel;
using Newtonsoft.Json;

namespace Vonage.Messages.Viber
{
    public class ViberRequestData
    {
        /// <summary>
        /// The use of different category tags enables the business to send messages for
        /// different use cases. For Viber Service Messages the first message sent from a
        /// business to a user must be personal, informative & a targeted message - not promotional.
        /// By default Vonage sends the transaction category to Viber Service Messages.
        /// </summary>
        [JsonProperty("category")]
        [DefaultValue(99)]
        public ViberMessageCategory? Category { get; set; }
        
        /// <summary>
        /// Set the time-to-live of message to be delivered in seconds. i.e. if the
        /// message is not delivered in 600 seconds then delete the message.
        /// </summary>
        [JsonProperty("ttl")]
        public int? TTL { get; set; }
        
        /// <summary>
        /// Viber-specific type definition. To use "template", please contact your
        /// Vonage Account Manager to setup your templates.
        /// </summary>
        [JsonProperty("type")]
        public object Type { get; set; }
    }
}