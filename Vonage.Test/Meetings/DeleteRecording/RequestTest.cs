using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Meetings.DeleteRecording;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Guid recordingId = new Fixture().Create<Guid>();

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        Vonage.Meetings.DeleteRecording.Request.Parse(this.recordingId)
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess(
                $"/v1/meetings/recordings/{this.recordingId}");

    [Fact]
    public void Parse_ShouldReturnFailure_GivenRecordingIdIsNullOrWhitespace() =>
        Vonage.Meetings.DeleteRecording.Request.Parse(Guid.Empty)
            .Should()
            .BeParsingFailure("RecordingId cannot be empty.");

    [Fact]
    public void Parse_ShouldReturnSuccess_GivenValuesAreProvided() =>
        Vonage.Meetings.DeleteRecording.Request.Parse(this.recordingId)
            .Should()
            .BeSuccess(request => request.RecordingId.Should().Be(this.recordingId));
}