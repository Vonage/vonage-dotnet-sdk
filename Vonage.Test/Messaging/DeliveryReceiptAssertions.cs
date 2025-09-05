#region
using FluentAssertions;
using Vonage.Messaging;
#endregion

namespace Vonage.Test.Messaging;

internal static class DeliveryReceiptAssertions
{
    internal static void ShouldMatchExpectedDeliveryReceipt(this DeliveryReceipt actual)
    {
        actual.Msisdn.Should().Be("447700900000");
        actual.To.Should().Be("AcmeInc");
        actual.NetworkCode.Should().Be("12345");
        actual.MessageId.Should().Be("0A0000001234567B");
        actual.Price.Should().Be("0.03330000");
        actual.Status.Should().Be(DlrStatus.delivered);
        actual.Scts.Should().Be("2001011400");
        actual.ErrorCode.Should().Be("0");
        actual.ApiKey.Should().Be("abcd1234");
        actual.MessageTimestamp.Should().Be("2020-01-01T12:00:00.000+00:00");
        actual.Timestamp.Should().Be(1582650446);
        actual.Nonce.Should().Be("ec11dd3e-1e7f-4db5-9467-82b02cd223b9");
        actual.Sig.Should().Be("1A20E4E2069B609FDA6CECA9DE18D5CAFE99720DDB628BD6BE8B19942A336E1C");
        actual.ClientRef.Should().Be("steve");
    }
}