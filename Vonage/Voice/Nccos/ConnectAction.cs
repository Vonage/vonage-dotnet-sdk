#region
using Newtonsoft.Json;
using Vonage.Voice.Nccos.Endpoints;
#endregion

namespace Vonage.Voice.Nccos;

public class ConnectAction : NccoAction
{
    public override ActionType Action => ActionType.Connect;

    /// <summary>
    ///     Configure the behavior of Vonage's advanced machine detection. Overrides machineDetection if both are set.
    /// </summary>
    [JsonProperty("advanced_machine_detection", Order = 9)]
    public AdvancedMachineDetectionProperties AdvancedMachineDetection { get; set; }

    /// <summary>
    ///     Connect to a single endpoint.
    /// </summary>
    [JsonProperty("endpoint", Order = 0)]
    public Endpoint[] Endpoint { get; set; }

    /// <summary>
    ///     The HTTP method Vonage uses to make the request to eventUrl. The default value is POST.
    /// </summary>
    [JsonProperty("eventMethod", Order = 7)]
    public string EventMethod { get; set; }

    /// <summary>
    ///     Set to synchronous to:
    ///     make the connect action synchronous
    ///     enable eventUrl to return an NCCO that overrides the current NCCO when a call moves to specific states.
    /// </summary>
    [JsonProperty("eventType", Order = 2)]
    public string EventType { get; set; }

    /// <summary>
    ///     Set the webhook endpoint that Vonage calls asynchronously on each of the possible Call States.
    ///     If eventType is set to synchronous the eventUrl can return an NCCO that overrides the current
    ///     NCCO when a timeout occurs.
    /// </summary>
    [JsonProperty("eventUrl", Order = 6)]
    public string[] EventUrl { get; set; }

    /// <summary>
    ///     A number in E.164 format that identifies the caller.
    ///     This must be one of your Vonage virtual numbers,
    ///     another value will result in the caller ID being unknown.
    ///     If the caller is an app user, this option should be omitted.
    /// </summary>
    [JsonProperty("from", Order = 1)]
    public string From { get; set; }

    /// <summary>
    ///     Maximum length of the call in seconds. The default value is 7200 seconds (2 hours).
    /// </summary>
    [JsonProperty("limit", Order = 4)]
    public string Limit { get; set; }

    /// <summary>
    ///     Configure the behavior when Vonage detects that a destination is an answerphone. Set to either:
    ///     continue - Vonage sends an HTTP request to event_url with the Call event machine
    ///     hangup - end the Call
    /// </summary>
    [JsonProperty("machineDetection", Order = 5)]
    public string MachineDetection { get; set; }

    /// <summary>
    ///     A URL value that points to a ringbackTone to be played back on repeat to the caller,
    ///     so they don't hear silence. The ringbackTone will automatically stop playing when the call is
    ///     fully connected. It's not recommended to use this parameter when connecting to a phone endpoint,
    ///     as the carrier will supply their own ringbackTone.
    ///     Example: "ringbackTone": "http://example.com/ringbackTone.wav".
    /// </summary>
    [JsonProperty("ringbackTone", Order = 8)]
    public string RingbackTone { get; set; }

    /// <summary>
    ///     If the call is unanswered, set the number in seconds before Vonage stops ringing endpoint. The default value is 60.
    /// </summary>
    [JsonProperty("timeout", Order = 3)]
    public string Timeout { get; set; }
}