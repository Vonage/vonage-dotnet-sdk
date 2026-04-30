using FluentAssertions;
using Vonage.ApplicationsNew;
using Vonage.ApplicationsNew.Capabilities;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.ApplicationsNew;

[Trait("Category", "Serialization")]
[Trait("Product", "ApplicationsNew")]
public class ApplicationResponseSerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(ApplicationResponseSerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    internal static void VerifyExpectedResponse(ApplicationResponse response)
    {
        response.Id.Should().Be("78d335fa-323d-0114-9c3d-d6f0d48968cf");
        response.Name.Should().Be("My Application");
        response.Privacy!.ImproveAi.Should().BeTrue();
        response.Keys!.PublicKey.Should().StartWith("-----BEGIN PUBLIC KEY-----");
        response.Keys.PrivateKey.Should().StartWith("-----BEGIN PRIVATE KEY-----");
        response.Capabilities!.Voice!.Webhooks!.AnswerUrl!.Address.Should().Be("https://example.com/webhooks/answer");
        response.Capabilities.Voice.Webhooks.AnswerUrl.Method.Should().Be(WebhookMethod.Post);
        response.Capabilities.Voice.Webhooks.AnswerUrl.ConnectionTimeout.Should().Be(500);
        response.Capabilities.Voice.Webhooks.AnswerUrl.SocketTimeout.Should().Be(3000);
        response.Capabilities.Voice.UseSignedCallbacks.Should().BeFalse();
        response.Capabilities.Voice.ConversationsTtl.Should().Be(12);
        response.Capabilities.Voice.LegPersistenceTime.Should().Be(10);
        response.Capabilities.Voice.Region.Should().Be(VoiceRegion.EuropeWest);
        response.Capabilities.Messages!.Webhooks!.InboundUrl!.Address.Should().Be("https://example.com/webhooks/inbound");
        response.Capabilities.Rtc!.UseSignedCallbacks.Should().BeFalse();
        response.Capabilities.NetworkApis!.NetworkApplicationId.Should().Be("2bzfIFqRG128IcjSj1YhZNtw6LADG");
        response.Capabilities.Meetings!.Webhooks!.RecordingChanged!.Address.Should().Be("https://example.com/webhooks/recordings");
        response.Capabilities.Verify!.Webhooks!.StatusUrl!.Address.Should().Be("https://example.com/webhooks/status");
    }

    [Fact]
    public void ShouldDeserialize200() =>
        this.helper.Serializer
            .DeserializeObject<ApplicationResponse>(this.helper.GetResponseJson())
            .Should()
            .BeSuccess(VerifyExpectedResponse);
}
