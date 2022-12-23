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

        public CreateArchiveRequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<string>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace(string value) =>
            CreateArchiveRequest.Parse(value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            CreateArchiveRequest.Parse(this.applicationId)
                .Should()
                .BeSuccess(request => request.ApplicationId.Should().Be(this.applicationId));

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            CreateArchiveRequest.Parse(this.applicationId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/project/{this.applicationId}/archive");
    }
}