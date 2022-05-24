using System;
using Newtonsoft.Json;

namespace Vonage.Messages
{
    public class MessagesResponse
    {
        [JsonProperty("message_uuid")]
        public Guid MessageUuid { get; private set; }
    }
}