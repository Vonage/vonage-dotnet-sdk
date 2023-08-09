using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Users.CreateUser;
using Vonage.Users.DeleteUser;
using Vonage.Users.GetUser;
using Vonage.Users.GetUsers;

namespace Vonage.Users;

/// <summary>
///     Exposes User management features.
/// </summary>
public interface IUsersClient
{
    /// <summary>
    ///     Deletes a user.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<Unit>> DeleteUserAsync(Result<DeleteUserRequest> request);

    /// <summary>
    ///     Retrieves a user.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<User>> GetUserAsync(Result<GetUserRequest> request);

    /// <summary>
    ///     Retrieves users.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<GetUsersResponse>> GetUsersAsync(Result<GetUsersRequest> request);

    /// <summary>
    ///     Creates a user.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<User>> CreateUserAsync(Result<CreateUserRequest> request);
}