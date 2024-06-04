using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Conversations;
using Vonage.Conversations.CreateMember;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.CreateMember;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    private const string ValidConversationId = "CON-123";
    private const CreateMemberRequest.AvailableStates ValidState = CreateMemberRequest.AvailableStates.Invited;
    private const string ValidUserId = "USR-123";
    private const string ValidNumber = "123456789";
    private const string ValidId = "1";
    
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());
    
    private static MemberUser ValidUser => new MemberUser("USR-123", "User 123");
    
    [Fact]
    public void ShouldDeserialize200() => this.helper.Serializer
        .DeserializeObject<Member>(this.helper.GetResponseJson())
        .Should()
        .BeSuccess(VerifyResponse);
    
    internal static void VerifyResponse(Member response)
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
        response.Initiator.Should().Be(new MemberInitiator(
            new MemberInitiatorJoined(true, "USR-82e028d9-5201-4f1e-8188-604b2d3471ec",
                "MEM-63f61863-4a51-4f6b-86e1-46edebio0391"),
            null
        ));
        response.Channel.Should().Be(new MemberChannel(
            ChannelType.App,
            MemberChannelFrom.FromChannels(ChannelType.App),
            new MemberChannelToV(ChannelType.App, "string", null, null)
        ));
    }
    
    [Fact]
    public void ShouldSerializeApp() =>
        BuildAppRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
    
    internal static Result<CreateMemberRequest> BuildAppRequest() =>
        CreateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithState(ValidState)
            .WithUser(ValidUser)
            .WithApp(ValidUserId, ChannelType.App, ChannelType.Phone, ChannelType.Sms)
            .Create();
    
    [Fact]
    public void ShouldSerializePhone() =>
        BuildPhoneRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
    
    internal static Result<CreateMemberRequest> BuildPhoneRequest() =>
        CreateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithState(ValidState)
            .WithUser(ValidUser)
            .WithPhone(ValidNumber, ChannelType.App, ChannelType.Phone, ChannelType.Sms)
            .Create();
    
    [Fact]
    public void ShouldSerializeSms() =>
        BuildSmsRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
    
    internal static Result<CreateMemberRequest> BuildSmsRequest() =>
        CreateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithState(ValidState)
            .WithUser(ValidUser)
            .WithSms(ValidNumber, ChannelType.App, ChannelType.Phone, ChannelType.Sms)
            .Create();
    
    [Fact]
    public void ShouldSerializeMms() =>
        BuildMmsRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
    
    internal static Result<CreateMemberRequest> BuildMmsRequest() =>
        CreateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithState(ValidState)
            .WithUser(ValidUser)
            .WithMms(ValidNumber, ChannelType.App, ChannelType.Phone, ChannelType.Sms)
            .Create();
    
    [Fact]
    public void ShouldSerializeWhatsApp() =>
        BuildWhatsAppRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
    
    internal static Result<CreateMemberRequest> BuildWhatsAppRequest() =>
        CreateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithState(ValidState)
            .WithUser(ValidUser)
            .WithWhatsApp(ValidNumber, ChannelType.App, ChannelType.Phone, ChannelType.Sms)
            .Create();
    
    [Fact]
    public void ShouldSerializeViber() =>
        BuildViberRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
    
    internal static Result<CreateMemberRequest> BuildViberRequest() =>
        CreateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithState(ValidState)
            .WithUser(ValidUser)
            .WithViber(ValidId, ChannelType.App, ChannelType.Phone, ChannelType.Sms)
            .Create();
    
    [Fact]
    public void ShouldSerializeMessenger() =>
        BuildMessengerRequest()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
    
    internal static Result<CreateMemberRequest> BuildMessengerRequest() =>
        CreateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithState(ValidState)
            .WithUser(ValidUser)
            .WithMessenger(ValidId, ChannelType.App, ChannelType.Phone, ChannelType.Sms)
            .Create();
}