using System;
using FluentAssertions;
using Vonage.Common.Test;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Serialization;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.GetBroadcast
{
    public class GetBroadcastSerializationTest
    {
        private readonly SerializationTestHelper helper;

        public GetBroadcastSerializationTest() =>
            this.helper = new SerializationTestHelper(typeof(GetBroadcastSerializationTest).Namespace,
                JsonSerializerBuilder.Build());

        [Fact]
        public void ShouldDeserialize200() =>
            this.helper.Serializer
                .DeserializeObject<Server.Video.Broadcast.Common.Broadcast>(this.helper.GetResponseJson())
                .Should()
                .BeSuccess(success =>
                {
                    success.Id.Should().Be("1748b7070a81464c9759c46ad10d3734");
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
                    success.Status.Should().Be(Server.Video.Broadcast.Common.Broadcast.BroadcastStatus.Started);
                    success.BroadcastUrls.Hls.Should()
                        .Be(new Uri("https://example.com/movie1/fileSequenceA.ts"));
                    success.BroadcastUrls.Rtmp.Should().HaveCount(1);
                    success.BroadcastUrls.Rtmp[0].Id.Should().Be("abc123");
                    success.BroadcastUrls.Rtmp[0].Status.Should().Be(Server.Video.Broadcast.Common.Broadcast
                        .BroadcastUrl.RtmpStream.RtmpStreamStatus.Live);
                    success.BroadcastUrls.Rtmp[0].StreamName.Should().Be("abc147");
                    success.BroadcastUrls.Rtmp[0].ServerUrl.Should().Be("abc789");
                    success.Settings.Hls.Dvr.Should().BeTrue();
                    success.Settings.Hls.LowLatency.Should().BeTrue();
                    success.Streams.Should().HaveCount(1);
                    success.Streams[0].StreamId.Should().Be("abc123");
                    success.Streams[0].HasVideo.Should().BeTrue();
                    success.Streams[0].HasAudio.Should().BeTrue();
                });
    }
}