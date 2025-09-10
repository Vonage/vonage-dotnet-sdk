#region
using System;
using System.Globalization;
using FluentAssertions;
using Vonage.Common;
using Vonage.Voice;
#endregion

namespace Vonage.Test.Voice;

internal static class VoiceTestAssertions
{
    internal static void ShouldMatchExpectedCallResponse(this CallResponse actual)
    {
        actual.Uuid.Should().Be("63f61863-4a51-4f6b-86e1-46edebcf9356");
        actual.ConversationUuid.Should().Be("CON-f972836a-550f-45fa-956c-12a2ab5b7d22");
        actual.Direction.Should().Be("outbound");
        actual.Status.Should().Be("started");
    }

    internal static void ShouldMatchExpectedCallRecord(this CallRecord actual)
    {
        actual.Uuid.Should().Be("63f61863-4a51-4f6b-86e1-46edebcf9356");
        actual.ConversationUuid.Should().Be("CON-f972836a-550f-45fa-956c-12a2ab5b7d22");
        actual.To.Number.Should().Be("447700900000");
        actual.To.Type.Should().Be("phone");
        actual.From.Type.Should().Be("phone");
        actual.From.Number.Should().Be("447700900001");
        actual.Status.Should().Be("started");
        actual.Direction.Should().Be("outbound");
        actual.Rate.Should().Be("0.39");
        actual.Price.Should().Be("23.40");
        actual.Duration.Should().Be("60");
        actual.StartTime.Should().Be(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal));
        actual.EndTime.Should().Be(DateTime.ParseExact("2020-01-01T12:00:00.000Z", "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal));
    }

    internal static void ShouldMatchExpectedCallsList(this PageResponse<CallList> actual)
    {
        actual.Count.Should().Be(100);
        actual.PageSize.Should().Be(10);
        actual.PageIndex.Should().Be(0);
        actual.Links.Self.Href.Should().Be("/calls?page_size=10&record_index=20&order=asc");
        var callRecord = actual.Embedded.Calls[0];
        callRecord.Links.Self.Href.Should().Be("/calls/63f61863-4a51-4f6b-86e1-46edebcf9356");
        callRecord.ShouldMatchExpectedCallRecord();
    }

    internal static void ShouldMatchExpectedDtmfResponse(this CallCommandResponse actual)
    {
        actual.Message.Should().Be("DTMF sent");
        actual.Uuid.Should().Be("63f61863-4a51-4f6b-86e1-46edebcf9356");
    }

    internal static void ShouldMatchExpectedStreamResponse(this CallCommandResponse actual)
    {
        actual.Message.Should().Be("Stream started");
        actual.Uuid.Should().Be("63f61863-4a51-4f6b-86e1-46edebcf9356");
    }

    internal static void ShouldMatchExpectedTalkResponse(this CallCommandResponse actual)
    {
        actual.Message.Should().Be("Talk started");
        actual.Uuid.Should().Be("63f61863-4a51-4f6b-86e1-46edebcf9356");
    }

    internal static void ShouldMatchExpectedStopStreamResponse(this CallCommandResponse actual)
    {
        actual.Message.Should().Be("Stream stopped");
        actual.Uuid.Should().Be("63f61863-4a51-4f6b-86e1-46edebcf9356");
    }

    internal static void ShouldMatchExpectedStopTalkResponse(this CallCommandResponse actual)
    {
        actual.Message.Should().Be("Talk stopped");
        actual.Uuid.Should().Be("63f61863-4a51-4f6b-86e1-46edebcf9356");
    }

    internal static void ShouldMatchExpectedRecording(this GetRecordingResponse actual, byte[] expectedData)
    {
        actual.ResultStream.Should().Equal(expectedData);
    }

    internal static void ShouldBeTrue(this bool actual) => actual.Should().BeTrue();

    internal static void ShouldHaveValidAdvancedMachineDetectionProperties(
        this AdvancedMachineDetectionProperties actual, int expectedBeepTimeout)
    {
        actual.Behavior.Should().Be(AdvancedMachineDetectionProperties.MachineDetectionBehavior.Continue);
        actual.Mode.Should().Be(AdvancedMachineDetectionProperties.MachineDetectionMode.Detect);
        actual.BeepTimeout.Should().Be(expectedBeepTimeout);
    }
}