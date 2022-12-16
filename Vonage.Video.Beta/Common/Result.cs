using System;
using System.Threading.Tasks;

namespace Vonage.Video.Beta.Common;

/// <summary>
///     Represents the result of an operation. Can be in one of two states: Success, or Failure.
/// </summary>
/// <typeparam name="T">Bound value type.</typeparam>
public readonly struct Result<T>
{
    private readonly ResultFailure failure;
    private readonly ResultState state;
    private readonly T success;

    /// <summary>
    ///     Constructor for a Success.
    /// </summary>
    /// <param name="success">Success value.</param>
    private Result(T success)
    {
        this.state = ResultState.Success;
        this.success = success;
        this.failure = default;
    }

    /// <summary>
    ///     Constructor for a Failure.
    /// </summary>
    /// <param name="failure">Failure value.</param>
    private Result(ResultFailure failure)
    {
        this.state = ResultState.Failure;
        this.success = default;
        this.failure = failure;
    }

    /// <summary>
    ///     Indicates if in Failure state.
    /// </summary>
    public bool IsFailure => this.state == ResultState.Failure;

    /// <summary>
    ///     Indicates if in Success state.
    /// </summary>
    public bool IsSuccess => this.state == ResultState.Success;

    /// <summary>
    ///     Construct Result from Failure.
    /// </summary>
    /// <param name="failure">Failure value.</param>
    /// <returns>Failure Result.</returns>
    public static Result<T> FromFailure(ResultFailure failure) => new(failure);

    /// <summary>
    ///     Construct Result from Success.
    /// </summary>
    /// <param name="value">Success value.</param>
    /// <returns>Success Result.</returns>
    public static Result<T> FromSuccess(T value) => new(value);

    /// <summary>
    ///     Enum representing the state of Result.
    /// </summary>
    private enum ResultState
    {
        Success,
        Failure,
    }

    /// <summary>
    ///     Projects from one value to another.
    /// </summary>
    /// <param name="map">Projection function.</param>
    /// <typeparam name="TB">Resulting functor value type.</typeparam>
    /// <returns>Mapped functor.</returns>
    public Result<TB> Map<TB>(Func<T, TB> map) => this.IsFailure
        ? Result<TB>.FromFailure(this.failure)
        : Result<TB>.FromSuccess(map(this.success));

    /// <summary>
    ///     Match the two states of the Result and return a non-null TB.
    /// </summary>
    /// <param name="successOperation">Success match operation.</param>
    /// <param name="failureOperation">Failure match operation.</param>
    /// <typeparam name="TB">Return type.</typeparam>
    /// <returns>A non-null TB.</returns>
    public TB Match<TB>(Func<T, TB> successOperation, Func<ResultFailure, TB> failureOperation) =>
        this.IsFailure ? failureOperation(this.failure) : successOperation(this.success);

    /// <summary>
    ///     Monadic bind operation.
    /// </summary>
    /// <param name="bind">Bind operation.</param>
    /// <typeparam name="TB">Return type.</typeparam>
    /// <returns>Bound functor.</returns>
    public Result<TB> Bind<TB>(Func<T, Result<TB>> bind) =>
        this.IsFailure ? Result<TB>.FromFailure(this.failure) : bind(this.success);

    /// <inheritdoc />
    public override bool Equals(object obj) => obj is Result<T> result && this.Equals(result);

    /// <inheritdoc />
    public override int GetHashCode() => this.IsSuccess ? this.success.GetHashCode() : this.failure.GetHashCode();

    /// <summary>
    ///     Verifies of both Results are either Failure or Success with the same values.
    /// </summary>
    /// <param name="other">Other Result to be compared with.</param>
    /// <returns>Whether both Results are equal.</returns>
    private bool Equals(Result<T> other) => this.failure.Equals(other.failure) && this.success.Equals(other.success);

    /// <summary>
    ///     Implicit operator from TA to Result of TA.
    /// </summary>
    /// <param name="value">Value to be converted.</param>
    /// <returns>Success.</returns>
    public static implicit operator Result<T>(T value) => FromSuccess(value);

    /// <summary>
    ///     Implicit operator from TA to Result of TA.
    /// </summary>
    /// <param name="value">Value to be converted.</param>
    /// <returns>Failure.</returns>
    public static implicit operator Result<T>(ResultFailure value) => FromFailure(value);

    /// <summary>
    ///     Invokes the action if Result is in the Failure state, otherwise nothing happens.
    /// </summary>
    /// <param name="action">Action to invoke.</param>
    public void IfFailure(Action<ResultFailure> action)
    {
        if (this.IsFailure)
        {
            action(this.failure);
        }
    }

    /// <summary>
    ///     Invokes the action if Result is in the Success state, otherwise nothing happens.
    /// </summary>
    /// <param name="action">Action to invoke.</param>
    public void IfSuccess(Action<T> action)
    {
        if (this.IsSuccess)
        {
            action(this.success);
        }
    }

    /// <summary>
    ///     Projects from one value to another.
    /// </summary>
    /// <param name="map">Asynchronous projection function.</param>
    /// <typeparam name="TB">Resulting functor value type.</typeparam>
    /// <returns>Asynchronous mapped functor.</returns>
    public async Task<Result<TB>> MapAsync<TB>(Func<T, Task<TB>> map) =>
        this.IsFailure
            ? Result<TB>.FromFailure(this.failure)
            : Result<TB>.FromSuccess(await map(this.success));

    /// <summary>
    ///     Monadic bind operation.
    /// </summary>
    /// <param name="bind">Asynchronous bind operation.</param>
    /// <typeparam name="TB">Return type.</typeparam>
    /// <returns>Asynchronous bound functor.</returns>
    public async Task<Result<TB>> BindAsync<TB>(Func<T, Task<Result<TB>>> bind) =>
        this.IsFailure ? Result<TB>.FromFailure(this.failure) : await bind(this.success);
}