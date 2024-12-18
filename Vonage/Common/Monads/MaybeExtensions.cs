#region
using System;
using System.Threading.Tasks;
#endregion

namespace Vonage.Common.Monads;

/// <summary>
///     Exposes a set of extensions for Maybe.
/// </summary>
public static class MaybeExtensions
{
    /// <summary>
    ///     Creates a Maybe from a string value.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <returns>A None state if the value is null or whitespace. A Some state otherwise.</returns>
    public static Maybe<string> From(string value) =>
        string.IsNullOrWhiteSpace(value)
            ? Maybe<string>.None
            : Maybe<string>.Some(value);

    /// <summary>
    ///     Monadic bind operation.
    /// </summary>
    /// <param name="bind">Bind operation.</param>
    /// <returns>Bound functor.</returns>
    public static async Task<Maybe<TDestination>> Bind<TSource, TDestination>(this Task<Maybe<TSource>> task,
        Func<TSource, Maybe<TDestination>> bind)
    {
        var result = await task.ConfigureAwait(false);
        return result.Bind(bind);
    }

    /// <summary>
    ///     Monadic bind operation.
    /// </summary>
    /// <param name="bind">Bind operation.</param>
    /// <returns>Bound functor.</returns>
    public static async Task<Maybe<TDestination>> BindAsync<TSource, TDestination>(this Task<Maybe<TSource>> task,
        Func<TSource, Task<Maybe<TDestination>>> bind)
    {
        var result = await task.ConfigureAwait(false);
        return await result.BindAsync(bind).ConfigureAwait(false);
    }

    /// <summary>
    ///     Projects from one value to another.
    /// </summary>
    /// <param name="map">Projection function.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Resulting functor value type.</typeparam>
    /// <returns>Mapped functor.</returns>
    public static async Task<Maybe<TDestination>> Map<TSource, TDestination>(this Task<Maybe<TSource>> task,
        Func<TSource, TDestination> map)
    {
        var result = await task.ConfigureAwait(false);
        return result.Map(map);
    }

    /// <summary>
    ///     Projects from one value to another.
    /// </summary>
    /// <param name="map">Projection function.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Resulting functor value type.</typeparam>
    /// <returns>Mapped functor.</returns>
    public static async Task<Maybe<TDestination>> MapAsync<TSource, TDestination>(this Task<Maybe<TSource>> task,
        Func<TSource, Task<TDestination>> map)
    {
        var result = await task.ConfigureAwait(false);
        return await result.MapAsync(map).ConfigureAwait(false);
    }

    /// <summary>
    ///     Match the two states of the Maybe and return a non-null TDestination.
    /// </summary>
    /// <param name="task">Initial Maybe.</param>
    /// <param name="some">Some match operation.</param>
    /// <param name="none">None match operation.</param>
    /// <typeparam name="TDestination">Return type.</typeparam>
    /// <typeparam name="TSource"></typeparam>
    /// <returns>A non-null TDestination.</returns>
    public static async Task<TDestination> Match<TSource, TDestination>(this Task<Maybe<TSource>> task,
        Func<TSource, TDestination> some, Func<TDestination> none)
    {
        var result = await task.ConfigureAwait(false);
        return result.Match(some, none);
    }

    /// <summary>
    ///     Returns the specified value if Maybe is in the None state, the Some value otherwise.
    /// </summary>
    /// <param name="task">Initial Maybe.</param>
    /// <param name="noneValue">The value to return if in None state.</param>
    /// <returns>A value.</returns>
    public static async Task<TSource> IfNone<TSource>(this Task<Maybe<TSource>> task, TSource noneValue)
    {
        var result = await task.ConfigureAwait(false);
        return result.IfNone(noneValue);
    }

    /// <summary>
    ///     Executes operations depending on the current state.
    /// </summary>
    /// <param name="task">Initial Maybe.</param>
    /// <param name="someOperation">Some operation.</param>
    /// <param name="noneOperation">None operation.</param>
    /// <returns>Initial Maybe.</returns>
    public static async Task<Maybe<TSource>> Do<TSource>(this Task<Maybe<TSource>> task, Action<TSource> someOperation,
        Action noneOperation)
    {
        var result = await task.ConfigureAwait(false);
        return result.Do(someOperation, noneOperation);
    }

    /// <summary>
    ///     Executes an operation if some.
    /// </summary>
    /// <param name="task">Initial Maybe.</param>
    /// <param name="someOperation">Some operation.</param>
    /// <returns>Initial Maybe.</returns>
    public static async Task<Maybe<TSource>> DoWhenSome<TSource>(this Task<Maybe<TSource>> task,
        Action<TSource> someOperation)
    {
        var result = await task.ConfigureAwait(false);
        return result.DoWhenSome(someOperation);
    }

    /// <summary>
    ///     Executes an operation if none.
    /// </summary>
    /// <param name="task">Initial Maybe.</param>
    /// <param name="noneOperation">None operation.</param>
    /// <returns>Initial Maybe.</returns>
    public static async Task<Maybe<TSource>> DoWhenNone<TSource>(this Task<Maybe<TSource>> task, Action noneOperation)
    {
        var result = await task.ConfigureAwait(false);
        return result.DoWhenNone(noneOperation);
    }
}