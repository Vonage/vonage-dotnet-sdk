#region
using System;
using FluentAssertions;
using Vonage.Common.Monads;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Maybe;

public class StateTest
{
    [Fact]
    public void None_ShouldReturnNone()
    {
        var maybe = MaybeBehaviors.CreateNone<int>();
        maybe.IsNone.Should().BeTrue();
        maybe.IsSome.Should().BeFalse();
    }

    [Fact]
    public void Some_ShouldReturnSome()
    {
        var maybe = MaybeBehaviors.CreateSome(10);
        maybe.IsNone.Should().BeFalse();
        maybe.IsSome.Should().BeTrue();
    }

    [Fact]
    public void Some_ShouldThrowException_GivenValueIsNone()
    {
        Action act = () => Maybe<int?>.Some<int?>(null);
        act.Should().Throw<InvalidOperationException>().WithMessage(Maybe<int>.NullValueMessage);
    }
}