using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.Common;
using Vonage.Server.Video.Broadcast.StartBroadcast;
using Xunit;

namespace Vonage.Server.Test.Video.Broadcast.StartBroadcast
{
    public class StartBroadcastRequestBuilderTest
    {
        private readonly Guid applicationId;
        private readonly string sessionId;
        private readonly string layout;
        private readonly string outputs;

        public StartBroadcastRequestBuilderTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.sessionId = fixture.Create<string>();
            this.layout = fixture.Create<string>();
            this.outputs = fixture.Create<string>();
        }

        [Fact]
        public void Build_ShouldAssignMaxBitrate_GivenWithMaxBitrateIsUsed() =>
            this.BuildWithMandatoryValues()
                .WithMaxBitrate(500)
                .Create()
                .Map(request => request.MaxBitrate)
                .Should()
                .BeSuccess(500);

        [Fact]
        public void Build_ShouldAssignMaxDuration_GivenMaxDurationIsMaximumValue() =>
            this.BuildWithMandatoryValues()
                .WithMaxDuration(36000)
                .Create()
                .Map(request => request.MaxDuration)
                .Should()
                .BeSuccess(36000);

        [Fact]
        public void Build_ShouldAssignMaxDuration_GivenMaxDurationIsMinimumValue() =>
            this.BuildWithMandatoryValues()
                .WithMaxDuration(60)
                .Create()
                .Map(request => request.MaxDuration)
                .Should()
                .BeSuccess(60);

        [Fact]
        public void Build_ShouldAssignMultiBroadcastTag_GivenWithMultiBroadcastTagIsUsed() =>
            this.BuildWithMandatoryValues()
                .WithMultiBroadcastTag("Test")
                .Create()
                .Map(request => request.MultiBroadcastTag)
                .Should()
                .BeSuccess(success => success.Should().BeSome("Test"));

        [Fact]
        public void Build_ShouldAssignResolution_GivenWithResolutionIsUsed() =>
            this.BuildWithMandatoryValues()
                .WithResolution(RenderResolution.FullHighDefinitionLandscape)
                .Create()
                .Map(request => request.Resolution)
                .Should()
                .BeSuccess(RenderResolution.FullHighDefinitionLandscape);

        [Fact]
        public void Build_ShouldAssignStreamMode_GivenWithStreamModeUsed() =>
            this.BuildWithMandatoryValues()
                .WithManualStreamMode()
                .Create()
                .Map(request => request.StreamMode)
                .Should()
                .BeSuccess(StreamMode.Manual);

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            StartBroadcastRequestBuilder.Build(Guid.Empty)
                .WithSessionId(this.sessionId)
                .WithLayout(this.layout)
                .WithOutputs(this.outputs)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenMaxDurationIsHigherThanMaximumValue() =>
            this.BuildWithMandatoryValues()
                .WithMaxDuration(59)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("MaxDuration cannot be lower than 60."));

        [Fact]
        public void Build_ShouldReturnFailure_GivenMaxDurationIsLowerThanMinimumValue() =>
            this.BuildWithMandatoryValues()
                .WithMaxDuration(59)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("MaxDuration cannot be lower than 60."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            StartBroadcastRequestBuilder.Build(this.applicationId)
                .WithSessionId(value)
                .WithLayout(this.layout)
                .WithOutputs(this.outputs)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

        [Fact]
        public void Build_ShouldReturnSuccess_WithDefaultValues() =>
            this.BuildWithMandatoryValues()
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.Layout.Should().Be(this.layout);
                    request.Outputs.Should().Be(this.outputs);
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.MaxDuration.Should().Be(14400);
                    request.Resolution.Should().Be(RenderResolution.StandardDefinitionLandscape);
                    request.StreamMode.Should().Be(StreamMode.Auto);
                    request.MultiBroadcastTag.Should().BeNone();
                    request.MaxBitrate.Should().Be(1000);
                });

        private IBuilderForOptional BuildWithMandatoryValues() =>
            StartBroadcastRequestBuilder.Build(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithLayout(this.layout)
                .WithOutputs(this.outputs);
    }
}