using System;
using System.Threading.Tasks;
using Vonage.Common.Failures;

namespace Vonage.Common.Monads;

/// <summary>
///     Represents the result of an operation. Can be in one of two states: Success, or Failure.
/// </summary>
/// <typeparam name="T">Bound value type.</typeparam>
public readonly struct Result<T>
{
    private readonly IResultFailure failure;
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
    private Result(IResultFailure failure)
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
    ///     Projects from one value to another for each state of the Monad.
    /// </summary>
    /// <param name="successMap">Projection function for success state.</param>
    /// <param name="failureMap">Projection function for failure state.</param>
    /// <typeparam name="TB">Resulting functor value type.</typeparam>
    /// <returns>Mapped functor.</returns>
    public Result<TB> BiMap<TB>(Func<T, TB> successMap, Func<IResultFailure, IResultFailure> failureMap)
    {
        try
        {
            return this.IsFailure
                ? Result<TB>.FromFailure(failureMap(this.failure))
                : Result<TB>.FromSuccess(successMap(this.success));
        }
        catch (Exception exception)
        {
            return SystemFailure.FromException(exception).ToResult<TB>();
        }
    }
    
    /// <summary>
    ///     Monadic bind operation.
    /// </summary>
    /// <param name="bind">Bind operation.</param>
    /// <typeparam name="TB">Return type.</typeparam>
    /// <returns>Bound functor.</returns>
    public Result<TB> Bind<TB>(Func<T, Result<TB>> bind)
    {
        try
        {
            return this.IsFailure
                ? Result<TB>.FromFailure(this.failure)
                : bind(this.success);
        }
        catch (Exception exception)
        {
            return SystemFailure.FromException(exception).ToResult<TB>();
        }
    }
    
    /// <summary>
    ///     Monadic bind operation.
    /// </summary>
    /// <param name="bind">Asynchronous bind operation.</param>
    /// <typeparam name="TB">Return type.</typeparam>
    /// <returns>Asynchronous bound functor.</returns>
    public async Task<Result<TB>> BindAsync<TB>(Func<T, Task<Result<TB>>> bind)
    {
        try
        {
            return this.IsFailure
                ? Result<TB>.FromFailure(this.failure)
                : await bind(this.success).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            return SystemFailure.FromException(exception).ToResult<TB>();
        }
    }
    
    /// <inheritdoc />
    public override bool Equals(object obj) => obj is Result<T> result && this.Equals(result);
    
    /// <summary>
    ///     Construct Result from Failure.
    /// </summary>
    /// <param name="failure">Failure value.</param>
    /// <returns>Failure Result.</returns>
    public static Result<T> FromFailure(IResultFailure failure) => new(failure);
    
    /// <summary>
    ///     Construct Result from Success.
    /// </summary>
    /// <param name="value">Success value.</param>
    /// <returns>Success Result.</returns>
    public static Result<T> FromSuccess(T value) => new(value);
    
    /// <summary>
    ///     Retrieves the Failure value. This method is unsafe and will throw an exception if in Success state.
    /// </summary>
    /// <returns>The Failure value if in Failure state.</returns>
    /// <exception cref="InvalidOperationException">When Result is not in Failure state.</exception>
    public IResultFailure GetFailureUnsafe() => this.IsFailure
        ? this.failure
        : throw new InvalidOperationException("Result is not in Failure state.");
    
    /// <inheritdoc />
    public override int GetHashCode() => this.IsSuccess ? this.success.GetHashCode() : this.failure.GetHashCode();
    
    /// <summary>
    ///     Retrieves the Success value. This method is unsafe and will throw an exception if in Failure state.
    /// </summary>
    /// <returns>The Success value if in Success state.</returns>
    public T GetSuccessUnsafe() => this.IfFailure(value => throw value.ToException());
    
    /// <summary>
    ///     Invokes the action if Result is in the Failure state, otherwise nothing happens.
    /// </summary>
    /// <param name="action">Action to invoke.</param>
    public void IfFailure(Action<IResultFailure> action)
    {
        if (this.IsFailure)
        {
            action(this.failure);
        }
    }
    
    /// <summary>
    ///     Returns the invocation result if the Result is in the Failure state, the success value otherwise.
    /// </summary>
    /// <param name="operation">Operation to invoke if the Result is in the Failure state.</param>
    /// <returns>The invocation result if the Result is in the Failure state, the success value otherwise.</returns>
    public T IfFailure(Func<IResultFailure, T> operation) => this.IsFailure ? operation(this.failure) : this.success;
    
    /// <summary>
    ///     Returns the default value if the Result is in the Failure state, the success value otherwise.
    /// </summary>
    /// <param name="defaultValue">Value to return if in the Failure state.</param>
    /// <returns>The default value if the Result is in the Failure state, the success value otherwise.</returns>
    public T IfFailure(T defaultValue) => this.IsFailure ? defaultValue : this.success;
    
    /// <summary>
    ///     Invokes the action if Result is in the Success state, otherwise nothing happens.
    /// </summary>
    /// <param name="action">Action to invoke.</param>
    /// <returns>The initial result.</returns>
    public Result<T> IfSuccess(Action<T> action)
    {
        if (this.IsSuccess)
        {
            action(this.success);
        }
        
        return this;
    }
    
    /// <summary>
    ///     Invokes the action if Result is in the Success state, otherwise nothing happens.
    /// </summary>
    /// <param name="action">Action to invoke.</param>
    /// <returns>The initial result.</returns>
    public async Task<Result<T>> IfSuccessAsync(Func<T, Task> action)
    {
        if (this.IsSuccess)
        {
            await action(this.success).ConfigureAwait(false);
        }
        
        return this;
    }
    
    /// <summary>
    ///     Projects from one value to another.
    /// </summary>
    /// <param name="map">Projection function.</param>
    /// <typeparam name="TB">Resulting functor value type.</typeparam>
    /// <returns>Mapped functor.</returns>
    public Result<TB> Map<TB>(Func<T, TB> map)
    {
        try
        {
            return this.IsFailure
                ? Result<TB>.FromFailure(this.failure)
                : Result<TB>.FromSuccess(map(this.success));
        }
        catch (Exception exception)
        {
            return SystemFailure.FromException(exception).ToResult<TB>();
        }
    }
    
    /// <summary>
    ///     Projects from one value to another.
    /// </summary>
    /// <param name="map">Asynchronous projection function.</param>
    /// <typeparam name="TB">Resulting functor value type.</typeparam>
    /// <returns>Asynchronous mapped functor.</returns>
    public async Task<Result<TB>> MapAsync<TB>(Func<T, Task<TB>> map)
    {
        try
        {
            return this.IsFailure
                ? Result<TB>.FromFailure(this.failure)
                : Result<TB>.FromSuccess(await map(this.success).ConfigureAwait(false));
        }
        catch (Exception exception)
        {
            return SystemFailure.FromException(exception).ToResult<TB>();
        }
    }
    
    /// <summary>
    ///     Match the two states of the Result and return a non-null TB.
    /// </summary>
    /// <param name="successOperation">Success match operation.</param>
    /// <param name="failureOperation">Failure match operation.</param>
    /// <typeparam name="TB">Return type.</typeparam>
    /// <returns>A non-null TB.</returns>
    public TB Match<TB>(Func<T, TB> successOperation, Func<IResultFailure, TB> failureOperation) =>
        this.IsFailure ? failureOperation(this.failure) : successOperation(this.success);
    
    /// <summary>
    ///     Match the two states of the Result.
    /// </summary>
    /// <param name="successOperation">Success match operation.</param>
    /// <param name="failureOperation">Failure match operation.</param>
    public void Match(Action<T> successOperation, Action<IResultFailure> failureOperation)
    {
        if (this.IsFailure)
        {
            failureOperation(this.failure);
        }
        else
        {
            successOperation(this.success);
        }
    }
    
    /// <summary>
    ///     Merge two results together. The merge operation will be used if they're both in a Success state.
    /// </summary>
    /// <param name="other">The other result.</param>
    /// <param name="merge">The operation used if they're both in a Success state.</param>
    /// <typeparam name="TSource">The secondary result type.</typeparam>
    /// <typeparam name="TDestination">The return type.</typeparam>
    /// <returns>A result.</returns>
    public Result<TDestination> Merge<TSource, TDestination>(Result<TSource> other,
        Func<T, TSource, TDestination> merge) =>
        this.IsSuccess && other.IsSuccess
            ? Result<TDestination>.FromSuccess(merge(this.success, other.success))
            : Result<TDestination>.FromFailure(this.FetchFailure(other));
    
    /// <summary>
    ///     Implicit operator from TA to Result of TA.
    /// </summary>
    /// <param name="value">Value to be converted.</param>
    /// <returns>Success.</returns>
    public static implicit operator Result<T>(T value) => FromSuccess(value);
    
    /// <summary>
    ///     Verifies if both Results are either Failure or Success with the same values.
    /// </summary>
    /// <param name="other">Other Result to be compared with.</param>
    /// <returns>Whether both Results are equal.</returns>
    private bool Equals(Result<T> other) => this.EqualsFailure(other) && this.EqualsSuccess(other);
    
    /// <summary>
    ///     Verifies if both failures are equal.
    /// </summary>
    /// <param name="other">Other Result to be compared with.</param>
    /// <returns>Whether both failures are equal.</returns>
    /// <remarks>Using IResultFailure for the Failure value makes it nullable. Comparing both cases is now mandatory.</remarks>
    private bool EqualsFailure(Result<T> other) =>
        this.IsFailure
            ? this.failure.Equals(other.failure)
            : other.IsSuccess;
    
    /// <summary>
    ///     Verifies if both successes are equal.
    /// </summary>
    /// <param name="other">Other Result to be compared with.</param>
    /// <returns>Whether both successes are equal.</returns>
    private bool EqualsSuccess(Result<T> other) =>
        this.IsSuccess
            ? this.success.Equals(other.success)
            : other.IsFailure;
    
    private IResultFailure FetchFailure<TSource>(Result<TSource> other) =>
        this.IsFailure ? this.failure : other.failure;
    
    /// <summary>
    ///     Enum representing the state of Result.
    /// </summary>
    private enum ResultState
    {
        Success,
        Failure,
    }
}