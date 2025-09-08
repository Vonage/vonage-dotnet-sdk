#region
using FluentAssertions;
using Vonage.Accounts;
#endregion

namespace Vonage.Test.Accounts;

internal static class AccountAssertions
{
    internal static void ShouldMatchExpectedSecret(this Secret actual)
    {
        actual.Id.Should().Be("ad6dc56f-07b5-46e1-a527-85530e625800");
        actual.CreatedAt.Should().Be("2017-03-02T16:34:49Z");
        actual.Links.Self.Href.Should().Be("abc123");
    }

    internal static void ShouldMatchExpectedBalance(this Balance actual)
    {
        actual.Value.Should().Be(3.14159m);
        actual.AutoReload.Should().BeFalse();
    }

    internal static void ShouldMatchExpectedSecretsResult(this SecretsRequestResult actual)
    {
        actual.Embedded.Secrets[0].Id.Should().Be("ad6dc56f-07b5-46e1-a527-85530e625800");
        actual.Embedded.Secrets[0].CreatedAt.Should().Be("2017-03-02T16:34:49Z");
        actual.Embedded.Secrets[0].Links.Self.Href.Should().Be("abc123");
        actual.Links.Self.Href.Should().Be("abc123");
    }

    internal static void ShouldMatchExpectedAccountSettings(this AccountSettingsResult actual)
    {
        actual.DrCallbackurl.Should().Be("https://example.com/webhooks/delivery-receipt");
        actual.MoCallbackUrl.Should().Be("https://example.com/webhooks/inbound-sms");
        actual.MaxCallsPerSecond.Should().Be(4);
        actual.MaxInboundRequest.Should().Be(30);
        actual.MaxOutboundRequest.Should().Be(15);
    }

    internal static void ShouldMatchExpectedTopUpResult(this TopUpResult actual) =>
        actual.Response.Should().Be("abc123");

    internal static void ShouldBeSuccessfulRevocation(this bool actual) => actual.Should().BeTrue();
}