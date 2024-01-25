using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Meetings.DeleteRecording;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Meetings.DeleteRecording;

public class DeleteRecordingRequestTest
{
    private readonly Guid recordingId;

    public DeleteRecordingRequestTest() => this.recordingId = new Fixture().Create<Guid>();

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        DeleteRecordingRequest.Parse(this.recordingId)
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess(
                $"/v1/meetings/recordings/{this.recordingId}");

    [Fact]
    public void Parse_ShouldReturnFailure_GivenRecordingIdIsNullOrWhitespace() =>
        DeleteRecordingRequest.Parse(Guid.Empty)
            .Should()
            .BeParsingFailure("RecordingId cannot be empty.");

    [Fact]
    public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
        DeleteRecordingRequest.Parse(this.recordingId)
            .Should()
            .BeSuccess(request => request.RecordingId.Should().Be(this.recordingId));
}