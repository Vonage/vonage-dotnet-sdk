using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Moderation.MuteStream;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Moderation.MuteStream
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task MuteStream()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath(
                        "/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/session/flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN/stream/97425ae1-4722-4dbf-b395-6169f08ebab3/mute")
                    .WithHeader("Authorization", "Bearer *")
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.ModerationClient.MuteStreamAsync(MuteStreamRequest.Build()
                    .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                    .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
                    .WithStreamId("97425ae1-4722-4dbf-b395-6169f08ebab3")
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyResponse);
        }
    }
}