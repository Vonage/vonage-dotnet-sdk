﻿using AutoFixture;
using FluentAssertions;
using Vonage.Server.Common.Failures;
using Vonage.Server.Test.Extensions;
using Vonage.Server.Video.Archives.Common;
using Vonage.Server.Video.Archives.CreateArchive;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.CreateArchive
{
    public class CreateArchiveRequestTest
    {
        private readonly string applicationId;
        private readonly Fixture fixture;
        private readonly string sessionId;

        public CreateArchiveRequestTest()
        {
            this.fixture = new Fixture();
            this.applicationId = this.fixture.Create<string>();
            this.sessionId = this.fixture.Create<string>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            CreateArchiveRequest.Parse(this.applicationId, this.sessionId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/archive");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace(string value) =>
            CreateArchiveRequest.Parse(value, this.fixture.Create<string>())
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            CreateArchiveRequest.Parse(this.fixture.Create<string>(), value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenAllValuesAreProvided() =>
            CreateArchiveRequest.Parse(
                    this.applicationId,
                    this.sessionId,
                    false,
                    false,
                    "name",
                    OutputMode.Individual,
                    RenderResolution.FullHighDefinitionLandscape,
                    StreamMode.Manual,
                    new ArchiveLayout(LayoutType.Custom, "some css", LayoutType.Custom))
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.HasAudio.Should().BeFalse();
                    request.HasVideo.Should().BeFalse();
                    request.Name.Should().Be("name");
                    request.OutputMode.Should().Be(OutputMode.Individual);
                    request.Resolution.Should().Be(RenderResolution.FullHighDefinitionLandscape);
                    request.StreamMode.Should().Be(StreamMode.Manual);
                    request.Layout.Should().Be(new ArchiveLayout(LayoutType.Custom, "some css", LayoutType.Custom));
                });

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenMandatoryValuesAreProvided() =>
            CreateArchiveRequest.Parse(this.applicationId, this.sessionId)
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.HasAudio.Should().BeTrue();
                    request.HasVideo.Should().BeTrue();
                    request.Name.Should().BeEmpty();
                    request.OutputMode.Should().Be(OutputMode.Composed);
                    request.Resolution.Should().Be(RenderResolution.StandardDefinitionLandscape);
                    request.StreamMode.Should().Be(StreamMode.Auto);
                    request.Layout.Should().Be(default(ArchiveLayout));
                });
    }
}