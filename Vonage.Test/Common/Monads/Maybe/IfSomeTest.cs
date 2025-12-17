#region
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Maybe;

public class IfSomeTest
{
    [Fact]
    public void IfSome_ShouldBeExecuted_GivenValueIsSome()
    {
        var test = 10;
        var result = MaybeBehaviors.CreateSome(10).IfSome(value => test += value);
        test.Should().Be(20);
        result.Should().Be(MaybeBehaviors.CreateSome(10));
    }

    [Fact]
    public void IfSome_ShouldNotBeExecuted_GivenValueIsNone()
    {
        var test = 10;
        var result = MaybeBehaviors.CreateNone<int>().IfSome(value => test += value);
        test.Should().Be(10);
        result.Should().Be(MaybeBehaviors.CreateNone<int>());
    }

    [Fact]
    public async Task IfSomeAsync_ShouldBeExecuted_GivenValueIsSome()
    {
        var test = 10;
        var result = await MaybeBehaviors.CreateSome(10).IfSomeAsync(value => Task.FromResult(test += value));
        test.Should().Be(20);
        result.Should().Be(MaybeBehaviors.CreateSome(10));
    }

    [Fact]
    public async Task IfSomeAsync_ShouldNotBeExecuted_GivenValueIsNone()
    {
        var test = 10;
        var result = await MaybeBehaviors.CreateNone<int>().IfSomeAsync(value => Task.FromResult(test += value));
        test.Should().Be(10);
        result.Should().Be(MaybeBehaviors.CreateNone<int>());
    }
}