using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Signaling.Common;
using Vonage.Server.Video.Signaling.SendSignals;
using Xunit;

namespace Vonage.Server.Test.Video.Signaling.SendSignals
{
    public class RequestTest
    {
        private readonly Fixture fixture;
        private readonly Guid applicationId;
        private readonly SignalContent content;
        private readonly string sessionId;

        public RequestTest()
        {
            this.fixture = new Fixture();
            this.applicationId = this.fixture.Create<Guid>();
            this.sessionId = this.fixture.Create<string>();
            this.content = this.fixture.Create<SignalContent>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            SendSignalsRequest.Parse(this.applicationId, this.sessionId, this.content)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/v2/project/{this.applicationId}/session/{this.sessionId}/signal");

        [Fact]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            SendSignalsRequest.Parse(Guid.Empty, this.sessionId, this.content)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenContentDataIsNull(string value) =>
            SendSignalsRequest.Parse(this.applicationId, this.sessionId,
                    new SignalContent(this.fixture.Create<string>(), value))
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Data cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenContentTypeIsNull(string value) =>
            SendSignalsRequest.Parse(this.applicationId, this.sessionId,
                    new SignalContent(value, this.fixture.Create<string>()))
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Type cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            SendSignalsRequest.Parse(this.applicationId, value, this.content)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

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
    }
}