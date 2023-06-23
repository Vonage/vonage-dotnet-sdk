using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.GetRecordings;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRecordings
{
    public class RequestTest
    {
        private readonly string sessionId;

        public RequestTest()
        {
            var fixture = new Fixture();
            this.sessionId = fixture.Create<string>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            GetRecordingsRequest.Parse(this.sessionId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/meetings/sessions/{this.sessionId}/recordings");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenSessionIdIsNullOrWhitespace(string value) =>
            GetRecordingsRequest.Parse(value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("SessionId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            GetRecordingsRequest.Parse(this.sessionId)
                .Should()
                .BeSuccess(request => request.SessionId.Should().Be(this.sessionId));
    }
}