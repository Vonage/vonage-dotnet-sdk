#region
using System.Collections.Generic;
using Vonage.Common;
#endregion

namespace Vonage.Applications.Capabilities;

public class Rtc : Capability
{
    public Rtc()
    {
        this.Type = CapabilityType.Rtc;
        this.Webhooks = new Dictionary<Webhook.Type, Webhook>();
    }

    public Rtc(IDictionary<Webhook.Type, Webhook> webhooks)
    {
        this.Webhooks = webhooks;
        this.Type = CapabilityType.Rtc;
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public static Rtc Build() => new Rtc();

    /// <summary>
    ///     Sets the event URL webhook for RTC capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <param name="method">The HTTP method (GET or POST).</param>
    /// <returns>The RTC capability instance for fluent chaining.</returns>
    public Rtc WithEventUrl(string url, WebhookHttpMethod method)
    {
        this.Webhooks[Webhook.Type.EventUrl] = new Webhook
        {
            Address = url,
            Method = method.ToString().ToUpperInvariant(),
        };
        return this;
    }
}