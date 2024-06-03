using Vonage.Common.Monads;
using Vonage.Conversations;
using Vonage.Conversations.CreateMember;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Conversations.CreateMember;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    private const string ValidConversationId = "CON-123";
    private const CreateMemberRequest.AvailableStates ValidState = CreateMemberRequest.AvailableStates.Invited;
    private const string ValidUserId = "USR-123";
    
    private static MemberUser ValidUser => new MemberUser("USR-123", "User 123");
    
    [Fact]
    public void Build_ShouldSetConversationId() =>
        BuildDefaultBuilder()
            .Create()
            .Map(request => request.ConversationId)
            .Should()
            .BeSuccess(ValidConversationId);
    
    [Fact]
    public void Build_ShouldSetMedia() =>
        BuildDefaultBuilder()
            .WithMedia(new MemberMedia(new MemberMediaSettings(true, true, true), true))
            .Create()
            .Map(request => request.Media)
            .Should()
            .BeSuccess(new MemberMedia(new MemberMediaSettings(true, true, true), true));
    
    [Fact]
    public void Build_ShouldHaveNoMedia_GivenDefault() =>
        BuildDefaultBuilder()
            .Create()
            .Map(request => request.Media)
            .Should()
            .BeSuccess(Maybe<MemberMedia>.None);
    
    [Fact]
    public void Build_ShouldSetKnockingId() =>
        BuildDefaultBuilder()
            .WithKnockingId("123")
            .Create()
            .Map(request => request.KnockingId)
            .Should()
            .BeSuccess("123");
    
    [Fact]
    public void Build_ShouldHaveNoKnockingId_GivenDefault() =>
        BuildDefaultBuilder()
            .Create()
            .Map(request => request.KnockingId)
            .Should()
            .BeSuccess(Maybe<string>.None);
    
    [Fact]
    public void Build_ShouldSetInvitingMemberId() =>
        BuildDefaultBuilder()
            .WithInvitingMemberId("123")
            .Create()
            .Map(request => request.InvitingMemberId)
            .Should()
            .BeSuccess("123");
    
    [Fact]
    public void Build_ShouldHaveNoInvitingMemberId_GivenDefault() =>
        BuildDefaultBuilder()
            .Create()
            .Map(request => request.InvitingMemberId)
            .Should()
            .BeSuccess(Maybe<string>.None);
    
    [Fact]
    public void Build_ShouldSetFrom() =>
        BuildDefaultBuilder()
            .WithFrom("From")
            .Create()
            .Map(request => request.From)
            .Should()
            .BeSuccess("From");
    
    [Fact]
    public void Build_ShouldHaveNoFrom_GivenDefault() =>
        BuildDefaultBuilder()
            .Create()
            .Map(request => request.From)
            .Should()
            .BeSuccess(Maybe<string>.None);
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenConversationIdIsEmpty(string invalidValue) =>
        CreateMemberRequest.Build()
            .WithConversationId(invalidValue)
            .WithState(ValidState)
            .WithUser(ValidUser)
            .WithApp(ValidUserId, ChannelType.App, ChannelType.Phone, ChannelType.Sms)
            .Create()
            .Map(request => request.ConversationId)
            .Should()
            .BeParsingFailure("ConversationId cannot be null or whitespace.");
    
    [Fact]
    public void Build_ShouldReturnFailure_GivenUserIsNull() =>
        CreateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithState(ValidState)
            .WithUser(null)
            .WithMms("NUM-123", ChannelType.Mms)
            .Create()
            .Should()
            .BeParsingFailure("User cannot be null.");
    
    [Fact]
    public void Build_ShouldSetState() =>
        BuildDefaultBuilder()
            .Create()
            .Map(request => request.State)
            .Should()
            .BeSuccess(ValidState);
    
    [Fact]
    public void Build_ShouldSetUser() =>
        BuildDefaultBuilder()
            .Create()
            .Map(request => request.User)
            .Should()
            .BeSuccess(ValidUser);
    
    [Fact]
    public void Build_ShouldSetAppChannel() =>
        CreateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithState(ValidState)
            .WithUser(ValidUser)
            .WithApp(ValidUserId, ChannelType.App, ChannelType.Phone, ChannelType.Sms)
            .Create()
            .Map(request => request.Channel)
            .Should()
            .BeSuccess(new MemberChannel(ChannelType.App,
                MemberChannelFrom.FromChannels(ChannelType.App, ChannelType.Phone, ChannelType.Sms),
                new MemberChannelToV(ChannelType.App, ValidUserId, Maybe<string>.None, Maybe<string>.None)));
    
    [Fact]
    public void Build_ShouldReturnFailure_GivenChannelTypesAreEmpty() =>
        CreateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithState(ValidState)
            .WithUser(ValidUser)
            .WithApp(ValidUserId)
            .Create()
            .Should()
            .BeParsingFailure("Type cannot be null or whitespace.");
    
    private static IBuilderForOptional BuildDefaultBuilder() =>
        CreateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithState(ValidState)
            .WithUser(ValidUser)
            .WithApp(ValidUserId, ChannelType.App, ChannelType.Phone, ChannelType.Sms);
    
    [Fact]
    public void Build_ShouldSetPhoneChannel() =>
        CreateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithState(ValidState)
            .WithUser(ValidUser)
            .WithPhone("NUM-123", ChannelType.Phone, ChannelType.Messenger)
            .Create()
            .Map(request => request.Channel)
            .Should()
            .BeSuccess(new MemberChannel(ChannelType.Phone,
                MemberChannelFrom.FromChannels(ChannelType.Phone, ChannelType.Messenger),
                new MemberChannelToV(ChannelType.Phone, Maybe<string>.None, "NUM-123", Maybe<string>.None)));
    
    [Fact]
    public void Build_ShouldSetSmsChannel() =>
        CreateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithState(ValidState)
            .WithUser(ValidUser)
            .WithSms("NUM-123", ChannelType.Sms, ChannelType.Viber)
            .Create()
            .Map(request => request.Channel)
            .Should()
            .BeSuccess(new MemberChannel(ChannelType.Sms,
                MemberChannelFrom.FromChannels(ChannelType.Sms, ChannelType.Viber),
                new MemberChannelToV(ChannelType.Sms, Maybe<string>.None, "NUM-123", Maybe<string>.None)));
    
    [Fact]
    public void Build_ShouldSetMmsChannel() =>
        CreateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithState(ValidState)
            .WithUser(ValidUser)
            .WithMms("NUM-123", ChannelType.Mms, ChannelType.Viber)
            .Create()
            .Map(request => request.Channel)
            .Should()
            .BeSuccess(new MemberChannel(ChannelType.Mms,
                MemberChannelFrom.FromChannels(ChannelType.Mms, ChannelType.Viber),
                new MemberChannelToV(ChannelType.Mms, Maybe<string>.None, "NUM-123", Maybe<string>.None)));
    
    [Fact]
    public void Build_ShouldSetWhatsAppChannel() =>
        CreateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithState(ValidState)
            .WithUser(ValidUser)
            .WithWhatsApp("NUM-123", ChannelType.Whatsapp, ChannelType.Viber)
            .Create()
            .Map(request => request.Channel)
            .Should()
            .BeSuccess(new MemberChannel(ChannelType.Whatsapp,
                MemberChannelFrom.FromChannels(ChannelType.Whatsapp, ChannelType.Viber),
                new MemberChannelToV(ChannelType.Whatsapp, Maybe<string>.None, "NUM-123", Maybe<string>.None)));
    
    [Fact]
    public void Build_ShouldSetViberChannel() =>
        CreateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithState(ValidState)
            .WithUser(ValidUser)
            .WithViber(ValidUserId, ChannelType.Viber)
            .Create()
            .Map(request => request.Channel)
            .Should()
            .BeSuccess(new MemberChannel(ChannelType.Viber,
                MemberChannelFrom.FromChannels(ChannelType.Viber),
                new MemberChannelToV(ChannelType.Viber, Maybe<string>.None, Maybe<string>.None, ValidUserId)));
    
    [Fact]
    public void Build_ShouldSetMessengerChannel() =>
        CreateMemberRequest.Build()
            .WithConversationId(ValidConversationId)
            .WithState(ValidState)
            .WithUser(ValidUser)
            .WithMessenger(ValidUserId, ChannelType.Messenger, ChannelType.Phone)
            .Create()
            .Map(request => request.Channel)
            .Should()
            .BeSuccess(new MemberChannel(ChannelType.Messenger,
                MemberChannelFrom.FromChannels(ChannelType.Messenger, ChannelType.Phone),
                new MemberChannelToV(ChannelType.Messenger, Maybe<string>.None, Maybe<string>.None, ValidUserId)));
}