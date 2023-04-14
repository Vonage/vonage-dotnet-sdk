using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Vonage.Common.Monads;
using Vonage.Logger;
using Vonage.Request;

namespace Vonage;

/// <summary>
///     Represents the SDK Configuration.
/// </summary>
public sealed class Configuration
{
    private const string LoggerCategory = "Vonage.Configuration";

    private static Maybe<double> RequestsPerSecond =>
        double.TryParse(Instance.Settings["appSettings:Vonage.RequestsPerSecond"], out var requestsPerSecond)
            ? requestsPerSecond
            : Maybe<double>.None;

    static Configuration()
    {
    }

    private Configuration()
    {
        var logger = LogProvider.GetLogger(LoggerCategory);
        var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"appSettings:Vonage.Url.Rest", "https://rest.nexmo.com"},
                    {"appSettings:Vonage.Url.Api", "https://api.nexmo.com"},
                    {"appSettings:Vonage.Meetings.Url.Api", "https://api-eu.vonage.com"},
                    {"appSettings:Vonage.Video.Url.Api", "https://video.api.vonage.com"},
                    {"appSettings:Vonage.EnsureSuccessStatusCode", "false"},
                })
                .AddJsonFile("settings.json", true, true)
                .AddJsonFile("appsettings.json", true, true)
            ;
        this.Settings = builder.Build();

            // verify we have a minimum amount of configuration
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
    ///     Retrieves the Api secret.
    /// </summary>
    public string ApiKey => this.Settings["appSettings:Vonage_key"] ?? string.Empty;

        /// <summary>
        ///     Retrieves the Api secret.
        /// </summary>
        public string ApiSecret => this.Settings["appSettings:Vonage_secret"] ?? string.Empty;

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
    ///     Retrieves the unique instance (Singleton).
    /// </summary>
    public static Configuration Instance { get; } = new();

    /// <summary>
    ///     Retrieves the Meetings Api Url.
    /// </summary>
    public Uri MeetingsApiUrl => new(this.Settings["appSettings:Vonage.Meetings.Url.Api"] ?? string.Empty);

        /// <summary>
        ///     Retrieves the Nexmo Api Url.
        /// </summary>
        public Uri NexmoApiUrl => new(this.Settings["appSettings:Vonage.Url.Api"] ?? string.Empty);

        /// <summary>
        ///     Exposes the configuration's content.
        /// </summary>
        public IConfiguration Settings { get; }

    /// <summary>
    ///     Retrieves the Video Api Url.
    /// </summary>
    public Uri VideoApiUrl => new(this.Settings["appSettings:Vonage.Video.Url.Api"] ?? string.Empty);

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