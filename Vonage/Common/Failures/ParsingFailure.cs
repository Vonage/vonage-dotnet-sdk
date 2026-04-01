#region
using System;
using System.Linq;
using Vonage.Common.Exceptions;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Common.Failures;

/// <inheritdoc />
/// <example>
///     <code><![CDATA[
/// // Create a parsing failure from multiple validation errors
/// var failures = new[]
/// {
///     ResultFailure.FromErrorMessage("Name is required"),
///     ResultFailure.FromErrorMessage("Age must be positive")
/// };
/// ParsingFailure failure = ParsingFailure.FromFailures(failures);
/// Console.WriteLine(failure.GetFailureMessage());
/// // "Parsing failed with the following errors: Name is required, Age must be positive."
/// ]]></code>
/// </example>
public readonly struct ParsingFailure : IResultFailure
{
    private readonly ResultFailure[] failures;

    private ParsingFailure(ResultFailure[] failures) => this.failures = failures;

    /// <inheritdoc />
    public Type Type => typeof(ParsingFailure);

    /// <inheritdoc />
    public string GetFailureMessage() =>
        $"Parsing failed with the following errors: {string.Join(", ", this.failures.Select(failure => failure.GetFailureMessage()))}.";

    /// <inheritdoc />
    public Exception ToException() => new VonageException(this.GetFailureMessage());

    /// <inheritdoc />
    public Result<T> ToResult<T>() => Result<T>.FromFailure(this);

    /// <inheritdoc />
    public override bool Equals(object obj) =>
        obj is ParsingFailure failure && this.GetHashCode().Equals(failure.GetHashCode());

    /// <summary>
    ///     Creates a ParsingFailure from a list of failures.
    /// </summary>
    /// <param name="failures">The failures.</param>
    /// <returns>The parsing failure.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// ParsingFailure failure = ParsingFailure.FromFailures(
    ///     ResultFailure.FromErrorMessage("Field1 is invalid"),
    ///     ResultFailure.FromErrorMessage("Field2 is missing")
    /// );
    /// ]]></code>
    /// </example>
    public static ParsingFailure FromFailures(params ResultFailure[] failures) => new(failures);

    /// <inheritdoc />
    public override int GetHashCode() => this.GetFailureMessage().GetHashCode();
}