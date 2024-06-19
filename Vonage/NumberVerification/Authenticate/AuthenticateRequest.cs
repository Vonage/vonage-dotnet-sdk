using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.NumberVerification.Authenticate;

/// <summary>
///     Represents a request to authenticate towards NumberVerification API.
/// </summary>
public readonly struct AuthenticateRequest
{
    /// <summary>
    ///     Parses the input into an AuthenticateRequest.
    /// </summary>
    /// <param name="number">The phone number.</param>
    /// <param name="tokenScope">The authorization scope for the token.</param>
    /// <returns>Success if the input matches all requirements. Failure otherwise.</returns>
    public static Result<AuthenticateRequest> Parse(string number, string tokenScope) =>
        PhoneNumber.Parse(number).Map(phoneNumber => new AuthenticateRequest
            {
                PhoneNumber = phoneNumber,
                Scope = tokenScope,
            })
            .Map(InputEvaluation<AuthenticateRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyScope));

    /// <summary>
    ///     Subscriber number in E.164 format (starting with country code). Optionally prefixed with '+'.
    /// </summary>
    public PhoneNumber PhoneNumber { get; private init; }

    /// <summary>
    ///     The authorization scope for the token.
    /// </summary>
    public string Scope { get; private init; }

    private static Result<AuthenticateRequest> VerifyScope(AuthenticateRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Scope, nameof(request.Scope));

    internal AuthorizeRequest BuildAuthorizeRequest() => new AuthorizeRequest(this.PhoneNumber, this.Scope);
}