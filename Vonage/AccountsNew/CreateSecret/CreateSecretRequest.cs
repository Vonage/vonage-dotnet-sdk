using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using Vonage.Common.Client;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
using Vonage.Serialization;

namespace Vonage.AccountsNew.CreateSecret;

/// <summary>
///     Represents a request to create a new API secret for an account.
/// </summary>
[Builder]
public readonly partial struct CreateSecretRequest : IVonageRequest
{
    private const int SecretMinLength = 8;
    private const int SecretMaxLength = 25;

    /// <summary>
    ///     Sets the API key of the account to create the secret for.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithApiKey("abcd1234")
    /// ]]></code>
    /// </example>
    [JsonIgnore]
    [Mandatory(0)]
    public string ApiKey { get; internal init; }

    /// <summary>
    ///     Sets the new API secret. Must be 8–25 characters and contain at least one lowercase letter, one uppercase letter, and one digit.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// .WithSecret("example-4PI-s3cret")
    /// ]]></code>
    /// </example>
    [Mandatory(1)]
    public string Secret { get; internal init; }

    /// <inheritdoc />
    public HttpRequestMessage BuildRequestMessage() => VonageRequestBuilder
        .Initialize(HttpMethod.Post, $"/accounts/{this.ApiKey}/secrets")
        .WithContent(this.GetRequestContent())
        .Build();

    private StringContent GetRequestContent() =>
        new StringContent(JsonSerializerBuilder.BuildWithSnakeCase().SerializeObject(this),
            Encoding.UTF8, "application/json");

    [ValidationRule]
    internal static Result<CreateSecretRequest> VerifyApiKey(CreateSecretRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.ApiKey, nameof(request.ApiKey));

    [ValidationRule]
    internal static Result<CreateSecretRequest> VerifySecret(CreateSecretRequest request) =>
        InputValidation.VerifyNotEmpty(request, request.Secret, nameof(request.Secret));

    [ValidationRule]
    internal static Result<CreateSecretRequest> VerifySecretMinLength(CreateSecretRequest request) =>
        string.IsNullOrWhiteSpace(request.Secret)
            ? request
            : InputValidation.VerifyLengthHigherOrEqualThan(request, request.Secret, SecretMinLength,
                nameof(request.Secret));

    [ValidationRule]
    internal static Result<CreateSecretRequest> VerifySecretMaxLength(CreateSecretRequest request) =>
        string.IsNullOrWhiteSpace(request.Secret)
            ? request
            : InputValidation.VerifyLengthLowerOrEqualThan(request, request.Secret, SecretMaxLength,
                nameof(request.Secret));

    [ValidationRule]
    internal static Result<CreateSecretRequest> VerifySecretHasLowercase(CreateSecretRequest request) =>
        string.IsNullOrWhiteSpace(request.Secret) || System.Linq.Enumerable.Any(request.Secret, char.IsLower)
            ? request
            : Result<CreateSecretRequest>.FromFailure(
                ResultFailure.FromErrorMessage("Secret must contain at least 1 lowercase character."));

    [ValidationRule]
    internal static Result<CreateSecretRequest> VerifySecretHasUppercase(CreateSecretRequest request) =>
        string.IsNullOrWhiteSpace(request.Secret) || System.Linq.Enumerable.Any(request.Secret, char.IsUpper)
            ? request
            : Result<CreateSecretRequest>.FromFailure(
                ResultFailure.FromErrorMessage("Secret must contain at least 1 uppercase character."));

    [ValidationRule]
    internal static Result<CreateSecretRequest> VerifySecretHasDigit(CreateSecretRequest request) =>
        string.IsNullOrWhiteSpace(request.Secret) || System.Linq.Enumerable.Any(request.Secret, char.IsDigit)
            ? request
            : Result<CreateSecretRequest>.FromFailure(
                ResultFailure.FromErrorMessage("Secret must contain at least 1 digit."));
}
