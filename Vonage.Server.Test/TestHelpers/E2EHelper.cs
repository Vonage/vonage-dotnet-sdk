using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Vonage.Request;
using Vonage.Server.Video;
using WireMock.Server;

namespace Vonage.Server.Test.TestHelpers
{
    public class E2EHelper : IDisposable
    {
        private E2EHelper(string appSettingsKey, Credentials credentials)
        {
            this.Server = WireMockServer.Start();
            var configuration = Configuration.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {$"appSettings:{appSettingsKey}", this.Server.Url},
                }).Build());
            this.VonageClient = new VideoClient(credentials, configuration);
        }

        public WireMockServer Server { get; }
        public VideoClient VonageClient { get; }

        public void Dispose()
        {
            this.Server.Stop();
            this.Server.Dispose();
        }

        public static E2EHelper WithBearerCredentials(string appSettingsKey) =>
            new E2EHelper(appSettingsKey, CreateBearerCredentials());

        private static Credentials CreateBearerCredentials() => Credentials.FromAppIdAndPrivateKey(
            Guid.NewGuid().ToString(),
            Environment.GetEnvironmentVariable("Vonage.Test.RsaPrivateKey"));
    }
}