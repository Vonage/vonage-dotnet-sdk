using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Signaling;
using Vonage.Video.Signaling.SendSignal;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Video.Signaling.SendSignal;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task SendSignal()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath(
                    "/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/session/flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN/connection/f0f01910-8797-4a22-aeb3-fcd5edb55ebe/signal")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK));
        await this.Helper.VonageClient.VideoClient.SignalingClient.SendSignalAsync(SendSignalRequest.Build()
                .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                .WithSessionId("flR1ZSBPY3QgMjkgMTI6MTM6MjMgUERUIDIwMTN")
                .WithConnectionId("f0f01910-8797-4a22-aeb3-fcd5edb55ebe")
                .WithContent(new SignalContent("chat", "Text of the chat message"))
                .Create())
            .Should()
            .BeSuccessAsync();
    }
}