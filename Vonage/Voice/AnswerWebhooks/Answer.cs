#region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Vonage.Voice.EventWebhooks;
using Vonage.Voice.Nccos.Endpoints;
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
    ///     The username that called to only if the call was made using the Client SDK. In this case, from will be absent (that
    ///     is, from and from_user will never both be present together).
    /// </summary>
    [JsonProperty("from_user")]
    [JsonPropertyName("from_user")]
    public string FromUser { get; set; }

    /// <summary>
    ///     The voice channel type that answered the call. Possible values are phone, sip, websocket, app, vbc.
    /// </summary>
    [JsonProperty("endpoint_type")]
    [JsonPropertyName("endpoint_type")]
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter<Endpoint.EndpointType>))]
    public Endpoint.EndpointType EndpointType { get; set; }

    /// <summary>
    ///     Regional API endpoint which should be used to control the call with REST API.
    /// </summary>
    [JsonProperty("region_url")]
    [JsonPropertyName("region_url")]
    public Uri RegionUrl { get; set; }

    /// <summary>
    ///     A custom data object, optionally passed as parameter on the serverCall method when a call is initiated from an
    ///     application using the Client SDK
    /// </summary>
    [JsonProperty("custom_data")]
    [JsonPropertyName("custom_data")]
    public Dictionary<string, string> CustomData { get; set; }

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