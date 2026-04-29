using System.Net.Http;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.AccountsNew.RevokeSecret;

/// <summary>
///     Represents a request to revoke (delete) an API secret.
/// </summary>
[Builder]
public readonly partial struct RevokeSecretRequest : IVonageRequest
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
    ///     Sets the unique identifier of the secret to revoke.
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
        .Initialize(HttpMethod.Delete, $"/accounts/{this.ApiKey}/secrets/{this.SecretId}")
        .Build();

    [ValidationRule]
    internal static Result<RevokeSecretRequest> VerifyApiKey(RevokeSecretRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApiKey, nameof(request.ApiKey));

    [ValidationRule]
    internal static Result<RevokeSecretRequest> VerifySecretId(RevokeSecretRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.SecretId, nameof(request.SecretId));
}
