using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Conversations.CreateConversation;
using Vonage.Conversations.DeleteConversation;
using Vonage.Conversations.GetConversation;
using Vonage.Conversations.GetConversations;
using Vonage.Serialization;

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
    ///     Deletes a conversation.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Unit>> DeleteConversationAsync(Result<DeleteConversationRequest> request);

    /// <summary>
    ///     Retrieves a conversation.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Conversation>> GetConversationAsync(Result<GetConversationRequest> request);

    /// <summary>
    ///     Retrieves conversations.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<GetConversationsResponse>> GetConversationsAsync(Result<GetConversationsRequest> request);
}

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
    public Task<Result<Conversation>> GetConversationAsync(Result<GetConversationRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetConversationRequest, Conversation>(request);

    /// <inheritdoc />
    public Task<Result<GetConversationsResponse>> GetConversationsAsync(Result<GetConversationsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetConversationsRequest, GetConversationsResponse>(request);
}