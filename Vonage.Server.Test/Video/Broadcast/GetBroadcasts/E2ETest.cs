using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Broadcast.GetBroadcasts;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.GetBroadcasts
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task GetBroadcasts()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath(
                        "/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/broadcast")
                    .WithParam("offset", "1000")
                    .WithParam("count", "100")
                    .WithParam("sessionId", "flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
                    .WithHeader("Authorization", "Bearer *")
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.BroadcastClient.GetBroadcastsAsync(GetBroadcastsRequest
                    .Build()
                    .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                    .WithCount(100)
                    .WithOffset(1000)
                    .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyBroadcasts);
        }
    }
}