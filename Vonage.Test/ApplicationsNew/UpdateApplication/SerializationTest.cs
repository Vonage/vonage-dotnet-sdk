using Vonage.ApplicationsNew;
using Vonage.ApplicationsNew.Capabilities;
using Vonage.ApplicationsNew.UpdateApplication;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.ApplicationsNew.UpdateApplication;

[Trait("Category", "Serialization")]
[Trait("Product", "ApplicationsNew")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    internal static Result<UpdateApplicationRequest> BuildRequest() =>
        UpdateApplicationRequest.Build()
            .WithApplicationId("78d335fa-323d-0114-9c3d-d6f0d48968cf")
            .WithName("Demo Application")
            .WithVoice(new VoiceCapability
            {
                Webhooks = new VoiceWebhooks
                {
                    AnswerUrl = new VoiceWebhook("https://example.com/webhooks/answer", WebhookMethod.Get, 500, 3000),
                    EventUrl = new VoiceWebhook("https://example.com/webhooks/event", WebhookMethod.Post, 500, 3000)
                }
            })
            .WithMessages(new MessagesCapability
            {
                Webhooks = new MessagesWebhooks
                {
                    InboundUrl = new PostOnlyWebhook("https://example.com/webhooks/inbound"),
                    StatusUrl = new PostOnlyWebhook("https://example.com/webhooks/status")
                }
            })
            .WithKeys(new ApplicationKeys("-----BEGIN PUBLIC KEY-----"))
            .WithPrivacy(new ApplicationPrivacy(true))
            .Create();

    internal static Result<UpdateApplicationRequest> BuildRequestWithRequiredFieldsOnly() =>
        UpdateApplicationRequest.Build()
            .WithApplicationId("78d335fa-323d-0114-9c3d-d6f0d48968cf")
            .WithName("Demo Application")
            .Create();

    [Fact]
    public void ShouldSerialize() =>
        BuildRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    [Fact]
    public void ShouldSerializeWithRequiredFieldsOnly() =>
        BuildRequestWithRequiredFieldsOnly()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
}
