using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Server.Video.Moderation.MuteStreams;
using Xunit;

namespace Vonage.Server.Test.Video.Moderation.MuteStreams
{
    public class RequestTest
    {
        private readonly Fixture fixture;
        private readonly Guid applicationId;
        private readonly MuteStreamsRequest.MuteStreamsConfiguration configuration;
        private readonly string sessionId;

        public RequestTest()
        {
            this.fixture = new Fixture();
            this.applicationId = this.fixture.Create<Guid>();
            this.sessionId = this.fixture.Create<string>();
            this.configuration = this.fixture.Create<MuteStreamsRequest.MuteStreamsConfiguration>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            MuteStreamsRequest.Parse(this.applicationId, this.sessionId, this.configuration)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess(
                    $"/v2/project/{this.applicationId}/session/{this.sessionId}/mute");

        [Fact]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace() =>
            MuteStreamsRequest.Parse(Guid.Empty, this.sessionId, this.configuration)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be empty."));

        [Fact]
        public void Parse_ShouldReturnFailure_GivenExcludedStreamsIdsAreNull() =>
            MuteStreamsRequest.Parse(this.applicationId, this.sessionId,
                    new MuteStreamsRequest.MuteStreamsConfiguration(this.fixture.Create<bool>(), null))
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ExcludedStreamIds cannot be null."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            MuteStreamsRequest.Parse(this.applicationId, value, this.configuration)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            MuteStreamsRequest.Parse(this.applicationId, this.sessionId, this.configuration)
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