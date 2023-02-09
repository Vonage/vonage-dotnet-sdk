using System;
using System.Threading.Tasks;

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
        var result = await task;
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
        var result = await task;
        return await result.BindAsync(bind);
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
        var result = await task;
        return await result.IfSuccessAsync(action);
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
        var result = await task;
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
        var result = await task;
        return await result.MapAsync(map);
    }
}