using System.Collections.Generic;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Common.Monads;

namespace Vonage.Video.Beta.Common.Identifiers;

/// <summary>
///     Represents a string identifier.
/// </summary>
public class StringIdentifier : ValueObject<string>
{
    private const string CannotBeNullOrWhitespace = "Value cannot be null or whitespace.";

    private StringIdentifier(string value)
    {
        this.Value = value;
    }

    /// <summary>
    ///     Retrieves the identifier value.
    /// </summary>
    public string Value { get; }

    /// <summary>
    ///     Parses the value into an Identifier.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>A success state with the value if the parsing succeeded. A failure state with an error if it failed.</returns>
    public static Result<StringIdentifier> Parse(string value) =>
        string.IsNullOrWhiteSpace(value)
            ? Result<StringIdentifier>.FromFailure(CreateFailure())
            : new StringIdentifier(value);

    private static IResultFailure CreateFailure() =>
        ResultFailure.FromErrorMessage("Value cannot be null or whitespace.");

    /// <inheritdoc />
    protected override IEnumerable<object> GetEqualityComponents() => new[] {this.Value};
}