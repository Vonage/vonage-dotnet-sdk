using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Users.GetUser;

/// <inheritdoc />
public readonly struct GetUserRequest : IVonageRequest
{
    private GetUserRequest(string userId) => this.UserId = userId;

    /// <summary>
    ///     ID of the user.
    /// </summary>
    public string UserId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, this.GetEndpointPath())
        .Build();

    /// <inheritdoc />
    public string GetEndpointPath() => $"/v1/users/{this.UserId}";

    /// <summary>
    ///     Parses the input into a GetUserRequest.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<GetUserRequest> Parse(string userId) =>
        Result<GetUserRequest>
            .FromSuccess(new GetUserRequest(userId))
            .Map(InputEvaluation<GetUserRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifyUserId));

    private static Result<GetUserRequest> VerifyUserId(GetUserRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.UserId, nameof(UserId));
}