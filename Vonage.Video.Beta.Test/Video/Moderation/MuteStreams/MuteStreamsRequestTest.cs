using AutoFixture;
using FluentAssertions;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Vonage.Video.Beta.Video.Moderation.MuteStreams;
using Xunit;

namespace Vonage.Video.Beta.Test.Video.Moderation.MuteStreams
{
    public class MuteStreamsRequestTest
    {
        private readonly string applicationId;
        private readonly MuteStreamsRequest.MuteStreamsConfiguration configuration;
        private readonly Fixture fixture;
        private readonly string sessionId;

        public MuteStreamsRequestTest()
        {
            this.fixture = new Fixture();
            this.applicationId = this.fixture.Create<string>();
            this.sessionId = this.fixture.Create<string>();
            this.configuration = this.fixture.Create<MuteStreamsRequest.MuteStreamsConfiguration>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenApplicationIdIsNullOrWhitespace(string value) =>
            MuteStreamsRequest.Parse(value, this.sessionId, this.configuration)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ApplicationId cannot be null or whitespace."));

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            MuteStreamsRequest.Parse(this.applicationId, value, this.configuration)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnFailure_GivenExcludedStreamsIdsAreNull() =>
            MuteStreamsRequest.Parse(this.applicationId, this.sessionId,
                    new MuteStreamsRequest.MuteStreamsConfiguration(this.fixture.Create<bool>(), null))
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("ExcludedStreamIds cannot be null."));

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

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            MuteStreamsRequest.Parse(this.applicationId, this.sessionId, this.configuration)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess(
                    $"/project/{this.applicationId}/session/{this.sessionId}/mute");
    }
}