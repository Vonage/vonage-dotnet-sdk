using AutoFixture;
using FluentAssertions;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Signaling;
using Vonage.Video.Beta.Video.Signaling.SendSignal;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Signaling.SendSignal
{
    public class SendSignalRequestTest
    {
        private readonly string applicationId;
        private readonly string connectionId;
        private readonly SignalContent content;
        private readonly Fixture fixture;
        private readonly string sessionId;

        public SendSignalRequestTest()
        {
            this.fixture = new Fixture();
            this.applicationId = this.fixture.Create<string>();
            this.sessionId = this.fixture.Create<string>();
            this.connectionId = this.fixture.Create<string>();
            this.content = this.fixture.Create<SignalContent>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace(string value) =>
            SendSignalRequest.Parse(value, this.sessionId, this.connectionId, this.content)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            SendSignalRequest.Parse(this.applicationId, value, this.connectionId, this.content)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenConnectionIdIsNullOrWhitespace(string value) =>
            SendSignalRequest.Parse(this.applicationId, this.sessionId, value, this.content)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ConnectionId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenContentTypeIsNull(string value) =>
            SendSignalRequest.Parse(this.applicationId, this.sessionId, this.connectionId,
                    new SignalContent(value, this.fixture.Create<string>()))
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Type cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenContentDataIsNull(string value) =>
            SendSignalRequest.Parse(this.applicationId, this.sessionId, this.connectionId,
                    new SignalContent(this.fixture.Create<string>(), value))
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Data cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            SendSignalRequest.Parse(this.applicationId, this.sessionId, this.connectionId, this.content)
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.ConnectionId.Should().Be(this.connectionId);
                    request.Content.Should().BeEquivalentTo(this.content);
                });

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            SendSignalRequest.Parse(this.applicationId, this.sessionId, this.connectionId, this.content)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess(
                    $"/v2/project/{this.applicationId}/session/{this.sessionId}/connection/{this.connectionId}/signal");
    }
}