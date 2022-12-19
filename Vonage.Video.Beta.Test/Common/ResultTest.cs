using System;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Video.Beta.Common;
using Vonage.Video.Beta.Common.Failures;
using Vonage.Video.Beta.Test.Extensions;
using Xunit;

namespace Vonage.Video.Beta.Test.Common
{
    public class ResultTest
    {
        [Fact]
        public void FromError_ShouldReturnError()
        {
            var result = CreateFailure();
            result.IsFailure.Should().BeTrue();
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void FromSuccess_ShouldReturnSuccess()
        {
            var result = CreateSuccess(0);
            result.IsFailure.Should().BeFalse();
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Map_ShouldReturnFailure_GivenValueIsFailure() =>
            CreateFailure()
                .Map(Increment)
                .Should()
                .Be(CreateResultFailure());

        [Fact]
        public async Task MapAsync_ShouldReturnFailure_GivenValueIsFailure() =>
            (await CreateFailure()
                .MapAsync(IncrementAsync))
            .Should()
            .Be(CreateResultFailure());

        [Fact]
        public void Map_ShouldReturnSuccess_GivenValueIsSuccess() =>
            CreateSuccess(5)
                .Map(Increment)
                .Should()
                .Be(6);

        [Fact]
        public async Task MapAsync_ShouldReturnSuccess_GivenValueIsSuccess() =>
            (await CreateSuccess(5)
                .MapAsync(IncrementAsync))
            .Should()
            .Be(6);

        [Fact]
        public void Bind_ShouldReturnFailure_GivenValueIsFailure() =>
            CreateFailure()
                .Bind(IncrementBind)
                .Should()
                .Be(CreateResultFailure());

        [Fact]
        public async Task BindAsync_ShouldReturnFailure_GivenValueIsFailure() =>
            (await CreateFailure()
                .BindAsync(IncrementBindAsync))
            .Should()
            .Be(CreateResultFailure());

        [Fact]
        public void Bind_ShouldReturnSuccess_GivenValueIsSuccess() =>
            CreateSuccess(5)
                .Bind(IncrementBind)
                .Should()
                .Be(6);

        [Fact]
        public async Task BindAsync_ShouldReturnSuccess_GivenValueIsSuccess() =>
            (await CreateSuccess(5)
                .BindAsync(IncrementBindAsync))
            .Should()
            .Be(6);

        [Fact]
        public void Match_ShouldReturnFailureOperation_GivenValueIsFailure() =>
            CreateFailure()
                .Match(value => value.ToString(), failure => failure.GetFailureMessage())
                .Should()
                .Be("Some error");

        [Fact]
        public void Match_ShouldReturnSuccessOperation_GivenValueIsSuccess() =>
            CreateSuccess(5)
                .Match(Increment, _ => 0)
                .Should()
                .Be(6);

        [Fact]
        public void GetHashCode_ShouldReturnValue_GivenFailure()
        {
            const int value = 35;
            CreateSuccess(value).GetHashCode().Should().Be(value.GetHashCode());
        }

        [Fact]
        public void GetHashCode_ShouldReturnValue_GivenSuccess()
        {
            var failure = CreateResultFailure();
            Result<int>.FromFailure(failure).GetHashCode().Should().Be(failure.GetHashCode());
        }

        [Fact]
        public void ImplicitOperator_ShouldConvertToSuccess_GivenValueIsSuccess()
        {
            const int value = 55;
            Result<int> result = value;
            result.Should().Be(value);
        }

        [Fact]
        public void IfFailure_ShouldBeExecuted_GivenValueIsFailure()
        {
            var test = 10;
            CreateFailure().IfFailure(_ => test = 0);
            test.Should().Be(0);
        }

        [Fact]
        public void IfFailure_ShouldNotBeExecuted_GivenValueIsSuccess()
        {
            var test = 10;
            CreateSuccess(50).IfFailure(_ => test = 0);
            test.Should().Be(10);
        }

        [Fact]
        public void IfSuccess_ShouldNotBeExecuted_GivenValueIsFailure()
        {
            var test = 10;
            CreateFailure().IfSuccess(_ => test = 0);
            test.Should().Be(10);
        }

        [Fact]
        public void IfSuccess_ShouldBeExecuted_GivenValueIsSuccess()
        {
            var test = 10;
            CreateSuccess(10).IfSuccess(value => test += value);
            test.Should().Be(20);
        }

        [Fact]
        public void Equals_ShouldReturnTrue_GivenBothAreNone() =>
            Maybe<int>.None.Equals(Maybe<int>.None).Should().BeTrue();

        [Fact]
        public void Equals_ShouldReturnTrue_GivenBothAreSuccessWithSameValue() =>
            CreateSuccess(10).Equals(CreateSuccess(10)).Should().BeTrue();

        [Fact]
        public void Equals_ShouldReturnFalse_GivenOneIsFailureAndOtherIsSuccess() =>
            CreateSuccess(10).Equals(CreateFailure()).Should().BeFalse();

        [Fact]
        public void Equals_ShouldReturnFalse_GivenOneIsSuccessAndOtherIsFailure() =>
            CreateFailure().Equals(CreateSuccess(10)).Should().BeFalse();

        [Fact]
        public void Equals_ShouldReturnFalse_GivenBothAreSuccessWithDifferentValue() =>
            CreateSuccess(5).Equals(CreateSuccess(10)).Should().BeFalse();

        [Fact]
        public void GetFailureUnsafe_ShouldReturnFailure_GivenFailure() =>
            CreateFailure().GetFailureUnsafe().Should().Be(CreateResultFailure());

        [Fact]
        public void GetFailureUnsafe_ShouldThrowException_GivenSuccess()
        {
            Action act = () => CreateSuccess(5).GetFailureUnsafe();
            act.Should().Throw<UnsafeValueException>().WithMessage("State is Success.");
        }

        [Fact]
        public void GetSuccessUnsafe_ShouldThrowException_GivenFailure()
        {
            Action act = () => CreateFailure().GetSuccessUnsafe();
            act.Should().Throw<UnsafeValueException>().WithMessage("State is Failure.");
        }

        [Fact]
        public void GetSuccessUnsafe_ShouldReturn_GivenSuccess() =>
            CreateSuccess(5).GetSuccessUnsafe().Should().Be(5);

        private static Result<int> CreateSuccess(int value) => Result<int>.FromSuccess(value);

        private static Result<int> CreateFailure() =>
            Result<int>.FromFailure(CreateResultFailure());

        private static ResultFailure CreateResultFailure() => ResultFailure.FromErrorMessage("Some error");

        private static int Increment(int value) => value + 1;

        private static Task<int> IncrementAsync(int value) => Task.FromResult(value + 1);

        private static Result<int> IncrementBind(int value) => Result<int>.FromSuccess(value + 1);

        private static Task<Result<int>> IncrementBindAsync(int value) =>
            Task.FromResult(Result<int>.FromSuccess(value + 1));
    }
}