#region
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Voice.Emergency.AssignNumber;
using Vonage.Voice.Emergency.GetAddress;
using Vonage.Voice.Emergency.GetNumber;
#endregion

namespace Vonage.Voice.Emergency;

/// <summary>
///     Exposes Emergency API features.
/// </summary>
public interface IEmergencyClient
{
    /// <summary>
    ///     Gets an emergency number details.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<EmergencyNumberResponse>> GetNumberAsync(Result<GetNumberRequest> request);

    /// <summary>
    ///     Assigns an emergency number.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<EmergencyNumberResponse>> AssignNumberAsync(Result<AssignNumberRequest> request);

    /// <summary>
    ///     Gets an address details.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Address>> GetAddressAsync(Result<GetAddressRequest> request);
}