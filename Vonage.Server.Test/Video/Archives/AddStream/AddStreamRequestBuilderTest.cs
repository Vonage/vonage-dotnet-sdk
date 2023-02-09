using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.AddStream;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.AddStream
{
    public class AddStreamRequestBuilderTest
    {
        private readonly Guid applicationId;
        private readonly Guid archiveId;
        private readonly Guid streamId;

        public AddStreamRequestBuilderTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.archiveId = fixture.Create<Guid>();
            this.streamId = fixture.Create<Guid>();
        }

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            AddStreamRequestBuilder.Build(Guid.Empty, this.archiveId, this.streamId)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenArchiveIdIsEmpty() =>
            AddStreamRequestBuilder.Build(this.applicationId, Guid.Empty, this.streamId)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ArchiveId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenStreamIdIsEmpty() =>
            AddStreamRequestBuilder.Build(this.applicationId, this.archiveId, Guid.Empty)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("StreamId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnSuccess_WithDefaultValues() =>
            AddStreamRequestBuilder.Build(this.applicationId, this.archiveId, this.streamId)
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

        [Fact]
        public void Parse_ShouldReturnDisabledAudio_WhenUsingDisableAudio() =>
            AddStreamRequestBuilder.Build(this.applicationId, this.archiveId, this.streamId)
                .DisableAudio()
                .Create()
                .Should()
                .BeSuccess(request => request.HasAudio.Should().Be(false));

        [Fact]
        public void Parse_ShouldReturnDisabledVideo_WhenUsingDisableVideo() =>
            AddStreamRequestBuilder.Build(this.applicationId, this.archiveId, this.streamId)
                .DisableVideo()
                .Create()
                .Should()
                .BeSuccess(request => request.HasVideo.Should().Be(false));
    }
}