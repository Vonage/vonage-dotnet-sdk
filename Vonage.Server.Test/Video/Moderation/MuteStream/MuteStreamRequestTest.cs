using AutoFixture;
using FluentAssertions;
using Vonage.Server.Common.Failures;
using Vonage.Server.Test.Extensions;
using Vonage.Server.Video.Moderation.MuteStream;
using Xunit;

namespace Vonage.Server.Test.Video.Moderation.MuteStream
{
    public class MuteStreamRequestTest
    {
        private readonly string applicationId;
        private readonly string sessionId;
        private readonly string streamId;

        public MuteStreamRequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<string>();
            this.sessionId = fixture.Create<string>();
            this.streamId = fixture.Create<string>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            MuteStreamRequest.Parse(this.applicationId, this.sessionId, this.streamId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/session/{this.sessionId}/stream/{this.streamId}/mute");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace(string value) =>
            MuteStreamRequest.Parse(value, this.sessionId, this.streamId)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            MuteStreamRequest.Parse(this.applicationId, value, this.streamId)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenStreamIdIsNullOrWhitespace(string value) =>
            MuteStreamRequest.Parse(this.applicationId, this.sessionId, value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("StreamId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            MuteStreamRequest.Parse(this.applicationId, this.sessionId, this.streamId)
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.StreamId.Should().Be(this.streamId);
                });
    }
}