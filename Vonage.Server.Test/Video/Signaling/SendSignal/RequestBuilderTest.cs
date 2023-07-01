using System;
using AutoFixture;
using FluentAssertions;
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
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            SendSignalRequest.Build()
                .WithApplicationId(Guid.Empty)
                .WithSessionId(this.sessionId)
                .WithConnectionId(this.connectionId)
                .WithContent(this.content)
                .Create()
                .Should()
                .BeParsingFailure("ApplicationId cannot be empty.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenConnectionIdIsNullOrWhitespace(string value) =>
            SendSignalRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithConnectionId(value)
                .WithContent(this.content)
                .Create()
                .Should()
                .BeParsingFailure("ConnectionId cannot be null or whitespace.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenContentDataIsNull(string value) =>
            SendSignalRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithConnectionId(this.connectionId)
                .WithContent(new SignalContent(this.fixture.Create<string>(), value))
                .Create()
                .Should()
                .BeParsingFailure("Data cannot be null or whitespace.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenContentTypeIsNull(string value) =>
            SendSignalRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithConnectionId(this.connectionId)
                .WithContent(new SignalContent(value, this.fixture.Create<string>()))
                .Create()
                .Should()
                .BeParsingFailure("Type cannot be null or whitespace.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            SendSignalRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(value)
                .WithConnectionId(this.connectionId)
                .WithContent(this.content)
                .Create()
                .Should()
                .BeParsingFailure("SessionId cannot be null or whitespace.");

        [Fact]
        public void Build_ShouldReturnSuccess_GivenValuesAreProvided() =>
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