using Nexmo.Api.Common;
using Newtonsoft.Json;
namespace Nexmo.Api.Messaging
{
    [System.Obsolete("The Nexmo.Api.Messaging.SendSmsResponse class is obsolete. " +
        "References to it should be switched to the new Vonage.Messaging.SendSmsResponse class.")]
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