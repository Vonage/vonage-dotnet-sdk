using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Test.Unit.TestHelpers;
using Vonage.VerifyV2.Cancel;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.VerifyV2.Cancel
{
    [Trait("Category", "E2E")]
    public class E2ETest
    {
        private readonly E2EHelper helper;

        public E2ETest() => this.helper = E2EHelper.WithBearerCredentials("Vonage.Url.Api");

        [Fact]
        public async Task CancelVerification()
        {
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v2/verify/68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb")
                    .WithHeader("Authorization", "Bearer *")
                    .UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            var result =
                await this.helper.VonageClient.VerifyV2Client.CancelAsync(
                    CancelRequest.Parse(Guid.Parse("68c2b32e-55ba-4a8e-b3fa-43b3ae6cd1fb")));
            result.Should().BeSuccess();
        }
    }
}