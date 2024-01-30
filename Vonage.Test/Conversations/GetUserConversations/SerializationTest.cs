using System;
using System.Linq;
using FluentAssertions;
using Vonage.Conversations;
using Vonage.Conversations.GetUserConversations;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.GetUserConversations;

public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());

    [Fact]
    public void ShouldDeserialize200() => this.helper.Serializer
        .DeserializeObject<GetUserConversationsResponse>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(VerifyExpectedResponse);

    internal static void VerifyExpectedResponse(GetUserConversationsResponse response)
    {
        response.PageSize.Should().Be(10);
        response.Embedded.Conversations.Should().HaveCount(1);
        var conversation = response.Embedded.Conversations.First();
        ConversationTests.VerifyExpectedResponse(conversation);
        conversation.Embedded.Should()
            .Be(new EmbeddedData("MEM-63f61863-4a51-4f6b-86e1-46edebio0391", MemberState.Left));
        response.Links.First.Href.Should()
            .Be(new Uri(
                "https://api.nexmo.com/v1/users/USR-82e028d9-5201-4f1e-8188-604b2d3471ec/conversations?order=desc&page_size=10"));
        response.Links.Self.Href.Should()
            .Be(new Uri(
                "https://api.nexmo.com/v1/users/USR-82e028d9-5201-4f1e-8188-604b2d3471ec/conversations?order=desc&page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D"));
        response.Links.Next.Href.Should()
            .Be(new Uri(
                "https://api.nexmo.com/v1/users/USR-82e028d9-5201-4f1e-8188-604b2d3471ec/conversations?order=desc&page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D"));
        response.Links.Previous.Href.Should()
            .Be(new Uri(
                "https://api.nexmo.com/v1/users/USR-82e028d9-5201-4f1e-8188-604b2d3471ec/conversations?order=desc&page_size=10&cursor=7EjDNQrAcipmOnc0HCzpQRkhBULzY44ljGUX4lXKyUIVfiZay5pv9wg%3D"));
    }
}