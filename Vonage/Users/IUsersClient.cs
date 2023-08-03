using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Users.DeleteUser;
using Vonage.Users.GetUser;

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
}