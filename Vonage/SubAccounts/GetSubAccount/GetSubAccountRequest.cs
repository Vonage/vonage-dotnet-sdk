#region
using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.SubAccounts.GetSubAccount;

/// <inheritdoc />
public readonly struct GetSubAccountRequest : IVonageRequest
{
    private readonly string apiKey = string.Empty;

    private GetSubAccountRequest(string apiKey, string subAccountKey)
    {
        this.apiKey = apiKey;
        this.SubAccountKey = subAccountKey;
    }

    /// <summary>
    ///     The SubAccount Id.
    /// </summary>
    public string SubAccountKey { get; }

    /// <summary>
    ///     Parses the input into a GetSubAccountRequest.
    /// </summary>
    /// <param name="subAccountKey">The SubAccount Id.</param>
    /// <returns>A success state with the request if the parsing succeeded. A failure state with an error if it failed.</returns>
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