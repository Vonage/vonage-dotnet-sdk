#region
using System;
using FluentAssertions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Result;

public class GetUnsafeTest
{
    [Fact]
    public void GetFailureUnsafe_ShouldReturn_GivenFailure() =>
        TestBehaviors.CreateFailure<int>().GetFailureUnsafe().Should().Be(TestBehaviors.CreateResultFailure());

    [Fact]
    public void GetFailureUnsafe_ShouldThrowResultException_GivenFailure()
    {
        Action act = () => TestBehaviors.CreateSuccess(5).GetFailureUnsafe();
        act.Should().Throw<InvalidOperationException>().Which.Message.Should()
            .Be("Result is not in Failure state.");
    }

    [Fact]
    public void GetSuccessUnsafe_ShouldReturn_GivenSuccess() =>
        TestBehaviors.CreateSuccess(5).GetSuccessUnsafe().Should().Be(5);

    [Fact]
    public void GetSuccessUnsafe_ShouldThrowResultException_GivenFailure()
    {
        var expectedException = TestBehaviors.CreateResultFailure().ToException();
        Action act = () => TestBehaviors.CreateFailure<int>().GetSuccessUnsafe();
        var exception = act.Should().Throw<Exception>().Which;
        exception.Should().BeOfType(expectedException.GetType());
        exception.Message.Should().Be(expectedException.Message);
    }
}