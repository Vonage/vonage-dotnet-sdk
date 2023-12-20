using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Test.Unit.Common.Extensions;
using Vonage.Video.Moderation.MuteStreams;
using Xunit;

namespace Vonage.Test.Unit.Video.Moderation.MuteStreams
{
    public class RequestBuilderTest
    {
        private readonly Fixture fixture;
        private readonly Guid applicationId;
        private readonly MuteStreamsRequest.MuteStreamsConfiguration configuration;
        private readonly string sessionId;

        public RequestBuilderTest()
        {
            this.fixture = new Fixture();
            this.applicationId = this.fixture.Create<Guid>();
            this.sessionId = this.fixture.Create<string>();
            this.configuration = this.fixture.Create<MuteStreamsRequest.MuteStreamsConfiguration>();
        }

        [Fact]
        public void Build_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace() =>
            MuteStreamsRequest.Build()
                .WithApplicationId(Guid.Empty)
                .WithSessionId(this.sessionId)
                .WithConfiguration(this.configuration)
                .Create()
                .Should()
                .BeParsingFailure("ApplicationId cannot be empty.");

        [Fact]
        public void Build_ShouldReturnFailure_GivenExcludedStreamsIdsAreNull() =>
            MuteStreamsRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithConfiguration(new MuteStreamsRequest.MuteStreamsConfiguration(this.fixture.Create<bool>(), null))
                .Create()
                .Should()
                .BeParsingFailure("ExcludedStreamIds cannot be null.");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Build_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            MuteStreamsRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(value)
                .WithConfiguration(this.configuration)
                .Create()
                .Should()
                .BeParsingFailure("SessionId cannot be null or whitespace.");

        [Fact]
        public void Build_ShouldReturnSuccess_GivenValuesAreProvided() =>
            MuteStreamsRequest.Build()
                .WithApplicationId(this.applicationId)
                .WithSessionId(this.sessionId)
                .WithConfiguration(this.configuration)
                .Create()
                .Should()
                .BeSuccess(request =>
                {
                    request.ApplicationId.Should().Be(this.applicationId);
                    request.SessionId.Should().Be(this.sessionId);
                    request.Configuration.Active.Should().Be(this.configuration.Active);
                    request.Configuration.ExcludedStreamIds.Should()
                        .BeEquivalentTo(this.configuration.ExcludedStreamIds);
                });
    }
}