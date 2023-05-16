using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Vonage.Common.Exceptions;
using Vonage.Voice.Nccos;
using Vonage.Voice.Nccos.Endpoints;

namespace Vonage.Voice;

/// <summary>
///     Represents a command to start a call.
/// </summary>

//[JsonConverter(typeof(CallCommandConverter))]
public class CallCommand
{
    /// <summary>
    ///     Configure the behavior of Vonage's advanced machine detection. Overrides machineDetection if both are set.
    /// </summary>
    [JsonProperty("advanced_machine_detection", Order = 8)]
    public AdvancedMachineDetectionProperties AdvancedMachineDetection { get; set; }

    /// <summary>
    ///     Optional. The HTTP method used to send event information to answer_url. The default value is GET.
    /// </summary>
    [JsonProperty("answer_method", Order = 4)]
    public string AnswerMethod { get; set; }

    /// <summary>
    ///     The webhook endpoint where you provide the Vonage Call Control Object that governs this call. As soon as your user
    ///     answers a call, Platform requests this NCCO from answer_url. Use answer_method to manage the HTTP method.
    /// </summary>
    [JsonProperty("answer_url", Order = 3)]
    public string[] AnswerUrl { get; set; }

    /// <summary>
    ///     Optional. The HTTP method used to send event information to event_url. The default value is POST.
    /// </summary>
    [JsonProperty("event_method", Order = 6)]
    public string EventMethod { get; set; }

    /// <summary>
    ///     Optional. Platform sends event information asynchronously to this endpoint when status changes. For more
    ///     information about the values sent, see callback.
    /// </summary>
    [JsonProperty("event_url", Order = 5)]
    public string[] EventUrl { get; set; }

    /// <summary>
    ///     The endpoint you are calling from. Possible value are the same as to.
    /// </summary>
    [JsonProperty(Required = Required.Always, PropertyName = "from", Order = 1)]
    public PhoneEndpoint From { get; set; }

    /// <summary>
    ///     Optional. Set the number of seconds that elapse before Vonage hangs up after the call state changes to in_progress.
    ///     The default value is 7200, two hours. This is also the maximum value.
    /// </summary>
    [JsonProperty("length_timer", Order = 9)]
    public uint? LengthTimer { get; set; }

    /// <summary>
    ///     Optional. Configure the behavior when Vonage detects that a destination is an answerphone.
    /// </summary>
    [JsonProperty("machine_detection", Order = 7)]
    public string MachineDetection { get; set; }

    /// <summary>
    ///     This will convert to ncco as per the CallCommandConverter - it is preferable to use this over the JArray Ncco
    /// </summary>
    [JsonProperty(Order = 2)]
    public Ncco Ncco { get; set; }

    /// <summary>
    ///     Set to <code>true</code> to use random phone number as the <code>from</code>. This number will be selected from the
    ///     list of nubmers assigned to the current application. <code>RandomFromNumber = true  </code> cannot be used together
    ///     with <code>From</code>
    /// </summary>
    [JsonProperty("random_from_number", Order = 11)]
    public bool? RandomFromNumber { get; set; }

    /// <summary>
    ///     Optional. Set the number of seconds that elapse before Vonage hangs up after the call state changes to 'ringing'.
    ///     The default value is 60, the maximum value is 120.
    /// </summary>
    [JsonProperty("ringing_timer", Order = 10)]
    public uint? RingingTimer { get; set; }

    /// <summary>
    ///     The single or mixed collection of endpoint types you connected to. Possible values.
    /// </summary>
    [JsonProperty(Required = Required.Always, PropertyName = "to", Order = 0)]
    public Endpoint[] To { get; set; }

    /// <summary>
    ///     Represents the properties for Advanced Machine Detection.
    /// </summary>
    public record AdvancedMachineDetectionProperties
    {
        /// <summary>
        ///     Maximum time in seconds Vonage should wait for a machine beep to be detected. A machine event
        ///     with sub_state set to beep_timeout will be sent if the timeout is exceeded.
        /// </summary>
        [JsonProperty("beep_timeout", Order = 2)]
        public int BeepTimeout { get; }

        /// <summary>
        ///     When hangup is used, the call will be terminated if a machine is detected. When continue is
        ///     used, the call will continue even if a machine is detected.
        /// </summary>
        [JsonProperty("behavior", Order = 0, DefaultValueHandling = DefaultValueHandling.Include)]
        [JsonConverter(typeof(StringEnumConverter))]
        public MachineDetectionBehavior Behavior { get; }

        /// <summary>
        ///     Detect if machine answered and sends a human or machine status in the webhook payload. When set to
        ///     detect_beep, the system also attempts to detect voice mail beep and sends an additional parameter sub_state in the
        ///     webhook with the value beep_start.
        /// </summary>
        [JsonProperty("mode", Order = 1, DefaultValueHandling = DefaultValueHandling.Include)]
        [JsonConverter(typeof(StringEnumConverter))]
        public MachineDetectionMode Mode { get; }

        /// <summary>
        ///     Constructor for AdvancedMachineDetectionProperties.
        /// </summary>
        /// <param name="behavior">
        ///     When hangup is used, the call will be terminated if a machine is detected. When continue is
        ///     used, the call will continue even if a machine is detected.
        /// </param>
        /// <param name="mode">
        ///     Detect if machine answered and sends a human or machine status in the webhook payload. When set to
        ///     detect_beep, the system also attempts to detect voice mail beep and sends an additional parameter sub_state in the
        ///     webhook with the value beep_start.
        /// </param>
        /// <param name="beepTimeout">
        ///     Maximum time in seconds Vonage should wait for a machine beep to be detected. A machine event
        ///     with sub_state set to beep_timeout will be sent if the timeout is exceeded.
        /// </param>
        /// <exception cref="VonageException">When Beep Timeout is lower than 45, or higher than 120.</exception>
        public AdvancedMachineDetectionProperties(MachineDetectionBehavior behavior, MachineDetectionMode mode,
            int beepTimeout)
        {
            if (beepTimeout is < 45 or > 120)
            {
                throw new VonageException("Beep Timeout has a minimal value of 45, and a maximal value of 120.");
            }

            this.Behavior = behavior;
            this.Mode = mode;
            this.BeepTimeout = beepTimeout;
        }

        /// <summary>
        ///     Represents the behavior for AdvancedMachineDetection.
        /// </summary>
        public enum MachineDetectionBehavior
        {
            /// <summary>
            ///     Indicates the call will continue even if a machine is detected.
            /// </summary>
            [EnumMember(Value = "continue")] Continue,

            /// <summary>
            ///     Indicates the call will be terminated if a machine is detected.
            /// </summary>
            [EnumMember(Value = "hangup")] Hangup,
        }

        /// <summary>
        ///     Represents the mode for AdvancedMachineDetection.
        /// </summary>
        public enum MachineDetectionMode
        {
            /// <summary>
            ///     Indicates the system detects if a machine answered and send a status in the webhook payload.
            /// </summary>
            [EnumMember(Value = "detect")] Detect,

            /// <summary>
            ///     Indicates the system also attempts to detect voice mail beep and sends an additional parameter sub_state in the
            ///     webhook with the value beep_start.
            /// </summary>
            [EnumMember(Value = "detect_beep")] DetectBeep,
        }
    }
}