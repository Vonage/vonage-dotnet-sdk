﻿#region
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Users.DeleteUser;

/// <inheritdoc />
public readonly struct DeleteUserRequest : IVonageRequest
{
    private DeleteUserRequest(string userId) => this.UserId = userId;

    /// <summary>
    ///     ID of the user.
    /// </summary>
    public string UserId { get; internal init; }

    /// <summary>
    ///     Parses the input into a DeleteUserRequest.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
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