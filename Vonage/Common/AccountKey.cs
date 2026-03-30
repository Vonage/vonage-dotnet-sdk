#region
using System;
using System.Text.RegularExpressions;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Common;

/// <summary>
///     Represents a validated Vonage Account API key. The key must be exactly 8 alphanumeric characters.
/// </summary>
/// <remarks>
///     <para>Use <see cref="Parse"/> to create an instance with validation.</para>
///     <para>Validation rules: non-empty, exactly 8 characters, alphanumeric only.</para>
/// </remarks>
public readonly struct AccountKey
{
    private const string AlphaNumericError = $"{nameof(AccountKey)} should only contain alphanumeric characters.";

    private static readonly Regex
        AlphaNumericRegex = new("^[a-zA-Z0-9]+$", RegexOptions.None, TimeSpan.FromSeconds(1));

    private AccountKey(string apiKey) => this.ApiKey = apiKey;

    /// <summary>
    ///     Gets the account API key value.
    /// </summary>
    public string ApiKey { get; }

    /// <summary>
    ///     Parses and validates a Vonage Account API key.
    /// </summary>
    /// <param name="key">The API key to validate. Must be exactly 8 alphanumeric characters.</param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the validated <see cref="AccountKey"/> on success,
    ///     or an <see cref="IResultFailure"/> describing the validation error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// // Parse a valid account key
    /// var result = AccountKey.Parse("abc12345");
    /// result.Match(
    ///     success => Console.WriteLine($"Valid key: {success.ApiKey}"),
    ///     failure => Console.WriteLine($"Invalid: {failure.GetFailureMessage()}")
    /// );
    /// ]]></code>
    /// </example>
    public static Result<AccountKey> Parse(string key) =>
        Result<AccountKey>.FromSuccess(new AccountKey(key))
            .Bind(VerifyNotEmpty)
            .Bind(VerifyLength)
            .Bind(VerifyAlphaNumericOnly);

    /// <summary>
    ///     Returns the account ApiKey.
    /// </summary>
    /// <returns>The account ApiKey.</returns>
    public override string ToString() => this.ApiKey;

    private static Result<AccountKey> VerifyAlphaNumericOnly(AccountKey key) =>
        AlphaNumericRegex.IsMatch(key.ApiKey)
            ? key
            : Result<AccountKey>.FromFailure(
                ResultFailure.FromErrorMessage(AlphaNumericError));

    private static Result<AccountKey> VerifyLength(AccountKey key) =>
        InputValidation.VerifyLength(key, key.ApiKey, 8, nameof(AccountKey));

    private static Result<AccountKey> VerifyNotEmpty(AccountKey key) =>
        InputValidation.VerifyNotEmpty(key, key.ApiKey, nameof(AccountKey));
}