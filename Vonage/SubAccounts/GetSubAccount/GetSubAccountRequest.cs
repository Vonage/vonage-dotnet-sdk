#region
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.SubAccounts.GetSubAccount;

/// <summary>
///     Represents a request to retrieve a specific subaccount by its API key.
/// </summary>
public readonly struct GetSubAccountRequest : IVonageRequest
{
    private readonly string apiKey = string.Empty;

    private GetSubAccountRequest(string apiKey, string subAccountKey)
    {
        this.apiKey = apiKey;
        this.SubAccountKey = subAccountKey;
    }

    /// <summary>
    ///     The unique API key of the subaccount to retrieve.
    /// </summary>
    public string SubAccountKey { get; }

    /// <summary>
    ///     Creates a request to retrieve a specific subaccount.
    /// </summary>
    /// <param name="subAccountKey">The unique API key of the subaccount to retrieve.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the request on success,
    ///     or a validation error if the subaccount key is empty.
    /// </returns>
    public static Result<GetSubAccountRequest> Parse(string subAccountKey) =>
        Result<GetSubAccountRequest>
            .FromSuccess(new GetSubAccountRequest(string.Empty, subAccountKey))
            .Map(InputEvaluation<GetSubAccountRequest>.Evaluate)
            .Bind(evaluation => evaluation.WithRules(VerifySubAccountKey));

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, $"/accounts/{this.apiKey}/subaccounts/{this.SubAccountKey}")
        .Build();

    private static Result<GetSubAccountRequest> VerifySubAccountKey(GetSubAccountRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SubAccountKey, nameof(SubAccountKey));

    internal GetSubAccountRequest WithApiKey(string primaryAccountKey) => new(primaryAccountKey, this.SubAccountKey);
}