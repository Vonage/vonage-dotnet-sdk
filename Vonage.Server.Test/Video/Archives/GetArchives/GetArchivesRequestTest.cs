using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Archives.GetArchives;
using Xunit;

namespace Vonage.Server.Test.Video.Archives.GetArchives
{
    public class GetArchivesRequestTest
    {
        private readonly Fixture fixture;

        public GetArchivesRequestTest()
        {
            this.fixture = new Fixture();
        }

        [Theory]
        [InlineData("appId", GetArchivesRequest.DefaultOffset, GetArchivesRequest.DefaultCount, null,
            "/v2/project/appId/archive?offset=0&count=50")]
        [InlineData("appId", GetArchivesRequest.DefaultOffset, GetArchivesRequest.DefaultCount, "",
            "/v2/project/appId/archive?offset=0&count=50")]
        [InlineData("appId", GetArchivesRequest.DefaultOffset, GetArchivesRequest.DefaultCount, " ",
            "/v2/project/appId/archive?offset=0&count=50")]
        [InlineData("appId2", 50, 800, "sessionId",
            "/v2/project/appId2/archive?offset=50&count=800&sessionId=sessionId")]
        public void GetEndpointPath_ShouldReturnApiEndpoint(string applicationId, int offset, int count,
            string sessionId, string expected) =>
            GetArchivesRequest.Parse(applicationId, offset, count, sessionId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess(expected);

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace(string value) =>
            GetArchivesRequest.Parse(value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnFailure_GivenCountIsHigherThanThreshold() =>
            GetArchivesRequest.Parse(this.fixture.Create<string>(), count: 1001)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Count cannot be higher than 1000."));

        [Fact]
        public void Parse_ShouldReturnFailure_GivenCountIsNegative() =>
            GetArchivesRequest.Parse(this.fixture.Create<string>(), count: -1)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Count cannot be negative."));

        [Fact]
        public void Parse_ShouldReturnFailure_GivenOffsetIsNegative() =>
            GetArchivesRequest.Parse(this.fixture.Create<string>(), -1)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Offset cannot be negative."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenAllValuesAreProvided() =>
            GetArchivesRequest.Parse("appId", 1000, 1000, "Some value")
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be("appId");
                    request.Offset.Should().Be(1000);
                    request.Count.Should().Be(1000);
                    request.SessionId.Should().Be("Some value");
                });

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenOnlyApplicationIdIsProvided() =>
            GetArchivesRequest.Parse("appId")
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be("appId");
                    request.Offset.Should().Be(GetArchivesRequest.DefaultOffset);
                    request.Count.Should().Be(GetArchivesRequest.DefaultCount);
                    request.SessionId.Should().BeNull();
                });
    }
}