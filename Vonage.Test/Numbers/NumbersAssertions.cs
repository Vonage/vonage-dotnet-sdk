#region
using FluentAssertions;
using Vonage.Numbers;
#endregion

namespace Vonage.Test.Numbers;

internal static class NumbersAssertions
{
    internal static void ShouldBeSuccessfulTransaction(this NumberTransactionResponse actual)
    {
        actual.ErrorCode.Should().Be("200");
        actual.ErrorCodeLabel.Should().Be("success");
    }

    internal static void ShouldMatchBasicNumberSearch(this NumbersSearchResponse actual)
    {
        actual.Count.Should().Be(1234);
        var number = actual.Numbers[0];
        number.Country.Should().Be("GB");
        number.Msisdn.Should().Be("447700900000");
        number.Type.Should().Be("mobile-lvn");
        number.Cost.Should().Be("1.25");
        number.Features[0].Should().Be("VOICE");
        number.Features[1].Should().Be("SMS");
    }

    internal static void ShouldMatchNumberSearchWithAdditionalData(this NumbersSearchResponse actual)
    {
        actual.ShouldMatchBasicNumberSearch();
        var number = actual.Numbers[0];
        number.MoHttpUrl.Should().Be("https://example.com/webhooks/inbound-sms");
        number.MessagesCallbackType.Should().Be("app");
        number.MessagesCallbackValue.Should().Be("aaaaaaaa-bbbb-cccc-dddd-0123456789ab");
        number.VoiceCallbackType.Should().Be("app");
        number.VoiceCallbackValue.Should().Be("aaaaaaaa-bbbb-cccc-dddd-0123456789ab");
    }

    internal static void ShouldMatchOwnedNumbersBasic(this NumbersSearchResponse actual)
    {
        actual.ShouldMatchBasicNumberSearch();
        actual.Numbers[0].ApplicationId.Should().BeNull();
    }

    internal static void ShouldMatchOwnedNumbersWithAdditionalData(this NumbersSearchResponse actual)
    {
        actual.ShouldMatchBasicNumberSearch();
        var number = actual.Numbers[0];
        number.ApplicationId.Should().BeNull();
        number.MoHttpUrl.Should().Be("https://example.com/webhooks/inbound-sms");
        number.MessagesCallbackType.Should().Be("app");
        number.MessagesCallbackValue.Should().Be("aaaaaaaa-bbbb-cccc-dddd-0123456789ab");
        number.VoiceCallbackType.Should().Be("app");
        number.VoiceCallbackValue.Should().Be("aaaaaaaa-bbbb-cccc-dddd-0123456789ab");
    }

    internal static void ShouldMatchOwnedNumbersWithApplication(this NumbersSearchResponse actual)
    {
        actual.ShouldMatchBasicNumberSearch();
        actual.Numbers[0].ApplicationId.Should().Be("9907a0d2-5206-4ec0-af8c-b335685ef9b8");
    }
}