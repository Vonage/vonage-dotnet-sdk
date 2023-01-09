using AutoFixture;
using FluentAssertions;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Sessions.GetStream;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Sessions.GetStream
{
    public class GetStreamRequestTest
    {
        private readonly string applicationId;
        private readonly string sessionId;
        private readonly string streamId;

        public GetStreamRequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<string>();
            this.sessionId = fixture.Create<string>();
            this.streamId = fixture.Create<string>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace(string value) =>
            GetStreamRequest.Parse(value, this.sessionId, this.streamId)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            GetStreamRequest.Parse(this.applicationId, value, this.streamId)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenStreamIdIsNullOrWhitespace(string value) =>
            GetStreamRequest.Parse(this.applicationId, this.sessionId, value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("StreamId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            GetStreamRequest.Parse(this.applicationId, this.sessionId, this.streamId)
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.StreamId.Should().Be(this.streamId);
                });

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            GetStreamRequest.Parse(this.applicationId, this.sessionId, this.streamId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/session/{this.sessionId}/stream/{this.streamId}");
    }
}