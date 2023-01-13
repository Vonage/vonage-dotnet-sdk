using AutoFixture;
using FluentAssertions;
using Vonage.Server.Common.Failures;
using Vonage.Server.Test.Extensions;
using Vonage.Server.Video.Moderation.DisconnectConnection;
using Xunit;

namespace Vonage.Server.Test.Video.Moderation.DisconnectConnection
{
    public class DisconnectConnectionRequestTest
    {
        private readonly string applicationId;
        private readonly string connectionId;
        private readonly string sessionId;

        public DisconnectConnectionRequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<string>();
            this.sessionId = fixture.Create<string>();
            this.connectionId = fixture.Create<string>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            DisconnectConnectionRequest.Parse(this.applicationId, this.sessionId, this.connectionId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/session/{this.sessionId}/connection/{this.connectionId}");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace(string value) =>
            DisconnectConnectionRequest.Parse(value, this.sessionId, this.connectionId)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenConnectionIdIsNullOrWhitespace(string value) =>
            DisconnectConnectionRequest.Parse(this.applicationId, this.sessionId, value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ConnectionId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            DisconnectConnectionRequest.Parse(this.applicationId, value, this.connectionId)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            DisconnectConnectionRequest.Parse(this.applicationId, this.sessionId, this.connectionId)
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.ConnectionId.Should().Be(this.connectionId);
                });
    }
}