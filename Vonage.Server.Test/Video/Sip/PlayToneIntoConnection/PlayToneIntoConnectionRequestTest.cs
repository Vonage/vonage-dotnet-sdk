using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Sip.PlayToneIntoConnection;
using Xunit;

namespace Vonage.Server.Test.Video.Sip.PlayToneIntoConnection
{
    public class PlayToneIntoConnectionRequestTest
    {
        private readonly Guid applicationId;
        private readonly string sessionId;
        private readonly string connectionId;
        private readonly string digits;

        public PlayToneIntoConnectionRequestTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.sessionId = fixture.Create<string>();
            this.connectionId = fixture.Create<string>();
            this.digits = fixture.Create<string>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            PlayToneIntoConnectionRequest.Parse(this.applicationId, this.sessionId, this.connectionId, this.digits)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess(
                    $"/v2/project/{this.applicationId}/session/{this.sessionId}/connection/{this.connectionId}/play-dtmf");

        [Fact]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            PlayToneIntoConnectionRequest.Parse(Guid.Empty, this.sessionId, this.connectionId, this.digits)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenConnectionIdIsNullOrWhitespace(string value) =>
            PlayToneIntoConnectionRequest.Parse(this.applicationId, this.sessionId, value, this.digits)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ConnectionId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenDigitsIsNullOrWhitespace(string value) =>
            PlayToneIntoConnectionRequest.Parse(this.applicationId, this.sessionId, this.connectionId, value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Digits cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            PlayToneIntoConnectionRequest.Parse(this.applicationId, value, this.connectionId, this.digits)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenAllValuesAreProvided() =>
            PlayToneIntoConnectionRequest.Parse(this.applicationId, this.sessionId, this.connectionId, this.digits)
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.ConnectionId.Should().Be(this.connectionId);
                    request.Digits.Should().Be(this.digits);
                });
    }
}