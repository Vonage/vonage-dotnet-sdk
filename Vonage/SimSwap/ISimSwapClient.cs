using System;
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.SimSwap.Authenticate;
using Vonage.SimSwap.Check;
using Vonage.SimSwap.GetSwapDate;

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
    
    /// <summary>
    ///     Check if SIM swap has been performed during a past period.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<bool>> CheckAsync(Result<CheckRequest> request);
    
    /// <summary>
    ///     Get timestamp of last MSISDN/IMSI pairing change for a mobile user account provided with MSIDN.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<DateTimeOffset>> GetSwapDateAsync(Result<GetSwapDateRequest> request);
}