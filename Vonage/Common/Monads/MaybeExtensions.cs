#region
using System;
using System.Threading.Tasks;
#endregion

namespace Vonage.Common.Monads;

/// <summary>
///     Exposes a set of extensions for Maybe, including async operations on Task of Maybe.
/// </summary>
public static class MaybeExtensions
{
    /// <summary>
    ///     Creates a Maybe from a string value.
    /// </summary>
    /// <param name="value">The string value.</param>
    /// <returns>A None state if the value is null or whitespace. A Some state otherwise.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Maybe<string> name = MaybeExtensions.FromNonEmptyString("Alice"); // Some("Alice")
    /// Maybe<string> empty = MaybeExtensions.FromNonEmptyString(""); // None
    /// Maybe<string> whitespace = MaybeExtensions.FromNonEmptyString("   "); // None
    /// ]]></code>
    /// </example>
    public static Maybe<string> FromNonEmptyString(string value) =>
        string.IsNullOrWhiteSpace(value)
            ? Maybe<string>.None
            : Maybe<string>.Some(value);

    /// <summary>
    ///     Monadic bind operation on an asynchronous Maybe.
    /// </summary>
    /// <param name="task">Asynchronous Maybe.</param>
    /// <param name="bind">Bind operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Return type.</typeparam>
    /// <returns>Bound functor.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Maybe<string>> asyncValue = GetValueAsync();
    /// Maybe<int> result = await asyncValue.Bind(value => ParseInt(value));
    /// ]]></code>
    /// </example>
    public static async Task<Maybe<TDestination>> Bind<TSource, TDestination>(this Task<Maybe<TSource>> task,
        Func<TSource, Maybe<TDestination>> bind)
    {
        var result = await task.ConfigureAwait(false);
        return result.Bind(bind);
    }

    /// <summary>
    ///     Monadic bind operation with an asynchronous bind function.
    /// </summary>
    /// <param name="task">Asynchronous Maybe.</param>
    /// <param name="bind">Asynchronous bind operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Return type.</typeparam>
    /// <returns>Bound functor.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Maybe<int>> asyncUserId = GetUserIdAsync();
    /// Maybe<User> user = await asyncUserId.BindAsync(id => FetchUserAsync(id));
    /// ]]></code>
    /// </example>
    public static async Task<Maybe<TDestination>> BindAsync<TSource, TDestination>(this Task<Maybe<TSource>> task,
        Func<TSource, Task<Maybe<TDestination>>> bind)
    {
        var result = await task.ConfigureAwait(false);
        return await result.BindAsync(bind).ConfigureAwait(false);
    }

    /// <summary>
    ///     Projects from one value to another on an asynchronous Maybe.
    /// </summary>
    /// <param name="task">Asynchronous Maybe.</param>
    /// <param name="map">Projection function.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Resulting functor value type.</typeparam>
    /// <returns>Mapped functor.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Maybe<string>> asyncName = GetNameAsync();
    /// Maybe<int> length = await asyncName.Map(name => name.Length);
    /// ]]></code>
    /// </example>
    public static async Task<Maybe<TDestination>> Map<TSource, TDestination>(this Task<Maybe<TSource>> task,
        Func<TSource, TDestination> map)
    {
        var result = await task.ConfigureAwait(false);
        return result.Map(map);
    }

    /// <summary>
    ///     Projects from one value to another using an asynchronous projection function.
    /// </summary>
    /// <param name="task">Asynchronous Maybe.</param>
    /// <param name="map">Asynchronous projection function.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Resulting functor value type.</typeparam>
    /// <returns>Mapped functor.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Maybe<int>> asyncId = GetIdAsync();
    /// Maybe<string> name = await asyncId.MapAsync(id => FetchNameAsync(id));
    /// ]]></code>
    /// </example>
    public static async Task<Maybe<TDestination>> MapAsync<TSource, TDestination>(this Task<Maybe<TSource>> task,
        Func<TSource, Task<TDestination>> map)
    {
        var result = await task.ConfigureAwait(false);
        return await result.MapAsync(map).ConfigureAwait(false);
    }

