using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.AccountsNew.GetSecret;

/// <summary>
///     Represents a request to retrieve a specific API secret.
/// </summary>
[Builder]
public readonly partial struct GetSecretRequest : IVonageRequest
{
    /// <summary>
    ///     Sets the API key of the account the secret belongs to.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithApiKey("abcd1234")
    /// ]]></code>
    /// </example>
    [Mandatory(0)]
    public string ApiKey { get; internal init; }

    /// <summary>
    ///     Sets the unique identifier of the secret to retrieve.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithSecretId("ad6dc56f-07b5-46e1-a527-85530e625800")
    /// ]]></code>
    /// </example>
    [Mandatory(1)]
    public string SecretId { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Get, $"/accounts/{this.ApiKey}/secrets/{this.SecretId}")
        .Build();

    [ValidationRule]
    internal static Result<GetSecretRequest> VerifyApiKey(GetSecretRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApiKey, nameof(request.ApiKey));

    [ValidationRule]
    internal static Result<GetSecretRequest> VerifySecretId(GetSecretRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SecretId, nameof(request.SecretId));
}
