#region
using FluentAssertions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Result;

public class StateTest
{
    [Fact]
    public void Failure_ShouldHaveFailureState()
    {
        var result = TestBehaviors.CreateFailure<int>();
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Success_ShouldHaveSuccessState()
    {
        var result = TestBehaviors.CreateSuccess(0);
        result.IsFailure.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
    }
}