#region
using System;
using AutoFixture;
using FluentAssertions;
using Vonage.Meetings.DeleteRecording;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Meetings.DeleteRecording;

[Trait("Category", "Request")]
public class RequestTest
{
    private readonly Guid recordingId = new Fixture().Create<Guid>();

    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        DeleteRecordingRequest.Parse(this.recordingId)
            .Map(request => request.BuildRequestMessage().RequestUri!.ToString())
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