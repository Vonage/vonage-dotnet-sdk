using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.ChangeLayout;
using Vonage.Server.Video.Archives.Common;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.ChangeLayout
{
    public class ChangeLayoutRequestTest
    {
        private readonly string applicationId;
        private readonly string archiveId;
        private readonly Fixture fixture;
        private readonly ArchiveLayout layout;

        public ChangeLayoutRequestTest()
        {
            this.fixture = new Fixture();
            this.applicationId = this.fixture.Create<string>();
            this.archiveId = this.fixture.Create<string>();
            this.layout = this.fixture.Create<ArchiveLayout>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            ChangeLayoutRequest.Parse(this.applicationId, this.archiveId, this.layout)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/archive/{this.archiveId}/layout");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace(string value) =>
            ChangeLayoutRequest.Parse(value, this.fixture.Create<string>(), this.layout)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenArchiveIdIsNullOrWhitespace(string value) =>
            ChangeLayoutRequest.Parse(this.fixture.Create<string>(), value, this.layout)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ArchiveId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenAllValuesAreProvided() =>
            ChangeLayoutRequest.Parse(this.applicationId, this.archiveId, this.layout)
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.ArchiveId.Should().Be(this.archiveId);
                });
    }
}