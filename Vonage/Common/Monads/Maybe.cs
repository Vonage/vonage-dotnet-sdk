#region
using System;
using System.Threading.Tasks;
using Vonage.Common.Failures;
using Vonage.Common.Monads.Exceptions;
#endregion

namespace Vonage.Common.Monads;

/// <summary>
///     Discriminated union type. Can be in one of two states: Some, or None.
/// </summary>
/// <typeparam name="TSource">Bound value type.</typeparam>
public readonly struct Maybe<TSource>
{
    /// <summary>
    ///     Message indicating Value cannot be null.
    /// </summary>
    public const string NullValueMessage = "Value cannot be null.";

    /// <summary>
    ///     Construct a Maybe in a None state.
    /// </summary>
    /// <example>
    /// <code><![CDATA[
    /// Maybe<string> noValue = Maybe<string>.None;
    /// Console.WriteLine(noValue.IsNone); // true
    /// ]]></code>
    /// </example>
    public static readonly Maybe<TSource> None = new Maybe<TSource>();

    private readonly TSource value = default;

    /// <summary>
    ///     Constructor for a Some.
    /// </summary>
    /// <param name="value">Value to bind.</param>
    private Maybe(TSource value)
    {
        this.value = value;
        this.IsSome = true;
    }

    /// <summary>
    ///     Constructor for a None.
    /// </summary>
    public Maybe() => this.IsSome = false;

    /// <summary>
    ///     Indicates if in None state.
    /// </summary>
    public bool IsNone => !this.IsSome;

    /// <summary>
    ///     Indicates if in Some state.
    /// </summary>
    public bool IsSome { get; }

    /// <summary>
    ///     Monadic bind operation. Chains operations that return Maybe, short-circuiting on None.
    /// </summary>
    /// <param name="bind">Bind operation.</param>
    /// <typeparam name="TDestination">Return type.</typeparam>
    /// <returns>Bound functor.</returns>
    /// <example>
    /// <code><![CDATA[
    /// Maybe<int> ParseInt(string value) =>
    ///     int.TryParse(value, out var result) ? result : Maybe<int>.None;
    /// Maybe<string> input = "42";
    /// Maybe<int> parsed = input.Bind(ParseInt);
    /// Console.WriteLine(parsed.IsSome); // true
    /// ]]></code>
    /// </example>
    public Maybe<TDestination> Bind<TDestination>(Func<TSource, Maybe<TDestination>> bind) =>
        !this.IsSome ? Maybe<TDestination>.None : bind(this.value);

    /// <summary>
    ///     Monadic bind operation. Chains asynchronous operations that return Maybe, short-circuiting on None.
    /// </summary>
    /// <param name="bind">Bind operation.</param>
    /// <typeparam name="TDestination">Return type.</typeparam>
    /// <returns>Bound functor.</returns>
    /// <example>
    /// <code><![CDATA[
    /// async Task<Maybe<User>> FetchUserAsync(int id) => await userRepository.FindByIdAsync(id);
    /// Maybe<int> userId = 123;
    /// Maybe<User> user = await userId.BindAsync(FetchUserAsync);
    /// ]]></code>
    /// </example>
    public Task<Maybe<TDestination>> BindAsync<TDestination>(Func<TSource, Task<Maybe<TDestination>>> bind) =>
        !this.IsSome ? Task.FromResult(Maybe<TDestination>.None) : bind(this.value);

    /// <inheritdoc />
    public override bool Equals(object obj) => obj is Maybe<TSource> maybe && this.Equals(maybe);

    /// <inheritdoc />
    public override int GetHashCode() => this.IsSome ? this.value.GetHashCode() : 0;

    /// <summary>
    ///     Retrieves the Maybe's value. This method is unsafe and will throw an exception if in None state.
    /// </summary>
    /// <returns>The value if in Some state.</returns>
    /// <exception cref="NoneStateException">When in None state.</exception>
    /// <example>
    /// <code><![CDATA[
    /// Maybe<string> name = "Alice";
    /// string value = name.GetUnsafe(); // "Alice"
    ///
    /// Maybe<string> noName = Maybe<string>.None;
    /// // noName.GetUnsafe(); // Throws NoneStateException
    /// ]]></code>
    /// </example>
    public TSource GetUnsafe() => this.IfNone(() => throw new NoneStateException());

    /// <summary>
    ///     Returns the result of the operation if Maybe is in the None state, the Some value otherwise.
    /// </summary>
    /// <param name="operation">Operation to return a value.</param>
    /// <returns>A value.</returns>
    /// <example>
    /// <code><![CDATA[
    /// Maybe<string> name = Maybe<string>.None;
    /// string result = name.IfNone(() => "Default Name"); // "Default Name"
    /// ]]></code>
    /// </example>
    public TSource IfNone(Func<TSource> operation) => this.IsNone ? operation() : this.value;

    /// <summary>
    ///     Returns the specified value if Maybe is in the None state, the Some value otherwise.
    /// </summary>
    /// <param name="noneValue">The value to return if in None state.</param>
    /// <returns>A value.</returns>
    /// <example>
    /// <code><![CDATA[
    /// Maybe<int> count = Maybe<int>.None;
    /// int result = count.IfNone(0); // 0
    ///
    /// Maybe<int> hasValue = 42;
    /// int existing = hasValue.IfNone(0); // 42
    /// ]]></code>
    /// </example>
    public TSource IfNone(TSource noneValue) => this.IsNone ? noneValue : this.value;

    /// <summary>
    ///     Invokes the action if Maybe is in the Some state, otherwise nothing happens.
    /// </summary>
    /// <param name="some">Action to invoke</param>
    /// <returns>The current Maybe instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// Maybe<string> name = "Alice";
    /// name.IfSome(value => Console.WriteLine($"Hello, {value}!")); // Prints: Hello, Alice!
    ///
    /// Maybe<string>.None.IfSome(value => Console.WriteLine(value)); // Does nothing
    /// ]]></code>
    /// </example>
    public Maybe<TSource> IfSome(Action<TSource> some)
    {
        if (this.IsSome)
        {
            some(this.value);
        }

        return this;
    }

    /// <summary>
    ///     Invokes the asynchronous action if Maybe is in the Some state, otherwise nothing happens.
    /// </summary>
    /// <param name="some">Asynchronous action to invoke</param>
    /// <returns>The current Maybe instance for method chaining.</returns>
    /// <example>
    /// <code><![CDATA[
    /// Maybe<string> userId = "user-123";
    /// await userId.IfSomeAsync(async id => await notificationService.SendAsync(id));
    /// ]]></code>
    /// </example>
    public async Task<Maybe<TSource>> IfSomeAsync(Func<TSource, Task> some)
    {
        if (this.IsSome)
        {
            await some(this.value).ConfigureAwait(false);
        }

        return this;
    }

    /// <summary>
    ///     Projects from one value to another. Transforms the inner value if Some, otherwise returns None.
    /// </summary>
    /// <param name="map">Projection function.</param>
    /// <typeparam name="TDestination">Resulting functor value type.</typeparam>
    /// <returns>Mapped functor.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Maybe<string> name = "Alice";
    /// Maybe<int> length = name.Map(n => n.Length); // Some(5)
    /// Maybe<string>.None.Map(n => n.Length); // None
    /// ]]></code>
    /// </example>
    public Maybe<TDestination> Map<TDestination>(Func<TSource, TDestination> map) =>
        !this.IsSome ? Maybe<TDestination>.None : Some(map(this.value));

    /// <summary>
    ///     Projects from one value to another using an asynchronous function.
    /// </summary>
    /// <param name="map">Asynchronous projection function.</param>
    /// <typeparam name="TDestination">Resulting functor value type.</typeparam>
    /// <returns>Mapped functor.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Maybe<int> userId = 123;
    /// Maybe<string> userName = await userId.MapAsync(async id => await userService.GetNameAsync(id));
    /// ]]></code>
    /// </example>
    public async Task<Maybe<TDestination>> MapAsync<TDestination>(Func<TSource, Task<TDestination>> map) =>
        !this.IsSome ? Maybe<TDestination>.None : await map(this.value).ConfigureAwait(false);

    /// <summary>
    ///     Match the two states of the Maybe and return a non-null TDestination.
    /// </summary>
    /// <param name="some">Some match operation.</param>
    /// <param name="none">None match operation.</param>
    /// <typeparam name="TDestination">Return type.</typeparam>
    /// <returns>A non-null TDestination.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Maybe<string> name = "Alice";
    /// string greeting = name.Match(
    ///     some: n => $"Hello, {n}!",
    ///     none: () => "Hello, stranger!"
    /// ); // "Hello, Alice!"
    /// ]]></code>
    /// </example>
    public TDestination Match<TDestination>(Func<TSource, TDestination> some, Func<TDestination> none) =>
        !this.IsSome ? none() : some(this.value);

    /// <summary>
    ///     Merge two maybes together. The merge operation will be used if they're both in a Some state.
    /// </summary>
    /// <param name="other">The other maybe.</param>
    /// <param name="merge">The operation used if they're both in a Some state.</param>
    /// <typeparam name="TDestination">The return type.</typeparam>
    /// <returns>A Maybe.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Maybe<int> first = 10;
    /// Maybe<int> second = 20;
    /// Maybe<int> sum = first.Merge(second, (a, b) => a + b); // Some(30)
    /// Maybe<int>.None.Merge(second, (a, b) => a + b); // None
    /// ]]></code>
    /// </example>
    public Maybe<TDestination> Merge<TDestination>(Maybe<TSource> other, Func<TSource, TSource, TDestination> merge) =>
        this.IsSome && other.IsSome
            ? Maybe<TDestination>.Some(merge(this.value, other.value))
            : Maybe<TDestination>.None;

    /// <summary>
    ///     Implicit operator from TSource to Maybe of TSource.
    /// </summary>
    /// <param name="value">Value to be converted.</param>
    /// <returns>None if the value is null, Some otherwise.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Maybe<string> name = "Alice"; // Implicit conversion to Some("Alice")
    /// Maybe<string> noName = null;  // Implicit conversion to None
    /// ]]></code>
    /// </example>
    public static implicit operator Maybe<TSource>(TSource value) => value is null ? None : Some(value);

    /// <summary>
    ///     Construct a Maybe in a Some state.
    /// </summary>
    /// <param name="value">Value to bind, must be non-null.</param>
    /// <typeparam name="TDestination">Bound value type.</typeparam>
    /// <returns>Maybe containing Some value.</returns>
    /// <exception cref="InvalidOperationException">Given value is null.</exception>
    /// <example>
    ///     <code><![CDATA[
    /// Maybe<string> name = Maybe<string>.Some("Alice");
    /// Console.WriteLine(name.IsSome); // true
    /// // Maybe<string>.Some(null); // Throws InvalidOperationException
    /// ]]></code>
    /// </example>
    public static Maybe<TDestination> Some<TDestination>(TDestination value) => value is null
        ? throw new InvalidOperationException(NullValueMessage)
        : new Maybe<TDestination>(value);

    /// <inheritdoc />
    public override string ToString() => this.IsSome ? $"Some({this.value.ToString()})" : "None";

    /// <summary>
    ///     Verifies of both Maybes are either None or Some with the same values.
    /// </summary>
    /// <param name="other">Other maybe to be compared with.</param>
    /// <returns>Whether both Maybes are equal.</returns>
    private bool Equals(Maybe<TSource> other) => this.EqualsNone(other) && this.EqualsSome(other);

    private bool EqualsNone(Maybe<TSource> other) =>
        this.IsNone ? other.IsNone : other.IsSome;

    private bool EqualsSome(Maybe<TSource> other) =>
        this.IsSome
            ? this.value.Equals(other.value)
            : other.IsNone;

    /// <summary>
    ///     Converts this instance to a Result.
    /// </summary>
    /// <returns>A Result with a Success state when Some, or with a Failure state when None.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Maybe<string> name = "Alice";
    /// Result<string> result = name.ToResult();
    /// Console.WriteLine(result.IsSuccess); // true
    /// Maybe<string>.None.ToResult().IsFailure; // true
    /// ]]></code>
    /// </example>
    public Result<TSource> ToResult() =>
        this.IsSome
            ? Result<TSource>.FromSuccess(this.value)
            : Result<TSource>.FromFailure(NoneFailure.Value);

    /// <summary>
    ///     Executes operations depending on the current state.
    /// </summary>
    /// <param name="someOperation">Some operation.</param>
    /// <param name="noneOperation">None operation.</param>
    /// <returns>The current Maybe instance for method chaining.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Maybe<string> name = "Alice";
    /// name.Do(
    ///     someOperation: value => Console.WriteLine($"Found: {value}"),
    ///     noneOperation: () => Console.WriteLine("Not found")
    /// ); // Prints: Found: Alice
    /// ]]></code>
    /// </example>
    public Maybe<TSource> Do(Action<TSource> someOperation, Action noneOperation)
    {
        if (this.IsSome)
        {
            someOperation(this.value);
        }
        else
        {
            noneOperation();
        }

        return this;
    }

    /// <summary>
    ///     Executes an operation if in Some state.
    /// </summary>
    /// <param name="someOperation">Some operation.</param>
    /// <returns>The current Maybe instance for method chaining.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Maybe<string> name = "Alice";
    /// name.DoWhenSome(value => Console.WriteLine($"Hello, {value}!")); // Prints: Hello, Alice!
    /// ]]></code>
    /// </example>
    public Maybe<TSource> DoWhenSome(Action<TSource> someOperation)
    {
        if (this.IsSome)
        {
            someOperation(this.value);
        }

        return this;
    }

    /// <summary>
    ///     Executes an operation if in None state.
    /// </summary>
    /// <param name="noneOperation">None operation.</param>
    /// <returns>The current Maybe instance for method chaining.</returns>
    /// <example>
    ///     <code><![CDATA[
    /// Maybe<string> name = Maybe<string>.None;
    /// name.DoWhenNone(() => Console.WriteLine("No value found")); // Prints: No value found
    /// ]]></code>
    /// </example>
    public Maybe<TSource> DoWhenNone(Action noneOperation)
    {
        if (this.IsNone)
        {
            noneOperation();
        }

        return this;
    }
}