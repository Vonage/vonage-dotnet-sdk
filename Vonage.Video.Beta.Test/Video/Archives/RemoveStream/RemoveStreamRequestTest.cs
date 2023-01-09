using AutoFixture;
using FluentAssertions;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Archives.RemoveStream;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Archives.RemoveStream
{
    public class RemoveStreamRequestTest
    {
        private readonly string applicationId;
        private readonly string archiveId;
        private readonly string streamId;

        public RemoveStreamRequestTest()
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
            RemoveStreamRequest.Parse(value, this.archiveId, this.streamId)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenArchiveIdIsNullOrWhitespace(string value) =>
            RemoveStreamRequest.Parse(this.applicationId, value, this.streamId)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ArchiveId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenStreamIdIsNullOrWhitespace(string value) =>
            RemoveStreamRequest.Parse(this.applicationId, this.archiveId, value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("StreamId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            RemoveStreamRequest.Parse(this.applicationId, this.archiveId, this.streamId)
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.ArchiveId.Should().Be(this.archiveId);
                    request.StreamId.Should().Be(this.streamId);
                });

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            RemoveStreamRequest.Parse(this.applicationId, this.archiveId, this.streamId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/archive/{this.archiveId}/streams");
    }
}