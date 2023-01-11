using System;
using System.Threading.Tasks;

namespace Vonage.Video.Beta.Common.Monads;

/// <summary>
///     Extensions for Result.
/// </summary>
public static class ResultExtensions
{
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
}