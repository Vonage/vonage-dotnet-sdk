#region
using FluentAssertions;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Result;

public class MergeTest
{
    [Fact]
    public void Merge_ShouldReturnFailure_GivenFirstMonadIsFailure() =>
        TestBehaviors.CreateFailure<int>()
            .Merge(TestBehaviors.CreateSuccess(5), (first, second) => new {First = first, Second = second})
            .Should()
            .BeFailure(TestBehaviors.CreateResultFailure());

    [Fact]
    public void Merge_ShouldReturnFailure_GivenSecondMonadIsFailure() =>
        TestBehaviors.CreateSuccess(5)
            .Merge(TestBehaviors.CreateFailure<int>(), (first, second) => new {First = first, Second = second})
            .Should()
            .BeFailure(TestBehaviors.CreateResultFailure());

    [Fact]
    public void Merge_ShouldReturnSuccess_GivenBothResultsAreSuccess() =>
        TestBehaviors.CreateSuccess(5)
            .Merge(TestBehaviors.CreateSuccess(10), (first, second) => new {First = first, Second = second})
            .Should()
            .BeSuccess(success =>
            {
                success.First.Should().Be(5);
                success.Second.Should().Be(10);
            });
}