#region
using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Maybe;

public class EqualityTest
{
    [Theory]
    [InlineData(10, 5)]
    [InlineData("10", "010")]
    [InlineData(true, false)]
    public void Equals_ShouldReturnFalse_GivenBothAreSomeWithDifferentValue<T>(T first, T second) =>
        MaybeBehaviors.CreateSome(first).Equals(MaybeBehaviors.CreateSome(second)).Should().BeFalse();

    [Theory]
    [InlineData(10)]
    [InlineData("eee")]
    public void Equals_ShouldReturnFalse_GivenOneIsNoneAndOtherIsSome<T>(T value) =>
        MaybeBehaviors.CreateSome(value).Equals(Maybe<T>.None).Should().BeFalse();

    [Theory]
    [InlineData(10)]
    [InlineData("eee")]
    public void Equals_ShouldReturnFalse_GivenOneIsSomeAndOtherIsNone<T>(T value) =>
        Maybe<T>.None.Equals(MaybeBehaviors.CreateSome(value)).Should().BeFalse();

    [Fact]
    public void Equals_ShouldReturnTrue_GivenBothAreNone() =>
        MaybeBehaviors.CreateNone<int>().Equals(MaybeBehaviors.CreateNone<int>()).Should().BeTrue();

    [Theory]
    [InlineData(10)]
    [InlineData("eee")]
    public void Equals_ShouldReturnTrue_GivenBothAreSomeWithSameValue<T>(T value) =>
        MaybeBehaviors.CreateSome(value).Equals(MaybeBehaviors.CreateSome(value)).Should().BeTrue();

    [Theory]
    [InlineData(10)]
    [InlineData("eee")]
    public void GetHashCode_ShouldReturnValue_GivenSome<T>(T value) =>
        MaybeBehaviors.CreateSome(value).GetHashCode().Should().Be(value.GetHashCode());

    [Fact]
    public void GetHashCode_ShouldReturnZero_GivenNone() =>
        Maybe<string>.None.GetHashCode().Should().Be(0);

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
}