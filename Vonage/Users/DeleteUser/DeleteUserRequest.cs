#region
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Users.DeleteUser;

/// <summary>
///     Represents a request to delete an existing user from the Vonage platform.
/// </summary>
public readonly struct DeleteUserRequest : IVonageRequest
{
    private DeleteUserRequest(string userId) => this.UserId = userId;

    /// <summary>
    ///     The unique identifier of the user to delete (e.g., "USR-12345678-1234-1234-1234-123456789012").
    /// </summary>
    public string UserId { get; internal init; }

    /// <summary>
    ///     Creates a DeleteUserRequest from the specified user ID.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to delete. Must not be empty.</param>
    /// <returns>A result containing the <see cref="DeleteUserRequest"/> on success, or validation error details if the user ID is empty.</returns>
    public static Result<DeleteUserRequest> Parse(string userId) =>
        Result<DeleteUserRequest>
            .FromSuccess(new DeleteUserRequest(userId))
            .Map(InputEvaluation<DeleteUserRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyUserId));

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Delete, $"/v1/users/{this.UserId}")
        .Build();

    private static Result<DeleteUserRequest> VerifyUserId(DeleteUserRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.UserId, nameof(UserId));
}