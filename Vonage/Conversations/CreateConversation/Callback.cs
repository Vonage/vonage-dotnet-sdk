using System;
using System.Net.Http;

namespace Vonage.Conversations.CreateConversation;

public record Callback(
    Uri Url,
    string EventMask,
    CallbackParameters Parameters,
    HttpMethod Method);