#region
using System;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Common.Monads.Exceptions;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads;

[Trait("Category", "Unit")]
public class MaybeTest
{
    [Fact]
    public void Constructor_ShouldReturnNone() => new Maybe<int>().Should().BeNone();

    [Fact]
    public void ToString_ShouldReturnNone_GivenValueIsNone() =>
        MaybeBehaviors.CreateNone<int>().ToString().Should().Be("None");

    [Fact]
    public void ToString_ShouldReturnSome_GivenValueIsSome() => MaybeBehaviors.CreateSome(10).ToString().Should()
        .Be("Some(Vonage.Common.Monads.Maybe`1[System.Int32])");

    public class Bind
    {
        [Fact]
        public void Bind_ShouldReturnNone_GivenValueIsNone() =>
            MaybeBehaviors.CreateNone<int>().Bind(MaybeBehaviors.BindToString).Should().BeNone();

        [Theory]
        [InlineData(10, "10")]
        [InlineData("10", "10")]
        [InlineData(true, "True")]
        public void Bind_ShouldReturnSome_GivenValueIsSome<T>(T value, string expected) =>
            MaybeBehaviors.CreateSome(value).Bind(MaybeBehaviors.BindToString).Should().BeSome(expected);

        [Fact]
        public async Task BindAsync_ShouldReturnNone_GivenValueIsNone()
        {
            var result = await MaybeBehaviors.CreateNone<int>()
                .BindAsync(_ => Task.FromResult(Maybe<string>.Some(string.Empty)));
            result.Should().BeNone();
        }

        [Fact]
        public async Task BindAsync_ShouldReturnSome_GivenValueIsSome()
        {
            var result = await MaybeBehaviors.CreateSome(10)
                .BindAsync(_ => Task.FromResult(Maybe<string>.Some("Success")));
            result.Should().BeSome("Success");
        }
    }

    public class Equality
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

    public class GetUnsafe
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

    public class IfNone
    {
        [Fact]
        public void IfNone_Operation_ShouldReturnOperation_GivenValueIsNone() =>
            MaybeBehaviors.CreateNone<int>().IfNone(() => 5).Should().Be(5);

        [Fact]
        public void IfNone_Operation_ShouldReturnValue_GivenValueIsSome() =>
            MaybeBehaviors.CreateSome(10).IfNone(() => 5).Should().Be(10);

        [Fact]
        public void IfNone_Value_ShouldReturnSpecifiedValue_GivenValueIsNone() =>
            MaybeBehaviors.CreateNone<int>().IfNone(5).Should().Be(5);

        [Fact]
        public void IfNone_Value_ShouldReturnValue_GivenValueIsSome() =>
            MaybeBehaviors.CreateSome(10).IfNone(5).Should().Be(10);
    }

    public class IfSome
    {
        [Fact]
        public void IfSome_ShouldBeExecuted_GivenValueIsSome()
        {
            var test = 10;
            var result = MaybeBehaviors.CreateSome(10).IfSome(value => test += value);
            test.Should().Be(20);
            result.Should().Be(MaybeBehaviors.CreateSome(10));
        }

        [Fact]
        public void IfSome_ShouldNotBeExecuted_GivenValueIsNone()
        {
            var test = 10;
            var result = MaybeBehaviors.CreateNone<int>().IfSome(value => test += value);
            test.Should().Be(10);
            result.Should().Be(MaybeBehaviors.CreateNone<int>());
        }

        [Fact]
        public async Task IfSomeAsync_ShouldBeExecuted_GivenValueIsSome()
        {
            var test = 10;
            var result = await MaybeBehaviors.CreateSome(10).IfSomeAsync(value => Task.FromResult(test += value));
            test.Should().Be(20);
            result.Should().Be(MaybeBehaviors.CreateSome(10));
        }

        [Fact]
        public async Task IfSomeAsync_ShouldNotBeExecuted_GivenValueIsNone()
        {
            var test = 10;
            var result = await MaybeBehaviors.CreateNone<int>().IfSomeAsync(value => Task.FromResult(test += value));
            test.Should().Be(10);
            result.Should().Be(MaybeBehaviors.CreateNone<int>());
        }
    }

    public class Map
    {
        [Fact]
        public void Map_ShouldReturnNone_GivenValueIsNone() =>
            MaybeBehaviors.CreateNone<int>().Map(MaybeBehaviors.MapToString).Should().BeNone();

