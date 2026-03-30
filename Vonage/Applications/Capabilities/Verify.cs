#region
using System.Collections.Generic;
using Newtonsoft.Json;
using Vonage.Common;
#endregion

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents the Verify capability for an application. Configures webhooks for verification status updates.
/// </summary>
public class Verify : Capability
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Verify"/> class.
    /// </summary>
    public Verify()
    {
        this.Type = CapabilityType.Verify;
        this.Webhooks = new Dictionary<Webhook.Type, Webhook>();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Verify"/> class with the specified webhooks.
    /// </summary>
    /// <param name="webhooks">The webhook configuration dictionary.</param>
    public Verify(IDictionary<Webhook.Type, Webhook> webhooks)
    {
        this.Webhooks = webhooks;
        this.Type = CapabilityType.Verify;
    }

    /// <summary>
    ///     The version of the Verify API to use. For example: "v2".
    /// </summary>
    [JsonProperty("version", Order = 0)]
    public string Version { get; set; }

    /// <summary>
    ///     Creates a new Verify capability builder for fluent configuration.
    /// </summary>
    /// <returns>A new Verify capability instance.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var verifyCapability = Verify.Build()
    ///     .WithVersion("v2")
    ///     .WithStatusUrl("https://example.com/webhooks/status");
    /// ]]></code>
    /// </example>
    public static Verify Build() => new Verify();

    /// <summary>
    ///     Sets the status URL webhook. Vonage will send Verify status updates to this URL.
    /// </summary>
    /// <param name="url">The webhook URL that will receive verification status updates.</param>
    /// <returns>The Verify capability instance for fluent chaining.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var verify = Verify.Build()
    ///     .WithStatusUrl("https://example.com/webhooks/status");
    /// ]]></code>
    /// </example>
    public Verify WithStatusUrl(string url)
    {
        this.Webhooks[Webhook.Type.StatusUrl] = new Webhook
        {
            Address = url,
            Method = nameof(WebhookHttpMethod.Post).ToUpperInvariant(),
        };
        return this;
    }

    /// <summary>
    ///     Sets the version of the Verify API.
    /// </summary>
    /// <param name="version">The version of the Verify API to use. For example: "v2".</param>
    /// <returns>The Verify capability instance for fluent chaining.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var verify = Verify.Build()
    ///     .WithVersion("v2");
    /// ]]></code>
    /// </example>
    public Verify WithVersion(string version)
    {
        this.Version = version;
        return this;
    }
}