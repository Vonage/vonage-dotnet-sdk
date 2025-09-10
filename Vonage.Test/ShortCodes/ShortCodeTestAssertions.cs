#region
using FluentAssertions;
using Vonage.ShortCodes;
#endregion

namespace Vonage.Test.ShortCodes;

internal static class ShortCodeTestAssertions
{
    internal static void ShouldMatchExpectedOptInResponse(this OptInRecord actual)
    {
        actual.Msisdn.Should().Be("15559301529");
        actual.OptIn.Should().BeTrue();
        actual.OptInDate.Should().Be("2014-08-21 17:34:47");
        actual.OptOut.Should().BeFalse();
    }

    internal static void ShouldMatchExpectedOptInQueryResponse(this OptInSearchResponse actual) =>
        actual.OptInCount.Should().Be("3");

    internal static void ShouldMatchExpectedAlertResponse(this AlertResponse actual)
    {
        actual.MessageCount.Should().Be("1");
        actual.Messages.Should().NotBeEmpty();
        actual.Messages[0].Status.Should().Be("delivered");
        actual.Messages[0].MessageId.Should().Be("abcdefg");
        actual.Messages[0].To.Should().Be("16365553226");
        actual.Messages[0].ErrorCode.Should().Be("0");
    }

    internal static void ShouldMatchExpectedTwoFactorAuthResponse(this TwoFactorAuthResponse actual) =>
        actual.MessageCount.Should().Be("1");
}