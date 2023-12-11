using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Video.Moderation.DisconnectConnection;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Video.Moderation.DisconnectConnection
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task DisconnectConnection()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath(
                        "/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/session/flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN/connection/97425ae1-4722-4dbf-b395-6169f08ebab3")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            await this.Helper.VonageClient.VideoClient.ModerationClient.DisconnectConnectionAsync(DisconnectConnectionRequest
                    .Build()
                    .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                    .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
                    .WithConnectionId("97425ae1-4722-4dbf-b395-6169f08ebab3")
                    .Create())
                .Should()
                .BeSuccessAsync();
        }
    }
}