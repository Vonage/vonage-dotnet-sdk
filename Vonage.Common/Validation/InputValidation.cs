using System;
using System.Collections.Generic;
using System.Linq;
using Vonage.Common.Failures;
using Vonage.Common.Monads;

namespace Vonage.Common.Validation;

/// <summary>
///     Static methods to facilitate input validation.
/// </summary>
public static class InputValidation
{
    private const string CannotBeHigherThan = "cannot be higher than {value}.";
    private const string CannotBeLowerThan = "cannot be lower than {value}.";
    private const string CollectionCannotBeNull = "cannot be null.";
    private const string GuidCannotBeNullOrWhitespace = "cannot be empty.";
    private const string IntCannotBeNegative = "cannot be negative.";
    private const string StringCannotBeNullOrWhitespace = "cannot be null or whitespace.";
    private const string UnexpectedLength = "length should be {value}.";

    /// <summary>
    ///     Verifies if count lower or equal than specified threshold.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="value">The value.</param>
    /// <param name="maximumCount">The threshold.</param>
    /// <param name="name">The display name.</param>
    /// <typeparam name="T">The request type.</typeparam>
    /// <typeparam name="TItem">The collection item type.</typeparam>
    /// <returns>Success or Failure.</returns>
    public static Result<T> VerifyCountLowerOrEqualThan<T, TItem>(T request, IEnumerable<TItem> value, int maximumCount,
        string name) =>
        value.Count() > maximumCount
            ? Result<T>.FromFailure(
                ResultFailure.FromErrorMessage(
                    $"{name} count {CannotBeHigherThan.Replace("{value}", maximumCount.ToString())}"))
            : request;

    /// <summary>
    ///     Verifies if higher or equal than specified threshold.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="value">The value.</param>
    /// <param name="minValue">The threshold.</param>
    /// <param name="name">The display name.</param>
    /// <typeparam name="T">The request type.</typeparam>
    /// <returns>Success or Failure.</returns>
    public static Result<T> VerifyHigherOrEqualThan<T>(T request, int value, int minValue, string name) =>
        value < minValue
            ? Result<T>.FromFailure(
                ResultFailure.FromErrorMessage(
                    $"{name} {CannotBeLowerThan.Replace("{value}", minValue.ToString())}"))
            : request;

    /// <summary>
    ///     Verifies string length.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="value">The string.</param>
    /// <param name="expectedLength">The expected length.</param>
    /// <param name="name">The display name.</param>
    /// <typeparam name="T">The request type.</typeparam>
    /// <returns>Success or Failure.</returns>
    public static Result<T> VerifyLength<T>(T request, string value, int expectedLength, string name) =>
        value.Length != expectedLength
            ? Result<T>.FromFailure(
                ResultFailure.FromErrorMessage(
                    $"{name} {UnexpectedLength.Replace("{value}", expectedLength.ToString())}"))
            : request;

    /// <summary>
    ///     Verifies if length higher or equal than specified threshold.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="value">The value.</param>
    /// <param name="minimumLength">The threshold.</param>
    /// <param name="name">The display name.</param>
    /// <typeparam name="T">The request type.</typeparam>
    /// <returns>Success or Failure.</returns>
    public static Result<T> VerifyLengthHigherOrEqualThan<T>(T request, string value, int minimumLength, string name) =>
        value.Length < minimumLength
            ? Result<T>.FromFailure(
                ResultFailure.FromErrorMessage(
                    $"{name} length {CannotBeLowerThan.Replace("{value}", minimumLength.ToString())}"))
            : request;

    /// <summary>
    ///     Verifies if length lower or equal than specified threshold.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="value">The value.</param>
    /// <param name="maximumLength">The threshold.</param>
    /// <param name="name">The display name.</param>
    /// <typeparam name="T">The request type.</typeparam>
    /// <returns>Success or Failure.</returns>
    public static Result<T> VerifyLengthLowerOrEqualThan<T>(T request, string value, int maximumLength, string name) =>
        value?.Length > maximumLength
            ? Result<T>.FromFailure(
                ResultFailure.FromErrorMessage(
                    $"{name} length {CannotBeHigherThan.Replace("{value}", maximumLength.ToString())}"))
            : request;

    /// <summary>
    ///     Verifies if lower or equal than specified threshold.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="value">The value.</param>
    /// <param name="maxValue">The threshold.</param>
    /// <param name="name">The display name.</param>
    /// <typeparam name="T">The request type.</typeparam>
    /// <returns>Success or Failure.</returns>
    public static Result<T> VerifyLowerOrEqualThan<T>(T request, int value, int maxValue, string name) =>
        value > maxValue
            ? Result<T>.FromFailure(
                ResultFailure.FromErrorMessage(
                    $"{name} {CannotBeHigherThan.Replace("{value}", maxValue.ToString())}"))
            : request;

    /// <summary>
    ///     Verifies if not null or empty.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="value">The string.</param>
    /// <param name="name">The display name.</param>
    /// <typeparam name="T">The request type.</typeparam>
    /// <returns>Success or Failure.</returns>
    public static Result<T> VerifyNotEmpty<T>(T request, string value, string name) =>
        string.IsNullOrWhiteSpace(value)
            ? Result<T>.FromFailure(
                ResultFailure.FromErrorMessage($"{name} {StringCannotBeNullOrWhitespace}"))
            : request;

    /// <summary>
    ///     Verifies if not null or empty.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="value">The guid.</param>
    /// <param name="name">The display name.</param>
    /// <typeparam name="T">The request type.</typeparam>
    /// <returns>Success or Failure.</returns>
    public static Result<T> VerifyNotEmpty<T>(T request, Guid value, string name) =>
        value.Equals(Guid.Empty)
            ? Result<T>.FromFailure(
                ResultFailure.FromErrorMessage($"{name} {GuidCannotBeNullOrWhitespace}"))
            : request;

    /// <summary>
    ///     Verifies if not negative.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="value">The value.</param>
    /// <param name="name">The display name.</param>
    /// <typeparam name="T">The request type.</typeparam>
    /// <returns>Success or Failure.</returns>
    public static Result<T> VerifyNotNegative<T>(T request, int value, string name) =>
        value < 0
            ? Result<T>.FromFailure(
                ResultFailure.FromErrorMessage($"{name} {IntCannotBeNegative}"))
            : request;

    /// <summary>
    ///     Verifies if not null.
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