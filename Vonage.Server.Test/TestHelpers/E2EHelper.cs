using System;
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
            var configuration = new Configuration
            {
                Settings =
                {
                    [$"appSettings:{appSettingsKey}"] = this.Server.Url,
                },
            };
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