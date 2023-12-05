using System;
using System.Net.Http;
using Vonage.Common.Client;

namespace Vonage.Conversations.CreateConversation;

/// <inheritdoc />
public readonly struct CreateConversationRequest : IVonageRequest
{
    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => throw new NotImplementedException();

    /// <inheritdoc />
    public string GetEndpointPath() => "/v1/conversations";
}