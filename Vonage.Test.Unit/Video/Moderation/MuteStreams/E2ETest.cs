using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.Video.Moderation.MuteStreams;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Video.Moderation.MuteStreams
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task MuteStreams()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath(
                        "/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/session/flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN/mute")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.VideoClient.ModerationClient.MuteStreamsAsync(MuteStreamsRequest.Build()
                    .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                    .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
                    .WithConfiguration(new MuteStreamsRequest.MuteStreamsConfiguration(true,
                        new[] {"excludedStream1", "excludedStream2"}))
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyResponse);
        }
    }
}