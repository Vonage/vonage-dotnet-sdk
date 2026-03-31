#region
using System;
using System.Threading.Tasks;
using Vonage.Common.Failures;
#endregion

namespace Vonage.Common.Monads;

/// <summary>
///     Extensions for Result, including async operations on Task of Result.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    ///     Monadic bind operation on an asynchronous Result.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="bind">Bind operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Return type.</typeparam>
    /// <returns>Bound functor.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Result<int>> asyncResult = GetResultAsync();
    /// Result<string> bound = await asyncResult.Bind(value => Result<string>.FromSuccess(value.ToString()));
    /// ]]></code>
    /// </example>
    public static async Task<Result<TDestination>> Bind<TSource, TDestination>(this Task<Result<TSource>> task,
        Func<TSource, Result<TDestination>> bind)
    {
        var result = await task.ConfigureAwait(false);
        return result.Bind(bind);
    }

    /// <summary>
    ///     Monadic bind operation with an asynchronous bind function.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="bind">Asynchronous bind operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Return type.</typeparam>
    /// <returns>Asynchronous bound functor.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Result<int>> asyncUserId = GetUserIdAsync();
    /// Result<User> user = await asyncUserId.BindAsync(id => FetchUserAsync(id));
    /// ]]></code>
    /// </example>
    public static async Task<Result<TDestination>> BindAsync<TSource, TDestination>(this Task<Result<TSource>> task,
        Func<TSource, Task<Result<TDestination>>> bind)
    {
        var result = await task.ConfigureAwait(false);
        return await result.BindAsync(bind).ConfigureAwait(false);
    }

    /// <summary>
    ///     Executes operations depending on the current state of an asynchronous Result.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="successOperation">Success operation.</param>
    /// <param name="failureOperation">Failure operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <returns>The initial result.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Result<string>> asyncResult = GetResultAsync();
    /// await asyncResult.Do(
    ///     successOperation: value => Console.WriteLine($"Success: {value}"),
    ///     failureOperation: error => Console.WriteLine($"Error: {error.GetFailureMessage()}")
    /// );
    /// ]]></code>
    /// </example>
    public static async Task<Result<TSource>> Do<TSource>(this Task<Result<TSource>> task,
        Action<TSource> successOperation, Action<IResultFailure> failureOperation)
    {
        var result = await task.ConfigureAwait(false);
        return result.Do(successOperation, failureOperation);
    }

    /// <summary>
    ///     Executes an operation if the asynchronous Result is in Failure state.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="failureOperation">Failure operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <returns>The initial result.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Result<string>> asyncResult = GetResultAsync();
    /// await asyncResult.DoWhenFailure(error => logger.LogError(error.GetFailureMessage()));
    /// ]]></code>
    /// </example>
    public static async Task<Result<TSource>> DoWhenFailure<TSource>(this Task<Result<TSource>> task,
        Action<IResultFailure> failureOperation)
    {
        var result = await task.ConfigureAwait(false);
        return result.DoWhenFailure(failureOperation);
    }

    /// <summary>
    ///     Executes an operation if the asynchronous Result is in Success state.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="successOperation">Success operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <returns>The initial result.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Result<string>> asyncResult = GetResultAsync();
    /// await asyncResult.DoWhenSuccess(value => Console.WriteLine($"Got: {value}"));
    /// ]]></code>
    /// </example>
    public static async Task<Result<TSource>> DoWhenSuccess<TSource>(this Task<Result<TSource>> task,
        Action<TSource> successOperation)
    {
        var result = await task.ConfigureAwait(false);
        return result.DoWhenSuccess(successOperation);
    }

    /// <summary>
    ///     Returns the default value if the asynchronous Result is in the Failure state, the success value otherwise.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="defaultValue">Value to return if in the Failure state.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <returns>The default value if the Result is in the Failure state, the success value otherwise.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Result<int>> asyncResult = GetResultAsync();
    /// int value = await asyncResult.IfFailure(0); // Returns 0 if Failure
    /// ]]></code>
    /// </example>
    public static async Task<TSource> IfFailure<TSource>(this Task<Result<TSource>> task, TSource defaultValue)
    {
        var result = await task.ConfigureAwait(false);
        return result.IfFailure(defaultValue);
    }

    /// <summary>
    ///     Invokes the action if the asynchronous Result is in the Success state, otherwise nothing happens.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="action">Action to invoke.</param>
    /// <typeparam name="T">The source type.</typeparam>
    /// <returns>The initial result.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Result<string>> asyncResult = GetResultAsync();
    /// await asyncResult.IfSuccess(value => Console.WriteLine($"Got: {value}"));
    /// ]]></code>
    /// </example>
    public static async Task<Result<T>> IfSuccess<T>(this Task<Result<T>> task, Action<T> action)
    {
        var result = await task.ConfigureAwait(false);
        return result.IfSuccess(action);
    }

    /// <summary>
    ///     Invokes the asynchronous action if the Result is in the Success state, otherwise nothing happens.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="action">Asynchronous action to invoke.</param>
    /// <typeparam name="T">The source type.</typeparam>
    /// <returns>The initial result.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Result<string>> asyncResult = GetResultAsync();
    /// await asyncResult.IfSuccessAsync(async value => await SaveAsync(value));
    /// ]]></code>
    /// </example>
    public static async Task<Result<T>> IfSuccessAsync<T>(this Task<Result<T>> task, Func<T, Task> action)
    {
        var result = await task.ConfigureAwait(false);
        return await result.IfSuccessAsync(action).ConfigureAwait(false);
    }

    /// <summary>
    ///     Projects from one value to another on an asynchronous Result.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="map">Projection function.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Resulting functor value type.</typeparam>
    /// <returns>Asynchronous mapped functor.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Result<int>> asyncResult = GetResultAsync();
    /// Result<string> mapped = await asyncResult.Map(value => value.ToString());
    /// ]]></code>
    /// </example>
    public static async Task<Result<TDestination>> Map<TSource, TDestination>(this Task<Result<TSource>> task,
        Func<TSource, TDestination> map)
    {
        var result = await task.ConfigureAwait(false);
        return result.Map(map);
    }

    /// <summary>
    ///     Projects from one value to another using an asynchronous projection function.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="map">Asynchronous projection function.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Resulting functor value type.</typeparam>
    /// <returns>Asynchronous mapped functor.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Result<int>> asyncResult = GetIdAsync();
    /// Result<User> user = await asyncResult.MapAsync(id => FetchUserAsync(id));
    /// ]]></code>
    /// </example>
    public static async Task<Result<TDestination>> MapAsync<TSource, TDestination>(this Task<Result<TSource>> task,
        Func<TSource, Task<TDestination>> map)
    {
        var result = await task.ConfigureAwait(false);
        return await result.MapAsync(map).ConfigureAwait(false);
    }

    /// <summary>
    ///     Match the two states of an asynchronous Result and return a non-null TDestination.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="successOperation">Success match operation.</param>
    /// <param name="failureOperation">Failure match operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Return type.</typeparam>
    /// <returns>A non-null TDestination.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Result<int>> asyncResult = GetResultAsync();
    /// string message = await asyncResult.Match(
    ///     successOperation: value => $"Success: {value}",
    ///     failureOperation: error => $"Error: {error.GetFailureMessage()}"
    /// );
    /// ]]></code>
    /// </example>
    public static async Task<TDestination> Match<TSource, TDestination>(this Task<Result<TSource>> task,
        Func<TSource, TDestination> successOperation, Func<IResultFailure, TDestination> failureOperation)
    {
        var result = await task.ConfigureAwait(false);
        return result.Match(successOperation, failureOperation);
    }
}