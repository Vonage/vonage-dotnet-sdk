using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.AccountsNew.ChangeAccountSettings;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.AccountsNew.ChangeAccountSettings;

[Trait("Category", "E2E")]
[Trait("Product", "AccountsNew")]
public class E2ETest : E2EBase
{
    private readonly SerializationTestHelper localSerialization =
        new SerializationTestHelper(typeof(E2ETest).Namespace, JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public async Task ChangeAccountSettings()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/account/settings")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(this.localSerialization.GetRequestJson(nameof(SerializationTest.ShouldSerialize)))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.localSerialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
        await this.Helper.VonageClient.AccountsNewClient
            .ChangeAccountSettingsAsync(ChangeAccountSettingsRequest.Build()
                .WithInboundSmsCallbackUrl("https://example.com/webhooks/inbound-sms")
                .WithDeliveryReceiptCallbackUrl("https://example.com/webhooks/delivery-receipt")
                .WithHttpForwardMethod(HttpForwardMethod.PostJson)
                .Create())
            .Should()
            .BeSuccessAsync(response =>
            {
                response.InboundSmsCallbackUrl.Should().Be("https://example.com/webhooks/inbound-sms");
                response.DeliveryReceiptCallbackUrl.Should().Be("https://example.com/webhooks/delivery-receipt");
                response.MaxCallsPerSecond.Should().Be(30);
            });
    }
}
