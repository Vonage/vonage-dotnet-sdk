#region
using System;
using System.Threading.Tasks;
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
    ///     Monadic bind operation.
    /// </summary>
    /// <param name="bind">Bind operation.</param>
    /// <typeparam name="TDestination">Return type.</typeparam>
    /// <returns>Bound functor.</returns>
    public Maybe<TDestination> Bind<TDestination>(Func<TSource, Maybe<TDestination>> bind) =>
        !this.IsSome ? Maybe<TDestination>.None : bind(this.value);

    /// <summary>
    ///     Monadic bind operation.
    /// </summary>
    /// <param name="bind">Bind operation.</param>
    /// <typeparam name="TDestination">Return type.</typeparam>
    /// <returns>Bound functor.</returns>
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
    public TSource GetUnsafe() => this.IfNone(() => throw new NoneStateException());

    /// <summary>
    ///     Returns the result of the operation if Maybe is in the None state, the Some value otherwise.
    /// </summary>
    /// <param name="operation">Operation to return a value.</param>
    /// <returns>A value.</returns>
    public TSource IfNone(Func<TSource> operation) => this.IsNone ? operation() : this.value;

    /// <summary>
    ///     Returns the specified value if Maybe is in the None state, the Some value otherwise.
    /// </summary>
    /// <param name="noneValue">The value to return if in None state.</param>
    /// <returns>A value.</returns>
    public TSource IfNone(TSource noneValue) => this.IsNone ? noneValue : this.value;

    /// <summary>
    ///     Invokes the action if Maybe is in the Some state, otherwise nothing happens.
    /// </summary>
    /// <param name="some">Action to invoke</param>
    /// <returns>Unit.</returns>
    public Maybe<TSource> IfSome(Action<TSource> some)
    {
        if (this.IsSome)
        {
            some(this.value);
        }

        return this;
    }

    /// <summary>
    ///     Invokes the action if Maybe is in the Some state, otherwise nothing happens.
    /// </summary>
    /// <param name="some">Action to invoke</param>
    /// <returns>Unit.</returns>
    public async Task<Maybe<TSource>> IfSomeAsync(Func<TSource, Task> some)
    {
        if (this.IsSome)
        {
            await some(this.value).ConfigureAwait(false);
        }

        return this;
    }

    /// <summary>
    ///     Projects from one value to another.
    /// </summary>
    /// <param name="map">Projection function.</param>
    /// <typeparam name="TDestination">Resulting functor value type.</typeparam>
    /// <returns>Mapped functor.</returns>
    public Maybe<TDestination> Map<TDestination>(Func<TSource, TDestination> map) =>
        !this.IsSome ? Maybe<TDestination>.None : Some(map(this.value));

    /// <summary>
    ///     Projects from one value to another.
    /// </summary>
    /// <param name="map">Projection function.</param>
    /// <typeparam name="TDestination">Resulting functor value type.</typeparam>
    /// <returns>Mapped functor.</returns>
    public async Task<Maybe<TDestination>> MapAsync<TDestination>(Func<TSource, Task<TDestination>> map) =>
        !this.IsSome ? Maybe<TDestination>.None : await map(this.value).ConfigureAwait(false);

    /// <summary>
    ///     Match the two states of the Maybe and return a non-null TDestination.
    /// </summary>
    /// <param name="some">Some match operation.</param>
    /// <param name="none">None match operation.</param>
    /// <typeparam name="TDestination">Return type.</typeparam>
    /// <returns>A non-null TDestination.</returns>
    public TDestination Match<TDestination>(Func<TSource, TDestination> some, Func<TDestination> none) =>
        !this.IsSome ? none() : some(this.value);

    /// <summary>
    ///     Merge two maybes together. The merge operation will be used if they're both in a Some state.
    /// </summary>
    /// <param name="other">The other maybe.</param>
    /// <param name="merge">The operation used if they're both in a Some state.</param>
    /// <typeparam name="TDestination">The return type.</typeparam>
    /// <returns>A Maybe.</returns>
    public Maybe<TDestination> Merge<TDestination>(Maybe<TSource> other, Func<TSource, TSource, TDestination> merge) =>
        this.IsSome && other.IsSome
            ? Maybe<TDestination>.Some(merge(this.value, other.value))
            : Maybe<TDestination>.None;

    /// <summary>
    ///     Implicit operator from TSource to Maybe of TSource.
    /// </summary>
    /// <param name="value">Value to be converted.</param>
    /// <returns>None if the value is null, Some otherwise.</returns>
    public static implicit operator Maybe<TSource>(TSource value) => value is null ? None : Some(value);

    /// <summary>
    ///     Construct a Maybe in a Some state.
    /// </summary>
    /// <param name="value">Value to bind, must be non-null.</param>
    /// <typeparam name="TDestination">Bound value type.</typeparam>
    /// <returns>Maybe containing Some value.</returns>
    /// <exception cref="InvalidOperationException">Given value is null.</exception>
    public static Maybe<TDestination> Some<TDestination>(TDestination value) => value is null
        ? throw new InvalidOperationException(NullValueMessage)
        : new Maybe<TDestination>(value);

    /// <inheritdoc />
    public override string ToString() => this.IsSome ? $"Some({base.ToString()})" : "None";

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
    ///     Executes operations depending on the current state.
    /// </summary>
    /// <param name="someOperation">Some operation.</param>
    /// <param name="noneOperation">None operation.</param>
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
    ///     Executes an operation if some.
    /// </summary>
    /// <param name="someOperation">Some operation.</param>
    public Maybe<TSource> DoWhenSome(Action<TSource> someOperation)
    {
        if (this.IsSome)
        {
            someOperation(this.value);
        }

        return this;
    }

    /// <summary>
    ///     Executes an operation if none.
    /// </summary>
    /// <param name="noneOperation">None operation.</param>
    public Maybe<TSource> DoWhenNone(Action noneOperation)
    {
        if (this.IsNone)
        {
            noneOperation();
        }

        return this;
    }
}