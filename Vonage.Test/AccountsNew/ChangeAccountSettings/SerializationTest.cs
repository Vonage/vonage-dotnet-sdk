using FluentAssertions;
using Vonage.AccountsNew.ChangeAccountSettings;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.AccountsNew.ChangeAccountSettings;

[Trait("Category", "Serialization")]
[Trait("Product", "AccountsNew")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldSerialize() =>
        ChangeAccountSettingsRequest.Build()
            .WithInboundSmsCallbackUrl("https://example.com/webhooks/inbound-sms")
            .WithDeliveryReceiptCallbackUrl("https://example.com/webhooks/delivery-receipt")
            .WithHttpForwardMethod(HttpForwardMethod.PostJson)
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    [Fact]
    public void ShouldSerializeWithRequiredFieldsOnly() =>
        ChangeAccountSettingsRequest.Build()
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<ChangeAccountSettingsResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(response =>
            {
                response.InboundSmsCallbackUrl.Should().Be("https://example.com/webhooks/inbound-sms");
                response.DeliveryReceiptCallbackUrl.Should().Be("https://example.com/webhooks/delivery-receipt");
                response.MaxOutboundRequest.Should().Be(30);
                response.MaxInboundRequest.Should().Be(30);
                response.MaxCallsPerSecond.Should().Be(30);
            });
}
