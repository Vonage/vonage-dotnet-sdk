using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Users.CreateUser;
using Vonage.Users.DeleteUser;
using Vonage.Users.GetUser;
using Vonage.Users.GetUsers;
using Vonage.Users.UpdateUser;

namespace Vonage.Users;

internal class UsersClient : IUsersClient
{
    private readonly VonageHttpClient vonageClient;

    /// <summary>
    ///     Creates a new client.
    /// </summary>
    /// <param name="configuration">The client configuration.</param>
    internal UsersClient(VonageHttpClientConfiguration configuration) =>
        this.vonageClient = new VonageHttpClient(configuration, JsonSerializer.BuildWithSnakeCase());

    /// <inheritdoc />
    public Task<Result<User>> CreateUserAsync(Result<CreateUserRequest> request) =>
        this.vonageClient.SendWithResponseAsync<CreateUserRequest, User>(request);

    /// <inheritdoc />
    public Task<Result<Unit>> DeleteUserAsync(Result<DeleteUserRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <inheritdoc />
    public Task<Result<User>> GetUserAsync(Result<GetUserRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetUserRequest, User>(request);

    /// <inheritdoc />
    public Task<Result<GetUsersResponse>> GetUsersAsync(Result<GetUsersRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetUsersRequest, GetUsersResponse>(request);

    /// <inheritdoc />
    public Task<Result<User>> UpdateUserAsync(Result<UpdateUserRequest> request) =>
        this.vonageClient.SendWithResponseAsync<UpdateUserRequest, User>(request);
}