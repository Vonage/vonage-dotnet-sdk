using System.Collections.Generic;
using System.Linq;
using EnumsNET;
using Vonage.Common.Client.Builders;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Video.Authentication;

/// <summary>
///     Represents additional claims for the Video Client's token.
/// </summary>
public readonly struct TokenAdditionalClaims : IHasSessionId
{
    internal const string ReservedClaimRole = "role";
    internal const string ReservedClaimSessionId = "session_id";
    internal const string ReservedClaimScope = "scope";

    /// <summary>
    ///     Represents the default scope (session.connect).
    /// </summary>
    public const string DefaultScope = "session.connect";

    private TokenAdditionalClaims(string scope, string sessionId, Role role, Dictionary<string, object> claims)
    {
        this.Scope = scope;
        this.SessionId = sessionId;
        this.Role = role;
        this.Claims = claims;
    }

    /// <summary>
    ///     This defines the role the user will have. There are three roles: subscriber, publisher, and moderator. Subscribers
    ///     can only subscribe to streams in the session (they cannot publish). Publishers can subscribe and publish streams to
    ///     the session, and they can use the signaling API. Moderators have the privileges of publishers and, in addition,
    ///     they can also force other users to disconnect from the session or to cease publishing. The default role (if no
    ///     value is passed) is publisher.
    /// </summary>
    public Role Role { get; }

    /// <summary>
    ///     The session's scope.
    /// </summary>
    public string Scope { get; }

    /// <inheritdoc />
    public string SessionId { get; }

    /// <summary>
    ///     The custom claims.
    /// </summary>
    public Dictionary<string, object> Claims { get; }

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
    /// <param name="claims">The custom claims.</param>
    /// <returns>A success state with claims if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<TokenAdditionalClaims> Parse(string sessionId,
        string scope = DefaultScope,
        Role role = Role.Publisher,
        Dictionary<string, object> claims = null)
        => Result<TokenAdditionalClaims>
            .FromSuccess(new TokenAdditionalClaims(scope, sessionId, role, claims ?? new Dictionary<string, object>()))
            .Map(InputEvaluation<TokenAdditionalClaims>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(
                VerifySessionId,
                request => VerifyReservedClaims(request, ReservedClaimScope),
                request => VerifyReservedClaims(request, ReservedClaimRole),
                request => VerifyReservedClaims(request, ReservedClaimSessionId),
                request => VerifyReservedClaims(request, Jwt.ReservedClaimApplicationId),
                request => VerifyReservedClaims(request, Jwt.ReservedClaimIssuedAt),
                request => VerifyReservedClaims(request, Jwt.ReservedClaimTokenId)));

    /// <summary>
    ///     Converts claims to a dictionary.
    /// </summary>
    /// <returns>The claims dictionary.</returns>
    public Dictionary<string, object> ToDataDictionary()
    {
        var claims = new Dictionary<string, object>
        {
            {ReservedClaimRole, this.Role.AsString(EnumFormat.Description)},
            {ReservedClaimSessionId, this.SessionId},
            {ReservedClaimScope, this.Scope},
        };
        this.Claims.ToList().ForEach(pair => claims.Add(pair.Key, pair.Value));
        return claims;
    }

    private static Result<TokenAdditionalClaims> VerifySessionId(TokenAdditionalClaims request) =>
        InputValidation.VerifyNotEmpty(request, request.SessionId, nameof(request.SessionId));

    private static Result<TokenAdditionalClaims> VerifyReservedClaims(TokenAdditionalClaims request,
        string reservedClaim) =>
        request.Claims.Keys.Contains(reservedClaim)
            ? Result<TokenAdditionalClaims>.FromFailure(
                ResultFailure.FromErrorMessage($"Claims key '{reservedClaim}' is reserved."))
            : request;
}