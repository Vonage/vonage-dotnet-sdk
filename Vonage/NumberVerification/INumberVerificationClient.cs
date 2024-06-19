using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.NumberVerification.Authenticate;

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
}