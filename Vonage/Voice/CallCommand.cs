#region
using Newtonsoft.Json;
using Vonage.Voice.Nccos;
using Vonage.Voice.Nccos.Endpoints;
#endregion

namespace Vonage.Voice;

/// <summary>
///     Represents a request to create an outbound voice call via the Voice API.
/// </summary>
public class CallCommand
{
    /// <summary>
    ///     Configures advanced machine detection behavior. Overrides <see cref="MachineDetection"/> if both are set. When enabled, Vonage detects whether a human or machine answered and reports the result via webhook.
    /// </summary>
    [JsonProperty("advanced_machine_detection", Order = 8)]
    public AdvancedMachineDetectionProperties AdvancedMachineDetection { get; set; }

    /// <summary>
    ///     The HTTP method used to fetch the NCCO from <see cref="AnswerUrl"/>. Default is "GET". Possible values: "GET" or "POST".
    /// </summary>
    [JsonProperty("answer_method", Order = 4)]
    public string AnswerMethod { get; set; }

    /// <summary>
    ///     The webhook endpoint where you provide the NCCO that governs this call. Vonage requests this URL as soon as the call is answered. Mutually exclusive with <see cref="Ncco"/>.
    /// </summary>
    [JsonProperty("answer_url", Order = 3)]
    public string[] AnswerUrl { get; set; }

    /// <summary>
    ///     The HTTP method used to send event information to <see cref="EventUrl"/>. Default is "POST". Possible values: "GET" or "POST".
    /// </summary>
    [JsonProperty("event_method", Order = 6)]
    public string EventMethod { get; set; }

    /// <summary>
    ///     The webhook endpoint where call progress events are sent asynchronously when the call status changes. Required unless configured at the application level.
    /// </summary>
    [JsonProperty("event_url", Order = 5)]
    public string[] EventUrl { get; set; }

    /// <summary>
    ///     The phone endpoint to use as the caller ID. Mutually exclusive with <see cref="RandomFromNumber"/>.
    /// </summary>
    [JsonProperty(Required = Required.Always, PropertyName = "from", Order = 1)]
    public PhoneEndpoint From { get; set; }

    /// <summary>
    ///     The number of seconds that elapse before Vonage hangs up after the call state changes to "answered". Range: 1-86400. Default is 7200 (two hours).
    /// </summary>
    [JsonProperty("length_timer", Order = 9)]
    public uint? LengthTimer { get; set; }

    /// <summary>
    ///     Configures behavior when Vonage detects an answering machine. Set to "continue" to receive a machine event via webhook, or "hangup" to end the call. Overridden by <see cref="AdvancedMachineDetection"/> if both are set.
    /// </summary>
    [JsonProperty("machine_detection", Order = 7)]
    public string MachineDetection { get; set; }

    /// <summary>
    ///     An inline NCCO to execute when the call is answered. Mutually exclusive with <see cref="AnswerUrl"/>.
    /// </summary>
    [JsonProperty("ncco", Order = 2)]
    public Ncco Ncco { get; set; }

    /// <summary>
    ///     Set to <c>true</c> to use a random phone number from your application's assigned numbers as the caller ID. Mutually exclusive with <see cref="From"/>.
    /// </summary>
    [JsonProperty("random_from_number", Order = 11)]
    public bool? RandomFromNumber { get; set; }

    /// <summary>
    ///     The number of seconds that elapse before Vonage hangs up after the call state changes to "ringing". Range: 1-120. Default is 60.
    /// </summary>
    [JsonProperty("ringing_timer", Order = 10)]
    public uint? RingingTimer { get; set; }

    /// <summary>
    ///     The destination endpoints to call. Supports phone numbers, SIP URIs, WebSocket endpoints, app users, and VBC extensions.
    /// </summary>
    [JsonProperty(Required = Required.Always, PropertyName = "to", Order = 0)]
    public Endpoint[] To { get; set; }
}