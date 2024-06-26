﻿using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Conversations.CreateConversation;
using Vonage.Conversations.CreateEvent;
using Vonage.Conversations.CreateMember;
using Vonage.Conversations.DeleteConversation;
using Vonage.Conversations.DeleteEvent;
using Vonage.Conversations.GetConversation;
using Vonage.Conversations.GetConversations;
using Vonage.Conversations.GetEvent;
using Vonage.Conversations.GetEvents;
using Vonage.Conversations.GetMember;
using Vonage.Conversations.GetMembers;
using Vonage.Conversations.GetUserConversations;
using Vonage.Conversations.UpdateConversation;
using Vonage.Conversations.UpdateMember;

namespace Vonage.Conversations;

/// <summary>
///     Exposes Conversations features.
/// </summary>
public interface IConversationsClient
{
    /// <summary>
    ///     Creates a conversation.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Conversation>> CreateConversationAsync(Result<CreateConversationRequest> request);

    /// <summary>
    ///     Creates an event.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Event>> CreateEventAsync(Result<CreateEventRequest> request);

    /// <summary>
    ///     Deletes a conversation.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Unit>> DeleteConversationAsync(Result<DeleteConversationRequest> request);

    /// <summary>
    ///     Deletes an event.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Unit>> DeleteEventAsync(Result<DeleteEventRequest> request);

    /// <summary>
    ///     Retrieves a conversation.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Conversation>> GetConversationAsync(Result<GetConversationRequest> request);

    /// <summary>
    ///     Retrieves an event.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Event>> GetEventAsync(Result<GetEventRequest> request);

    /// <summary>
    ///     Retrieves events.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<GetEventsResponse>> GetEventsAsync(Result<GetEventsRequest> request);

    /// <summary>
    ///     Creates a member.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Member>> CreateMemberAsync(Result<CreateMemberRequest> request);

    /// <summary>
    ///     Updates a member.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Member>> UpdateMemberAsync(Result<UpdateMemberRequest> request);

    /// <summary>
    ///     Retrieves a member.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Member>> GetMemberAsync(Result<GetMemberRequest> request);

    /// <summary>
    ///     Retrieves conversations.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<GetConversationsResponse>> GetConversationsAsync(Result<GetConversationsRequest> request);

    /// <summary>
    ///     Retrieves members.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<GetMembersResponse>> GetMembersAsync(Result<GetMembersRequest> request);

    /// <summary>
    ///     Retrieves conversations for a user.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<GetUserConversationsResponse>> GetUserConversationsAsync(Result<GetUserConversationsRequest> request);

    /// <summary>
    ///     Updates a conversation.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Conversation>> UpdateConversationAsync(Result<UpdateConversationRequest> request);
}