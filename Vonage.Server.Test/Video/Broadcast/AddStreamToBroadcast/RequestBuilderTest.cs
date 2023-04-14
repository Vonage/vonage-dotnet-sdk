using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Broadcast.AddStreamToBroadcast;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.AddStreamToBroadcast
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
            AddStreamToBroadcastRequestBuilder.Build()
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
            AddStreamToBroadcastRequestBuilder.Build()
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
            AddStreamToBroadcastRequestBuilder.Build()
                .WithApplicationId(Guid.Empty)
                .WithBroadcastId(this.broadcastId)
                .WithStreamId(this.streamId)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenBroadcastIdIsEmpty() =>
            AddStreamToBroadcastRequestBuilder.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(Guid.Empty)
                .WithStreamId(this.streamId)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("BroadcastId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenStreamIdIsEmpty() =>
            AddStreamToBroadcastRequestBuilder.Build()
                .WithApplicationId(this.applicationId)
                .WithBroadcastId(this.broadcastId)
                .WithStreamId(Guid.Empty)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("StreamId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnSuccess_WithMandatoryValues() =>
            AddStreamToBroadcastRequestBuilder.Build()
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