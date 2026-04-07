using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Users.CreateUser;
using Vonage.Users.DeleteUser;
using Vonage.Users.GetUser;
using Vonage.Users.GetUsers;
using Vonage.Users.UpdateUser;

namespace Vonage.Users;

/// <summary>
///     Exposes methods for managing users in the Vonage platform, including creating, retrieving, updating, and deleting users with their associated communication channels.
/// </summary>
public interface IUsersClient
{
    /// <summary>
    ///     Deletes an existing user from the Vonage platform.
    /// </summary>
    /// <param name="request">The request containing the user ID to delete. Use <see cref="DeleteUserRequest.Parse"/> to create the request.</param>
    /// <returns>
    ///     A result indicating success or failure. On success, returns <see cref="Unit"/>. On failure, contains error details.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = DeleteUserRequest.Parse("USR-12345678-1234-1234-1234-123456789012");
    /// var result = await client.DeleteUserAsync(request);
    /// ]]></code>
    /// </example>
    Task<Result<Unit>> DeleteUserAsync(Result<DeleteUserRequest> request);

    /// <summary>
    ///     Retrieves a specific user by their unique identifier.
    /// </summary>
    /// <param name="request">The request containing the user ID to retrieve. Use <see cref="GetUserRequest.Parse"/> to create the request.</param>
    /// <returns>
    ///     A result containing the <see cref="User"/> on success, or error details on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetUserRequest.Parse("USR-12345678-1234-1234-1234-123456789012");
    /// var result = await client.GetUserAsync(request);
    /// ]]></code>
    /// </example>
    Task<Result<User>> GetUserAsync(Result<GetUserRequest> request);

    /// <summary>
    ///     Retrieves a paginated list of users with optional filtering by name.
    /// </summary>
    /// <param name="request">The request containing pagination and filter options. Use <see cref="GetUsersRequest.Build"/> to create the request.</param>
    /// <returns>
    ///     A result containing the <see cref="GetUsersResponse"/> with user summaries and pagination links on success, or error details on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = GetUsersRequest.Build()
    ///     .WithPageSize(20)
    ///     .WithOrder(FetchOrder.Descending)
    ///     .Create();
    /// var result = await client.GetUsersAsync(request);
    /// ]]></code>
    /// </example>
    Task<Result<GetUsersResponse>> GetUsersAsync(Result<GetUsersRequest> request);

    /// <summary>
    ///     Creates a new user in the Vonage platform with optional profile information and communication channels.
    /// </summary>
    /// <param name="request">The request containing user details. Use <see cref="CreateUserRequest.Build"/> to create the request.</param>
    /// <returns>
    ///     A result containing the created <see cref="User"/> with their assigned ID on success, or error details on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = CreateUserRequest.Build()
    ///     .WithName("my-user")
    ///     .WithDisplayName("John Doe")
    ///     .Create();
    /// var result = await client.CreateUserAsync(request);
    /// ]]></code>
    /// </example>
    Task<Result<User>> CreateUserAsync(Result<CreateUserRequest> request);

    /// <summary>
    ///     Updates an existing user's profile information and communication channels.
    /// </summary>
    /// <param name="request">The request containing the user ID and updated details. Use <see cref="UpdateUserRequest.Build"/> to create the request.</param>
    /// <returns>
    ///     A result containing the updated <see cref="User"/> on success, or error details on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var request = UpdateUserRequest.Build()
    ///     .WithId("USR-12345678-1234-1234-1234-123456789012")
    ///     .WithDisplayName("Jane Doe")
    ///     .Create();
    /// var result = await client.UpdateUserAsync(request);
    /// ]]></code>
    /// </example>
    Task<Result<User>> UpdateUserAsync(Result<UpdateUserRequest> request);
}