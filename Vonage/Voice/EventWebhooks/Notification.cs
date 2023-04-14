using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vonage.Voice.EventWebhooks;

public class Notification<T> : EventBase
{
    /// <summary>
    /// Custom payload of for the notification action
    /// </summary>
    [JsonProperty("payload")]
    public T Payload { get; set; }

    /// <summary>
    /// A unique identifier for this conversation
    /// </summary>
    [JsonProperty("conversation_uuid")]
    public string ConversationUuid { get; set; }
}