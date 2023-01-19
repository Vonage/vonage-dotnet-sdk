using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.GetRecording;
using Xunit;

namespace Vonage.Test.Unit.Meetings.GetRecording
{
    public class GetRecordingRequestTest
    {
        private readonly string recordingId;

        public GetRecordingRequestTest()
        {
            var fixture = new Fixture();
            this.recordingId = fixture.Create<string>();
        }

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            GetRecordingRequest.Parse(this.recordingId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess($"/beta/meetings/recordings/{this.recordingId}");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenRoomIdIsNullOrWhitespace(string value) =>
            GetRecordingRequest.Parse(value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("RecordingId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            GetRecordingRequest.Parse(this.recordingId)
                .Should()
                .BeSuccess(request => request.RecordingId.Should().Be(this.recordingId));
    }
}