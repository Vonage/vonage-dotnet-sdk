﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using EnumsNET;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Vonage.Common.Monads;
using Vonage.Cryptography;
using Vonage.Logger;
using Vonage.Request;

namespace Vonage;

/// <summary>
///     Represents the SDK Configuration.
/// </summary>
public sealed class Configuration
{
    private const string LoggerCategory = "Vonage.Configuration";

    static Configuration()
    {
    }

    private Configuration(IConfiguration configuration)
    {
        this.Settings = configuration;
        this.LogAuthenticationCapabilities(LogProvider.GetLogger(LoggerCategory));
    }

    internal Configuration()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("settings.json", true, true)
            .AddJsonFile("appsettings.json", true, true);
        this.Settings = builder.Build();
        this.LogAuthenticationCapabilities(LogProvider.GetLogger(LoggerCategory));
    }

    private static Maybe<double> RequestsPerSecond =>
        double.TryParse(Instance.Settings["vonage:RequestsPerSecond"], out var requestsPerSecond)
            ? requestsPerSecond
            : Maybe<double>.None;

    /// <summary>
    ///     Retrieves the Api secret.
    /// </summary>
    public string ApiKey => this.Settings["vonage:Api.Key"] ?? string.Empty;

    /// <summary>
    ///     Retrieves the Api secret.
    /// </summary>
    public string ApiSecret => this.Settings["vonage:Api.Secret"] ?? string.Empty;

    /// <summary>
    ///     Retrieves the Application Id.
    /// </summary>
    public string ApplicationId => this.Settings["vonage:Application.Id"] ?? string.Empty;

    /// <summary>
    ///     Retrieves the Application Key.
    /// </summary>
    public string ApplicationKey => this.Settings["vonage:Application.Key"] ?? string.Empty;

    /// <summary>
    ///     Retrieves a configured HttpClient.
    /// </summary>
    public HttpClient Client
    {
        get
        {
            var handler = RequestsPerSecond
                .Map(BuildSemaphore)
                .Map(this.GetThrottlingMessageHandler)
                .IfNone(this.ClientHandler);
            var client = new HttpClient(handler);
            this.RequestTimeout.IfSome(value => client.Timeout = value);
            return client;
        }
    }

    /// <summary>
    ///     Exposes an HttpMessageHandler.
    /// </summary>
    public HttpMessageHandler ClientHandler { get; set; } = new HttpClientHandler();

    /// <summary>
    ///     Retrieves the unique instance (Singleton).
    /// </summary>
    public static Configuration Instance { get; } = new Configuration();

    /// <summary>
    ///     The timeout (in seconds) applied to every request. If not provided, the default timeout will be applied.
    /// </summary>
    public Maybe<TimeSpan> RequestTimeout =>
        int.TryParse(this.Settings["vonage:RequestTimeout"], out var timeout)
            ? Maybe<TimeSpan>.Some(TimeSpan.FromSeconds(timeout))
            : Maybe<TimeSpan>.None;

    /// <summary>
    ///     Retrieves the Security Secret.
    /// </summary>
    public string SecuritySecret => this.Settings["vonage:Security_secret"] ?? string.Empty;

    /// <summary>
    ///     Exposes the configuration's content.
    /// </summary>
    public IConfiguration Settings { get; }

    /// <summary>
    ///     Retrieves the SigningMethod.
    /// </summary>
    public string SigningMethod => this.Settings["vonage:Signing_method"] ?? string.Empty;

    /// <summary>
    ///     Retrieves the User Agent.
    /// </summary>
    public string UserAgent => this.Settings["vonage:UserAgent"] ?? string.Empty;

    /// <summary>
    ///     Provide urls to all Vonage APIs.
    /// </summary>
    public VonageUrls VonageUrls => VonageUrls.FromConfiguration(this.Settings);

    /// <summary>
    ///     Builds a Credentials from the current Configuration.
    /// </summary>
    /// <returns>The Credentials.</returns>
    public Credentials BuildCredentials() =>
        new Credentials
        {
            ApiKey = this.ApiKey,
            ApiSecret = this.ApiSecret,
            ApplicationId = this.ApplicationId,
            ApplicationKey = this.ApplicationKey,
            SecuritySecret = this.SecuritySecret,
            AppUserAgent = this.UserAgent,
            Method = Enum.TryParse(this.SigningMethod,
                out SmsSignatureGenerator.Method result)
                ? result
                : default,
        };

    /// <summary>
    ///     Builds a Configuration from an IConfiguration.
    /// </summary>
    /// <param name="configuration">The configuration properties.</param>
    /// <returns>The Configuration.</returns>
    public static Configuration FromConfiguration(IConfiguration configuration) => new Configuration(configuration);

    private static TimeSpanSemaphore BuildSemaphore(double requestsPerSecond)
    {
        var delay = 1 / requestsPerSecond;
        var execTimeSpanSemaphore = new TimeSpanSemaphore(1, TimeSpan.FromSeconds(delay));
        return execTimeSpanSemaphore;
    }

    private HttpMessageHandler GetThrottlingMessageHandler(TimeSpanSemaphore semaphore) =>
        this.ClientHandler != null
            ? new ThrottlingMessageHandler(semaphore, this.ClientHandler)
            : new ThrottlingMessageHandler(semaphore);

    private void LogAuthenticationCapabilities(ILogger logger)
    {
        var authCapabilities = new List<string>();
        if (!string.IsNullOrWhiteSpace(this.ApiKey) &&
            !string.IsNullOrWhiteSpace(this.ApiSecret))
        {
            authCapabilities.Add("Key/Secret");
        }

        if (!string.IsNullOrWhiteSpace(this.Settings["vonage:Security_secret"]))
        {
            authCapabilities.Add("Security/Signing");
        }

        if (!string.IsNullOrWhiteSpace(this.Settings["vonage:Application.Id"]) &&
            !string.IsNullOrWhiteSpace(this.Settings["vonage:Application.Key"]))
        {
            authCapabilities.Add("Application");
        }

        if (authCapabilities.Count == 0)
        {
            logger.LogInformation("No authentication found via configuration. Remember to provide your own.");
        }
        else
        {
            logger.LogInformation("Available authentication: {0}", string.Join(",", authCapabilities));
        }
    }
}

