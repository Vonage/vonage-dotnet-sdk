#region
using System.Collections.Generic;
using Newtonsoft.Json;
using Vonage.Common;
#endregion

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents the RTC (Real-Time Communication) capability for an application. Configures webhooks for
///     Client SDK events.
/// </summary>
public class Rtc : Capability
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Rtc" /> class.
    /// </summary>
    public Rtc()
    {
        this.Type = CapabilityType.Rtc;
        this.Webhooks = new Dictionary<Webhook.Type, Webhook>();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Rtc" /> class with the specified webhooks.
    /// </summary>
    /// <param name="webhooks">The webhook configuration dictionary.</param>
    public Rtc(IDictionary<Webhook.Type, Webhook> webhooks)
    {
        this.Webhooks = webhooks;
        this.Type = CapabilityType.Rtc;
    }

    /// <summary>
    ///     Whether to use signed webhooks. This verifies that webhook requests are coming from Vonage.
    /// </summary>
    [JsonProperty("signed_callbacks", Order = 0)]
    public bool SignedCallbacks { get; set; }

    /// <summary>
    ///     Creates a new RTC capability builder for fluent configuration.
    /// </summary>
    /// <returns>A new RTC capability instance.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var rtcCapability = Rtc.Build()
    ///     .WithEventUrl("https://example.com/webhooks/event", WebhookHttpMethod.Post)
    ///     .EnableSignedCallbacks();
    /// ]]></code>
    /// </example>
    public static Rtc Build() => new Rtc();

    /// <summary>
    ///     Sets the event URL webhook. Vonage will send RTC events to this URL.
    /// </summary>
    /// <param name="url">The webhook URL that will receive RTC events.</param>
    /// <param name="method">The HTTP method (GET or POST).</param>
    /// <returns>The RTC capability instance for fluent chaining.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var rtc = Rtc.Build()
    ///     .WithEventUrl("https://example.com/webhooks/event", WebhookHttpMethod.Post);
    /// ]]></code>
    /// </example>
    public Rtc WithEventUrl(string url, WebhookHttpMethod method)
    {
        this.Webhooks[Webhook.Type.EventUrl] = new Webhook
        {
            Address = url,
            Method = method.ToString().ToUpperInvariant(),
        };
        return this;
    }

    /// <summary>
    ///     Enables signed callbacks to verify webhook requests are from Vonage.
    /// </summary>
    /// <returns>The RTC capability instance for fluent chaining.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var rtc = Rtc.Build()
    ///     .WithEventUrl("https://example.com/webhooks/event", WebhookHttpMethod.Post)
    ///     .EnableSignedCallbacks();
    /// ]]></code>
    /// </example>
    public Rtc EnableSignedCallbacks()
    {
        this.SignedCallbacks = true;
        return this;
    }

    /// <summary>
    ///     Disables signed callbacks.
    /// </summary>
    /// <returns>The RTC capability instance for fluent chaining.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var rtc = Rtc.Build()
    ///     .DisableSignedCallbacks();
    /// ]]></code>
    /// </example>
    public Rtc DisableSignedCallbacks()
    {
        this.SignedCallbacks = false;
        return this;
    }
}