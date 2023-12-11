using System;
using System.Text.Json;
using FluentAssertions;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Serialization;
using Vonage.Video.Broadcast.GetBroadcasts;
using Xunit;

namespace Vonage.Test.Unit.Video.Broadcast.GetBroadcasts
{
    public class SerializationTest
    {
        private readonly SerializationTestHelper helper;

        public SerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(SerializationTest).Namespace,
                JsonSerializerBuilder.Build(JsonNamingPolicy.CamelCase));

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<GetBroadcastsResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(VerifyBroadcasts);

        internal static void VerifyBroadcasts(GetBroadcastsResponse success)
        {
            success.Count.Should().Be(1);
            success.Items.Should().HaveCount(1);
            success.Items[0].Id.Should().Be(new Guid("6c2bc486-0f4c-49cd-877c-7b609ec5dd19"));
            success.Items[0].SessionId.Should().Be("2_MX4xMDBfjE0Mzc2NzY1NDgwMTJ-TjMzfn4");
            success.Items[0].MultiBroadcastTag.Should().Be("broadcast_tag_provided");
            success.Items[0].ApplicationId.Should().Be(new Guid("af4cba75-3e4b-48d2-806b-7b9aecef7988"));
            success.Items[0].CreatedAt.Should().Be(1676124372);
            success.Items[0].UpdatedAt.Should().Be(1676624372);
            success.Items[0].MaxDuration.Should().Be(5400);
            success.Items[0].MaxBitrate.Should().Be(2000000);
            success.Items[0].Resolution.Should().Be("640x480");
            success.Items[0].HasAudio.Should().Be(true);
            success.Items[0].HasVideo.Should().Be(true);
            success.Items[0].StreamMode.Should().Be("manual");
            success.Items[0].Status.Should()
                .Be(Vonage.Video.Broadcast.Broadcast.BroadcastStatus.Started);
            success.Items[0].BroadcastUrls.Hls.Should()
                .Be(new Uri("http://server/fakepath/playlist.m3u8"));
            success.Items[0].BroadcastUrls.Rtmp.Should().HaveCount(1);
            success.Items[0].BroadcastUrls.Rtmp[0].Id.Should()
                .Be(new Guid("432c916e-22fb-492e-b45b-b96ef3b90297"));
            success.Items[0].BroadcastUrls.Rtmp[0].Status.Should()
                .Be(Vonage.Video.Broadcast.Broadcast.RtmpStatus.Live);
            success.Items[0].BroadcastUrls.Rtmp[0].StreamName.Should().Be("myfooapp");
            success.Items[0].BroadcastUrls.Rtmp[0].ServerUrl.Should().Be("rtmps://myfooserver/myfooapp");
            success.Items[0].Settings.Hls.Dvr.Should().BeTrue();
            success.Items[0].Settings.Hls.LowLatency.Should().BeTrue();
            success.Items[0].Streams.Should().HaveCount(1);
            success.Items[0].Streams[0].StreamId.Should().Be(new Guid("cbad214d-4712-40dd-88fd-82412bf66dd5"));
            success.Items[0].Streams[0].HasVideo.Should().BeTrue();
            success.Items[0].Streams[0].HasAudio.Should().BeTrue();
        }
    }
}