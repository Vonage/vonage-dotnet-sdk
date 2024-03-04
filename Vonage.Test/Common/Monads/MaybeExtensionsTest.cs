using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Common.Monads;

public class MaybeExtensionsTest
{
    [Fact]
    public async Task BindAsync_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await CreateNoneAsync<int>().BindAsync(IncrementBindAsync);
        result.Should().BeNone();
    }

    [Fact]
    public async Task BindAsync_ShouldReturnSome_GivenValueIsSome()
    {
        var result = await CreateSomeAsync(10).BindAsync(IncrementBindAsync);
        result.Should().BeSome(11);
    }

    [Fact]
    public async Task Bind_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await CreateNoneAsync<int>().Bind(IncrementBind);
        result.Should().BeNone();
    }

    [Fact]
    public async Task Bind_ShouldReturnSome_GivenValueIsSome()
    {
        var result = await CreateSomeAsync(10).Bind(IncrementBind);
        result.Should().BeSome(11);
    }

    [Fact]
    public async Task Map_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await CreateNoneAsync<int>().Map(Increment);
        result.Should().BeNone();
    }

    [Fact]
    public async Task Map_ShouldReturnSome_GivenValueIsSome()
    {
        var result = await CreateSomeAsync(10).Map(Increment);
        result.Should().BeSome(11);
    }

    [Fact]
    public async Task MapAsync_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await CreateNoneAsync<int>().MapAsync(IncrementAsync);
        result.Should().BeNone();
    }

    [Fact]
    public async Task MapAsync_ShouldReturnSome_GivenValueIsSome()
    {
        var result = await CreateSomeAsync(10).MapAsync(IncrementAsync);
        result.Should().BeSome(11);
    }

    [Fact]
    public async Task Match_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await CreateNoneAsync<int>().Match(_ => "some", () => "none");
        result.Should().Be("none");
    }

    [Fact]
    public async Task Match_ShouldReturnSome_GivenValueIsSome()
    {
        var result = await CreateSomeAsync(10).Match(_ => "some", () => "none");
        result.Should().Be("some");
    }

    private static Task<Maybe<T>> CreateSomeAsync<T>(T value) => Task.FromResult(Maybe<T>.Some(value));
    private static Task<Maybe<T>> CreateNoneAsync<T>() => Task.FromResult(Maybe<T>.None);
    private static int Increment(int value) => value + 1;
    private static Task<int> IncrementAsync(int value) => Task.FromResult(value + 1);
    private static Maybe<int> IncrementBind(int value) => value + 1;
    private static Task<Maybe<int>> IncrementBindAsync(int value) => Task.FromResult(Maybe<int>.Some(value + 1));
}