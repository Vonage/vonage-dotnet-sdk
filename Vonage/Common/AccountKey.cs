using System;
using System.Text.RegularExpressions;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Common;

/// <summary>
///     Represents a Vonage Account Key.
/// </summary>
public readonly struct AccountKey
{
    private const string AlphaNumericError = $"{nameof(AccountKey)} should only contain alphanumeric characters.";

    private static readonly Regex
        AlphaNumericRegex = new("^[a-zA-Z0-9]+$", RegexOptions.None, TimeSpan.FromSeconds(1));

    private AccountKey(string apiKey) => this.ApiKey = apiKey;

    /// <summary>
    ///     The account ApiKey.
    /// </summary>
    public string ApiKey { get; }

    /// <summary>
    ///     Creates an AccountKey.
    /// </summary>
    /// <param name="key">The ApiKey.</param>
    /// <returns>Success if the parsing succeeds. Failure otherwise.</returns>
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