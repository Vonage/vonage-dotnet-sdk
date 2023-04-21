using System.Linq;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Validation;

namespace Vonage.Common;

/// <summary>
///     Represents a E.164 phone number.
/// </summary>
/// <remarks>
///     See https://en.wikipedia.org/wiki/E.164.
/// </remarks>
public readonly struct PhoneNumber
{
    private const int MaximumLength = 15;
    private const int MinimumLength = 7;
    private const string InternationalIndicator = "+";
    private const string MustContainDigits = "Number can only contain digits.";

    private PhoneNumber(string number) => this.Number = number;

    /// <summary>
    ///     Gets the phone number without international indicator.
    /// </summary>
    public string Number { get; }

    /// <summary>
    ///     Gets the phone number with international indicator.
    /// </summary>
    public string NumberWithInternationalIndicator => string.Concat("+", this.Number);

    /// <summary>
    ///     Parses the input into a PhoneNumber following E.164 specifications.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <returns>Success if the input matches all requirements. Failure otherwise.</returns>
    public static Result<PhoneNumber> Parse(string number) =>
        Result<PhoneNumber>.FromSuccess(new PhoneNumber(number))
            .Bind(VerifyNumberNotEmpty)
            .Map(RemoveInternationalIndicator)
            .Bind(VerifyLengthHigherThanMinimum)
            .Bind(VerifyLengthLowerThanMaximum)
            .Bind(VerifyDigitsOnly);

    /// <inheritdoc />
    public override string ToString() => this.Number;

    private static PhoneNumber RemoveInternationalIndicator(PhoneNumber value) =>
        new(value.Number.Replace(InternationalIndicator, string.Empty));

    private static Result<PhoneNumber> VerifyDigitsOnly(
        PhoneNumber request) =>
        request.Number.Select(char.IsDigit).All(_ => _)
            ? request
            : Result<PhoneNumber>.FromFailure(ResultFailure.FromErrorMessage(MustContainDigits));

    private static Result<PhoneNumber> VerifyLengthHigherThanMinimum(
        PhoneNumber request) =>
        InputValidation
            .VerifyLengthHigherOrEqualThan(request, request.Number, MinimumLength, nameof(request.Number));

    private static Result<PhoneNumber> VerifyLengthLowerThanMaximum(
        PhoneNumber request) =>
        InputValidation
            .VerifyLengthLowerOrEqualThan(request, request.Number, MaximumLength, nameof(request.Number));

    private static Result<PhoneNumber> VerifyNumberNotEmpty(PhoneNumber number) =>
        InputValidation
            .VerifyNotEmpty(number, number.Number, nameof(number.Number));
}