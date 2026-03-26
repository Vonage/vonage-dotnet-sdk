#region
using System;
using System.Collections.Generic;
using System.ComponentModel;
using EnumsNET;
using Microsoft.Extensions.Configuration;
#endregion

namespace Vonage;

/// <summary>
///     Represents Vonage API Urls.
/// </summary>
public readonly struct VonageUrls
{
    /// <summary>
    /// </summary>
    public enum Region
    {
        /// <summary>
        /// </summary>
        [Description("AMER")] US,

        /// <summary>
        /// </summary>
        [Description("EMEA")] EU,

        /// <summary>
        /// </summary>
        [Description("APAC")] APAC,
    }

    private const string DefaultApiUrlApac = "https://api-ap.vonage.com";
    private const string DefaultApiUrlEu = "https://api-eu.vonage.com";
    private const string DefaultApiUrlUs = "https://api-us.vonage.com";
    private const string DefaultNexmoApiUrl = "https://api.nexmo.com";
    private const string DefaultOidcUrl = "https://oidc.idp.vonage.com";
    private const string DefaultRestApiUrl = "https://rest.nexmo.com";
    private const string DefaultVideoApiUrl = "https://video.api.vonage.com";
    internal const string NexmoApiKey = "vonage:Url.Api";
    internal const string NexmoRestKey = "vonage:Url.Rest";
    internal const string OidcApiKey = "vonage:Url.OIDC";
    internal const string VideoApiKey = "vonage:Url.Api.Video";

    private readonly IConfiguration configuration;

    private readonly Dictionary<Region, string> regions = new Dictionary<Region, string>
    {
        {Region.US, DefaultApiUrlUs},
        {Region.EU, DefaultApiUrlEu},
        {Region.APAC, DefaultApiUrlApac},
    };

    private VonageUrls(IConfiguration configuration) => this.configuration = configuration;

    /// <summary>
    ///     The Nexmo Api Url.
    /// </summary>
    public Uri Nexmo => this.Evaluate(NexmoApiKey, DefaultNexmoApiUrl);

    /// <summary>
    ///     The Oidc Url.
    /// </summary>
    public Uri Oidc => this.Evaluate(OidcApiKey, DefaultOidcUrl);

    /// <summary>
    ///     The Nexmo REST Url.
    /// </summary>
    public Uri Rest => this.Evaluate(NexmoRestKey, DefaultRestApiUrl);

    /// <summary>
    ///     The Video Api Url.
    /// </summary>
    public Uri Video => this.Evaluate(VideoApiKey, DefaultVideoApiUrl);

    /// <summary>
    ///     Creates a set of urls from configuration.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <returns>A set of Urls.</returns>
    public static VonageUrls FromConfiguration(IConfiguration configuration) => new VonageUrls(configuration);

    /// <summary>
    ///     Retrieves a region-specific Url.
    /// </summary>
    /// <param name="region">The selected region.</param>
    /// <returns>The Url.</returns>
    public Uri Get(Region region) =>
        this.Evaluate(string.Concat(NexmoApiKey, ".", region.AsString(EnumFormat.Description)), this.regions[region]);

    private Uri Evaluate(string key, string defaultValue) =>
        this.configuration[key] is null
            ? new Uri($"{defaultValue.TrimEnd('/')}/")
            : new Uri($"{this.configuration[key].TrimEnd('/')}/");
}