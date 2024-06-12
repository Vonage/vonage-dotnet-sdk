using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Conversations;
using Vonage.Conversations.GetEvents;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Vonage.Test.Conversations.GetEvents;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldDeserialize200() => this.helper.Serializer
        .DeserializeObject<GetEventsResponse>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(VerifyExpectedResponse);

    internal static void VerifyExpectedResponse(GetEventsResponse response)
    {
        response.PageSize.Should().Be(10);
        response.Embedded[0].Id.Should().Be(100);
        response.Embedded[0].Type.Should().Be("message");
        response.Embedded[0].From.Should().Be("string");
        response.Embedded[0].Body.Should()
            .Be(JsonSerializer.SerializeToElement(new {message_type = "text", text = "string"}));
        response.Embedded[0].Embedded.Member.Should().Be(new EmbeddedEventMember("string"));
        response.Embedded[0].Embedded.User.Id.Should().Be("USR-82e028d9-5201-4f1e-8188-604b2d3471ec");
        response.Embedded[0].Embedded.User.Name.Should().Be("my_user_name");
        response.Embedded[0].Embedded.User.DisplayName.Should().Be("My User Name");
        response.Embedded[0].Embedded.User.ImageUrl.Should().Be("https://example.com/image.png");
        response.Embedded[0].Embedded.User.CustomData.Should()
            .Be(JsonSerializer.SerializeToElement(new {field_1 = "value_1", field_2 = "value_2"}));
        response.Embedded[0].Links.Should()
            .Be(new Links(new HalLink(new Uri("https://api.nexmo.com/v1/conversations/CON-123/events/100"))));
        response.Links.First.Href.Should()
            .Be(new Uri(
                "https://api.nexmo.com/v1/conversations/CON-123/events?page_size=10&order=asc&exclude_deleted_events=false"));
        response.Links.Self.Href.Should()
            .Be(new Uri(
                "https://api.nexmo.com/v1/conversations/CON-123/events?page_size=10&order=asc&exclude_deleted_events=false"));
        response.Links.Next.Href.Should()
            .Be(new Uri(
                "https://api.nexmo.com/v1/conversations/CON-123/events?page_size=10&order=asc&exclude_deleted_events=false"));
        response.Links.Previous.Href.Should()
            .Be(new Uri(
                "https://api.nexmo.com/v1/conversations/CON-123/events?page_size=10&order=asc&exclude_deleted_events=false"));
    }
}