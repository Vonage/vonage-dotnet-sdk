using System.ComponentModel;

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents a Vonage Voice routing region. Selecting a region means all inbound, programmable SIP, and SIP
///     connect calls will be routed to that region.
/// </summary>
public enum VoiceRegion
{
    /// <summary>North America — East (api-us-3.vonage.com).</summary>
    [Description("na-east")] NorthAmericaEast,

    /// <summary>North America — West (api-us-4.vonage.com).</summary>
    [Description("na-west")] NorthAmericaWest,

    /// <summary>Europe — East (api-eu-4.vonage.com).</summary>
    [Description("eu-east")] EuropeEast,

    /// <summary>Europe — West (api-eu-3.vonage.com).</summary>
    [Description("eu-west")] EuropeWest,

    /// <summary>Asia-Pacific — Singapore (api-ap-3.vonage.com).</summary>
    [Description("apac-sng")] AsiaPacificSingapore,

    /// <summary>Asia-Pacific — Australia (api-ap-4.vonage.com).</summary>
    [Description("apac-australia")] AsiaPacificAustralia
}
