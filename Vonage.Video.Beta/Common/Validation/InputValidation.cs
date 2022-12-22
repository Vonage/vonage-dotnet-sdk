using System.Collections.Generic;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Common.Monads;
using Vonage.Video.Beta.Video;

namespace Vonage.Video.Beta.Common.Validation;

/// <summary>
/// </summary>
public static class InputValidation
{
    private const string CollectionCannotBeNull = "cannot be null.";
    private const string StringCannotBeNullOrWhitespace = "cannot be null or whitespace.";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="value"></param>
    /// <param name="name"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Result<T> VerifyNotEmpty<T>(T request, string value, string name) where T : IVideoRequest =>
        string.IsNullOrWhiteSpace(value)
            ? Result<T>.FromFailure(
                ResultFailure.FromErrorMessage($"{name} {StringCannotBeNullOrWhitespace}"))
            : request;

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="value"></param>
    /// <param name="name"></param>
    /// <typeparam name="TA"></typeparam>
    /// <typeparam name="TB"></typeparam>
    /// <returns></returns>
    public static Result<TA> VerifyItems<TA, TB>(TA request, IEnumerable<TB> value, string name) =>
        value is null
            ? Result<TA>.FromFailure(
                ResultFailure.FromErrorMessage($"{name} {CollectionCannotBeNull}"))
            : request;
}