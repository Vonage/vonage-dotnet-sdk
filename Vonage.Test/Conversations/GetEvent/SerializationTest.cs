using System;
using Vonage.Common;
using Vonage.Conversations;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.GetEvent;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldDeserializeTextMessage() => this.helper.Serializer
        .DeserializeObject<Event>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(BuildExpectedEvent(new EventBodyTextMessage("string")));

    private static Event BuildExpectedEvent(EventBodyBase body) => new Event(100, "message", "string", body,
        DateTimeOffset.Parse("2020-01-01T14:00:00.00Z"),
        new EmbeddedEventData(
            new EmbeddedEventUser("USR-82e028d9-5201-4f1e-8188-604b2d3471ec", "my_user_name", "My User Name",
                "https://example.com/image.png", new { }), new EmbeddedEventMember("string")),
        new Links(new HalLink(new Uri("https://api.nexmo.com/v0.1/conversations/CON-1234/events/100"))));

    [Fact]
    public void ShouldDeserializeImageMessage() => this.helper.Serializer
        .DeserializeObject<Event>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(
            BuildExpectedEvent(new EventBodyImageMessage(new EventBodyImageUrl("https://example.com/image.png"))));

    [Fact]
    public void ShouldDeserializeAudioMessage() => this.helper.Serializer
        .DeserializeObject<Event>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(
            BuildExpectedEvent(new EventBodyAudioMessage(new EventBodyAudioUrl("https://example.com/audio.mp3"))));

    [Fact]
    public void ShouldDeserializeVideoMessage() => this.helper.Serializer
        .DeserializeObject<Event>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(
            BuildExpectedEvent(new EventBodyVideoMessage(new EventBodyVideoUrl("https://example.com/video.mkv"))));

    [Fact]
    public void ShouldDeserializeFileMessage() => this.helper.Serializer
        .DeserializeObject<Event>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(BuildExpectedEvent(new EventBodyFileMessage(new EventBodyFileUrl("https://example.com/file.txt"))));

    [Fact]
    public void ShouldDeserializeTemplateMessage() => this.helper.Serializer
        .DeserializeObject<Event>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(BuildExpectedEvent(new EventBodyTemplateMessage("Template", Array.Empty<object>(),
            new EventBodyTemplateWhatsApp("Deterministic", "en-US"))));

    [Fact]
    public void ShouldDeserializeCustomMessage() => this.helper.Serializer
        .DeserializeObject<Event>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(BuildExpectedEvent(new EventBodyCustomMessage(new { })));

    [Fact]
    public void ShouldDeserializeVcardMessage() => this.helper.Serializer
        .DeserializeObject<Event>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(BuildExpectedEvent(new EventBodyVcardMessage(new EventBodyVcardUrl("https://example.com/file.txt"),
            new EventBodyImageUrl("https://example.com/image.png"))));

    [Fact]
    public void ShouldDeserializeLocationMessage() => this.helper.Serializer
        .DeserializeObject<Event>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(BuildExpectedEvent(
            new EventBodyLocationMessage(new EventBodyLocation("Longitude", "Latitude", "Name", "Address"))));
}