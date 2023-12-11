using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Test.Extensions;
using Vonage.Video.Archives.CreateArchive;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.CreateArchive
{
    public class RequestBuilderTest
    {
        private readonly Guid applicationId;
        private readonly Layout layout;
        private readonly OutputMode outputMode;
        private readonly RenderResolution resolution;
        private readonly StreamMode streamMode;
        private readonly string sessionId;
        private readonly string name;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            fixture.Customize(new SupportMutableValueTypesCustomization());
            this.applicationId = fixture.Create<Guid>();
            this.sessionId = fixture.Create<string>();
            this.name = fixture.Create<string>();
            this.streamMode = fixture.Create<StreamMode>();
            this.resolution = fixture.Create<RenderResolution>();
            this.outputMode = fixture.Create<OutputMode>();
            this.layout = fixture.Create<Layout>();
        }

        [Fact]
        public void Build_ShouldAssignArchiveLayout_GivenWithArchiveLayoutIsUsed() =>
            CreateArchiveRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithArchiveLayout(this.layout)
                .Create()
                .Should()
                .BeSuccess(request => request.OutputMode.Should().Be(this.outputMode));

        [Fact]
        public void Build_ShouldAssignName_GivenWithNameIsUsed() =>
            CreateArchiveRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithName(this.name)
                .Create()
                .Should()
                .BeSuccess(request => request.Name.Should().BeSome(this.name));

        [Fact]
        public void Build_ShouldAssignOutputMode_GivenWithOutputModeIsUsed() =>
            CreateArchiveRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithOutputMode(this.outputMode)
                .Create()
                .Should()
                .BeSuccess(request => request.OutputMode.Should().Be(this.outputMode));

        [Fact]
        public void Build_ShouldAssignResolution_GivenWithRenderResolutionIsUsed() =>
            CreateArchiveRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithRenderResolution(this.resolution)
                .Create()
                .Should()
                .BeSuccess(request => request.Resolution.Should().Be(this.resolution));

        [Fact]
        public void Build_ShouldAssignStreamMode_GivenWithStreamModeIsUsed() =>
            CreateArchiveRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithStreamMode(this.streamMode)
                .Create()
                .Should()
                .BeSuccess(request => request.StreamMode.Should().Be(this.streamMode));

        [Fact]
        public void Build_ShouldReturnDisabledAudio_WhenUsingDisableAudio() =>
            CreateArchiveRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .DisableAudio()
                .Create()
                .Should()
                .BeSuccess(request => request.HasAudio.Should().Be(false));

        [Fact]
        public void Build_ShouldReturnDisabledVideo_WhenUsingDisableVideo() =>
            CreateArchiveRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .DisableVideo()
                .Create()
                .Should()
                .BeSuccess(request => request.HasVideo.Should().Be(false));

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            CreateArchiveRequest.Build()
                .WithApplicationId(Guid.Empty)
                .WithSessionId(this.sessionId)
                .Create()
                .Should()
                .BeParsingFailure("ApplicationId cannot be empty.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            CreateArchiveRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(value)
                .Create()
                .Should()
                .BeParsingFailure("SessionId cannot be null or whitespace.");

        [Fact]
        public void Build_ShouldReturnSuccess_WithDefaultValues() =>
            CreateArchiveRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
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
                    request.Layout.Should().Be(default(Layout));
                });
    }
}