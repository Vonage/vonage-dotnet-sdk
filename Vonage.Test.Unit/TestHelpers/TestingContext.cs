using System;
using Vonage.Common.Test.TestHelpers;
using Vonage.Request;
using WireMock.Server;
using TimeProvider = Vonage.Common.TimeProvider;

namespace Vonage.Test.Unit.TestHelpers
{
    internal class TestingContext : IDisposable
    {
        private TestingContext(string appSettingsKey, Credentials credentials, string authorizationHeaderValue)
        {
            this.ExpectedAuthorizationHeaderValue = authorizationHeaderValue;
            this.Server = WireMockServer.Start();
            var configuration = new Configuration
            {
                Settings =
                {
                    [$"appSettings:{appSettingsKey}"] = this.Server.Url,
                },
            };
            this.VonageClient = new VonageClient(credentials, configuration, new TimeProvider());
        }

        public string ExpectedAuthorizationHeaderValue { get; }
        public WireMockServer Server { get; }
        public VonageClient VonageClient { get; }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static TestingContext WithBasicCredentials(string appSettingsKey) =>
            new TestingContext(appSettingsKey, CreateBasicCredentials(), "Basic NzkwZmM1ZTU6QWEzNDU2Nzg5");

        public static TestingContext WithBearerCredentials(string appSettingsKey) =>
            new TestingContext(appSettingsKey, CreateBearerCredentials(), "Bearer *");

        private static Credentials CreateBasicCredentials() => Credentials.FromApiKeyAndSecret("790fc5e5", "Aa3456789");

        private static Credentials CreateBearerCredentials() => Credentials.FromAppIdAndPrivateKey(
            Guid.NewGuid().ToString(),
            TokenHelper.GetKey());

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            this.Server.Stop();
            this.Server.Dispose();
        }
    }
}