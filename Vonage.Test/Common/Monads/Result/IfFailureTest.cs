#region
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common.Monads;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Result;

public class IfFailureTest
{
    [Fact]
    public void IfFailure_ShouldBeExecuted_GivenValueIsFailure()
    {
        var test = 10;
        TestBehaviors.CreateFailure<int>().IfFailure(_ => test = 0);
        test.Should().Be(0);
    }

    [Fact]
    public void IfFailure_ShouldNotBeExecuted_GivenValueIsSuccess()
    {
        var test = 10;
        TestBehaviors.CreateSuccess(50).IfFailure(_ => test = 0);
        test.Should().Be(10);
    }

    [Fact]
    public void IfFailure_ShouldReturnDefaultValue_GivenValueIsFailure() =>
        TestBehaviors.CreateFailure<int>().IfFailure(10).Should().Be(10);

    [Fact]
    public void IfFailure_ShouldReturnFailureOperation_GivenValueIsFailure() =>
        TestBehaviors.CreateFailure<int>().IfFailure(_ => 0).Should().Be(0);

    [Fact]
    public void IfFailure_ShouldReturnSuccess_GivenValueIsSuccess() =>
        TestBehaviors.CreateSuccess(50).IfFailure(0).Should().Be(50);

    [Fact]
    public void IfFailure_ShouldReturnSuccessOperation_GivenValueIsSuccess() =>
        TestBehaviors.CreateSuccess(50).IfFailure(_ => 0).Should().Be(50);

    [Fact]
    public async Task IfFailureExtension_ShouldReturnFallbackValue_GivenFailure()
    {
        var result = await TestBehaviors.CreateFailureAsync<int>().IfFailure(50);
        result.Should().Be(50);
    }

    [Fact]
    public async Task IfFailureExtension_ShouldReturnSuccess_GivenSuccess()
    {
        var result = await TestBehaviors.CreateSuccessAsync(10).IfFailure(50);
        result.Should().Be(10);
    }
}