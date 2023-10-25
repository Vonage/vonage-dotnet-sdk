using System.Collections.Generic;
using Newtonsoft.Json;
using Vonage.Common;

namespace Vonage.Applications.Capabilities;

public class Voice : Capability
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

    public Voice(IDictionary<Webhook.Type, Webhook> webhooks)
    {
        this.Webhooks = webhooks;
        this.Type = CapabilityType.Voice;
    }
}