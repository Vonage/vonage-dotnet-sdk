using Vonage.Applications;
using Vonage.Applications.Capabilities;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;
using CreateApplicationRequest = Vonage.Applications.CreateApplication.CreateApplicationRequest;

namespace Vonage.Test.Applications.CreateApplication;

[Trait("Category", "Serialization")]
[Trait("Product", "ApplicationsNew")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    internal static Result<CreateApplicationRequest> BuildRequest() =>
        CreateApplicationRequest.Build()
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

    internal static Result<CreateApplicationRequest> BuildRequestWithRequiredFieldsOnly() =>
        CreateApplicationRequest.Build()
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

    [Fact]
    public void ShouldSerializeWithRtcCapability() =>
        CreateApplicationRequest.Build()
            .WithName("Demo Application")
            .WithRtc(new RtcCapability
            {
                Webhooks = new RtcWebhooks
                {
                    EventUrl = new ApplicationWebhook("https://example.com/webhooks/event", WebhookMethod.Post)
                },
                UseSignedCallbacks = true
            })
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    [Fact]
    public void ShouldSerializeWithVerifyCapability() =>
        CreateApplicationRequest.Build()
            .WithName("Demo Application")
            .WithVerify(new VerifyCapability
            {
                Webhooks = new VerifyWebhooks
                {
                    StatusUrl = new PostOnlyWebhook("https://example.com/webhooks/status")
                }
            })
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    [Fact]
    public void ShouldSerializeWithVideoCapability() =>
        CreateApplicationRequest.Build()
            .WithName("Demo Application")
            .WithVideo(new VideoCapability
            {
                Webhooks = new VideoWebhooks
                {
                    SessionCreated = new VideoWebhook("https://example.com/webhooks/session-created", true),
                    ArchiveStatus = new VideoWebhook("https://example.com/webhooks/archive", true)
                }
            })
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    [Fact]
    public void ShouldSerializeWithMeetingsCapability() =>
        CreateApplicationRequest.Build()
            .WithName("Demo Application")
            .WithMeetings(new MeetingsCapability
            {
                Webhooks = new MeetingsWebhooks
                {
                    RecordingChanged = new PostOnlyWebhook("https://example.com/webhooks/recording"),
                    RoomChanged = new PostOnlyWebhook("https://example.com/webhooks/room")
                }
            })
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    [Fact]
    public void ShouldSerializeWithNetworkApisCapability() =>
        CreateApplicationRequest.Build()
            .WithName("Demo Application")
            .WithNetworkApis(new NetworkApisCapability
            {
                NetworkApplicationId = "my-network-app-id",
                RedirectUri = "https://example.com/oauth/callback"
            })
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    [Fact]
    public void ShouldSerializeWithVbcCapability() =>
        CreateApplicationRequest.Build()
            .WithName("Demo Application")
            .WithVbc(new VbcCapability())
            .Create()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
}
