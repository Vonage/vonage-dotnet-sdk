#region
using System;
#endregion

namespace Vonage.Messages;

/// <summary>
///     Represents the response from sending a message through the Messages API.
/// </summary>
/// <param name="MessageUuid">The unique identifier for the message. Use this to track delivery status via webhooks.</param>
/// <param name="WorkflowId">The ID of the failover workflow. Only present if the request included failover messages.</param>
public record MessagesResponse(Guid MessageUuid, string WorkflowId = null);