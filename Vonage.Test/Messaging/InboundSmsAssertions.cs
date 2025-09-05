#region
using FluentAssertions;
using Vonage.Messaging;
#endregion

namespace Vonage.Test.Messaging;

internal static class InboundSmsAssertions
{
    internal static void ShouldMatchExpectedInboundSms(this InboundSms actual)
    {
        actual.ApiKey.Should().Be("abcd1234");
        actual.Msisdn.Should().Be("447700900001");
        actual.To.Should().Be("447700900000");
        actual.MessageId.Should().Be("0A0000000123ABCD1");
        actual.Text.Should().Be("Hello world");
        actual.Type.Should().Be("text");
        actual.Keyword.Should().Be("HELLO");
        actual.MessageTimestamp.Should().Be("2020-01-01T12:00:00.000+00:00");
        actual.Timestamp.Should().Be("1578787200");
        actual.Nonce.Should().Be("aaaaaaaa-bbbb-cccc-dddd-0123456789ab");
        actual.Concat.Should().BeTrue();
        actual.ConcatRef.Should().Be("1");
        actual.ConcatTotal.Should().Be("3");
        actual.ConcatPart.Should().Be("2");
        actual.Data.Should().Be("abc123");
        actual.Udh.Should().Be("abc123");
    }
}