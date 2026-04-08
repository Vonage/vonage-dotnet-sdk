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
    /// <example>
    /// <code><![CDATA[
    /// var result = await client.GetNumberAsync(GetNumberRequest.Parse("14155550100"));
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task<Result<EmergencyNumberResponse>> GetNumberAsync(Result<GetNumberRequest> request);

    /// <summary>
    ///     Assigns an emergency address to a phone number, enabling it for emergency calling.
    /// </summary>
    /// <param name="request">The request containing the country, number, and address ID to assign.</param>
    /// <returns>A <see cref="EmergencyNumberResponse"/> confirming the assignment, or Failure if the request is invalid.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = AssignNumberRequest.Build()
    ///     .WithNumber("14155550100")
    ///     .WithAddressId(Guid.Parse("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"))
    ///     .WithContactName("John Doe")
    ///     .Create();
    /// var result = await client.AssignNumberAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task<Result<EmergencyNumberResponse>> AssignNumberAsync(Result<AssignNumberRequest> request);

    /// <summary>
    ///     Retrieves the details of a specific emergency address by its ID.
    /// </summary>
    /// <param name="request">The request containing the address ID to retrieve.</param>
    /// <returns>The <see cref="Address"/> if found, or Failure if the address does not exist.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetAddressRequest.Build()
    ///     .WithId(Guid.Parse("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"))
    ///     .Create();
    /// var result = await client.GetAddressAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task<Result<Address>> GetAddressAsync(Result<GetAddressRequest> request);

    /// <summary>
    ///     Retrieves all emergency addresses configured on your account.
    /// </summary>
    /// <param name="request">The request for the address list.</param>
    /// <returns>A <see cref="GetAddressesResponse"/> containing all configured addresses, or Failure on error.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetAddressesRequest.Build()
    ///     .WithPage(1)
    ///     .WithPageSize(50)
    ///     .Create();
    /// var result = await client.GetAddressesAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task<Result<GetAddressesResponse>> GetAddressesAsync(Result<GetAddressesRequest> request);

    /// <summary>
    ///     Deletes an emergency address from your account.
    /// </summary>
    /// <param name="request">The request containing the address ID to delete.</param>
    /// <returns>Success if the address was deleted, or Failure if the address was not found.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = DeleteAddressRequest.Build()
    ///     .WithId(Guid.Parse("8f35a1a7-eb2f-4552-8fdf-fffdaee41bc9"))
    ///     .Create();
    /// var result = await client.DeleteAddressAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task<Result<Unit>> DeleteAddressAsync(Result<DeleteAddressRequest> request);

    /// <summary>
    ///     Creates a new emergency address on your account.
    /// </summary>
    /// <param name="request">The request containing the address details (street, city, state, country, postal code).</param>
    /// <returns>The created <see cref="Address"/> with its assigned ID, or Failure if validation fails.</returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = CreateAddressRequest.Build()
    ///     .WithName("Office HQ")
    ///     .WithFirstAddressLine("123 Main Street")
    ///     .WithCity("San Francisco")
    ///     .WithRegion("CA")
    ///     .WithPostalCode("94105")
    ///     .WithCountry("US")
    ///     .Create();
    /// var result = await client.CreateAddressAsync(request);
    /// ]]></code>
    /// </example>
    /// <seealso href="https://github.com/Vonage/vonage-dotnet-code-snippets/tree/master/DotNetCliCodeSnippets/Voice">More examples in the snippets repository</seealso>
    Task<Result<Address>> CreateAddressAsync(Result<CreateAddressRequest> request);
}