using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Vonage.Common.Exceptions;

namespace Vonage.Voice;

/// <summary>
///     Represents the properties for Advanced Machine Detection.
/// </summary>
public record AdvancedMachineDetectionProperties
{
    /// <summary>
    ///     Maximum time in seconds Vonage should wait for a machine beep to be detected. A machine event
    ///     with sub_state set to beep_timeout will be sent if the timeout is exceeded.
    /// </summary>
    [JsonProperty("beepTimeout", Order = 2)]
    [JsonIgnore]
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