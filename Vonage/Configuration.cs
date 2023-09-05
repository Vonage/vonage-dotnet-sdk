using System;
using System.Collections.Generic;
using System.Net.Http;
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
    private const string DefaultEuropeApiUrl = "https://api-eu.vonage.com";
    private const string DefaultNexmoApiUrl = "https://api.nexmo.com";
    private const string DefaultRestApiUrl = "https://rest.nexmo.com";
    private const string DefaultVideoApiUrl = "https://video.api.vonage.com";
    private const string LoggerCategory = "Vonage.Configuration";

    private static Maybe<double> RequestsPerSecond =>
        double.TryParse(Instance.Settings["appSettings:Vonage.RequestsPerSecond"], out var requestsPerSecond)
            ? requestsPerSecond
            : Maybe<double>.None;

    static Configuration()
    {
    }

    private Configuration(IConfiguration configuration)
    {
        this.Settings = configuration;
        this.LogAuthenticationCapabilities(LogProvider.GetLogger(LoggerCategory));
    }

    /// <summary>
    ///     Retrieves the Api secret.
    /// </summary>
    public string ApiKey => this.Settings["appSettings:Vonage_key"] ?? string.Empty;

    /// <summary>
    ///     Retrieves the Api secret.
    /// </summary>
    public string ApiSecret => this.Settings["appSettings:Vonage_secret"] ?? string.Empty;

    /// <summary>
    ///     Retrieves the Application Id.
    /// </summary>
    public string ApplicationId => this.Settings["appSettings:Vonage.Application.Id"] ?? string.Empty;

    /// <summary>
    ///     Retrieves the Application Key.
    /// </summary>
    public string ApplicationKey => this.Settings["appSettings:Vonage.Application.Key"] ?? string.Empty;

    /// <summary>
    ///     Retrieves a configured HttpClient.
    /// </summary>
    public HttpClient Client =>
        RequestsPerSecond
            .Map(BuildSemaphore)
            .Map(this.GetThrottlingMessageHandler)
            .Match(some => new HttpClient(some), this.BuildDefaultClient);

    /// <summary>
    ///     Exposes an HttpMessageHandler.
    /// </summary>
    public HttpMessageHandler ClientHandler { get; set; }

    /// <summary>
    ///     Retrieves the Europe Api Url.
    /// </summary>
    public Uri EuropeApiUrl => this.Settings["appSettings:Vonage.Url.Api.Europe"] is null
        ? new Uri(DefaultEuropeApiUrl)
        : new Uri(this.Settings["appSettings:Vonage.Url.Api.Europe"]);

    /// <summary>
    ///     Retrieves the unique instance (Singleton).
    /// </summary>
    public static Configuration Instance { get; } = new();

    /// <summary>
    ///     Retrieves the Nexmo Api Url.
    /// </summary>
    public Uri NexmoApiUrl => this.Settings["appSettings:Vonage.Url.Api"] is null
        ? new Uri(DefaultNexmoApiUrl)
        : new Uri(this.Settings["appSettings:Vonage.Url.Api"]);

    /// <summary>
    ///     Retrieves the Rest Api Url.
    /// </summary>
    public Uri RestApiUrl => this.Settings["appSettings:Vonage.Url.Rest"] is null
        ? new Uri(DefaultRestApiUrl)
        : new Uri(this.Settings["appSettings:Vonage.Url.Rest"]);

    /// <summary>
    ///     Retrieves the Security Secret.
    /// </summary>
    public string SecuritySecret => this.Settings["appSettings:Vonage.security_secret"] ?? string.Empty;

    /// <summary>
    ///     Exposes the configuration's content.
    /// </summary>
    public IConfiguration Settings { get; }

    /// <summary>
    ///     Retrieves the SigningMethod.
    /// </summary>
    public string SigningMethod => this.Settings["appSettings:Vonage.signing_method"] ?? string.Empty;

    /// <summary>
    ///     Retrieves the User Agent.
    /// </summary>
    public string UserAgent => this.Settings["appSettings:Vonage.UserAgent"] ?? string.Empty;

    /// <summary>
    ///     Retrieves the Video Api Url.
    /// </summary>
    public Uri VideoApiUrl => this.Settings["appSettings:Vonage.Url.Api.Video"] is null
        ? new Uri(DefaultVideoApiUrl)
        : new Uri(this.Settings["appSettings:Vonage.Url.Api.Video"]);

    internal Configuration()
    {
        var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.Url.Rest", DefaultRestApiUrl},
                    {"appSettings:Vonage.Url.Api", DefaultNexmoApiUrl},
                    {"appSettings:Vonage.Url.Api.Europe", DefaultEuropeApiUrl},
                    {"appSettings:Vonage.Url.Api.Video", DefaultVideoApiUrl},
                    {"appSettings:Vonage.EnsureSuccessStatusCode", "false"},
                })
                .AddJsonFile("settings.json", true, true)
                .AddJsonFile("appsettings.json", true, true);
        this.Settings = builder.Build();
        this.LogAuthenticationCapabilities(LogProvider.GetLogger(LoggerCategory));
    }

    private void LogAuthenticationCapabilities(ILogger logger)
    {
        var authCapabilities = new List<string>();
        if (!string.IsNullOrWhiteSpace(this.ApiKey) &&
            !string.IsNullOrWhiteSpace(this.ApiSecret))
        {
            authCapabilities.Add("Key/Secret");
        }

        if (!string.IsNullOrWhiteSpace(this.Settings["appSettings:Vonage.security_secret"]))
        {
            authCapabilities.Add("Security/Signing");
        }

        if (!string.IsNullOrWhiteSpace(this.Settings["appSettings:Vonage.Application.Id"]) &&
            !string.IsNullOrWhiteSpace(this.Settings["appSettings:Vonage.Application.Key"]))
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

    /// <summary>
    /// Builds a Configuration from an IConfiguration.
    /// </summary>
    /// <param name="configuration">The configuration properties.</param>    
    /// <returns>The Configuration.</returns>
    public static Configuration FromConfiguration(IConfiguration configuration) => new(configuration);

    /// <summary>
    ///     Builds a Credentials from the current Configuration.
    /// </summary>
    /// <returns>The Credentials.</returns>
    public Credentials BuildCredentials() => new()
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

    private HttpClient BuildDefaultClient() =>
        this.ClientHandler == null
            ? new HttpClient()
            : new HttpClient(this.ClientHandler);

    private static TimeSpanSemaphore BuildSemaphore(double requestsPerSecond)
    {
        var delay = 1 / requestsPerSecond;
        var execTimeSpanSemaphore = new TimeSpanSemaphore(1, TimeSpan.FromSeconds(delay));
        return execTimeSpanSemaphore;
    }

    private ThrottlingMessageHandler GetThrottlingMessageHandler(TimeSpanSemaphore semaphore) =>
        this.ClientHandler != null
            ? new ThrottlingMessageHandler(semaphore, this.ClientHandler)
            : new ThrottlingMessageHandler(semaphore);
}