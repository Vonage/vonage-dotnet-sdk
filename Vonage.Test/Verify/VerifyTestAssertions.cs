#region
using FluentAssertions;
using Vonage.Verify;
#endregion

namespace Vonage.Test.Verify;

internal static class VerifyTestAssertions
{
    internal static void ShouldMatchExpectedVerifyResponse(this VerifyResponse actual)
    {
        actual.RequestId.Should().Be("abcdef0123456789abcdef0123456789");
        actual.Status.Should().Be("0");
    }

    internal static void ShouldMatchExpectedVerifyCheckResponse(this VerifyCheckResponse actual)
    {
        actual.Price.Should().Be("0.10000000");
        actual.EstimatedPriceMessagesSent.Should().Be("0.03330000");
        actual.Currency.Should().Be("EUR");
        actual.EventId.Should().Be("0A00000012345678");
        actual.RequestId.Should().Be("abcdef0123456789abcdef0123456789");
        actual.Status.Should().Be("0");
    }

    internal static void ShouldMatchExpectedVerifyControlResponse(this VerifyControlResponse actual)
    {
        actual.Status.Should().Be("0");
        actual.Command.Should().Be("cancel");
    }

    internal static void ShouldMatchExpectedVerifySearchResponse(this VerifySearchResponse actual)
    {
        actual.RequestId.Should().Be("abcdef0123456789abcdef0123456789");
        actual.AccountId.Should().Be("abcdef01");
        actual.Status.Should().Be("IN PROGRESS");
        actual.Number.Should().Be("447700900000");
        actual.Price.Should().Be("0.10000000");
        actual.Currency.Should().Be("EUR");
        actual.SenderId.Should().Be("mySenderId");
        actual.DateSubmitted.Should().Be("2020-01-01 12:00:00");
        actual.DateFinalized.Should().Be("2020-01-01 12:00:00");
        actual.FirstEventDate.Should().Be("2020-01-01 12:00:00");
        actual.LastEventDate.Should().Be("2020-01-01 12:00:00");
        actual.Checks[0].DateReceived.Should().Be("2020-01-01 12:00:00");
        actual.Checks[0].Code.Should().Be("987654");
        actual.Checks[0].Status.Should().Be("abc123");
        actual.Checks[0].IpAddress.Should().Be("123.0.0.255");
        actual.Events[0].Type.Should().Be("abc123");
        actual.Events[0].Id.Should().Be("abc123");
        actual.EstimatedPriceMessagesSent.Should().Be("0.03330000");
    }

    internal static void ShouldThrowVerifyResponseException(this VonageVerifyResponseException actual)
    {
        actual.Response.Status.Should().Be("4");
        actual.Response.ErrorText.Should().Be("invalid credentials");
    }
}