#region
using System;
#endregion

namespace Vonage.Messages;

/// <summary>
/// </summary>
/// <param name="MessageUuid">The UUID of the message </param>
/// <param name="WorkflowId">The ID of the failover workflow. Only present if the request was sent with the failover property.</param>
public record MessagesResponse(Guid MessageUuid, string WorkflowId = null);