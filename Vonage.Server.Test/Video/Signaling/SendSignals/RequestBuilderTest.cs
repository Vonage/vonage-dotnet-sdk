using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Signaling;
using Vonage.Server.Video.Signaling.SendSignals;
using Xunit;

namespace Vonage.Server.Test.Video.Signaling.SendSignals
{
    public class RequestBuilderTest
    {
        private readonly Fixture fixture;
        private readonly Guid applicationId;
        private readonly SignalContent content;
        private readonly string sessionId;

        public RequestBuilderTest()
        {
            this.fixture = new Fixture();
            this.applicationId = this.fixture.Create<Guid>();
            this.sessionId = this.fixture.Create<string>();
            this.content = this.fixture.Create<SignalContent>();
        }

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            SendSignalsRequest.Build()
                .WithApplicationId(Guid.Empty)
                .WithSessionId(this.sessionId)
                .WithContent(this.content)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenContentDataIsNull(string value) =>
            SendSignalsRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithContent(new SignalContent(this.fixture.Create<string>(), value))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Data cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenContentTypeIsNull(string value) =>
            SendSignalsRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithContent(new SignalContent(value, this.fixture.Create<string>()))
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("Type cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            SendSignalsRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(value)
                .WithContent(this.content)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

        [Fact]
        public void Build_ShouldReturnSuccess_GivenValuesAreProvided() =>
            SendSignalsRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithContent(this.content)
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.Content.Should().BeEquivalentTo(this.content);
                });
    }
}