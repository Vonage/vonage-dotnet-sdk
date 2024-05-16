using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.SimSwap.Authenticate;

namespace Vonage.SimSwap;

/// <summary>
///     Exposes SimSwap features.
/// </summary>
public interface ISimSwapClient
{
    /// <summary>
    ///     Authenticates towards SimSwap API to retrieve an authentication token.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<AuthenticateResponse>> AuthenticateAsync(Result<AuthenticateRequest> request);
}