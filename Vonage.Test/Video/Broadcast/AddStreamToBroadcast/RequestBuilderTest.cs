using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Vonage.Video.Broadcast.AddStreamToBroadcast;
using Xunit;

namespace Vonage.Test.Video.Broadcast.AddStreamToBroadcast
{
    public class RequestBuilderTest
    {
        private readonly Guid applicationId;
        private readonly Guid streamId;
        private readonly Guid broadcastId;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.broadcastId = fixture.Create<Guid>();
            this.streamId = fixture.Create<Guid>();
        }

        [Fact]
        public void Build_ShouldDisableAudio_GivenWithDisabledAudioIsUsed() =>
            AddStreamToBroadcastRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(this.broadcastId)
                .WithStreamId(this.streamId)
                .WithDisabledAudio()
                .Create()
                .Map(request => request.HasAudio)
                .Should()
                .BeSuccess(false);

        [Fact]
        public void Build_ShouldDisableVideo_GivenWithDisabledVideoIsUsed() =>
            AddStreamToBroadcastRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(this.broadcastId)
                .WithStreamId(this.streamId)
                .WithDisabledVideo()
                .Create()
                .Map(request => request.HasVideo)
                .Should()
                .BeSuccess(false);

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            AddStreamToBroadcastRequest.Build()
                .WithApplicationId(Guid.Empty)
                .WithBroadcastId(this.broadcastId)
                .WithStreamId(this.streamId)
                .Create()
                .Should()
                .BeParsingFailure("ApplicationId cannot be empty.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenBroadcastIdIsEmpty() =>
            AddStreamToBroadcastRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(Guid.Empty)
                .WithStreamId(this.streamId)
                .Create()
                .Should()
                .BeParsingFailure("BroadcastId cannot be empty.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenStreamIdIsEmpty() =>
            AddStreamToBroadcastRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(this.broadcastId)
                .WithStreamId(Guid.Empty)
                .Create()
                .Should()
                .BeParsingFailure("StreamId cannot be empty.");

        [Fact]
        public void Build_ShouldReturnSuccess_WithMandatoryValues() =>
            AddStreamToBroadcastRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(this.broadcastId)
                .WithStreamId(this.streamId)
                .Create()
                .Should()
                .BeSuccess(success =>
                {
                    success.ApplicationId.Should().Be(this.applicationId);
                    success.BroadcastId.Should().Be(this.broadcastId);
                    success.StreamId.Should().Be(this.streamId);
                    success.HasAudio.Should().BeTrue();
                    success.HasVideo.Should().BeTrue();
                });
    }
}