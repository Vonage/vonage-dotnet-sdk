using System;

namespace Vonage.Video.Beta.Common;

public readonly struct Result<T>
{
    private readonly ResultFailure failure;
    private readonly ResultState state;
    private readonly T success;

    private Result(T success)
    {
        this.state = ResultState.Success;
        this.success = success;
        this.failure = default;
    }

    private Result(ResultFailure failure)
    {
        this.state = ResultState.Failure;
        this.success = default;
        this.failure = failure;
    }

    public bool IsFailure => this.state == ResultState.Failure;

    public bool IsSuccess => this.state == ResultState.Success;

    public static Result<T> FromFailure(ResultFailure failure)
    {
        return new Result<T>(failure);
    }

    public static Result<T> FromSuccess(T value)
    {
        return new Result<T>(value);
    }

    private enum ResultState
    {
        Success,
        Failure,
    }

    public Result<TB> Map<TB>(Func<T, TB> map) => this.IsFailure
        ? Result<TB>.FromFailure(this.failure)
        : Result<TB>.FromSuccess(map(this.success));

    public TB Match<TB>(Func<T, TB> successOperation, Func<ResultFailure, TB> failureOperation) =>
        this.IsFailure ? failureOperation(this.failure) : successOperation(this.success);
}