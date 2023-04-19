using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Moderation.MuteStream;
using Xunit;

namespace Vonage.Server.Test.Video.Moderation.MuteStream
{
    public class RequestBuilderTest
    {
        private readonly Guid applicationId;
        private readonly string sessionId;
        private readonly string streamId;

        public RequestBuilderTest()
        {
            var fixture = new Fixture();
            this.applicationId = fixture.Create<Guid>();
            this.sessionId = fixture.Create<string>();
            this.streamId = fixture.Create<string>();
        }

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsEmpty() =>
            MuteStreamRequest.Build()
                .WithApplicationId(Guid.Empty)
                .WithSessionId(this.sessionId)
                .WithStreamId(this.streamId)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            MuteStreamRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(value)
                .WithStreamId(this.streamId)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenStreamIdIsNullOrWhitespace(string value) =>
            MuteStreamRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithStreamId(value)
                .Create()
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("StreamId cannot be null or whitespace."));

        [Fact]
        public void Build_ShouldReturnSuccess_GivenValuesAreProvided() =>
            MuteStreamRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithStreamId(this.streamId)
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.StreamId.Should().Be(this.streamId);
                });
    }
}