using AutoFixture;
using FluentAssertions;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Signaling.SendSignals;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Signaling
{
    public class SendSignalsRequestTest
    {
        private readonly string applicationId;
        private readonly SendSignalsRequest.SignalContent content;
        private readonly Fixture fixture;
        private readonly string sessionId;

        public SendSignalsRequestTest()
        {
            this.fixture = new Fixture();
            this.applicationId = this.fixture.Create<string>();
            this.sessionId = this.fixture.Create<string>();
            this.content = this.fixture.Create<SendSignalsRequest.SignalContent>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace(string value) =>
            SendSignalsRequest.Parse(value, this.sessionId, this.content)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            SendSignalsRequest.Parse(this.applicationId, value, this.content)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenContentTypeIsNull(string value) =>
            SendSignalsRequest.Parse(this.applicationId, this.sessionId,
                    new SendSignalsRequest.SignalContent(value, this.fixture.Create<string>()))
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Type cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenContentDataIsNull(string value) =>
            SendSignalsRequest.Parse(this.applicationId, this.sessionId,
                    new SendSignalsRequest.SignalContent(this.fixture.Create<string>(), value))
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Data cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            SendSignalsRequest.Parse(this.applicationId, this.sessionId, this.content)
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.Content.Should().BeEquivalentTo(this.content);
                });

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            SendSignalsRequest.Parse(this.applicationId, this.sessionId, this.content)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/project/{this.applicationId}/session/{this.sessionId}/signal");
    }
}