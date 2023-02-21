using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Common;
using Vonage.Server.Video.Archives.CreateArchive;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.CreateArchive
{
    public class CreateArchiveRequestBuilderTest
    {
        private readonly ArchiveLayout layout;
        private readonly Guid applicationId;
        private readonly OutputMode outputMode;
        private readonly RenderResolution resolution;
        private readonly StreamMode streamMode;
        private readonly string sessionId;
        private readonly string name;

        public CreateArchiveRequestBuilderTest()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            this.applicationId = fixture.Create<Guid>();
            this.sessionId = fixture.Create<string>();
            this.name = fixture.Create<string>();
            this.streamMode = fixture.Create<StreamMode>();
            this.resolution = fixture.Create<RenderResolution>();
            this.outputMode = fixture.Create<OutputMode>();
            this.layout = fixture.Create<ArchiveLayout>();
        }

        [Fact]
        public void Build_ShouldAssignArchiveLayout_GivenWithArchiveLayoutIsUsed() =>
            CreateArchiveRequestBuilder.Build(this.applicationId, this.sessionId)
                .WithArchiveLayout(this.layout)
                .Create()
                .Should()
                .BeSuccess(request => request.OutputMode.Should().Be(this.outputMode));

        [Fact]
        public void Build_ShouldAssignName_GivenWithNameIsUsed() =>
            CreateArchiveRequestBuilder.Build(this.applicationId, this.sessionId)
                .WithName(this.name)
                .Create()
                .Should()
                .BeSuccess(request => request.Name.Should().BeSome(this.name));

        [Fact]
        public void Build_ShouldAssignOutputMode_GivenWithOutputModeIsUsed() =>
            CreateArchiveRequestBuilder.Build(this.applicationId, this.sessionId)
                .WithOutputMode(this.outputMode)
                .Create()
                .Should()
                .BeSuccess(request => request.OutputMode.Should().Be(this.outputMode));

        [Fact]
        public void Build_ShouldAssignResolution_GivenWithRenderResolutionIsUsed() =>
            CreateArchiveRequestBuilder.Build(this.applicationId, this.sessionId)
                .WithRenderResolution(this.resolution)
                .Create()
                .Should()
                .BeSuccess(request => request.Resolution.Should().Be(this.resolution));

        [Fact]
        public void Build_ShouldAssignStreamMode_GivenWithStreamModeIsUsed() =>
            CreateArchiveRequestBuilder.Build(this.applicationId, this.sessionId)
                .WithStreamMode(this.streamMode)
                .Create()
                .Should()
                .BeSuccess(request => request.StreamMode.Should().Be(this.streamMode));

        [Fact]
        public void Build_ShouldReturnSuccess_WithDefaultValues() =>
            CreateArchiveRequestBuilder.Build(this.applicationId, this.sessionId)
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.HasAudio.Should().BeTrue();
                    request.HasVideo.Should().BeTrue();
                    request.Name.Should().BeNone();
                    request.OutputMode.Should().Be(OutputMode.Composed);
                    request.Resolution.Should().Be(RenderResolution.StandardDefinitionLandscape);
                    request.StreamMode.Should().Be(StreamMode.Auto);
                    request.Layout.Should().Be(default(ArchiveLayout));
                });

        [Fact]
        public void Parse_ShouldReturnDisabledAudio_WhenUsingDisableAudio() =>
            CreateArchiveRequestBuilder.Build(this.applicationId, this.sessionId)
                .DisableAudio()
                .Create()
                .Should()
                .BeSuccess(request => request.HasAudio.Should().Be(false));

        [Fact]
        public void Parse_ShouldReturnDisabledVideo_WhenUsingDisableVideo() =>
            CreateArchiveRequestBuilder.Build(this.applicationId, this.sessionId)
                .DisableVideo()
                .Create()
                .Should()
                .BeSuccess(request => request.HasVideo.Should().Be(false));

        [Fact]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            CreateArchiveRequestBuilder.Build(Guid.Empty, this.sessionId)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            CreateArchiveRequestBuilder.Build(this.applicationId, value)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));
    }
}