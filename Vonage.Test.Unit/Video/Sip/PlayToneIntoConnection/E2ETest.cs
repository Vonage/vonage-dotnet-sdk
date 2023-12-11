using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.Video.Sip.PlayToneIntoConnection;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.Video.Sip.PlayToneIntoConnection
{
    [Trait("Category", "E2E")]
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(E2ETest).Namespace)
        {
        }

        [Fact]
        public async Task PLayToneIntoConnection()
        {
            this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath(
                        "/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/session/flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN/connection/414ac9c2-9a6f-4f4b-aad4-202dbe7b1d8d/play-dtmf")
                    .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                    .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                    .UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
            await this.Helper.VonageClient.VideoClient.SipClient.PlayToneIntoConnectionAsync(
                    PlayToneIntoConnectionRequest.Build()
                        .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                        .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
                        .WithConnectionId("414ac9c2-9a6f-4f4b-aad4-202dbe7b1d8d")
                        .WithDigits("1713")
                        .Create())
                .Should()
                .BeSuccessAsync();
        }
    }
}