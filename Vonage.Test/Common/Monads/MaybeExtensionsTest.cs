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
        var result = await MaybeBehaviors.CreateNoneAsync<int>().BindAsync(MaybeBehaviors.IncrementBindAsync);
        result.Should().BeNone();
    }

    [Fact]
    public async Task BindAsync_ShouldReturnSome_GivenValueIsSome()
    {
        var result = await MaybeBehaviors.CreateSomeAsync(10).BindAsync(MaybeBehaviors.IncrementBindAsync);
        result.Should().BeSome(11);
    }

    [Fact]
    public async Task Bind_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await MaybeBehaviors.CreateNoneAsync<int>().Bind(MaybeBehaviors.IncrementBind);
        result.Should().BeNone();
    }

    [Fact]
    public async Task Bind_ShouldReturnSome_GivenValueIsSome()
    {
        var result = await MaybeBehaviors.CreateSomeAsync(10).Bind(MaybeBehaviors.IncrementBind);
        result.Should().BeSome(11);
    }

    [Fact]
    public async Task Map_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await MaybeBehaviors.CreateNoneAsync<int>().Map(MaybeBehaviors.Increment);
        result.Should().BeNone();
    }

    [Fact]
    public async Task Map_ShouldReturnSome_GivenValueIsSome()
    {
        var result = await MaybeBehaviors.CreateSomeAsync(10).Map(MaybeBehaviors.Increment);
        result.Should().BeSome(11);
    }

    [Fact]
    public async Task MapAsync_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await MaybeBehaviors.CreateNoneAsync<int>().MapAsync(MaybeBehaviors.IncrementAsync);
        result.Should().BeNone();
    }

    [Fact]
    public async Task MapAsync_ShouldReturnSome_GivenValueIsSome()
    {
        var result = await MaybeBehaviors.CreateSomeAsync(10).MapAsync(MaybeBehaviors.IncrementAsync);
        result.Should().BeSome(11);
    }

    [Fact]
    public async Task Match_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await MaybeBehaviors.CreateNoneAsync<int>().Match(_ => "some", () => "none");
        result.Should().Be("none");
    }

    [Fact]
    public async Task Match_ShouldReturnSome_GivenValueIsSome()
    {
        var result = await MaybeBehaviors.CreateSomeAsync(10).Match(_ => "some", () => "none");
        result.Should().Be("some");
    }
}