#region
using System.Linq;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.Common;

/// <summary>
///     Represents a validated E.164 phone number. The number must be 7-15 digits, containing only numeric characters.
/// </summary>
/// <remarks>
///     <para>E.164 is the international telephone numbering plan that ensures each device has a globally unique number.</para>
///     <para>Use <see cref="Parse"/> to create an instance with validation. The international indicator (+) is optional and will be stripped during parsing.</para>
///     <para>See <see href="https://en.wikipedia.org/wiki/E.164">E.164 on Wikipedia</see> for more information.</para>
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
    ///     Parses and validates a phone number according to E.164 specifications.
    /// </summary>
    /// <param name="number">
    ///     The phone number to validate. May include the international indicator (+).
    ///     Must be 7-15 digits after removing the indicator.
    /// </param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the validated <see cref="PhoneNumber"/> on success,
    ///     or an <see cref="IResultFailure"/> describing the validation error on failure.
    /// </returns>
    /// <example>
    /// <code><![CDATA[
    /// var result = PhoneNumber.Parse("+14155552671");
    /// result.Match(
    ///     phone => Console.WriteLine($"Number: {phone.NumberWithInternationalIndicator}"),
    ///     failure => Console.WriteLine($"Invalid: {failure.GetFailureMessage()}")
    /// );
    /// ]]></code>
    /// </example>
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