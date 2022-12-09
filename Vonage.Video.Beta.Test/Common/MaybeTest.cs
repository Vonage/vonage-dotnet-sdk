using System;
using FluentAssertions;
using Vonage.Video.Beta.Common;
using Xunit;

namespace Vonage.Video.Beta.Test.Common
{
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
            var maybe = CreateSome(10);
            maybe.IsNone.Should().BeFalse();
            maybe.IsSome.Should().BeTrue();
        }

        [Fact]
        public void Map_ShouldReturnNone_GivenValueIsNone() =>
            Maybe<int>.None
                .Map(MapToString)
                .Should()
                .Be(Maybe<string>.None);

        [Fact]
        public void Map_ShouldReturnSome_GivenValueIsSome() =>
            CreateSome(10)
                .Map(MapToString)
                .Should()
                .Be(CreateSome("10"));

        [Fact]
        public void Match_ShouldReturnNoneOperation_GivenValueIsNone() =>
            Maybe<int>.None
                .Match(MapToString, GetStaticString)
                .Should()
                .Be("Some value");

        [Fact]
        public void Match_ShouldReturnSomeOperation_GivenValueIsSome() =>
            CreateSome(10)
                .Match(MapToString, GetStaticString)
                .Should()
                .Be("10");

        [Fact]
        public void Some_ShouldThrowException_GivenValueIsNone()
        {
            Action act = () => Maybe<int?>.Some<int?>(null);
            act.Should().Throw<InvalidOperationException>().WithMessage(Maybe<int>.NullValueMessage);
        }

        private static Maybe<T> CreateSome<T>(T value) => Maybe<T>.Some(value);

        private static string GetStaticString() => "Some value";

        private static string MapToString(int value) => value.ToString();
    }
}