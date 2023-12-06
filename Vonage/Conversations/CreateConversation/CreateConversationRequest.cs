using System;
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;

namespace Vonage.Conversations.CreateConversation;

/// <inheritdoc />
public readonly struct CreateConversationRequest : IVonageRequest
{
    /// <summary>
    /// </summary>
    public Maybe<string> DisplayName { get; internal init; }

    /// <summary>
    /// </summary>
    public Maybe<string> Name { get; internal init; }

    /// <summary>
    ///     Initializes a builder for CreateConversationRequest.
    /// </summary>
    /// <returns>The builder.</returns>
    public static IBuilderForOptional Build() => new CreateConversationRequestBuilder();

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => throw new NotImplementedException();

    /// <inheritdoc />
    public string GetEndpointPath() => "/v1/conversations";
}