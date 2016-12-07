using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Nexmo.Api.ConfigurationExtensions;

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
            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "appSettings:Nexmo.Url.Rest", "https://rest.nexmo.com"},
                    { "appSettings:Nexmo.Url.Api", "https://api.nexmo.com"}
                })
                .AddConfigFile("web.config", true)
                .AddConfigFile("app.config", true)
                .AddConfigFile($"{System.IO.Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName)}.config", true)
                .AddJsonFile("settings.json", true, true)
            ;

            Settings = builder.Build();
        }

        private HttpClient _client;

        public static Configuration Instance { get; } = new Configuration();

        public IConfiguration Settings { get; private set; }
        public HttpMessageHandler ClientHandler { get; set; }

        public HttpClient Client => _client ?? (_client = ClientHandler == null ? new HttpClient() : new HttpClient(ClientHandler));
    }
}