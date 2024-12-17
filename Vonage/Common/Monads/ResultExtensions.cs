#region
using System;
using System.Threading.Tasks;
using Vonage.Common.Failures;
#endregion

namespace Vonage.Common.Monads;

/// <summary>
///     Extensions for Result.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    ///     Monadic bind operation.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="bind">Bind operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Return type.</typeparam>
    /// <returns>Bound functor.</returns>
    public static async Task<Result<TDestination>> Bind<TSource, TDestination>(this Task<Result<TSource>> task,
        Func<TSource, Result<TDestination>> bind)
    {
        var result = await task.ConfigureAwait(false);
        return result.Bind(bind);
    }

    /// <summary>
    ///     Monadic bind operation.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="bind">Asynchronous bind operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Return type.</typeparam>
    /// <returns>Asynchronous bound functor.</returns>
    public static async Task<Result<TDestination>> BindAsync<TSource, TDestination>(this Task<Result<TSource>> task,
        Func<TSource, Task<Result<TDestination>>> bind)
    {
        var result = await task.ConfigureAwait(false);
        return await result.BindAsync(bind).ConfigureAwait(false);
    }

    /// <summary>
    ///     Returns the default value if the Result is in the Failure state, the success value otherwise.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="defaultValue">Value to return if in the Failure state.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <returns>The default value if the Result is in the Failure state, the success value otherwise.</returns>
    public static async Task<TSource> IfFailure<TSource>(this Task<Result<TSource>> task, TSource defaultValue)
    {
        var result = await task.ConfigureAwait(false);
        return result.IfFailure(defaultValue);
    }

    /// <summary>
    ///     Invokes the action if Result is in the Success state, otherwise nothing happens.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="action">Action to invoke.</param>
    /// <typeparam name="T">The source type.</typeparam>
    /// <returns>The initial result.</returns>
    public static async Task<Result<T>> IfSuccessAsync<T>(this Task<Result<T>> task, Func<T, Task> action)
    {
        var result = await task.ConfigureAwait(false);
        return await result.IfSuccessAsync(action).ConfigureAwait(false);
    }

    /// <summary>
    ///     Projects from one value to another.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="map">Projection function.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Resulting functor value type.</typeparam>
    /// <returns>Asynchronous mapped functor.</returns>
    public static async Task<Result<TDestination>> Map<TSource, TDestination>(this Task<Result<TSource>> task,
        Func<TSource, TDestination> map)
    {
        var result = await task.ConfigureAwait(false);
        return result.Map(map);
    }

    /// <summary>
    ///     Projects from one value to another.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="map">Asynchronous projection function.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Resulting functor value type.</typeparam>
    /// <returns>Asynchronous mapped functor.</returns>
    public static async Task<Result<TDestination>> MapAsync<TSource, TDestination>(this Task<Result<TSource>> task,
        Func<TSource, Task<TDestination>> map)
    {
        var result = await task.ConfigureAwait(false);
        return await result.MapAsync(map).ConfigureAwait(false);
    }

    /// <summary>
    ///     Match the two states of the Result and return a non-null TB.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="successOperation">Success match operation.</param>
    /// <param name="failureOperation">Failure match operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Return type.</typeparam>
    /// <returns>A non-null TB.</returns>
    public static async Task<TDestination> Match<TSource, TDestination>(this Task<Result<TSource>> task,
        Func<TSource, TDestination> successOperation, Func<IResultFailure, TDestination> failureOperation)
    {
        var result = await task.ConfigureAwait(false);
        return result.Match(successOperation, failureOperation);
    }

    /// <summary>
    ///     Executes operations depending on the current state.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="successOperation">Success operation.</param>
    /// <param name="failureOperation">Failure operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <returns>The initial result.</returns>
    public static async Task<Result<TSource>> Do<TSource>(this Task<Result<TSource>> task,
        Action<TSource> successOperation, Action<IResultFailure> failureOperation)
    {
        var result = await task.ConfigureAwait(false);
        return result.Do(successOperation, failureOperation);
    }

    /// <summary>
    ///     Executes an operation if success.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="successOperation">Success operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <returns>The initial result.</returns>
    public static async Task<Result<TSource>> DoWhenSuccess<TSource>(this Task<Result<TSource>> task,
        Action<TSource> successOperation)
    {
        var result = await task.ConfigureAwait(false);
        return result.DoWhenSuccess(successOperation);
    }

    /// <summary>
    ///     Executes an operation if failure.
    /// </summary>
    /// <param name="task">Asynchronous result.</param>
    /// <param name="failureOperation">Failure operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <returns>The initial result.</returns>
    public static async Task<Result<TSource>> DoWhenFailure<TSource>(this Task<Result<TSource>> task,
        Action<IResultFailure> failureOperation)
    {
        var result = await task.ConfigureAwait(false);
        return result.DoWhenFailure(failureOperation);
    }
}