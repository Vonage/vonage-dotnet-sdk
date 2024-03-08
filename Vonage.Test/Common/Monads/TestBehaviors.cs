using System.Threading.Tasks;
using Vonage.Common.Failures;
using Vonage.Common.Monads;

namespace Vonage.Test.Common.Monads;

internal static class TestBehaviors
{
    internal static Task<Result<T>> CreateSuccessAsync<T>(T value) => Task.FromResult(Result<T>.FromSuccess(value));
    internal static Result<T> CreateSuccess<T>(T value) => Result<T>.FromSuccess(value);

    internal static Task<Result<T>> CreateFailureAsync<T>() =>
        Task.FromResult(Result<T>.FromFailure(CreateResultFailure()));

    internal static Result<T> CreateFailure<T>() => Result<T>.FromFailure(CreateResultFailure());
    internal static ResultFailure CreateResultFailure() => ResultFailure.FromErrorMessage("Error");
    internal static int Increment(int value) => value + 1;
    internal static Task<int> IncrementAsync(int value) => Task.FromResult(value + 1);
    internal static Result<int> IncrementBind(int value) => value + 1;

    internal static Task<Result<int>> IncrementBindAsync(int value) =>
        Task.FromResult(Result<int>.FromSuccess(value + 1));
}

internal static class MaybeBehaviors
{
    internal static string MapToString<T>(T value) => value.ToString();
    internal static Maybe<string> BindToString<T>(T value) => Maybe<string>.Some(value.ToString());
    internal static Task<Maybe<T>> CreateSomeAsync<T>(T value) => Task.FromResult(Maybe<T>.Some(value));
    internal static Task<Maybe<T>> CreateNoneAsync<T>() => Task.FromResult(Maybe<T>.None);
    internal static Maybe<T> CreateSome<T>(T value) => Maybe<T>.Some(value);
    internal static Maybe<T> CreateNone<T>() => Maybe<T>.None;
    internal static int Increment(int value) => value + 1;
    internal static Task<int> IncrementAsync(int value) => Task.FromResult(value + 1);
    internal static Maybe<int> IncrementBind(int value) => value + 1;
    internal static Task<Maybe<int>> IncrementBindAsync(int value) => Task.FromResult(Maybe<int>.Some(value + 1));
}