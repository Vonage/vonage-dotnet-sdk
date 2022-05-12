using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Vonage.Request;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Vonage
{
    public sealed class Configuration
    {
        private const string LoggerCategory = "Vonage.Configuration";
        
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Configuration()
        {
        }

        private Configuration()
        {
            var logger = Logger.LogProvider.GetLogger(LoggerCategory);
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "appSettings:Vonage.Url.Rest", "https://rest.nexmo.com"},
                    { "appSettings:Vonage.Url.Api", "https://api.nexmo.com"},
                    { "appSettings:Vonage.EnsureSuccessStatusCode", "false" }
                })
                .AddJsonFile("settings.json", true, true)
                .AddJsonFile("appsettings.json", true, true)
            ;

            Settings = builder.Build();

            // verify we have a minimum amount of configuration

            var authCapabilities = new List<string>();

            if (!string.IsNullOrWhiteSpace(Settings["appSettings:Vonage_key"]) &&
                !string.IsNullOrWhiteSpace(Settings["appSettings:Vonage_secret"]))
            {
                authCapabilities.Add("Key/Secret");
            }
            if (!string.IsNullOrWhiteSpace(Settings["appSettings:Vonage.security_secret"]))
            {
                authCapabilities.Add("Security/Signing");
            }
            if (!string.IsNullOrWhiteSpace(Settings["appSettings:Vonage.Application.Id"]) &&
                !string.IsNullOrWhiteSpace(Settings["appSettings:Vonage.Application.Key"]))
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

        public static Configuration Instance { get; } = new Configuration();

        public IConfiguration Settings { get; }
        
        public HttpMessageHandler ClientHandler { get; set; }

        private HttpClient _client;
        public HttpClient Client
        {
            get
            {
                var reqPerSec = Instance.Settings["appSettings:Vonage.RequestsPerSecond"];
                if (string.IsNullOrEmpty(reqPerSec))
                    return _client ?? (_client = ClientHandler == null ? new HttpClient(): new HttpClient(ClientHandler));

                var delay = 1 / double.Parse(reqPerSec);
                var execTimeSpanSemaphore = new TimeSpanSemaphore(1, TimeSpan.FromSeconds(delay));
                // TODO: this messes up the unit test mock if throttle config is set
                var handler = ClientHandler != null ? new ThrottlingMessageHandler(execTimeSpanSemaphore, ClientHandler) : new ThrottlingMessageHandler(execTimeSpanSemaphore);
                return _client ?? (_client = new HttpClient(handler));
            }
        }
    }
}