#region
using System;
using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Common.Monads.Exceptions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Maybe;

public class GetUnsafeTest
{
    [Theory]
    [InlineData(10)]
    [InlineData("eee")]
    public void GetUnsafe_ShouldReturnValue_GivenSome<T>(T value) =>
        MaybeBehaviors.CreateSome(value).GetUnsafe().Should().Be(value);

    [Fact]
    public void GetUnsafe_ShouldThrowException_GivenNone()
    {
        Action act = () => Maybe<string>.None.GetUnsafe();
        act.Should().Throw<NoneStateException>();
    }
}