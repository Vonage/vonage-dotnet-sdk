#region
using System.Collections.Generic;
using Newtonsoft.Json;
using Vonage.Common;
#endregion

namespace Vonage.Applications.Capabilities;

/// <summary>
/// </summary>
public class Verify : Capability
{
    /// <summary>
    /// </summary>
    public Verify()
    {
        this.Type = CapabilityType.Verify;
        this.Webhooks = new Dictionary<Webhook.Type, Webhook>();
    }

    /// <summary>
    /// </summary>
    /// <param name="webhooks"></param>
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
    /// </summary>
    /// <returns></returns>
    public static Verify Build() => new Verify();

    /// <summary>
    ///     Sets the status URL webhook for Verify capability.
    /// </summary>
    /// <param name="url">The webhook URL.</param>
    /// <returns>The Verify capability instance</returns>
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
    /// <returns>The Verify capability instance</returns>
    public Verify WithVersion(string version)
    {
        this.Version = version;
        return this;
    }
}