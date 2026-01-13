#region
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Vonage.Voice.EventWebhooks;
#endregion

namespace Vonage.Voice.AnswerWebhooks;

public class Answer : EventBase
{
    /// <summary>
    /// The number the call came from (this could be your Vonage number if the call is started programmatically)
    /// </summary>
    [JsonProperty("to")]
    [JsonPropertyName("to")]
    public string To { get; set; }

    /// <summary>
    /// The call the number is to (this could be a Vonage number or another phone number)
    /// </summary>
    [JsonProperty("from")]
    [JsonPropertyName("from")]
    public string From { get; set; }

    /// <summary>
    /// A unique identifier for this call
    /// </summary>
    [JsonProperty("uuid")]
    [JsonPropertyName("uuid")]
    public string Uuid { get; set; }

    /// <summary>
    /// A unique identifier for this conversation
    /// </summary>
    [JsonProperty("conversation_uuid")]
    [JsonPropertyName("conversation_uuid")]
    public string ConversationUuid { get; set; }

    /// <summary>
    ///     Captures all additional unmapped properties from the JSON payload.
    ///     This is used internally to store extension data including SIP headers.
    /// </summary>
    [System.Text.Json.Serialization.JsonExtensionData]
    [Newtonsoft.Json.JsonExtensionData]

    // ReSharper disable once CollectionNeverUpdated.Global
    public Dictionary<string, object> ExtensionData { get; set; } = new Dictionary<string, object>();

    /// <summary>
    ///     Gets all SIP header properties (properties starting with "SipHeader_X-") from the webhook payload.
    ///     The dictionary key is the full property name (e.g., "SipHeader_X-Custom") and the value is the header value.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public Dictionary<string, string> SipHeaders =>
        this.ExtensionData
            .Where(kvp => kvp.Key.StartsWith("SipHeader_X-"))
            .ToDictionary(kvp => kvp.Key, kvp => GetStringValue(kvp.Value));

    private static string GetStringValue(object value) =>
        value switch
        {
            JsonElement jsonElement => jsonElement.GetString(),
            JToken jToken => jToken.Value<string>(),
            string str => str,
            _ => value?.ToString(),
        };
}