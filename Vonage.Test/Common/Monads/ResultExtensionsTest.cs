using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Common.Monads;

public class ResultExtensionsTest
{
    private int value;

    [Fact]
    public async Task BindAsync_ShouldReturnFailure_GivenValueIsFailure()
    {
        var result = await CreateFailureAsync<int>().BindAsync(IncrementBindAsync);
        result.Should().BeFailure(CreateFailure());
    }

    [Fact]
    public async Task BindAsync_ShouldReturnSuccess_GivenValueIsSuccess()
    {
        var result = await CreateSuccessAsync(10).BindAsync(IncrementBindAsync);
        result.Should().BeSuccess(11);
    }

    [Fact]
    public async Task Bind_ShouldReturnFailure_GivenValueIsFailure()
    {
        var result = await CreateFailureAsync<int>().Bind(IncrementBind);
        result.Should().BeFailure(CreateFailure());
    }

    [Fact]
    public async Task Bind_ShouldReturnSuccess_GivenValueIsSuccess()
    {
        var result = await CreateSuccessAsync(10).Bind(IncrementBind);
        result.Should().BeSuccess(11);
    }

    [Fact]
    public async Task Map_ShouldReturnFailure_GivenValueIsFailure()
    {
        var result = await CreateFailureAsync<int>().Map(Increment);
        result.Should().BeFailure(CreateFailure());
    }

    [Fact]
    public async Task Map_ShouldReturnSuccess_GivenValueIsSuccess()
    {
        var result = await CreateSuccessAsync(10).Map(Increment);
        result.Should().BeSuccess(11);
    }

    [Fact]
    public async Task MapAsync_ShouldReturnFailure_GivenValueIsFailure()
    {
        var result = await CreateFailureAsync<int>().MapAsync(IncrementAsync);
        result.Should().BeFailure(CreateFailure());
    }

    [Fact]
    public async Task MapAsync_ShouldReturnSuccess_GivenValueIsSuccess()
    {
        var result = await CreateSuccessAsync(10).MapAsync(IncrementAsync);
        result.Should().BeSuccess(11);
    }

    [Fact]
    public async Task IfSuccessAsync_ShouldExecuteOperation_GivenSuccess()
    {
        await CreateSuccessAsync(10).IfSuccessAsync(this.SuccessAction);
        this.value.Should().Be(10);
    }

    [Fact]
    public async Task IfSuccessAsync_ShouldReturnSuccess_GivenSuccess()
    {
        var result = await CreateSuccessAsync(10).IfSuccessAsync(this.SuccessAction);
        result.Should().BeSuccess(10);
    }

    [Fact]
    public async Task IfSuccessAsync_ShouldNotExecuteOperation_GivenFailure()
    {
        await CreateFailureAsync<int>().IfSuccessAsync(this.SuccessAction);
        this.value.Should().Be(default);
    }

    [Fact]
    public async Task IfSuccessAsync_ShouldReturnFailure_GivenFailure()
    {
        var result = await CreateFailureAsync<int>().IfSuccessAsync(this.SuccessAction);
        result.Should().BeFailure(CreateFailure());
    }

    [Fact]
    public async Task IfFailure_ShouldReturnFallbackValue_GivenFailure()
    {
        var result = await CreateFailureAsync<int>().IfFailure(50);
        result.Should().Be(50);
    }

    [Fact]
    public async Task IfFailure_ShouldReturnSuccess_GivenSuccess()
    {
        var result = await CreateSuccessAsync(10).IfFailure(50);
        result.Should().Be(10);
    }

    private Task SuccessAction(int input)
    {
        this.value = input;
        return Task.CompletedTask;
    }

    private static Task<Result<T>> CreateSuccessAsync<T>(T value) => Task.FromResult(Result<T>.FromSuccess(value));
    private static Task<Result<T>> CreateFailureAsync<T>() => Task.FromResult(Result<T>.FromFailure(CreateFailure()));
    private static ResultFailure CreateFailure() => ResultFailure.FromErrorMessage("Error");
    private static int Increment(int value) => value + 1;
    private static Task<int> IncrementAsync(int value) => Task.FromResult(value + 1);
    private static Result<int> IncrementBind(int value) => value + 1;

    private static Task<Result<int>> IncrementBindAsync(int value) =>
        Task.FromResult(Result<int>.FromSuccess(value + 1));
}