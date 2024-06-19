using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.NumberVerification.Authenticate;
using Vonage.NumberVerification.Verify;

namespace Vonage.NumberVerification;

/// <summary>
///     Exposes NumberVerification features.
/// </summary>
public interface INumberVerificationClient
{
    /// <summary>
    ///     Authenticates towards NumberVerification API to retrieve an authentication token.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<AuthenticateResponse>> AuthenticateAsync(Result<AuthenticateRequest> request);

    /// <summary>
    ///     Verifies if the specified phone number matches the one that the user is currently using.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<bool>> VerifyAsync(Result<VerifyRequest> request);
}