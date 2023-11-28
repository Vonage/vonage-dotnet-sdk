using System;
using Vonage.Common;
using Vonage.Common.Test.TestHelpers;
using Vonage.Request;
using WireMock.Server;

namespace Vonage.Test.Unit.TestHelpers
{
    internal class E2EHelper : IDisposable
    {
        private E2EHelper(string appSettingsKey, Credentials credentials)
        {
            this.Server = WireMockServer.Start();
            var configuration = new Configuration
            {
                Settings =
                {
                    [$"vonage:{appSettingsKey}"] = this.Server.Url,
                },
            };
            this.VonageClient = new VonageClient(credentials, configuration, new TimeProvider());
        }

        public WireMockServer Server { get; }
        public VonageClient VonageClient { get; }

        public void Dispose()
        {
            this.Server.Stop();
            this.Server.Dispose();
        }

        public static E2EHelper WithBasicCredentials(string appSettingsKey) =>
            new E2EHelper(appSettingsKey, CreateBasicCredentials());

        public static E2EHelper WithBearerCredentials(string appSettingsKey) =>
            new E2EHelper(appSettingsKey, CreateBearerCredentials());

        private static Credentials CreateBasicCredentials() => Credentials.FromApiKeyAndSecret("790fc5e5", "Aa3456789");

        private static Credentials CreateBearerCredentials() => Credentials.FromAppIdAndPrivateKey(
            Guid.NewGuid().ToString(),
            TokenHelper.GetKey());
    }
}