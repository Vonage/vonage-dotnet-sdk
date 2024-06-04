using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Conversations.UpdateMember;

internal struct UpdateMemberRequestBuilder : IBuilderForConversationId, IBuilderForMemberId, IBuilderForState,
    IBuilderForOptional
{
    private string conversationId;
    private string memberId;
    private UpdateMemberRequest.AvailableStates state;
    private Maybe<Reason> reason;
    private Maybe<string> from;
    
    IBuilderForMemberId IBuilderForConversationId.WithConversationId(string value) =>
        this with {conversationId = value};
    
    IBuilderForState IBuilderForMemberId.WithMemberId(string value) => this with {memberId = value};
    
    public Result<UpdateMemberRequest> Create() => Result<UpdateMemberRequest>.FromSuccess(new UpdateMemberRequest
    {
        ConversationId = this.conversationId,
        MemberId = this.memberId,
        State = this.state,
        Reason = this.reason,
        From = this.from,
    });
    
    public IBuilderForOptional WithFrom(string value) => this with {from = value};
    
    public IBuilderForOptional WithJoinedState() => this with {state = UpdateMemberRequest.AvailableStates.Joined};
    
    public IBuilderForOptional WithLeftState(Reason value) =>
        this with {state = UpdateMemberRequest.AvailableStates.Left, reason = value};
}

/// <summary>
///     Represents a builder for the ConversationId.
/// </summary>
public interface IBuilderForConversationId
{
    /// <summary>
    ///     Sets the Conversation Id.
    /// </summary>
    /// <param name="value">The conversation id.</param>
    /// <returns>The builder.</returns>
    IBuilderForMemberId WithConversationId(string value);
}

/// <summary>
///     Represents a builder for the MemberId.
/// </summary>
public interface IBuilderForMemberId
{
    /// <summary>
    ///     Sets the Member Id.
    /// </summary>
    /// <param name="value">The member id.</param>
    /// <returns>The builder.</returns>
    IBuilderForState WithMemberId(string value);
}

/// <summary>
///     Represents a builder for the state.
/// </summary>
public interface IBuilderForState
{
    /// <summary>
    ///     Sets the state to Joined.
    /// </summary>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithJoinedState();
    
    /// <summary>
    ///     Sets the state to Left.
    /// </summary>
    /// <param name="value">The reason</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithLeftState(Reason value);
}

/// <summary>
///     Represents a builder for optional values.
/// </summary>
public interface IBuilderForOptional : IVonageRequestBuilder<UpdateMemberRequest>
{
    /// <summary>
    ///     Sets the From.
    /// </summary>
    /// <param name="value">The From.</param>
    /// <returns>The builder.</returns>
    IBuilderForOptional WithFrom(string value);
}