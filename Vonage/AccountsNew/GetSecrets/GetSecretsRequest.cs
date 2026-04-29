using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.AccountsNew.GetSecrets;

/// <summary>
///     Represents a request to retrieve all API secrets for an account.
/// </summary>
[Builder]
public readonly partial struct GetSecretsRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the API key of the account whose secrets to retrieve.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithApiKey("abcd1234")
    /// ]]></code>
    /// </example>
    [Mandatory(0)]
    public string ApiKey { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, $"/accounts/{this.ApiKey}/secrets")
        .Build();

    [ValidationRule]
    internal static Result<GetSecretsRequest> VerifyApiKey(GetSecretsRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApiKey, nameof(request.ApiKey));
}
