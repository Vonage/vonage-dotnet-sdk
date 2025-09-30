#region
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Vonage.Server;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Broadcast.StartBroadcast;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.Video.Broadcast.StartBroadcast;

[Trait("Category", "E2E")]
public class E2ETest() : E2EBase(typeof(E2ETest).Namespace)
{
    [Fact]
    public async Task StartBroadcast()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/broadcast")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VideoClient.BroadcastClient.StartBroadcastsAsync(StartBroadcastRequest
                .Build()
                .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                .WithSessionId("2_MX4xMDBfjE0Mzc2NzY1NDgwMTJ-TjMzfn4")
                .WithLayout(new Layout(LayoutType.HorizontalPresentation,
                    null, LayoutType.BestFit))
                .WithOutputs(new StartBroadcastRequest.BroadcastOutput
                {
                    Hls = new Vonage.Video.Broadcast.Broadcast.HlsSettings(false, true),
                    Streams = new List<StartBroadcastRequest.BroadcastOutput.Stream>
                    {
                        new StartBroadcastRequest.BroadcastOutput.Stream(
                            new Guid("feab5ea7-951f-4dbb-b2f6-3195c3b4b062"), "rtmps://myfooserver/myfooapp",
                            "myfoostream"),
                    }.ToArray(),
                })
                .WithResolution(RenderResolution.FullHighDefinitionLandscape)
                .WithMaxBitrate(500)
                .WithMaxDuration(20000)
                .WithStreamMode(StreamMode.Manual)
                .WithMultiBroadcastTag("foo")
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyBroadcast);
    }

    [Fact]
    public async Task StartDefaultBroadcast()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/v2/project/5e782e3b-9f63-426f-bd2e-b7d618d546cd/broadcast")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.Serialization.GetRequestJson(nameof(SerializationTest
                    .ShouldSerializeWithDefaultValues)))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.VideoClient.BroadcastClient.StartBroadcastsAsync(StartBroadcastRequest
                .Build()
                .WithApplicationId(Guid.Parse("5e782e3b-9f63-426f-bd2e-b7d618d546cd"))
                .WithSessionId("2_MX4xMDBfjE0Mzc2NzY1NDgwMTJ-TjMzfn4")
                .WithLayout(new Layout(null,
                    "the layout stylesheet (only used with type == custom)", LayoutType.Custom))
                .WithOutputs(new StartBroadcastRequest.BroadcastOutput
                {
                    Streams = new List<StartBroadcastRequest.BroadcastOutput.Stream>
                    {
                        new StartBroadcastRequest.BroadcastOutput.Stream(
                            new Guid("feab5ea7-951f-4dbb-b2f6-3195c3b4b062"), "rtmps://myfooserver/myfooapp",
                            "myfoostream"),
                    }.ToArray(),
                })
                .Create())
            .Should()
            .BeSuccessAsync(SerializationTest.VerifyBroadcast);
    }
}