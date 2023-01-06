using AutoFixture;
using FluentAssertions;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Archives.AddStream;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Archives.AddStream
{
    public class AddStreamRequestTest
    {
        private readonly string applicationId;
        private readonly string archiveId;
        private readonly string streamId;

        public AddStreamRequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<string>();
            this.archiveId = fixture.Create<string>();
            this.streamId = fixture.Create<string>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace(string value) =>
            AddStreamRequest.Parse(value, this.archiveId, this.streamId)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenArchiveIdIsNullOrWhitespace(string value) =>
            AddStreamRequest.Parse(this.applicationId, value, this.streamId)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ArchiveId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenStreamIdIsNullOrWhitespace(string value) =>
            AddStreamRequest.Parse(this.applicationId, this.archiveId, value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("StreamId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenMandatoryValuesAreProvided() =>
            AddStreamRequest.Parse(this.applicationId, this.archiveId, this.streamId)
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
        public void Parse_ShouldReturnSuccess_GivenAllValuesAreProvided() =>
            AddStreamRequest.Parse(this.applicationId, this.archiveId, this.streamId, false, false)
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.ArchiveId.Should().Be(this.archiveId);
                    request.StreamId.Should().Be(this.streamId);
                    request.HasAudio.Should().Be(false);
                    request.HasVideo.Should().Be(false);
                });

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            AddStreamRequest.Parse(this.applicationId, this.archiveId, this.streamId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/project/{this.applicationId}/archive/{this.archiveId}/streams");
    }
}