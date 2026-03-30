#region
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using Vonage.Common;
#endregion

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Base class for application capabilities that support webhook configuration.
/// </summary>
public abstract class Capability
{
    /// <summary>
    ///     Defines the types of capabilities available for an application.
    /// </summary>
    public enum CapabilityType
    {
        /// <summary>
        ///     Voice capability for making and receiving calls.
        /// </summary>
        [Description("voice")] Voice,

        /// <summary>
        ///     RTC (Real-Time Communication) capability for Client SDK applications.
        /// </summary>
        [Description("rtc")] Rtc,

        /// <summary>
        ///     Messages capability for sending and receiving messages across multiple channels.
        /// </summary>
        [Description("messages")] Messages,

        /// <summary>
        ///     VBC (Vonage Business Communications) capability for zero-rated calls.
        /// </summary>
        [Description("vbc")] Vbc,

        /// <summary>
        ///     Video capability for in-app video calls.
        /// </summary>
        [Description("video")] Video,

        /// <summary>
        ///     Verify capability for user verification workflows.
        /// </summary>
        [Description("verify")] Verify,
    }

    /// <summary>
    ///     The collection of webhook URLs configured for this capability.
    /// </summary>
    [JsonProperty("webhooks")]
    public IDictionary<Webhook.Type, Webhook> Webhooks { get; set; }

    /// <summary>
    ///     The type of capability.
    /// </summary>
    [JsonIgnore]
    protected CapabilityType Type { get; set; }
}