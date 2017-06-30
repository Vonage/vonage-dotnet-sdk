using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nexmo.Api.Request;

namespace Nexmo.Api
{
    public sealed class Configuration
    {
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Configuration()
        {
        }

        private Configuration()
        {
            _serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider()
            ;
            var loggerFactory = _serviceProvider.GetService<ILoggerFactory>();

            var loggingConfiguration = new ConfigurationBuilder()
                .AddJsonFile("logging.json", true, true)
                .Build();

            loggerFactory.AddConsole(loggingConfiguration);

            var configLogger = loggerFactory.CreateLogger<Configuration>();
            ApiLogger = loggerFactory.CreateLogger("Nexmo.Api");
            AuthenticationLogger = loggerFactory.CreateLogger("Nexmo.Api.Authentication");

            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "appSettings:Nexmo.Url.Rest", "https://rest.nexmo.com"},
                    { "appSettings:Nexmo.Url.Api", "https://api.nexmo.com"}
                })
                .AddJsonFile("settings.json", true, true)
                .AddJsonFile("appsettings.json", true, true)
            ;

            Settings = builder.Build();

            // verify we have a minimum amount of configuration

            var authCapabilities = new List<string>();

            if (!string.IsNullOrWhiteSpace(Settings["appSettings:Nexmo.api_key"]) &&
                !string.IsNullOrWhiteSpace(Settings["appSettings:Nexmo.api_secret"]))
            {
                authCapabilities.Add("Key/Secret");
            }
            if (!string.IsNullOrWhiteSpace(Settings["appSettings:Nexmo.security_secret"]))
            {
                authCapabilities.Add("Security/Signing");
            }
            if (!string.IsNullOrWhiteSpace(Settings["appSettings:Nexmo.Application.Id"]) &&
                !string.IsNullOrWhiteSpace(Settings["appSettings:Nexmo.Application.Key"]))
            {
                authCapabilities.Add("Application");
            }

            if (authCapabilities.Count == 0)
            {
                configLogger.LogInformation("No authentication found via configuration. Remember to provide your own.");
            }
            else
            {
                configLogger.LogInformation("Available authentication: {0}", string.Join(",", authCapabilities));
            }
        }

        private HttpClient _client;
        private readonly IServiceProvider _serviceProvider;

        internal ILogger ApiLogger;
        internal ILogger AuthenticationLogger;

        // not convinced we want/need to expose this
        //public ILoggerFactory Logger => _serviceProvider.GetService<ILoggerFactory>();

        public static Configuration Instance { get; } = new Configuration();

        public IConfiguration Settings { get; }
        
        public HttpMessageHandler ClientHandler { get; set; }

        public HttpClient Client
        {
            get
            {
                var reqPerSec = Instance.Settings["appSettings:Nexmo.Api.RequestsPerSecond"];
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