using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Sessions.GetStreams;
using Xunit;

namespace Vonage.Server.Test.Video.Sessions.GetStreams
{
    public class RequestBuilderTest
    {
        private readonly Guid applicationId;
        private readonly string sessionId;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.sessionId = fixture.Create<string>();
        }

        [Fact]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            GetStreamsRequest.Build()
                .WithApplicationId(Guid.Empty)
                .WithSessionId(this.sessionId)
                .Create()
                .Should()
                .BeParsingFailure("ApplicationId cannot be empty.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            GetStreamsRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(value)
                .Create()
                .Should()
                .BeParsingFailure("SessionId cannot be null or whitespace.");

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            GetStreamsRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                });
    }
}