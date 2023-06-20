using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Request;
using Vonage.VerifyV2.Cancel;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.Cancel
{
    public class E2ETest
    {
        private readonly VonageClient vonageClient;
        private readonly WireMockServer server;

        public E2ETest()
        {
            this.server = WireMockServer.Start();
            var configuration = new Configuration
            {
                Settings =
                {
                    ["appSettings:Vonage.Url.Api"] = this.server.Url,
                },
            };
            this.vonageClient = new VonageClient(Credentials.FromAppIdAndPrivateKey(Guid.NewGuid().ToString(),
                Environment.GetEnvironmentVariable("Vonage.Test.RsaPrivateKey")), configuration);
        }

        [Fact]
        public async Task CancelVerificationRequest()
        {
            var requestId = Guid.NewGuid();
            this.server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithUrl($"{this.server.Url}/v2/verify/{requestId}")
                    .WithHeader("Authorization", "Bearer *")
                    .UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            var result = await this.vonageClient.VerifyV2Client.CancelAsync(CancelRequest.Parse(requestId));
            result.Should().BeSuccess();
        }
    }
}