using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Sessions.GetStream;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Server.Test.Video.Sessions.GetStream
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task GetStream()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath(
                        "/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/session/flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN/stream/stream99")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            await this.Helper.VonageClient.SessionClient.GetStreamAsync(GetStreamRequest.Build()
                    .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                    .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
                    .WithStreamId("stream99")
                    .Create())
                .Should()
                .BeSuccessAsync(SerializationTest.VerifyStream);
        }
    }
}