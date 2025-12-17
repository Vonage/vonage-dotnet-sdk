#region
using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Result;

public class EqualityTest
{
    [Fact]
    public void Equals_ShouldReturnFalse_GivenBothAreSuccessWithDifferentValue() =>
        TestBehaviors.CreateSuccess(5).Equals(TestBehaviors.CreateSuccess(10)).Should().BeFalse();

    [Fact]
    public void Equals_ShouldReturnFalse_GivenOneIsFailureAndOtherIsSuccess() =>
        TestBehaviors.CreateSuccess(10).Equals(TestBehaviors.CreateFailure<int>()).Should().BeFalse();

    [Fact]
    public void Equals_ShouldReturnFalse_GivenOneIsSuccessAndOtherIsFailure() =>
        TestBehaviors.CreateFailure<int>().Equals(TestBehaviors.CreateSuccess(10)).Should().BeFalse();

    [Fact]
    public void Equals_ShouldReturnTrue_GivenBothAreNone() =>
        Maybe<int>.None.Equals(Maybe<int>.None).Should().BeTrue();

    [Fact]
    public void Equals_ShouldReturnTrue_GivenBothAreSuccessWithSameValue() =>
        TestBehaviors.CreateSuccess(10).Equals(TestBehaviors.CreateSuccess(10)).Should().BeTrue();

    [Fact]
    public void GetHashCode_ShouldReturnValue_GivenFailure()
    {
        const int value = 35;
        TestBehaviors.CreateSuccess(value).GetHashCode().Should().Be(value.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ShouldReturnValue_GivenSuccess()
    {
        var failure = TestBehaviors.CreateResultFailure();
        Result<int>.FromFailure(failure).GetHashCode().Should().Be(failure.GetHashCode());
    }

    [Fact]
    public void ImplicitOperator_ShouldConvertToSuccess_GivenValueIsSuccess()
    {
        const int value = 55;
        Result<int> result = value;
        result.Should().BeSuccess(value);
    }
}