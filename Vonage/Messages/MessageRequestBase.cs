using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vonage.Messages
{
    public abstract class MessageRequestBase
    {
        /// <summary>
        /// The channel to send to.
        /// </summary>
        [JsonProperty("channel")]
        [DefaultValue(99)]
        [JsonConverter(typeof(StringEnumConverter))]
        public abstract MessagesChannel Channel { get; }

        /// <summary>
        /// The type of message to send.
        /// </summary>
        [JsonProperty("message_type")]
        [DefaultValue(99)]
        [JsonConverter(typeof(StringEnumConverter))]
        public abstract MessagesMessageType MessageType { get; }

        /// <summary>
        /// The phone number of the message recipient in the E.164 format. Don't use a leading + or 00 when entering a phone number,
        /// start with the country code, for example, 447700900000.
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <summary>
        /// The phone number of the message sender in the E.164 format. Don't use a leading + or 00 when entering a phone number,
        /// start with the country code, for example, 447700900000. For SMS in certain localities alpha-numeric sender id's will
        /// work as well.
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// Client reference of up to 40 characters. The reference will be present in every message status.
        /// </summary>
        [JsonProperty("client_ref")]
        public string ClientRef { get; set; }
    }
}