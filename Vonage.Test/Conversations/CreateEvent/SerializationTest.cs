using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Conversations;
using Vonage.Conversations.CreateEvent;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Vonage.Test.Conversations.CreateEvent;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldSerializeWithFrom() =>
        BuildRequestWithFrom()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    internal static Result<CreateEventRequest> BuildRequestWithFrom() =>
        CreateEventRequest.Build()
            .WithConversationId("CON-123")
            .WithType("message:submitted")
            .WithBody(new {event_id = "string"})
            .WithFrom("string")
            .Create();

    [Fact]
    public void ShouldSerialize() =>
        BuildRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());

    internal static Result<CreateEventRequest> BuildRequest() =>
        CreateEventRequest.Build()
            .WithConversationId("CON-123")
            .WithType("message:submitted")
            .WithBody(new {event_id = "string"})
            .Create();

    [Fact]
    public void ShouldDeserialize200() => this.helper.Serializer
        .DeserializeObject<Event>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(VerifyExpectedResponse);

    internal static void VerifyExpectedResponse(Event success)
    {
        success.Id.Should().Be(100);
        success.Type.Should().Be("message");
        success.From.Should().Be("string");
        success.Body.Should().Be(JsonSerializer.SerializeToElement(new {message_type = "text", text = "string"}));
        success.Embedded.Member.Should().Be(new EmbeddedEventMember("string"));
        success.Embedded.User.Id.Should().Be("USR-82e028d9-5201-4f1e-8188-604b2d3471ec");
        success.Embedded.User.Name.Should().Be("my_user_name");
        success.Embedded.User.DisplayName.Should().Be("My User Name");
        success.Embedded.User.ImageUrl.Should().Be("https://example.com/image.png");
        success.Embedded.User.CustomData.Should()
            .Be(JsonSerializer.SerializeToElement(new {field_1 = "value_1", field_2 = "value_2"}));
        success.Links.Should()
            .Be(new Links(new HalLink(new Uri("https://api.nexmo.com/v0.1/conversations/CON-1234/events/100"))));
    }
}