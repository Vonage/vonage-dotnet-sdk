using System;
using Vonage.Common.Monads.Exceptions;

namespace Vonage.Common.Monads;

/// <summary>
///     Discriminated union type. Can be in one of two states: Some, or None.
/// </summary>
/// <typeparam name="TA">Bound value type.</typeparam>
public readonly struct Maybe<TA>
{
    /// <summary>
    ///     Message indicating Value cannot be null.
    /// </summary>
    public const string NullValueMessage = "Value cannot be null.";

    private readonly TA value = default;

    /// <summary>
    ///     Constructor for a Some.
    /// </summary>
    /// <param name="value">Value to bind.</param>
    private Maybe(TA value)
    {
        this.value = value;
        this.IsSome = true;
    }

    /// <summary>
    ///     Indicates if in None state.
    /// </summary>
    public bool IsNone => !this.IsSome;

    /// <summary>
    ///     Indicates if in Some state.
    /// </summary>
    public bool IsSome { get; }

    /// <summary>
    ///     Constructor for a None.
    /// </summary>
    public Maybe() => this.IsSome = false;

    /// <summary>
    ///     Monadic bind operation.
    /// </summary>
    /// <param name="bind">Bind operation.</param>
    /// <typeparam name="TB">Return type.</typeparam>
    /// <returns>Bound functor.</returns>
    public Maybe<TB> Bind<TB>(Func<TA, Maybe<TB>> bind) => !this.IsSome ? Maybe<TB>.None : bind(this.value);

    /// <inheritdoc />
    public override bool Equals(object obj) => obj is Maybe<TA> maybe && this.Equals(maybe);

    /// <inheritdoc />
    public override int GetHashCode() => this.IsSome ? this.value.GetHashCode() : 0;

    /// <summary>
    ///     Retrieves the Maybe's value. This method is unsafe and will throw an exception if in None state.
    /// </summary>
    /// <returns>The value if in Some state.</returns>
    /// <exception cref="NoneStateException">When in None state.</exception>
    public TA GetUnsafe() => this.IfNone(() => throw new NoneStateException());

    /// <summary>
    ///     Returns the result of the operation if Maybe is in the None state, the Some value otherwise.
    /// </summary>
    /// <param name="operation">Operation to return a value.</param>
    /// <returns>A value.</returns>
    public TA IfNone(Func<TA> operation) => this.IsNone ? operation() : this.value;

    /// <summary>
    ///     Returns the specified value if Maybe is in the None state, the Some value otherwise.
    /// </summary>
    /// <param name="noneValue">The value to return if in None state.</param>
    /// <returns>A value.</returns>
    public TA IfNone(TA noneValue) => this.IsNone ? noneValue : this.value;

    /// <summary>
    ///     Invokes the action if Maybe is in the Some state, otherwise nothing happens.
    /// </summary>
    /// <param name="some">Action to invoke</param>
    /// <returns>Unit.</returns>
    public Unit IfSome(Action<TA> some)
    {
        if (this.IsSome)
        {
            some(this.value);
        }

        return Unit.Default;
    }

    /// <summary>
    ///     Projects from one value to another.
    /// </summary>
    /// <param name="map">Projection function.</param>
    /// <typeparam name="TB">Resulting functor value type.</typeparam>
    /// <returns>Mapped functor.</returns>
    public Maybe<TB> Map<TB>(Func<TA, TB> map) => !this.IsSome ? Maybe<TB>.None : Some(map(this.value));

    /// <summary>
    ///     Match the two states of the Maybe and return a non-null TB.
    /// </summary>
    /// <param name="some">Some match operation.</param>
    /// <param name="none">None match operation.</param>
    /// <typeparam name="TB">Return type.</typeparam>
    /// <returns>A non-null TB.</returns>
    public TB Match<TB>(Func<TA, TB> some, Func<TB> none) => !this.IsSome ? none() : some(this.value);

    /// <summary>
    ///     Merge two maybes together. The merge operation will be used if they're both in a Some state.
    /// </summary>
    /// <param name="other">The other maybe.</param>
    /// <param name="merge">The operation used if they're both in a Some state.</param>
    /// <typeparam name="TB">The return type.</typeparam>
    /// <returns>A Maybe.</returns>
    public Maybe<TB> Merge<TB>(Maybe<TA> other, Func<TA, TA, TB> merge) =>
        this.IsSome && other.IsSome
            ? Maybe<TB>.Some(merge(this.value, other.value))
            : Maybe<TB>.None;

    /// <summary>
    ///     Implicit operator from TA to Maybe of TA.
    /// </summary>
    /// <param name="value">Value to be converted.</param>
    /// <returns>None if the value is null, Some otherwise.</returns>
    public static implicit operator Maybe<TA>(TA value) => value is null ? None : Some(value);

    /// <summary>
    ///     Construct a Maybe in a Some state.
    /// </summary>
    /// <param name="value">Value to bind, must be non-null.</param>
    /// <typeparam name="TB">Bound value type.</typeparam>
    /// <returns>Maybe containing Some value.</returns>
    /// <exception cref="InvalidOperationException">Given value is null.</exception>
    public static Maybe<TB> Some<TB>(TB value) => value is null
        ? throw new InvalidOperationException(NullValueMessage)
        : new Maybe<TB>(value);

    /// <inheritdoc />
    public override string ToString() => this.IsSome ? $"Some({base.ToString()})" : "None";

    /// <summary>
    ///     Verifies of both Maybes are either None or Some with the same values.
    /// </summary>
    /// <param name="other">Other maybe to be compared with.</param>
    /// <returns>Whether both Maybes are equal.</returns>
    private bool Equals(Maybe<TA> other) => this.EqualsNone(other) && this.EqualsSome(other);

    private bool EqualsNone(Maybe<TA> other) =>
        this.IsNone ? other.IsNone : other.IsSome;

    private bool EqualsSome(Maybe<TA> other) =>
        this.IsSome
            ? this.value.Equals(other.value)
            : other.IsNone;

    /// <summary>
    ///     Construct a Maybe in a None state.
    /// </summary>
    public static readonly Maybe<TA> None = new();
}