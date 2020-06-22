using Nexmo.Api.Common;
using Newtonsoft.Json;
namespace Nexmo.Api.Messaging
{
    public class SendSmsResponse
    {
        /// <summary>
        /// The amount of messages in the request
        /// </summary>
        [JsonProperty("message-count")]
        public string MessageCount { get; set; }

        public SmsResponseMessage[] Messages { get; set; }
    }
}