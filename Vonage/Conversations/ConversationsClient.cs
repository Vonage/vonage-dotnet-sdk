using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Conversations.CreateConversation;
using Vonage.Conversations.CreateMember;
using Vonage.Conversations.DeleteConversation;
using Vonage.Conversations.DeleteEvent;
using Vonage.Conversations.GetConversation;
using Vonage.Conversations.GetConversations;
using Vonage.Conversations.GetEvent;
using Vonage.Conversations.GetMember;
using Vonage.Conversations.GetMembers;
using Vonage.Conversations.GetUserConversations;
using Vonage.Conversations.UpdateConversation;
using Vonage.Conversations.UpdateMember;
using Vonage.Serialization;

namespace Vonage.Conversations;

internal class ConversationsClient : IConversationsClient
{
    private readonly VonageHttpClient vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    internal ConversationsClient(VonageHttpClientConfiguration configuration) =>
        this.vonageClient = new VonageHttpClient(configuration, JsonSerializerBuilder.BuildWithSnakeCase());

    /// <inheritdoc />
    public Task<Result<Conversation>>
        CreateConversationAsync(Result<CreateConversationRequest> request) =>
        this.vonageClient.SendWithResponseAsync<CreateConversationRequest, Conversation>(request);

    /// <inheritdoc />
    public Task<Result<Unit>> DeleteConversationAsync(Result<DeleteConversationRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <inheritdoc />
    public Task<Result<Unit>> DeleteEventAsync(Result<DeleteEventRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <inheritdoc />
    public Task<Result<Conversation>> GetConversationAsync(Result<GetConversationRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetConversationRequest, Conversation>(request);

    /// <inheritdoc />
    public Task<Result<Event>> GetEventAsync(Result<GetEventRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetEventRequest, Event>(request);

    /// <inheritdoc />
    public Task<Result<Member>> CreateMemberAsync(Result<CreateMemberRequest> request) =>
        this.vonageClient.SendWithResponseAsync<CreateMemberRequest, Member>(request);

    /// <inheritdoc />
    public Task<Result<Member>> UpdateMemberAsync(Result<UpdateMemberRequest> request) =>
        this.vonageClient.SendWithResponseAsync<UpdateMemberRequest, Member>(request);

    /// <inheritdoc />
    public Task<Result<Member>> GetMemberAsync(Result<GetMemberRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetMemberRequest, Member>(request);

    /// <inheritdoc />
    public Task<Result<GetConversationsResponse>> GetConversationsAsync(Result<GetConversationsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetConversationsRequest, GetConversationsResponse>(request);

    /// <inheritdoc />
    public Task<Result<GetMembersResponse>> GetMembersAsync(Result<GetMembersRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetMembersRequest, GetMembersResponse>(request);

    /// <inheritdoc />
    public Task<Result<GetUserConversationsResponse>> GetUserConversationsAsync(
        Result<GetUserConversationsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetUserConversationsRequest, GetUserConversationsResponse>(request);

    /// <inheritdoc />
    public Task<Result<Conversation>> UpdateConversationAsync(Result<UpdateConversationRequest> request) =>
        this.vonageClient.SendWithResponseAsync<UpdateConversationRequest, Conversation>(request);
}