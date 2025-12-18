#region
using FluentAssertions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Maybe;

public class ToStringTest
{
    [Fact]
    public void ToString_ShouldReturnNone_GivenValueIsNone() =>
        MaybeBehaviors.CreateNone<int>().ToString().Should().Be("None");

    [Fact]
    public void ToString_ShouldReturnSome_GivenValueIsSome() => MaybeBehaviors.CreateSome(10).ToString().Should()
        .Be("Some(Vonage.Common.Monads.Maybe`1[System.Int32])");
}