    /// <summary>
    ///     Match the two states of an asynchronous Maybe and return a non-null TDestination.
    /// </summary>
    /// <param name="task">Asynchronous Maybe.</param>
    /// <param name="some">Some match operation.</param>
    /// <param name="none">None match operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <typeparam name="TDestination">Return type.</typeparam>
    /// <returns>A non-null TDestination.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Maybe<string>> asyncName = GetNameAsync();
    /// string greeting = await asyncName.Match(
    ///     some: name => $"Hello, {name}!",
    ///     none: () => "Hello, stranger!"
    /// );
    /// ]]></code>
    /// </example>
    public static async Task<TDestination> Match<TSource, TDestination>(this Task<Maybe<TSource>> task,
        Func<TSource, TDestination> some, Func<TDestination> none)
    {
        var result = await task.ConfigureAwait(false);
        return result.Match(some, none);
    }

    /// <summary>
    ///     Returns the specified value if the asynchronous Maybe is in the None state, the Some value otherwise.
    /// </summary>
    /// <param name="task">Asynchronous Maybe.</param>
    /// <param name="noneValue">The value to return if in None state.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <returns>A value.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Maybe<int>> asyncCount = GetCountAsync();
    /// int count = await asyncCount.IfNone(0); // Returns 0 if None
    /// ]]></code>
    /// </example>
    public static async Task<TSource> IfNone<TSource>(this Task<Maybe<TSource>> task, TSource noneValue)
    {
        var result = await task.ConfigureAwait(false);
        return result.IfNone(noneValue);
    }

    /// <summary>
    ///     Executes operations depending on the current state of an asynchronous Maybe.
    /// </summary>
    /// <param name="task">Asynchronous Maybe.</param>
    /// <param name="someOperation">Some operation.</param>
    /// <param name="noneOperation">None operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <returns>The original Maybe.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Maybe<string>> asyncName = GetNameAsync();
    /// await asyncName.Do(
    ///     someOperation: name => Console.WriteLine($"Found: {name}"),
    ///     noneOperation: () => Console.WriteLine("Not found")
    /// );
    /// ]]></code>
    /// </example>
    public static async Task<Maybe<TSource>> Do<TSource>(this Task<Maybe<TSource>> task, Action<TSource> someOperation,
        Action noneOperation)
    {
        var result = await task.ConfigureAwait(false);
        return result.Do(someOperation, noneOperation);
    }

    /// <summary>
    ///     Executes an operation if the asynchronous Maybe is in Some state.
    /// </summary>
    /// <param name="task">Asynchronous Maybe.</param>
    /// <param name="someOperation">Some operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <returns>The original Maybe.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Maybe<string>> asyncName = GetNameAsync();
    /// await asyncName.DoWhenSome(name => Console.WriteLine($"Hello, {name}!"));
    /// ]]></code>
    /// </example>
    public static async Task<Maybe<TSource>> DoWhenSome<TSource>(this Task<Maybe<TSource>> task,
        Action<TSource> someOperation)
    {
        var result = await task.ConfigureAwait(false);
        return result.DoWhenSome(someOperation);
    }

    /// <summary>
    ///     Executes an operation if the asynchronous Maybe is in None state.
    /// </summary>
    /// <param name="task">Asynchronous Maybe.</param>
    /// <param name="noneOperation">None operation.</param>
    /// <typeparam name="TSource">Source type.</typeparam>
    /// <returns>The original Maybe.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Task<Maybe<string>> asyncName = GetNameAsync();
    /// await asyncName.DoWhenNone(() => Console.WriteLine("No value found"));
    /// ]]></code>
    /// </example>
    public static async Task<Maybe<TSource>> DoWhenNone<TSource>(this Task<Maybe<TSource>> task, Action noneOperation)
    {
        var result = await task.ConfigureAwait(false);
        return result.DoWhenNone(noneOperation);
    }
}