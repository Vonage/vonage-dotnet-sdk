using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.Video.Sip.PlayToneIntoCall;
using Xunit;

namespace Vonage.Test.Unit.Video.Sip.PlayToneIntoCall
{
    public class RequestBuilderTest
    {
        private readonly Guid applicationId;
        private readonly string sessionId;
        private readonly string digits;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.sessionId = fixture.Create<string>();
            this.digits = fixture.Create<string>();
        }

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            PlayToneIntoCallRequest.Build()
                .WithApplicationId(Guid.Empty)
                .WithSessionId(this.sessionId)
                .WithDigits(this.digits)
                .Create()
                .Should()
                .BeParsingFailure("ApplicationId cannot be empty.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenDigitsIsNullOrWhitespace(string value) =>
            PlayToneIntoCallRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithDigits(value)
                .Create()
                .Should()
                .BeParsingFailure("Digits cannot be null or whitespace.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            PlayToneIntoCallRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(value)
                .WithDigits(this.digits)
                .Create()
                .Should()
                .BeParsingFailure("SessionId cannot be null or whitespace.");

        [Fact]
        public void Build_ShouldReturnSuccess_GivenAllValuesAreProvided() =>
            PlayToneIntoCallRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithDigits(this.digits)
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.Digits.Should().Be(this.digits);
                });
    }
}