#region
using System;
using Newtonsoft.Json;
#endregion

namespace Vonage.Applications.Capabilities;

/// <summary>
/// </summary>
public class NetworkApis
{
    /// <summary>
    /// </summary>
    [JsonProperty("network_application_id", Order = 0)]
    public string ApplicationId { get; private set; }

    /// <summary>
    /// </summary>
    [JsonProperty("redirect_uri", Order = 0)]
    public Uri RedirectUri { get; private set; }

    /// <summary>
    ///     Sets the application on the NetworkApis capability.
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns>The NetworkApis capability.</returns>
    public NetworkApis WithApplicationId(string applicationId)
    {
        this.ApplicationId = applicationId;
        return this;
    }

    /// <summary>
    ///     Sets the redirect uri on the NetworkApis capability.
    /// </summary>
    /// <param name="uri"></param>
    /// <returns>The NetworkApis capability.</returns>
    public NetworkApis WithRedirectUri(Uri uri)
    {
        this.RedirectUri = uri;
        return this;
    }
}