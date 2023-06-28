using System;
using Vonage.Request;
using WireMock.Server;

namespace Vonage.Test.Unit.TestHelpers
{
    internal class E2EHelper : IDisposable
    {
        public WireMockServer Server { get; }
        public VonageClient VonageClient { get; }

        /// <summary>
        ///     Initializes a helper for E2E testing.
        /// </summary>
        /// <param name="appSettingsKey">The appSettings key to override with Wiremock's server url.</param>
        /// <param name="credentials">Expected credentials.</param>
        public E2EHelper(string appSettingsKey, Credentials credentials)
        {
            this.Server = WireMockServer.Start();
            var configuration = new Configuration
            {
                Settings =
                {
                    [$"appSettings:{appSettingsKey}"] = this.Server.Url,
                },
            };
            this.VonageClient = new VonageClient(credentials, configuration);
        }

        public void Dispose()
        {
            this.Server.Stop();
            this.Server.Dispose();
        }
    }
}