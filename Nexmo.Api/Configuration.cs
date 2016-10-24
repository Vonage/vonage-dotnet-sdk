using System.Net.Http;
using Microsoft.Extensions.Configuration;

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
            .AddJsonFile("settings.json", false, true);

            Settings = builder.Build();
        }

        private HttpClient _client;

        public static Configuration Instance { get; } = new Configuration();

        public IConfiguration Settings { get; private set; }
        public HttpMessageHandler ClientHandler { get; set; }

        public HttpClient Client => _client ?? (_client = ClientHandler == null ? new HttpClient() : new HttpClient(ClientHandler));
    }
}