        [Theory]
        [InlineData(10, "10")]
        [InlineData("10", "10")]
        [InlineData(true, "True")]
        public void Map_ShouldReturnSome_GivenValueIsSome<T>(T value, string expected) =>
            MaybeBehaviors.CreateSome(value).Map(MaybeBehaviors.MapToString).Should().BeSome(expected);

        [Fact]
        public async Task MapAsync_ShouldReturnNone_GivenValueIsNone()
        {
            var result = await MaybeBehaviors.CreateNone<int>().MapAsync(_ => Task.FromResult("Success"));
            result.Should().BeNone();
        }

        [Theory]
        [InlineData(10, "10")]
        [InlineData("10", "10")]
        [InlineData(true, "True")]
        public void MapAsync_ShouldReturnSome_GivenValueIsSome<T>(T value, string expected) =>
            MaybeBehaviors.CreateSome(value).Map(MaybeBehaviors.MapToString).Should().BeSome(expected);
    }

    public class Match
    {
        [Fact]
        public void Match_ShouldReturnNoneOperation_GivenValueIsNone() =>
            MaybeBehaviors.CreateNone<int>().Match(MaybeBehaviors.MapToString, () => "Some value").Should()
                .Be("Some value");

        [Fact]
        public void Match_ShouldReturnSomeOperation_GivenValueIsSome() =>
            MaybeBehaviors.CreateSome(10).Match(MaybeBehaviors.MapToString, () => "Some value").Should().Be("10");

        [Fact]
        public void Merge_ShouldReturnNone_GivenFirstLegIsNone() =>
            MaybeBehaviors.CreateNone<int>()
                .Merge(MaybeBehaviors.CreateSome(5), (first, second) => new {First = first, Second = second})
                .Should()
                .BeNone();

        [Fact]
        public void Merge_ShouldReturnNone_GivenSecondLegIsNone() =>
            MaybeBehaviors.CreateSome(5)
                .Merge(MaybeBehaviors.CreateNone<int>(), (first, second) => new {First = first, Second = second})
                .Should()
                .BeNone();

        [Fact]
        public void Merge_ShouldReturnSome_GivenBothAreSome() =>
            MaybeBehaviors.CreateSome(5)
                .Merge(MaybeBehaviors.CreateSome(10), (first, second) => new {First = first, Second = second})
                .Should()
                .BeSome(some =>
                {
                    some.First.Should().Be(5);
                    some.Second.Should().Be(10);
                });
    }

    public class State
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

    public class Do
    {
        [Fact]
        public void Do_ShouldExecuteSomeAction_GivenStateIsSome()
        {
            var value = 0;
            MaybeBehaviors.CreateSome(5).Do(success => value += success, () => { });
            value.Should().Be(5);
        }

        [Fact]
        public void Do_ShouldExecuteNoneAction_GivenStateIsNone()
        {
            var value = 0;
            MaybeBehaviors.CreateNone<int>().Do(_ => { }, () => value = 10);
            value.Should().Be(10);
        }

        [Fact]
        public void Do_ShouldReturnInstance() =>
            MaybeBehaviors.CreateSome(5).Do(_ => { }, () => { }).Should().BeSome(5);

        [Fact]
        public void DoWhenSome_ShouldExecuteAction_GivenStateIsSome()
        {
            var value = 0;
            MaybeBehaviors.CreateSome(5).DoWhenSome(success => value += success);
            value.Should().Be(5);
        }

        [Fact]
        public void DoWhenSome_ShouldNotExecuteAction_GivenStateIsNone()
        {
            var value = 0;
            MaybeBehaviors.CreateNone<int>().DoWhenSome(_ => value = 10);
            value.Should().Be(0);
        }

        [Fact]
        public void DoWhenSome_ShouldReturnInstance() =>
            MaybeBehaviors.CreateSome(5).DoWhenSome(_ => { }).Should().BeSome(5);

        [Fact]
        public void DoWhenNone_ShouldExecuteAction_GivenStateIsNone()
        {
            var value = 0;
            MaybeBehaviors.CreateNone<int>().DoWhenNone(() => value = 10);
            value.Should().Be(10);
        }

        [Fact]
        public void DoWhenNone_ShouldNotExecuteAction_GivenStateIsSome()
        {
            var value = 0;
            MaybeBehaviors.CreateSome(5).DoWhenNone(() => value = 10);
            value.Should().Be(0);
        }

        [Fact]
        public void DoWhenFailure_ShouldReturnInstance() =>
            MaybeBehaviors.CreateSome(5).DoWhenNone(() => { }).Should().BeSome(5);
    }
}