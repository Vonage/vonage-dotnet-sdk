#region
using FluentAssertions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Result;

public class ToStringTest
{
    [Fact]
    public void ToString_ShouldReturnFailure_GivenStateIsFailure() =>
        TestBehaviors.CreateFailure<int>().ToString().Should().Be("Failure(Error)");

    [Fact]
    public void ToString_ShouldReturnSuccess_GivenStatsIsSuccess() => TestBehaviors.CreateSuccess(10).ToString()
        .Should()
        .Be("Success(10)");
}