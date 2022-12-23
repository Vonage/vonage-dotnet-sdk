using System.Collections.Generic;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Video;

namespace Vonage.Video.Beta.Common.Validation;

/// <summary>
/// Static methods to facilitate input validation.
/// </summary>
public static class InputValidation
{
    private const string CollectionCannotBeNull = "cannot be null.";
    private const string StringCannotBeNullOrWhitespace = "cannot be null or whitespace.";

    /// <summary>
    /// Verifies if not null or empty.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="value">The string.</param>
    /// <param name="name">The display name.</param>
    /// <typeparam name="T">The request type.</typeparam>
    /// <returns>Success or Failure.</returns>
    public static Result<T> VerifyNotEmpty<T>(T request, string value, string name) where T : IVideoRequest =>
        string.IsNullOrWhiteSpace(value)
            ? Result<T>.FromFailure(
                ResultFailure.FromErrorMessage($"{name} {StringCannotBeNullOrWhitespace}"))
            : request;

    /// <summary>
    /// Verifies if not null.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="value">The collection.</param>
    /// <param name="name">The display name.</param>
    /// <typeparam name="TA">The request type.</typeparam>
    /// <typeparam name="TB">The list content type.</typeparam>
    /// <returns>Success or Failure.</returns>
    public static Result<TA> VerifyNotNull<TA, TB>(TA request, IEnumerable<TB> value, string name) =>
        value is null
            ? Result<TA>.FromFailure(
                ResultFailure.FromErrorMessage($"{name} {CollectionCannotBeNull}"))
            : request;
}