#region
using System;
using Newtonsoft.Json;
#endregion

namespace Vonage.Applications.Capabilities;

/// <summary>
///     Represents the Network APIs capability for an application. Configures the network application ID and redirect URI
///     for Network API integrations.
/// </summary>
public class NetworkApis
{
    /// <summary>
    ///     The network application ID for Network API integrations.
    /// </summary>
    [JsonProperty("network_application_id", Order = 0)]
    public string ApplicationId { get; private set; }

    /// <summary>
    ///     The redirect URI for OAuth flows in Network API integrations.
    /// </summary>
    [JsonProperty("redirect_uri", Order = 0)]
    public Uri RedirectUri { get; private set; }

    /// <summary>
    ///     Creates a new NetworkApis capability builder for fluent configuration.
    /// </summary>
    /// <returns>A new NetworkApis capability instance.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var networkApisCapability = NetworkApis.Build()
    ///     .WithApplicationId("my-network-app-id")
    ///     .WithRedirectUri(new Uri("https://example.com/oauth/callback"));
    /// ]]></code>
    /// </example>
    public static NetworkApis Build() => new NetworkApis();

    /// <summary>
    ///     Sets the network application ID.
    /// </summary>
    /// <param name="applicationId">The network application ID.</param>
    /// <returns>The NetworkApis capability instance for fluent chaining.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var networkApis = NetworkApis.Build()
    ///     .WithApplicationId("my-network-app-id");
    /// ]]></code>
    /// </example>
    public NetworkApis WithApplicationId(string applicationId)
    {
        this.ApplicationId = applicationId;
        return this;
    }

    /// <summary>
    ///     Sets the redirect URI for OAuth flows.
    /// </summary>
    /// <param name="uri">The redirect URI.</param>
    /// <returns>The NetworkApis capability instance for fluent chaining.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var networkApis = NetworkApis.Build()
    ///     .WithRedirectUri(new Uri("https://example.com/oauth/callback"));
    /// ]]></code>
    /// </example>
    public NetworkApis WithRedirectUri(Uri uri)
    {
        this.RedirectUri = uri;
        return this;
    }
}