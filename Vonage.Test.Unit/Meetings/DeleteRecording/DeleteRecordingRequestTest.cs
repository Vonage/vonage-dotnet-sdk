using AutoFixture;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Test.Extensions;
using Vonage.Meetings.DeleteRecording;
using Xunit;

namespace Vonage.Test.Unit.Meetings.DeleteRecording
{
    public class DeleteRecordingRequestTest
    {
        private readonly string recordingId;

        public DeleteRecordingRequestTest() => this.recordingId = new Fixture().Create<string>();

        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            DeleteRecordingRequest.Parse(this.recordingId)
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess(
                    $"/beta/meetings/recordings/{this.recordingId}");

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Parse_ShouldReturnFailure_GivenRecordingIdIsNullOrWhitespace(string value) =>
            DeleteRecordingRequest.Parse(value)
                .Should()
                .BeFailure(ResultFailure.FromErrorMessage("RecordingId cannot be null or whitespace."));

        [Fact]
        public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
            DeleteRecordingRequest.Parse(this.recordingId)
                .Should()
                .BeSuccess(request => request.RecordingId.Should().Be(this.recordingId));
    }
}