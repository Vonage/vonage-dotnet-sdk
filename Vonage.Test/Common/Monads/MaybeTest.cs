using System;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Common.Monads.Exceptions;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Common.Monads;

[Trait("Category", "Unit")]
public class MaybeTest
{
    [Fact]
    public void Bind_ShouldReturnNone_GivenValueIsNone() =>
        Maybe<int>.None.Bind(BindToString).Should().BeNone();

    [Theory]
    [InlineData(10, "10")]
    [InlineData("10", "10")]
    [InlineData(true, "True")]
    public void Bind_ShouldReturnSome_GivenValueIsSome<T>(T value, string expected) =>
        CreateSome(value).Bind(BindToString).Should().BeSome(expected);

    [Fact]
    public async Task BindAsync_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await Maybe<int>.None.BindAsync(_ => Task.FromResult(Maybe<string>.Some(string.Empty)));
        result.Should().BeNone();
    }

    [Fact]
    public async Task BindAsync_ShouldReturnSome_GivenValueIsSome()
    {
        var result = await CreateSome(10).BindAsync(_ => Task.FromResult(Maybe<string>.Some("Success")));
        result.Should().BeSome("Success");
    }

    [Fact]
    public void Constructor_ShouldReturnNone() => new Maybe<int>().Should().BeNone();

    [Theory]
    [InlineData(10, 5)]
    [InlineData("10", "010")]
    [InlineData(true, false)]
    public void Equals_ShouldReturnFalse_GivenBothAreSomeWithDifferentValue<T>(T first, T second) =>
        CreateSome(first).Equals(CreateSome(second)).Should().BeFalse();

    [Theory]
    [InlineData(10)]
    [InlineData("eee")]
    public void Equals_ShouldReturnFalse_GivenOneIsNoneAndOtherIsSome<T>(T value) =>
        CreateSome(value).Equals(Maybe<T>.None).Should().BeFalse();

    [Theory]
    [InlineData(10)]
    [InlineData("eee")]
    public void Equals_ShouldReturnFalse_GivenOneIsSomeAndOtherIsNone<T>(T value) =>
        Maybe<T>.None.Equals(CreateSome(value)).Should().BeFalse();

    [Fact]
    public void Equals_ShouldReturnTrue_GivenBothAreNone() =>
        Maybe<int>.None.Equals(Maybe<int>.None).Should().BeTrue();

    [Theory]
    [InlineData(10)]
    [InlineData("eee")]
    public void Equals_ShouldReturnTrue_GivenBothAreSomeWithSameValue<T>(T value) =>
        CreateSome(value).Equals(CreateSome(value)).Should().BeTrue();

    [Theory]
    [InlineData(10)]
    [InlineData("eee")]
    public void GetHashCode_ShouldReturnValue_GivenSome<T>(T value) =>
        CreateSome(value).GetHashCode().Should().Be(value.GetHashCode());

    [Fact]
    public void GetHashCode_ShouldReturnZero_GivenNone() =>
        Maybe<string>.None.GetHashCode().Should().Be(0);

    [Theory]
    [InlineData(10)]
    [InlineData("eee")]
    public void GetUnsafe_ShouldReturnValue_GivenSome<T>(T value) =>
        CreateSome(value).GetUnsafe().Should().Be(value);

    [Fact]
    public void GetUnsafe_ShouldThrowException_GivenNone()
    {
        Action act = () => Maybe<string>.None.GetUnsafe();
        act.Should().Throw<NoneStateException>();
    }

    [Fact]
    public void IfNone_Operation_ShouldReturnOperation_GivenValueIsNone() =>
        Maybe<int>.None.IfNone(() => 5).Should().Be(5);

    [Fact]
    public void IfNone_Operation_ShouldReturnValue_GivenValueIsSome() =>
        CreateSome(10).IfNone(() => 5).Should().Be(10);

    [Fact]
    public void IfNone_Value_ShouldReturnSpecifiedValue_GivenValueIsNone() =>
        Maybe<int>.None.IfNone(5).Should().Be(5);

    [Fact]
    public void IfNone_Value_ShouldReturnValue_GivenValueIsSome() =>
        CreateSome(10).IfNone(5).Should().Be(10);

    [Fact]
    public void IfSome_ShouldBeExecuted_GivenValueIsSome()
    {
        var test = 10;
        var result = CreateSome(10).IfSome(value => test += value);
        test.Should().Be(20);
        result.Should().Be(CreateSome(10));
    }

    [Fact]
    public void IfSome_ShouldNotBeExecuted_GivenValueIsNone()
    {
        var test = 10;
        var result = Maybe<int>.None.IfSome(value => test += value);
        test.Should().Be(10);
        result.Should().Be(Maybe<int>.None);
    }

    [Fact]
    public async Task IfSomeAsync_ShouldBeExecuted_GivenValueIsSome()
    {
        var test = 10;
        var result = await CreateSome(10).IfSomeAsync(value => Task.FromResult(test += value));
        test.Should().Be(20);
        result.Should().Be(CreateSome(10));
    }

    [Fact]
    public async Task IfSomeAsync_ShouldNotBeExecuted_GivenValueIsNone()
    {
        var test = 10;
        var result = await Maybe<int>.None.IfSomeAsync(value => Task.FromResult(test += value));
        test.Should().Be(10);
        result.Should().Be(Maybe<int>.None);
    }

    [Fact]
    public void ImplicitOperator_ShouldConvertToNone_GivenValueIsNull()
    {
        Maybe<string> maybe = null;
        maybe.Should().BeNone();
    }

    [Theory]
    [InlineData(10)]
    [InlineData("eee")]
    public void ImplicitOperator_ShouldConvertToSome_GivenValueIsSome<T>(T value)
    {
        Maybe<T> maybe = value;
        maybe.Should().BeSome(some => some.Should().Be(value));
    }

    [Fact]
    public void Map_ShouldReturnNone_GivenValueIsNone() =>
        Maybe<int>.None.Map(MapToString).Should().BeNone();

    [Theory]
    [InlineData(10, "10")]
    [InlineData("10", "10")]
    [InlineData(true, "True")]
    public void Map_ShouldReturnSome_GivenValueIsSome<T>(T value, string expected) =>
        CreateSome(value).Map(MapToString).Should().BeSome(expected);

    [Fact]
    public async Task MapAsync_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await Maybe<int>.None.MapAsync(_ => Task.FromResult("Success"));
        result.Should().BeNone();
    }

    [Theory]
    [InlineData(10, "10")]
    [InlineData("10", "10")]
    [InlineData(true, "True")]
    public void MapAsync_ShouldReturnSome_GivenValueIsSome<T>(T value, string expected) =>
        CreateSome(value).Map(MapToString).Should().BeSome(expected);

    [Fact]
    public void Match_ShouldReturnNoneOperation_GivenValueIsNone() =>
        Maybe<int>.None.Match(MapToString, GetStaticString).Should().Be("Some value");

    [Fact]
    public void Match_ShouldReturnSomeOperation_GivenValueIsSome() =>
        CreateSome(10).Match(MapToString, GetStaticString).Should().Be("10");

    [Fact]
    public void Merge_ShouldReturnNone_GivenFirstLegIsNone() =>
        Maybe<int>.None
            .Merge(CreateSome(5), (first, second) => new {First = first, Second = second})
            .Should()
            .BeNone();

    [Fact]
    public void Merge_ShouldReturnNone_GivenSecondLegIsNone() =>
        CreateSome(5)
            .Merge(Maybe<int>.None, (first, second) => new {First = first, Second = second})
            .Should()
            .BeNone();

    [Fact]
    public void Merge_ShouldReturnSome_GivenBothAreSome() =>
        CreateSome(5)
            .Merge(CreateSome(10), (first, second) => new {First = first, Second = second})
            .Should()
            .BeSome(some =>
            {
                some.First.Should().Be(5);
                some.Second.Should().Be(10);
            });

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
        var maybe = CreateSome(10);
        maybe.IsNone.Should().BeFalse();
        maybe.IsSome.Should().BeTrue();
    }

    [Fact]
    public void Some_ShouldThrowException_GivenValueIsNone()
    {
        Action act = () => Maybe<int?>.Some<int?>(null);
        act.Should().Throw<InvalidOperationException>().WithMessage(Maybe<int>.NullValueMessage);
    }

    [Fact]
    public void ToString_ShouldReturnNone_GivenValueIsNone() => Maybe<int>.None.ToString().Should().Be("None");

    [Fact]
    public void ToString_ShouldReturnSome_GivenValueIsSome() => CreateSome(10).ToString().Should()
        .Be("Some(Vonage.Common.Monads.Maybe`1[System.Int32])");

    private static Maybe<string> BindToString<T>(T value) => Maybe<string>.Some(value.ToString());

    private static Maybe<T> CreateSome<T>(T value) => Maybe<T>.Some(value);

    private static string GetStaticString() => "Some value";

    private static string MapToString<T>(T value) => value.ToString();
}