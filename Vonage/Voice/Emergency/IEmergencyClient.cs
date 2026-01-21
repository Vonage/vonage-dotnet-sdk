#region
using System.Threading.Tasks;
using Vonage.Common.Monads;
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
    Task<Result<GetNumberResponse>> GetNumberAsync(Result<GetNumberRequest> request);
}