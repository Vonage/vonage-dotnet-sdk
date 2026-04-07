#region
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Users.GetUser;

/// <summary>
///     Represents a request to retrieve a specific user by their unique identifier.
/// </summary>
public readonly struct GetUserRequest : IVonageRequest
{
    private GetUserRequest(string userId) => this.UserId = userId;

    /// <summary>
    ///     The unique identifier of the user to retrieve (e.g., "USR-12345678-1234-1234-1234-123456789012").
    /// </summary>
    public string UserId { get; internal init; }

    /// <summary>
    ///     Creates a GetUserRequest from the specified user ID.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to retrieve. Must not be empty.</param>
    /// <returns>A result containing the <see cref="GetUserRequest"/> on success, or validation error details if the user ID is empty.</returns>
    public static Result<GetUserRequest> Parse(string userId) =>
        Result<GetUserRequest>
            .FromSuccess(new GetUserRequest(userId))
            .Map(InputEvaluation<GetUserRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyUserId));

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, $"/v1/users/{this.UserId}")
        .Build();

    private static Result<GetUserRequest> VerifyUserId(GetUserRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.UserId, nameof(UserId));
}