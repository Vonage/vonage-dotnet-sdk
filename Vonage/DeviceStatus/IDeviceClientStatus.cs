using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.DeviceStatus.Authenticate;

namespace Vonage.DeviceStatus;

/// <summary>
///     Exposes DeviceStatus features.
/// </summary>
public interface IDeviceStatusClient
{
    /// <summary>
    ///     Authenticates towards NumberVerification API to retrieve an authentication token.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<AuthenticateResponse>> AuthenticateAsync(Result<AuthenticateRequest> request);
    
    
}