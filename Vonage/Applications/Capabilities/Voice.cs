using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Vonage.Serialization;

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents Voice capabilities.
/// </summary>
public class Voice
{
    /// <summary>
    ///     The length of time named conversations will remain active for after creation, in hours. 0 means infinite. Maximum
    ///     value is 744 (i.e. 31 days).
    /// </summary>
    [JsonProperty("conversations_ttl", Order = 2)]
    public int ConversationsTimeToLive { get; set; }

    /// <summary>
    ///     Selecting a region means all inbound, programmable SIP and SIP connect calls will be sent to the selected region
    ///     unless the call is sent to a regional endpoint, if the call is using a regional endpoint this will override the
    ///     application setting.
    /// </summary>
    [JsonProperty("region", Order = 3)]
    public string Region { get; set; }

    /// <summary>
    ///     Whether to use signed webhooks. This is a way of verifying that the request is coming from Vonage. Refer to the
    ///     Webhooks documentation for more information.
    /// </summary>
    [JsonProperty("signed_callbacks", Order = 1)]
    public bool SignedCallbacks { get; set; }

    /// <summary>
    ///     Represents the collection of Webhook URLs with their configuration.
    /// </summary>
    [JsonProperty("webhooks")]
    public IDictionary<VoiceWebhookType, VoiceWebhook> Webhooks { get; set; }

    /// <summary>
    /// Represents a webhook for Voice API.
    /// </summary>
    /// <param name="Address">The webhook address.</param>
    /// <param name="Method">Must be one of GET or POST.</param>
    /// <param name="ConnectionTimeout">If Vonage can't connect to the webhook URL for this specified amount of time, then Vonage makes one additional attempt to connect to the webhook endpoint. This is an integer value specified in milliseconds.</param>
    /// <param name="SocketTimeout">If a response from the webhook URL can't be read for this specified amount of time, then Vonage makes one additional attempt to read the webhook endpoint. This is an integer value specified in milliseconds.</param>
    public record VoiceWebhook(
        [property: JsonProperty("address", Order = 1)]
        Uri Address,
        [property: JsonProperty("http_method", Order = 0)]
        [property: JsonConverter(typeof(HttpMethodConverter))]
        HttpMethod Method,
        [property: JsonProperty("connection_timeout", Order = 2)]
        int ConnectionTimeout = 0,
        [property: JsonProperty("socket_timeout", Order = 3)]
        int SocketTimeout = 0);
}

/// <summary>
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum VoiceWebhookType
{
    /// <summary>
    /// </summary>
    [EnumMember(Value = "answer_url")] AnswerUrl = 1,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "event_url")] EventUrl = 2,

    /// <summary>
    /// </summary>
    [EnumMember(Value = "fallback_answer_url")]
    FallbackAnswerUrl = 5,
}