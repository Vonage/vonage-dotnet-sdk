﻿using System.Threading.Tasks;
using Vonage.Request;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Video.Sessions.CreateSession;

namespace Vonage.Video.Beta.Video.Sessions;

/// <summary>
///     Exposes features for managing sessions.
/// </summary>
public interface ISessionClient
{
    /// <summary>
    ///     Represents the credentials that will be used for further connections.
    /// </summary>
    Credentials Credentials { get; }

    /// <summary>
    ///     Creates a new session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success if the operation succeeds, Failure it if fails.</returns>
    Task<Result<CreateSessionResponse>> CreateSessionAsync(CreateSessionRequest request);
}