using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Conversations;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.GetMember;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());
    
    [Fact]
    public void ShouldDeserializeApp() => this.helper.Serializer
        .DeserializeObject<Member>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(VerifyAppResponse);
    
    [Fact]
    public void ShouldDeserializeInitiatorAdmin() => this.helper.Serializer
        .DeserializeObject<Member>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(VerifyInitiatorAdminResponse);
    
    [Fact]
    public void ShouldDeserializeMessenger() => this.helper.Serializer
        .DeserializeObject<Member>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(VerifyMessengerResponse);
    
    [Fact]
    public void ShouldDeserializeViber() => this.helper.Serializer
        .DeserializeObject<Member>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(VerifyViberResponse);
    
    [Fact]
    public void ShouldDeserializeMMS() => this.helper.Serializer
        .DeserializeObject<Member>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(VerifyMMSResponse);
    
    [Fact]
    public void ShouldDeserializeWhatsApp() => this.helper.Serializer
        .DeserializeObject<Member>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(VerifyWhatsAppResponse);
    
    [Fact]
    public void ShouldDeserializePhone() => this.helper.Serializer
        .DeserializeObject<Member>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(VerifyPhoneResponse);
    
    [Fact]
    public void ShouldDeserializeSMS() => this.helper.Serializer
        .DeserializeObject<Member>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(VerifySMSResponse);
    
    internal static void VerifyAppResponse(Member response)
    {
        VerifyResponse(response);
        response.Initiator.Should().Be(new MemberInitiator(
            new MemberInitiatorJoined(true, "USR-82e028d9-5201-4f1e-8188-604b2d3471ec",
                "MEM-63f61863-4a51-4f6b-86e1-46edebio0391"),
            null
        ));
        response.Channel.Should().Be(new MemberChannel(
            "app",
            new MemberChannelFrom("app"),
            new MemberChannelTo("app", "string", null, null)
        ));
    }
    
    internal static void VerifyMessengerResponse(Member response)
    {
        VerifyResponse(response);
        response.Initiator.Should().Be(new MemberInitiator(
            new MemberInitiatorJoined(true, "USR-82e028d9-5201-4f1e-8188-604b2d3471ec",
                "MEM-63f61863-4a51-4f6b-86e1-46edebio0391"),
            null
        ));
        response.Channel.Should().Be(new MemberChannel(
            "messenger",
            new MemberChannelFrom("messenger"),
            new MemberChannelTo(null, null, null, "app")
        ));
    }
    
    internal static void VerifyViberResponse(Member response)
    {
        VerifyResponse(response);
        response.Initiator.Should().Be(new MemberInitiator(
            new MemberInitiatorJoined(true, "USR-82e028d9-5201-4f1e-8188-604b2d3471ec",
                "MEM-63f61863-4a51-4f6b-86e1-46edebio0391"),
            null
        ));
        response.Channel.Should().Be(new MemberChannel(
            "viber",
            new MemberChannelFrom("viber"),
            new MemberChannelTo(null, null, null, "app")
        ));
    }
    
    internal static void VerifyMMSResponse(Member response)
    {
        VerifyResponse(response);
        response.Initiator.Should().Be(new MemberInitiator(
            new MemberInitiatorJoined(true, "USR-82e028d9-5201-4f1e-8188-604b2d3471ec",
                "MEM-63f61863-4a51-4f6b-86e1-46edebio0391"),
            null
        ));
        response.Channel.Should().Be(new MemberChannel(
            "mms",
            new MemberChannelFrom("mms"),
            new MemberChannelTo(null, null, "string", null)
        ));
    }
    
    internal static void VerifyWhatsAppResponse(Member response)
    {
        VerifyResponse(response);
        response.Initiator.Should().Be(new MemberInitiator(
            new MemberInitiatorJoined(true, "USR-82e028d9-5201-4f1e-8188-604b2d3471ec",
                "MEM-63f61863-4a51-4f6b-86e1-46edebio0391"),
            null
        ));
        response.Channel.Should().Be(new MemberChannel(
            "whatsapp",
            new MemberChannelFrom("whatsapp"),
            new MemberChannelTo(null, null, "string", null)
        ));
    }
    
    internal static void VerifyPhoneResponse(Member response)
    {
        VerifyResponse(response);
        response.Initiator.Should().Be(new MemberInitiator(
            new MemberInitiatorJoined(true, "USR-82e028d9-5201-4f1e-8188-604b2d3471ec",
                "MEM-63f61863-4a51-4f6b-86e1-46edebio0391"),
            null
        ));
        response.Channel.Should().Be(new MemberChannel(
            "phone",
            new MemberChannelFrom("phone"),
            new MemberChannelTo("phone", null, "string", null)
        ));
    }
    
    internal static void VerifySMSResponse(Member response)
    {
        VerifyResponse(response);
        response.Initiator.Should().Be(new MemberInitiator(
            new MemberInitiatorJoined(true, "USR-82e028d9-5201-4f1e-8188-604b2d3471ec",
                "MEM-63f61863-4a51-4f6b-86e1-46edebio0391"),
            null
        ));
        response.Channel.Should().Be(new MemberChannel(
            "sms",
            new MemberChannelFrom("sms"),
            new MemberChannelTo("sms", null, "string", null)
        ));
    }
    
    internal static void VerifyInitiatorAdminResponse(Member response)
    {
        VerifyResponse(response);
        response.Initiator.Should().Be(new MemberInitiator(
            null,
            new MemberInitiatorInvited(true)
        ));
        response.Channel.Should().Be(new MemberChannel(
            "app",
            new MemberChannelFrom("string"),
            new MemberChannelTo("app", "string", null, null)
        ));
    }
    
    private static void VerifyResponse(Member response)
    {
        response.Id.Should().Be("MEM-63f61863-4a51-4f6b-86e1-46edebio0391");
        response.ConversationId.Should().Be("CON-d66d47de-5bcb-4300-94f0-0c9d4b948e9a");
        response.State.Should().Be("JOINED");
        response.KnockingId.Should().Be("string");
        response.InvitedBy.Should().Be("MEM-63f61863-4a51-4f6b-86e1-46edebio0378");
        response.Timestamp.Should().Be(new MemberTimestamp(
            DateTimeOffset.Parse("2020-01-01T14:00:00.00Z"),
            DateTimeOffset.Parse("2020-01-01T14:00:00.00Z"),
            DateTimeOffset.Parse("2020-01-01T14:00:00.00Z")
        ));
        response.Media.Should().Be(new MemberMedia(
            new MemberMediaSettings(true, true, true),
            true
        ));
        response.Links.Should()
            .Be(new HalLink(new Uri(
                "https://api.nexmo.com/v1/conversations/CON-63f61863-4a51-4f6b-86e1-46edebio0391/members/MEM-63f61863-4a51-4f6b-86e1-46edebio0391")));
        response.Embedded.Should().Be(new MemberEmbedded(
            new MemberEmbeddedUser(
                "USR-82e028d9-5201-4f1e-8188-604b2d3471ec",
                "my_user_name",
                "My User Name",
                new HalLinks<HalLink>
                {
                    Self = new HalLink(
                        new Uri("https://api.nexmo.com/v1/users/USR-82e028d9-5201-4f1e-8188-604b2d3471ec")),
                }
            )
        ));
    }
}