/// <summary>
///     Represents Vonage API Urls.
/// </summary>
public readonly struct VonageUrls
{
    private const string DefaultApiUrlApac = "https://api-ap.vonage.com";
    private const string DefaultApiUrlEu = "https://api-eu.vonage.com";
    private const string DefaultApiUrlUs = "https://api-us.vonage.com";
    private const string DefaultNexmoApiUrl = "https://api.nexmo.com";
    private const string DefaultRestApiUrl = "https://rest.nexmo.com";
    private const string DefaultVideoApiUrl = "https://video.api.vonage.com";
    private const string NexmoApiKey = "vonage:Url.Api";
    private const string NexmoRestKey = "vonage:Url.Rest";
    private const string VideoApiKey = "vonage:Url.Api.Video";

    private readonly Dictionary<Region, string> regions = new Dictionary<Region, string>
    {
        {Region.US, DefaultApiUrlUs},
        {Region.EU, DefaultApiUrlEu},
        {Region.APAC, DefaultApiUrlApac},
    };

    private readonly IConfiguration configuration;

    private VonageUrls(IConfiguration configuration) => this.configuration = configuration;

    /// <summary>
    ///     The Nexmo Api Url.
    /// </summary>
    public Uri Nexmo => this.Evaluate(NexmoApiKey, DefaultNexmoApiUrl);

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

    private Uri Evaluate(string key, string defaultValue) => this.configuration[key] is null
        ? new Uri(defaultValue)
        : new Uri(this.configuration[key]);

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
}