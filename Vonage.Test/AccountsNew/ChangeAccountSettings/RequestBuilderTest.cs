using Vonage.AccountsNew.ChangeAccountSettings;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.AccountsNew.ChangeAccountSettings;

[Trait("Category", "Request")]
[Trait("Product", "AccountsNew")]
public class RequestBuilderTest
{
    [Fact]
    public void Build_ShouldHaveNoInboundSmsCallbackUrl_GivenDefault() =>
        ChangeAccountSettingsRequest.Build()
            .Create()
            .Map(r => r.InboundSmsCallbackUrl)
            .Should()
            .BeSuccess(url => url.Should().BeNone());

    [Fact]
    public void Build_ShouldSetInboundSmsCallbackUrl() =>
        ChangeAccountSettingsRequest.Build()
            .WithInboundSmsCallbackUrl("https://example.com/webhooks/inbound-sms")
            .Create()
            .Map(r => r.InboundSmsCallbackUrl)
            .Should()
            .BeSuccess("https://example.com/webhooks/inbound-sms");

    [Fact]
    public void Build_ShouldHaveNoDeliveryReceiptCallbackUrl_GivenDefault() =>
        ChangeAccountSettingsRequest.Build()
            .Create()
            .Map(r => r.DeliveryReceiptCallbackUrl)
            .Should()
            .BeSuccess(url => url.Should().BeNone());

    [Fact]
    public void Build_ShouldSetDeliveryReceiptCallbackUrl() =>
        ChangeAccountSettingsRequest.Build()
            .WithDeliveryReceiptCallbackUrl("https://example.com/webhooks/delivery-receipt")
            .Create()
            .Map(r => r.DeliveryReceiptCallbackUrl)
            .Should()
            .BeSuccess("https://example.com/webhooks/delivery-receipt");

    [Fact]
    public void Build_ShouldHaveNoHttpForwardMethod_GivenDefault() =>
        ChangeAccountSettingsRequest.Build()
            .Create()
            .Map(r => r.HttpForwardMethod)
            .Should()
            .BeSuccess(method => method.Should().BeNone());

    [Fact]
    public void Build_ShouldSetHttpForwardMethod() =>
        ChangeAccountSettingsRequest.Build()
            .WithHttpForwardMethod(HttpForwardMethod.PostJson)
            .Create()
            .Map(r => r.HttpForwardMethod)
            .Should()
            .BeSuccess(HttpForwardMethod.PostJson);
}
