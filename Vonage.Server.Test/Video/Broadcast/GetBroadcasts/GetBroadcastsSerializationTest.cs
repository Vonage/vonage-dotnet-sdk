using System;
using FluentAssertions;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Serialization;
using Vonage.Server.Video.Broadcast.GetBroadcasts;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.GetBroadcasts
{
    public class GetBroadcastsSerializationTest
    {
        private readonly SerializationTestHelper helper;

        public GetBroadcastsSerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(GetBroadcastsSerializationTest).Namespace,
                JsonSerializerBuilder.Build());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<GetBroadcastsResponse>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Count.Should().Be(1);
                    success.Items.Should().HaveCount(1);
                    success.Items[0].Id.Should().Be("1748b7070a81464c9759c46ad10d3734");
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
                        .Be(Server.Video.Broadcast.Common.Broadcast.BroadcastStatus.Started);
                    success.Items[0].BroadcastUrls.Hls.Should()
                        .Be(new Uri("https://example.com/movie1/fileSequenceA.ts"));
                    success.Items[0].BroadcastUrls.Rtmp.Should().HaveCount(1);
                    success.Items[0].BroadcastUrls.Rtmp[0].Id.Should().Be("abc123");
                    success.Items[0].BroadcastUrls.Rtmp[0].Status.Should().Be(Server.Video.Broadcast.Common.Broadcast
                        .BroadcastUrl.RtmpStream.RtmpStreamStatus.Live);
                    success.Items[0].BroadcastUrls.Rtmp[0].StreamName.Should().Be("abc147");
                    success.Items[0].BroadcastUrls.Rtmp[0].ServerUrl.Should().Be("abc789");
                    success.Items[0].Settings.Hls.Dvr.Should().BeTrue();
                    success.Items[0].Settings.Hls.LowLatency.Should().BeTrue();
                    success.Items[0].Streams.Should().HaveCount(1);
                    success.Items[0].Streams[0].StreamId.Should().Be("abc123");
                    success.Items[0].Streams[0].HasVideo.Should().BeTrue();
                    success.Items[0].Streams[0].HasAudio.Should().BeTrue();
                });
    }
}