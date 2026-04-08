#region
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Voice.Emergency.AssignNumber;
using Vonage.Voice.Emergency.CreateAddress;
using Vonage.Voice.Emergency.DeleteAddress;
using Vonage.Voice.Emergency.GetAddress;
using Vonage.Voice.Emergency.GetAddresses;
using Vonage.Voice.Emergency.GetNumber;
#endregion

namespace Vonage.Voice.Emergency;

/// <summary>
///     Exposes Emergency Calling API features for managing emergency addresses and assigning emergency-capable numbers.
/// </summary>
public interface IEmergencyClient
{
    /// <summary>
    ///     Retrieves the details of an emergency number, including its assigned address.
    /// </summary>
    /// <param name="request">The request containing the country and number to look up.</param>
    /// <returns>A <see cref="EmergencyNumberResponse"/> with the number details, or Failure if the number is not found.</returns>
    Task<Result<EmergencyNumberResponse>> GetNumberAsync(Result<GetNumberRequest> request);

    /// <summary>
    ///     Assigns an emergency address to a phone number, enabling it for emergency calling.
    /// </summary>
    /// <param name="request">The request containing the country, number, and address ID to assign.</param>
    /// <returns>A <see cref="EmergencyNumberResponse"/> confirming the assignment, or Failure if the request is invalid.</returns>
    Task<Result<EmergencyNumberResponse>> AssignNumberAsync(Result<AssignNumberRequest> request);

    /// <summary>
    ///     Retrieves the details of a specific emergency address by its ID.
    /// </summary>
    /// <param name="request">The request containing the address ID to retrieve.</param>
    /// <returns>The <see cref="Address"/> if found, or Failure if the address does not exist.</returns>
    Task<Result<Address>> GetAddressAsync(Result<GetAddressRequest> request);

    /// <summary>
    ///     Retrieves all emergency addresses configured on your account.
    /// </summary>
    /// <param name="request">The request for the address list.</param>
    /// <returns>A <see cref="GetAddressesResponse"/> containing all configured addresses, or Failure on error.</returns>
    Task<Result<GetAddressesResponse>> GetAddressesAsync(Result<GetAddressesRequest> request);

    /// <summary>
    ///     Deletes an emergency address from your account.
    /// </summary>
    /// <param name="request">The request containing the address ID to delete.</param>
    /// <returns>Success if the address was deleted, or Failure if the address was not found.</returns>
    Task<Result<Unit>> DeleteAddressAsync(Result<DeleteAddressRequest> request);

    /// <summary>
    ///     Creates a new emergency address on your account.
    /// </summary>
    /// <param name="request">The request containing the address details (street, city, state, country, postal code).</param>
    /// <returns>The created <see cref="Address"/> with its assigned ID, or Failure if validation fails.</returns>
    Task<Result<Address>> CreateAddressAsync(Result<CreateAddressRequest> request);
}