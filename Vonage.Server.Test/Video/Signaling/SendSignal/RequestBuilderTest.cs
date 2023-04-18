using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Signaling;
using Vonage.Server.Video.Signaling.SendSignal;
using Xunit;

namespace Vonage.Server.Test.Video.Signaling.SendSignal
{
    public class RequestBuilderTest
    {
        private readonly Fixture fixture;
        private readonly Guid applicationId;
        private readonly SignalContent content;
        private readonly string connectionId;
        private readonly string sessionId;

        public RequestBuilderTest()
        {
            this.fixture = new Fixture();
            this.applicationId = this.fixture.Create<Guid>();
            this.sessionId = this.fixture.Create<string>();
            this.connectionId = this.fixture.Create<string>();
            this.content = this.fixture.Create<SignalContent>();
        }

        [Fact]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            SendSignalRequest.Build()
                .WithApplicationId(Guid.Empty)
                .WithSessionId(this.sessionId)
                .WithConnectionId(this.connectionId)
                .WithContent(this.content)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenConnectionIdIsNullOrWhitespace(string value) =>
            SendSignalRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithConnectionId(value)
                .WithContent(this.content)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ConnectionId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenContentDataIsNull(string value) =>
            SendSignalRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithConnectionId(this.connectionId)
                .WithContent(new SignalContent(this.fixture.Create<string>(), value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Data cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenContentTypeIsNull(string value) =>
            SendSignalRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithConnectionId(this.connectionId)
                .WithContent(new SignalContent(value, this.fixture.Create<string>()))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Type cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            SendSignalRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(value)
                .WithConnectionId(this.connectionId)
                .WithContent(this.content)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            SendSignalRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithConnectionId(this.connectionId)
                .WithContent(this.content)
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.ConnectionId.Should().Be(this.connectionId);
                    request.Content.Should().BeEquivalentTo(this.content);
                });
    }
}