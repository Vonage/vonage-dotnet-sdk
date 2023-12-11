using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Vonage.Common.Test.TestHelpers;
using Vonage.Request;
using WireMock.Server;

namespace Vonage.Test.Unit.Video.TestHelpers
{
    internal class E2EHelper : IDisposable
    {
        private E2EHelper(string appSettingsKey, Credentials credentials, string authorizationHeaderValue)
        {
            this.ExpectedAuthorizationHeaderValue = authorizationHeaderValue;
            this.Server = WireMockServer.Start();
            var configuration = Configuration.FromConfiguration(new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {$"appSettings:{appSettingsKey}", this.Server.Url},
                }).Build());
            this.VonageClient = new TestVideoClient(credentials, configuration);
        }

        public string ExpectedAuthorizationHeaderValue { get; }

        public WireMockServer Server { get; }

        public void Dispose()
        {
            this.Server.Stop();
            this.Server.Dispose();
        }

        public static E2EHelper WithBearerCredentials(string appSettingsKey) =>
            new E2EHelper(appSettingsKey, CreateBearerCredentials(), "Bearer *");

        private static Credentials CreateBearerCredentials() =>
            Credentials.FromAppIdAndPrivateKey(Guid.NewGuid().ToString(), TokenHelper.GetKey());

        internal TestVideoClient VonageClient { get; }
    }
}