using System;
using FluentAssertions;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Conversations;
using Vonage.Conversations.UpdateMember;
using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.UpdateMember;

[Trait("Category", "Serialization")]
public class SerializationTest
{
    internal const string ValidConversationId = "CON-123";
    internal const string ValidMemberId = "MEM-123";
    internal const string ValidFrom = "123456789";
    
    private readonly SerializationTestHelper helper = new SerializationTestHelper(
        typeof(SerializationTest).Namespace,
        JsonSerializerBuilder.BuildWithSnakeCase());
    
    internal static Reason ValidReason => new Reason("123", "Some reason.");
    
    [Fact]
    public void ShouldSerializeWithJoinedState() =>
        BuildRequestWithJoinedState()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
    
    internal static Result<UpdateMemberRequest> BuildRequestWithJoinedState() =>
        UpdateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithMemberId(ValidMemberId)
            .WithJoinedState()
            .Create();
    
    [Fact]
    public void ShouldSerializeWithLeftState() =>
        BuildRequestWithLeftState()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
    
    internal static Result<UpdateMemberRequest> BuildRequestWithLeftState() =>
        UpdateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithMemberId(ValidMemberId)
            .WithLeftState(ValidReason)
            .Create();
    
    [Fact]
    public void ShouldSerializeWithFrom() =>
        BuildRequestWithFrom()
            .GetStringContent()
            .Should()
            .BeSuccess(this.helper.GetRequestJson());
    
    internal static Result<UpdateMemberRequest> BuildRequestWithFrom() =>
        UpdateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithMemberId(ValidMemberId)
            .WithJoinedState()
            .WithFrom(ValidFrom)
            .Create();
    
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
}