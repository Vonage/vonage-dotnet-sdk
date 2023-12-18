using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Conversations.CreateConversation;
using Vonage.Conversations.DeleteConversation;
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
    Task<Result<CreateConversationResponse>> CreateConversationAsync(Result<CreateConversationRequest> request);

    /// <summary>
    ///     Deletes a conversation.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Unit>> DeleteConversationAsync(Result<DeleteConversationRequest> request);
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
    public Task<Result<CreateConversationResponse>>
        CreateConversationAsync(Result<CreateConversationRequest> request) =>
        this.vonageClient.SendWithResponseAsync<CreateConversationRequest, CreateConversationResponse>(request);

    /// <inheritdoc />
    public Task<Result<Unit>> DeleteConversationAsync(Result<DeleteConversationRequest> request) =>
        this.vonageClient.SendAsync(request);
}