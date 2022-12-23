using AutoFixture;
using FluentAssertions;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Archives.CreateArchive;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Archives.CreateArchive
{
    public class CreateArchiveRequestTest
    {
        private readonly string applicationId;
        private readonly string sessionId;
        private readonly Fixture fixture;

        public CreateArchiveRequestTest()
        {
            this.fixture = new Fixture();
            this.applicationId = this.fixture.Create<string>();
            this.sessionId = this.fixture.Create<string>();
        }

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
                    request.OutputMode.Should().Be("composed");
                    request.Resolution.Should().Be("640x480");
                    request.StreamMode.Should().Be("auto");
                    request.Layout.Should().Be(default(CreateArchiveRequest.ArchiveLayout));
                });

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenAllValuesAreProvided() =>
            CreateArchiveRequest.Parse(
                    this.applicationId,
                    this.sessionId,
                    false,
                    false,
                    "name",
                    "individual",
                    "1920x1080",
                    "manual",
                    new CreateArchiveRequest.ArchiveLayout("custom", "some css", "pip"))
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.HasAudio.Should().BeFalse();
                    request.HasVideo.Should().BeFalse();
                    request.Name.Should().Be("name");
                    request.OutputMode.Should().Be("individual");
                    request.Resolution.Should().Be("1920x1080");
                    request.StreamMode.Should().Be("manual");
                    request.Layout.Should().Be(new CreateArchiveRequest.ArchiveLayout("custom", "some css", "pip"));
                });

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            CreateArchiveRequest.Parse(this.applicationId, this.sessionId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/project/{this.applicationId}/archive");
    }
}