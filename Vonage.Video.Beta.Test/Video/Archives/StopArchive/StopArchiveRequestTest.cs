using AutoFixture;
using FluentAssertions;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Archives.StopArchive;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Archives.StopArchive
{
    public class StopArchiveRequestTest
    {
        private readonly string applicationId;
        private readonly string archiveId;

        public StopArchiveRequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<string>();
            this.archiveId = fixture.Create<string>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace(string value) =>
            StopArchiveRequest.Parse(value, this.archiveId)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenArchiveIdIsNullOrWhitespace(string value) =>
            StopArchiveRequest.Parse(this.applicationId, value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ArchiveId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            StopArchiveRequest.Parse(this.applicationId, this.archiveId)
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.ArchiveId.Should().Be(this.archiveId);
                });

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            StopArchiveRequest.Parse(this.applicationId, this.archiveId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/archive/{this.archiveId}/stop");
    }
}