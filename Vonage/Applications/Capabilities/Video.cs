using System.Collections.Generic;
using Vonage.Common;

namespace Vonage.Applications.Capabilities;

/// <summary>
/// Represents the Video capability.
/// </summary>
public class Video : Capability
{
    
    /// <summary>
    ///     Constructor.
    /// </summary>
    public Video()
    {
        this.Type = CapabilityType.Video;
        this.Webhooks = new Dictionary<Webhook.Type, Webhook>();
    }
}