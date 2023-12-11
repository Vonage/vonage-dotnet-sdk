using System;
using System.Collections.Generic;
using FluentAssertions;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Serialization;
using Vonage.Server;
using Vonage.Video.Broadcast.StartBroadcast;
using Xunit;

namespace Vonage.Test.Unit.Video.Broadcast.StartBroadcast
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.BuildWithCamelCase());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<Vonage.Video.Broadcast.Broadcast>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(VerifyBroadcast);

        [Fact]
        public void ShouldSerialize() =>
            StartBroadcastRequest.Build()
                .WithApplicationId(Guid.NewGuid())
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
                .WithManualStreamMode()
                .WithMultiBroadcastTag("foo")
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        [Fact]
        public void ShouldSerializeWithDefaultValues() =>
            StartBroadcastRequest.Build()
                .WithApplicationId(Guid.NewGuid())
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
                .Create()
                .GetStringContent()
                .Should()
                .BeSuccess(this.helper.GetRequestJson());

        internal static void VerifyBroadcast(Vonage.Video.Broadcast.Broadcast success)
        {
            success.Id.Should().Be(new Guid("6c2bc486-0f4c-49cd-877c-7b609ec5dd19"));
            success.SessionId.Should().Be("2_MX4xMDBfjE0Mzc2NzY1NDgwMTJ-TjMzfn4");
            success.MultiBroadcastTag.Should().Be("broadcast_tag_provided");
            success.ApplicationId.Should().Be(new Guid("af4cba75-3e4b-48d2-806b-7b9aecef7988"));
            success.CreatedAt.Should().Be(1676124372);
            success.UpdatedAt.Should().Be(1676624372);
            success.MaxDuration.Should().Be(5400);
            success.MaxBitrate.Should().Be(2000000);
            success.Resolution.Should().Be("640x480");
            success.HasAudio.Should().Be(true);
            success.HasVideo.Should().Be(true);
            success.StreamMode.Should().Be("manual");
            success.Status.Should().Be(Vonage.Video.Broadcast.Broadcast.BroadcastStatus.Started);
            success.BroadcastUrls.Hls.Should()
                .Be(new Uri("http://server/fakepath/playlist.m3u8"));
            success.BroadcastUrls.Rtmp.Should().HaveCount(1);
            success.BroadcastUrls.Rtmp[0].Id.Should().Be(new Guid("432c916e-22fb-492e-b45b-b96ef3b90297"));
            success.BroadcastUrls.Rtmp[0].Status.Should()
                .Be(Vonage.Video.Broadcast.Broadcast.RtmpStatus.Live);
            success.BroadcastUrls.Rtmp[0].StreamName.Should().Be("myfooapp");
            success.BroadcastUrls.Rtmp[0].ServerUrl.Should().Be("rtmps://myfooserver/myfooapp");
            success.Settings.Hls.Dvr.Should().BeTrue();
            success.Settings.Hls.LowLatency.Should().BeTrue();
            success.Streams.Should().HaveCount(1);
            success.Streams[0].StreamId.Should().Be(new Guid("cbad214d-4712-40dd-88fd-82412bf66dd5"));
            success.Streams[0].HasVideo.Should().BeTrue();
            success.Streams[0].HasAudio.Should().BeTrue();
        }
    }
}