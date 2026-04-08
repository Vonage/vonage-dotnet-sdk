using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vonage.Voice.Nccos;

/// <summary>
///     Represents an NCCO Notify action that sends a webhook notification to a specified URL with a custom payload without interrupting the call flow.
/// </summary>
public class NotifyAction : NccoAction
{
    /// <inheritdoc />
    public override ActionType Action => ActionType.Notify;

    /// <summary>
    /// The JSON object body to send to your event URL
    /// </summary>
    [JsonProperty("payload")]
    public object Payload { get; set; }

    /// <summary>
    /// The URL to send events to. If you return an NCCO when you receive a notification, it will replace the current NCCO
    /// </summary>
    [JsonProperty("eventUrl")]
    public string[] EventUrl { get; set; }

    /// <summary>
    /// The HTTP method to use when sending payload to your eventUrl
    /// </summary>
    [JsonProperty("eventMethod")]
    public string EventMethod { get; set; }
}