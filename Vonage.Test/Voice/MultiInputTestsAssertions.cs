#region
using FluentAssertions;
using Vonage.Voice.EventWebhooks;
#endregion

namespace Vonage.Test.Voice;

internal static class MultiInputTestsAssertions
{
    internal static void ShouldMatchExpectedWebhookSerialization(this MultiInput actual)
    {
        actual.Uuid.Should().Be("aaaaaaaa-bbbb-cccc-dddd-0123456789ab");
        actual.Speech.TimeoutReason.Should().Be("end_on_silence_timeout");
        actual.Speech.SpeechResults[0].Confidence.Should().Be("0.9405097");
        actual.Speech.SpeechResults[0].Text.Should().Be("Sales");
        actual.Speech.SpeechResults[2].Confidence.Should().Be("0.5949854");
        actual.Speech.SpeechResults[2].Text.Should().Be("Sale");
        actual.Dtmf.Digits.Should().BeNull();
        actual.Dtmf.TimedOut.Should().BeFalse();
    }

    internal static void ShouldMatchExpectedWebhookSerializationWithSpeechOverridden(this MultiInput actual)
    {
        actual.Uuid.Should().Be("aaaaaaaa-bbbb-cccc-dddd-0123456789ab");
        actual.Speech.Error.Should().Be("Speech overridden by DTMF");
        actual.Dtmf.Digits.Should().Be("1234");
        actual.Dtmf.TimedOut.Should().BeFalse();
    }

    internal static void ShouldMatchExpectedJson(this string actual, string expected)
    {
        actual.Should().Be(expected);
    }
}