#region
using System;
using System.Collections.Generic;
using System.Linq;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
#endregion

namespace Vonage.Common.Validation;

/// <summary>
///     Static methods to facilitate input validation.
/// </summary>
/// <example>
///     <code><![CDATA[
/// // Validate a request with multiple rules
/// var request = new MyRequest { Name = "John", Age = 25 };
/// Result<MyRequest> result = InputValidation.VerifyNotEmpty(request, request.Name, nameof(request.Name))
///     .Bind(r => InputValidation.VerifyHigherOrEqualThan(r, r.Age, 18, nameof(r.Age)));
/// ]]></code>
/// </example>
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
    /// <example>
    ///     <code><![CDATA[
    /// var request = new BatchRequest { Items = new[] { "a", "b", "c" } };
    /// Result<BatchRequest> result = InputValidation.VerifyCountLowerOrEqualThan(request, request.Items, 5, "Items");
    /// // Success because 3 items <= 5
    /// ]]></code>
    /// </example>
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
    /// <example>
    ///     <code><![CDATA[
    /// var request = new AgeRequest { Age = 21 };
    /// Result<AgeRequest> result = InputValidation.VerifyHigherOrEqualThan(request, request.Age, 18, "Age");
    /// // Success because 21 >= 18
    /// ]]></code>
    /// </example>
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
    /// <example>
    ///     <code><![CDATA[
    /// var request = new CountryRequest { CountryCode = "US" };
    /// Result<CountryRequest> result = InputValidation.VerifyLength(request, request.CountryCode, 2, "CountryCode");
    /// // Success because "US" has exactly 2 characters
    /// ]]></code>
    /// </example>
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
    /// <example>
    ///     <code><![CDATA[
    /// var request = new PasswordRequest { Password = "securePass123" };
    /// Result<PasswordRequest> result = InputValidation.VerifyLengthHigherOrEqualThan(request, request.Password, 8, "Password");
    /// // Success because password length >= 8
    /// ]]></code>
    /// </example>
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
    /// <example>
    ///     <code><![CDATA[
    /// var request = new UsernameRequest { Username = "john_doe" };
    /// Result<UsernameRequest> result = InputValidation.VerifyLengthLowerOrEqualThan(request, request.Username, 20, "Username");
    /// // Success because username length <= 20
    /// ]]></code>
    /// </example>
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
    /// <example>
    ///     <code><![CDATA[
    /// var request = new PageRequest { PageSize = 50 };
    /// Result<PageRequest> result = InputValidation.VerifyLowerOrEqualThan(request, request.PageSize, 100, "PageSize");
    /// // Success because 50 <= 100
    /// ]]></code>
    /// </example>
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
    /// <example>
    ///     <code><![CDATA[
    /// var request = new MessageRequest { Content = "Hello, world!" };
    /// Result<MessageRequest> result = InputValidation.VerifyNotEmpty(request, request.Content, "Content");
    /// // Success because Content is not empty
    /// ]]></code>
    /// </example>
    public static Result<T> VerifyNotEmpty<T>(T request, string value, string name) =>
        string.IsNullOrWhiteSpace(value)
            ? Result<T>.FromFailure(
                ResultFailure.FromErrorMessage($"{name} {StringCannotBeNullOrWhitespace}"))
            : request;

    /// <summary>
    ///     Verifies if not null or empty.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="value">The string.</param>
    /// <param name="name">The display name.</param>
    /// <typeparam name="T">The request type.</typeparam>
    /// <typeparam name="TElement">The nested element type.</typeparam>
    /// <returns>Success or Failure.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var request = new RecipientRequest { Recipients = new[] { "user1", "user2" } };
    /// Result<RecipientRequest> result = InputValidation.VerifyNotEmpty<RecipientRequest, string>(request, request.Recipients, "Recipients");
    /// // Success because Recipients has elements
    /// ]]></code>
    /// </example>
    public static Result<T> VerifyNotEmpty<T, TElement>(T request, IEnumerable<TElement> value, string name) =>
        !value.Any()
            ? Result<T>.FromFailure(
                ResultFailure.FromErrorMessage($"{name} {GuidCannotBeNullOrWhitespace}"))
            : request;

    /// <summary>
    ///     Verifies if not null or empty.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="value">The guid.</param>
    /// <param name="name">The display name.</param>
    /// <typeparam name="T">The request type.</typeparam>
    /// <returns>Success or Failure.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var request = new SessionRequest { SessionId = Guid.NewGuid() };
    /// Result<SessionRequest> result = InputValidation.VerifyNotEmpty(request, request.SessionId, "SessionId");
    /// // Success because SessionId is not empty
    /// ]]></code>
    /// </example>
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
    /// <example>
    ///     <code><![CDATA[
    /// var request = new PriceRequest { Amount = 99.99m };
    /// Result<PriceRequest> result = InputValidation.VerifyNotNegative(request, request.Amount, "Amount");
    /// // Success because Amount is not negative
    /// ]]></code>
    /// </example>
    public static Result<T> VerifyNotNegative<T>(T request, decimal value, string name) =>
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
    /// <example>
    ///     <code><![CDATA[
    /// var request = new TagsRequest { Tags = new List<string> { "urgent" } };
    /// Result<TagsRequest> result = InputValidation.VerifyNotNull<TagsRequest, string>(request, request.Tags, "Tags");
    /// // Success because Tags is not null
    /// ]]></code>
    /// </example>
    public static Result<TA> VerifyNotNull<TA, TB>(TA request, IEnumerable<TB> value, string name) =>
        value is null
            ? Result<TA>.FromFailure(
                ResultFailure.FromErrorMessage($"{name} {CollectionCannotBeNull}"))
            : request;

    /// <summary>
    ///     Verifies if not null.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="value">The object.</param>
    /// <param name="name">The display name.</param>
    /// <typeparam name="TA">The request type.</typeparam>
    /// <typeparam name="TB">The object type.</typeparam>
    /// <returns>Success or Failure.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// var request = new OrderRequest { Customer = new Customer { Name = "John" } };
    /// Result<OrderRequest> result = InputValidation.VerifyNotNull<OrderRequest, Customer>(request, request.Customer, "Customer");
    /// // Success because Customer is not null
    /// ]]></code>
    /// </example>
    public static Result<TA> VerifyNotNull<TA, TB>(TA request, TB value, string name) =>
        value is null
            ? Result<TA>.FromFailure(
                ResultFailure.FromErrorMessage($"{name} {CollectionCannotBeNull}"))
            : request;
}