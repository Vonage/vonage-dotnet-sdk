using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.AddStream;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.AddStream
{
    public class RequestBuilderTest
    {
        private readonly Guid applicationId;
        private readonly Guid archiveId;
        private readonly Guid streamId;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.archiveId = fixture.Create<Guid>();
            this.streamId = fixture.Create<Guid>();
        }

        [Fact]
        public void Build_ShouldReturnDisabledAudio_WhenUsingDisableAudio() =>
            AddStreamRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithArchiveId(this.archiveId)
                .WithStreamId(this.streamId)
                .DisableAudio()
                .Create()
                .Should()
                .BeSuccess(request => request.HasAudio.Should().Be(false));

        [Fact]
        public void Build_ShouldReturnDisabledVideo_WhenUsingDisableVideo() =>
            AddStreamRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithArchiveId(this.archiveId)
                .WithStreamId(this.streamId)
                .DisableVideo()
                .Create()
                .Should()
                .BeSuccess(request => request.HasVideo.Should().Be(false));

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            AddStreamRequest.Build()
                .WithApplicationId(Guid.Empty)
                .WithArchiveId(this.archiveId)
                .WithStreamId(this.streamId)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenArchiveIdIsEmpty() =>
            AddStreamRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithArchiveId(Guid.Empty)
                .WithStreamId(this.streamId)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ArchiveId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenStreamIdIsEmpty() =>
            AddStreamRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithArchiveId(this.archiveId)
                .WithStreamId(Guid.Empty)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("StreamId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnSuccess_WithDefaultValues() =>
            AddStreamRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithArchiveId(this.archiveId)
                .WithStreamId(this.streamId)
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.ArchiveId.Should().Be(this.archiveId);
                    request.StreamId.Should().Be(this.streamId);
                    request.HasAudio.Should().Be(true);
                    request.HasVideo.Should().Be(true);
                });
    }
}