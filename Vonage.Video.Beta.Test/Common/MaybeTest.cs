using FluentAssertions;
using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Test.Common;

public class MaybeTest
{
    [Fact]
    public void None_ShouldReturnNone()
    {
        var maybe = Maybe<int>.None;
        maybe.IsNone.Should().BeTrue();
        maybe.IsSome.Should().BeFalse();
    }

    [Fact]
    public void Some_ShouldReturnSome()
    {
        var maybe = Maybe<int>.Some(10);
        maybe.IsNone.Should().BeFalse();
        maybe.IsSome.Should().BeTrue();
    }

    [Fact]
    public void Map_ShouldReturnNone_GivenValueIsNone() =>
        Maybe<int>.None
            .Map(value => value.ToString())
            .Should()
            .Be(Maybe<string>.None);

    [Fact]
    public void Map_ShouldReturnSome_GivenValueIsSome() =>
        Maybe<int>.Some(10)
            .Map(value => value.ToString())
            .Should()
            .Be(Maybe<string>.Some("10"));

    [Fact]
    public void Match_ShouldReturnNoneOperation_GivenValueIsNone() =>
        Maybe<int>.None
            .Match(value => value.ToString(), () => "Some value")
            .Should()
            .Be("Some value");

    [Fact]
    public void Match_ShouldReturnSomeOperation_GivenValueIsSome() =>
        Maybe<int>.Some(10)
            .Match(value => value.ToString(), () => "Some value")
            .Should()
            .Be("10");

    [Fact]
    public void Some_ShouldThrowException_GivenValueIsNone()
    {
        Action act = () => Maybe<int?>.Some<int?>(null);
        act.Should().Throw<InvalidOperationException>().WithMessage("Value cannot be null.");
    }
}