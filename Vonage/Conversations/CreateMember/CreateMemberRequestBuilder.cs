using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Conversations.CreateMember;

internal struct CreateMemberRequestBuilder :
    IBuilderForConversationId,
    IBuilderForChannel,
    IBuilderForState,
    IBuilderForUser,
    IBuilderForOptional
{
    private string conversationId;
    private MemberChannel channel;
    private MemberMedia media;
    private string from;
    private string knockingId;
    private string invitingMemberId;
    private CreateMemberRequest.AvailableStates state;
    private MemberUser user;
    
    public IBuilderForOptional WithApp(string userId, params ChannelType[] types) =>
        this with
        {
            channel = new MemberChannel(ChannelType.App,
                MemberChannelFrom.FromChannels(types),
                new MemberChannelToV(ChannelType.App, userId, Maybe<string>.None, Maybe<string>.None)),
        };
    
    public IBuilderForOptional WithPhone(string number, params ChannelType[] types) =>
        this with
        {
            channel = new MemberChannel(ChannelType.Phone,
                MemberChannelFrom.FromChannels(types),
                new MemberChannelToV(ChannelType.Phone, Maybe<string>.None, number, Maybe<string>.None)),
        };
    
    public IBuilderForOptional WithSms(string number, params ChannelType[] types) =>
        this with
        {
            channel = new MemberChannel(ChannelType.Sms,
                MemberChannelFrom.FromChannels(types),
                new MemberChannelToV(ChannelType.Sms, Maybe<string>.None, number, Maybe<string>.None)),
        };
    
    public IBuilderForOptional WithMms(string number, params ChannelType[] types) =>
        this with
        {
            channel = new MemberChannel(ChannelType.Mms,
                MemberChannelFrom.FromChannels(types),
                new MemberChannelToV(ChannelType.Mms, Maybe<string>.None, number, Maybe<string>.None)),
        };
    
    public IBuilderForOptional WithWhatsApp(string number, params ChannelType[] types) =>
        this with
        {
            channel = new MemberChannel(ChannelType.Whatsapp,
                MemberChannelFrom.FromChannels(types),
                new MemberChannelToV(ChannelType.Whatsapp, Maybe<string>.None, number, Maybe<string>.None)),
        };
    
    public IBuilderForOptional WithViber(string id, params ChannelType[] types) =>
        this with
        {
            channel = new MemberChannel(ChannelType.Viber,
                MemberChannelFrom.FromChannels(types),
                new MemberChannelToV(ChannelType.Viber, Maybe<string>.None, Maybe<string>.None, id)),
        };
    
    public IBuilderForOptional WithMessenger(string id, params ChannelType[] types) => this with
    {
        channel = new MemberChannel(ChannelType.Messenger,
            MemberChannelFrom.FromChannels(types),
            new MemberChannelToV(ChannelType.Messenger, Maybe<string>.None, Maybe<string>.None, id)),
    };
    
    public IBuilderForState WithConversationId(string value) => this with {conversationId = value};
    
    public Result<CreateMemberRequest> Create() => Result<CreateMemberRequest>.FromSuccess(new CreateMemberRequest
        {
            ConversationId = this.conversationId,
            State = this.state,
            User = this.user,
            Channel = this.channel,
            Media = this.media,
            KnockingId = this.knockingId,
            InvitingMemberId = this.invitingMemberId,
            From = this.from,
        })
        .Map(InputEvaluation<CreateMemberRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules(VerifyConversationId, VerifyUser, VerifyChannelTypes));
    
    private static Result<CreateMemberRequest> VerifyConversationId(CreateMemberRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ConversationId, nameof(request.ConversationId));
    
    private static Result<CreateMemberRequest> VerifyUser(CreateMemberRequest request) =>
        InputValidation.VerifyNotNull(request, request.User, nameof(request.User));
    
    private static Result<CreateMemberRequest> VerifyChannelTypes(CreateMemberRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Channel.From.Type, nameof(request.Channel.From.Type));
    
    public IBuilderForOptional WithMedia(MemberMedia value) => this with {media = value};
    
    public IBuilderForOptional WithFrom(string value) => this with {from = value};
    
    public IBuilderForOptional WithKnockingId(string value) => this with {knockingId = value};
    
    public IBuilderForOptional WithInvitingMemberId(string value) => this with {invitingMemberId = value};
    
    public IBuilderForUser WithState(CreateMemberRequest.AvailableStates value) => this with {state = value};
    
    public IBuilderForChannel WithUser(MemberUser value) => this with {user = value};
}

/// <summary>
///     Represents a builder for ConversationId.
/// </summary>
public interface IBuilderForConversationId
{
    /// <summary>
    ///     Sets the ConversationId on the builder.
    /// </summary>
    /// <param name="value">The conversation Id.</param>
    /// <returns>The builder.</returns>
    IBuilderForState WithConversationId(string value);
}

/// <summary>
///     Represents a builder for State.
/// </summary>
public interface IBuilderForState
{
    /// <summary>
    ///     Sets the State on the builder.
    /// </summary>
    /// <param name="value">The state.</param>
    /// <returns>The builder.</returns>
    IBuilderForUser WithState(CreateMemberRequest.AvailableStates value);
}

/// <summary>
///     Represents a builder for User.
/// </summary>
public interface IBuilderForUser
{
    /// <summary>
    ///     Sets the User on the builder.
    /// </summary>
    /// <param name="value">The user.</param>
    /// <returns>The builder.</returns>
    IBuilderForChannel WithUser(MemberUser value);
}

/// <summary>
///     Represents a builder for Channel.
/// </summary>
public interface IBuilderForChannel
{
    /// <summary>
    ///     Sets the App channel on the builder.
    /// </summary>
    /// <param name="userId">The user Id.</param>
    /// <param name="types">The list of channels.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithApp(string userId, params ChannelType[] types);
    
    /// <summary>
    ///     Sets the Phone channel on the builder.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <param name="types">The list of channels.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithPhone(string number, params ChannelType[] types);
    
    /// <summary>
    ///     Sets the SMS channel on the builder.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <param name="types">The list of channels.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithSms(string number, params ChannelType[] types);
    
    /// <summary>
    ///     Sets the MMS channel on the builder.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <param name="types">The list of channels.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithMms(string number, params ChannelType[] types);
    
    /// <summary>
    ///     Sets the WhatsApp channel on the builder.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <param name="types">The list of channels.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithWhatsApp(string number, params ChannelType[] types);
    
    /// <summary>
    ///     Sets the Viber channel on the builder.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="types">The list of channels.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithViber(string id, params ChannelType[] types);
    
    /// <summary>
    ///     Sets the Messenger channel on the builder.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="types">The list of channels.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithMessenger(string id, params ChannelType[] types);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<CreateMemberRequest>
{
    /// <summary>
    ///     Sets the Media on the builder.
    /// </summary>
    /// <param name="value">The media.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithMedia(MemberMedia value);
    
    /// <summary>
    ///     Sets the From on the builder.
    /// </summary>
    /// <param name="value">The from.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithFrom(string value);
    
    /// <summary>
    ///     Sets the KnockingId on the builder.
    /// </summary>
    /// <param name="value">The knocking Id.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithKnockingId(string value);
    
    /// <summary>
    ///     Sets the InvitingMemberId on the builder.
    /// </summary>
    /// <param name="value">The inviting member Id.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithInvitingMemberId(string value);
}