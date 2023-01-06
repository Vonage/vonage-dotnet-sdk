﻿using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Common.Validation;

namespace Vonage.Video.Beta.Common.Tokens;

/// <summary>
///     Represents additional claims for the Video Client's token.
/// </summary>
public readonly struct TokenAdditionalClaims
{
    /// <summary>
    ///     Represents the default scope (session.connect).
    /// </summary>
    public const string DefaultScope = "session.connect";

    private TokenAdditionalClaims(string scope, string sessionId, Role role)
    {
        this.Scope = scope;
        this.SessionId = sessionId;
        this.Role = role;
    }

    /// <summary>
    ///     The session's scope.
    /// </summary>
    public string Scope { get; }

    /// <summary>
    ///     The session ID corresponding to the session to which the user will connect.
    /// </summary>
    public string SessionId { get; }

    /// <summary>
    ///     This defines the role the user will have. There are three roles: subscriber, publisher, and moderator. Subscribers
    ///     can only subscribe to streams in the session (they cannot publish). Publishers can subscribe and publish streams to
    ///     the session, and they can use the signaling API. Moderators have the privileges of publishers and, in addition,
    ///     they can also force other users to disconnect from the session or to cease publishing. The default role (if no
    ///     value is passed) is publisher.
    /// </summary>
    public Role Role { get; }

    /// <summary>
    ///     Creates claims.
    /// </summary>
    /// <param name="sessionId"> The session ID corresponding to the session to which the user will connect.</param>
    /// <param name="scope">The session's scope.</param>
    /// <param name="role">
    ///     This defines the role the user will have. There are three roles: subscriber, publisher, and
    ///     moderator. Subscribers can only subscribe to streams in the session (they cannot publish). Publishers can subscribe
    ///     and publish streams to the session, and they can use the signaling API. Moderators have the privileges of
    ///     publishers and, in addition, they can also force other users to disconnect from the session or to cease publishing.
    ///     The default role (if no value is passed) is publisher.
    /// </param>
    /// <returns>A success state with claims if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<TokenAdditionalClaims> Parse(
        string sessionId,
        string scope = DefaultScope,
        Role role = Role.Publisher)
        => Result<TokenAdditionalClaims>
            .FromSuccess(new TokenAdditionalClaims(scope, sessionId, role))
            .Bind(VerifySessionId);

    private static Result<TokenAdditionalClaims> VerifySessionId(TokenAdditionalClaims request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(SessionId));
}