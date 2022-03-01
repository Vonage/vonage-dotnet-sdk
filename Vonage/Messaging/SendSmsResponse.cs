using Newtonsoft.Json;
namespace Vonage.Messaging
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