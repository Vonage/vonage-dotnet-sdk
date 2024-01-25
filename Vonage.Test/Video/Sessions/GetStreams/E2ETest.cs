using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Sessions.GetStreams;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Video.Sessions.GetStreams;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task GetStreams()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath(
                    "/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/session/flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN/stream")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .UsingGet())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VideoClient.SessionClient.GetStreamsAsync(GetStreamsRequest.Build()
                .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyStream);
    }
}