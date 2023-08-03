using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Users.DeleteUser;

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
    public Task<Result<Unit>> DeleteUserAsync(Result<DeleteUserRequest> request) =>
        this.vonageClient.SendAsync(request);
}