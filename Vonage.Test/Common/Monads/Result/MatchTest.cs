#region
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common.Monads;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Result;

public class MatchTest
{
    [Fact]
    public void Match_ShouldExecuteFailureOperation_GivenValueIsFailure()
    {
        var value = 0;
        TestBehaviors.CreateFailure<int>().Match(success => value = success, _ => value = -1);
        value.Should().Be(-1);
    }

    [Fact]
    public void Match_ShouldExecuteSuccessOperation_GivenValueIsSuccess()
    {
        var value = 0;
        TestBehaviors.CreateSuccess(5).Match(success => value = success, _ => value = -1);
        value.Should().Be(5);
    }

    [Fact]
    public void Match_ShouldReturnFailureOperation_GivenValueIsFailure() =>
        TestBehaviors.CreateFailure<int>()
            .Match(value => value.ToString(), failure => failure.GetFailureMessage())
            .Should()
            .Be("Error");

    [Fact]
    public void Match_ShouldReturnSuccessOperation_GivenValueIsSuccess() =>
        TestBehaviors.CreateSuccess(5)
            .Match(TestBehaviors.Increment, _ => 0)
            .Should()
            .Be(6);

    [Fact]
    public async Task MatchExtension_ShouldReturnFallback_GivenFailure()
    {
        var result = await TestBehaviors.CreateFailureAsync<int>().Match(_ => "some", _ => "none");
        result.Should().Be("none");
    }

    [Fact]
    public async Task MatchExtension_ShouldReturnValue_GivenSuccess()
    {
        var result = await TestBehaviors.CreateSuccessAsync(10).Match(_ => "some", _ => "none");
        result.Should().Be("some");
    }
}