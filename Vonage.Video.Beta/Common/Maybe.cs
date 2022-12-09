using System;

namespace Vonage.Video.Beta.Common;

/// <summary>
///     Discriminated union type. Can be in one of two states: Some, or None.
/// </summary>
/// <typeparam name="TA">Bound value type.</typeparam>
public readonly struct Maybe<TA>
{
    public const string NullValueMessage = "Value cannot be null.";
    private readonly TA value = default;

    /// <summary>
    ///     Construct a Maybe in a None state.
    /// </summary>
    public static readonly Maybe<TA> None = new();

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

    /// <summary>
    ///     Constructor for a None.
    /// </summary>
    public Maybe()
    {
        this.IsSome = false;
    }

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
    ///     Indicates if the Maybe is in None state.
    /// </summary>
    public bool IsNone => !this.IsSome;

    /// <summary>
    ///     Indicates if the Maybe is in Some state.
    /// </summary>
    public bool IsSome { get; }

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

    public Maybe<TB> Bind<TB>(Func<TA, Maybe<TB>> bind) => !this.IsSome ? Maybe<TB>.None : bind(this.value);
}