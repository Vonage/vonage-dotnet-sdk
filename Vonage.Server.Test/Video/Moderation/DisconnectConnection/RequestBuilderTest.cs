using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Moderation.DisconnectConnection;
using Xunit;

namespace Vonage.Server.Test.Video.Moderation.DisconnectConnection
{
    public class RequestBuilderTest
    {
        private readonly Guid applicationId;
        private readonly string connectionId;
        private readonly string sessionId;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.sessionId = fixture.Create<string>();
            this.connectionId = fixture.Create<string>();
        }

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            DisconnectConnectionRequest.Build()
                .WithApplicationId(Guid.Empty)
                .WithSessionId(this.sessionId)
                .WithConnectionId(this.connectionId)
                .Create()
                .Should()
                .BeParsingFailure("ApplicationId cannot be empty.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenConnectionIdIsNullOrWhitespace(string value) =>
            DisconnectConnectionRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithConnectionId(value)
                .Create()
                .Should()
                .BeParsingFailure("ConnectionId cannot be null or whitespace.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            DisconnectConnectionRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(value)
                .WithConnectionId(this.connectionId)
                .Create()
                .Should()
                .BeParsingFailure("SessionId cannot be null or whitespace.");

        [Fact]
        public void Build_ShouldReturnSuccess_GivenValuesAreProvided() =>
            DisconnectConnectionRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithConnectionId(this.connectionId)
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.ConnectionId.Should().Be(this.connectionId);
                });
    }
}