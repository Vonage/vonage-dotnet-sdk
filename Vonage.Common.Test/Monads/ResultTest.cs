using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Common.Monads.Exceptions;
using Vonage.Common.Test.Extensions;

namespace Vonage.Common.Test.Monads
{
    public class ResultTest
    {
        [Fact]
        public void Bind_ShouldReturnFailure_GivenValueIsFailure() =>
            CreateFailure()
                .Bind(IncrementBind)
                .Should()
                .BeFailure(CreateResultFailure());

        [Fact]
        public void Bind_ShouldReturnSuccess_GivenValueIsSuccess() =>
            CreateSuccess(5)
                .Bind(IncrementBind)
                .Should()
                .BeSuccess(6);

        [Fact]
        public async Task BindAsync_ShouldReturnFailure_GivenValueBecomesFailure() =>
            (await CreateSuccess(5)
                .BindAsync(IncrementBindAsync)
                .BindAsync(IncrementBindAsync)
                .BindAsync(value => Task.FromResult(CreateFailure()))
                .BindAsync(IncrementBindAsync)
                .BindAsync(IncrementBindAsync))
            .Should()
            .BeFailure(CreateResultFailure());

        [Fact]
        public async Task BindAsync_ShouldReturnFailure_GivenValueIsChainedFailure() =>
            (await CreateFailure()
                .BindAsync(IncrementBindAsync)
                .Bind(IncrementBind)
                .BindAsync(IncrementBindAsync)
                .BindAsync(IncrementBindAsync)
                .BindAsync(IncrementBindAsync))
            .Should()
            .BeFailure(CreateResultFailure());

        [Fact]
        public async Task BindAsync_ShouldReturnFailure_GivenValueIsFailure() =>
            (await CreateFailure()
                .BindAsync(IncrementBindAsync))
            .Should()
            .BeFailure(CreateResultFailure());

        [Fact]
        public async Task BindAsync_ShouldReturnSuccess_GivenValueIsChainedSuccess() =>
            (await CreateSuccess(5)
                .BindAsync(IncrementBindAsync)
                .Bind(IncrementBind)
                .BindAsync(IncrementBindAsync)
                .BindAsync(IncrementBindAsync)
                .BindAsync(IncrementBindAsync))
            .Should()
            .BeSuccess(10);

        [Fact]
        public async Task BindAsync_ShouldReturnSuccess_GivenValueIsSuccess() =>
            (await CreateSuccess(5)
                .BindAsync(IncrementBindAsync))
            .Should()
            .BeSuccess(6);

        [Fact]
        public void Equals_ShouldReturnFalse_GivenBothAreSuccessWithDifferentValue() =>
            CreateSuccess(5).Equals(CreateSuccess(10)).Should().BeFalse();

        [Fact]
        public void Equals_ShouldReturnFalse_GivenOneIsFailureAndOtherIsSuccess() =>
            CreateSuccess(10).Equals(CreateFailure()).Should().BeFalse();

        [Fact]
        public void Equals_ShouldReturnFalse_GivenOneIsSuccessAndOtherIsFailure() =>
            CreateFailure().Equals(CreateSuccess(10)).Should().BeFalse();

        [Fact]
        public void Equals_ShouldReturnTrue_GivenBothAreNone() =>
            Maybe<int>.None.Equals(Maybe<int>.None).Should().BeTrue();

        [Fact]
        public void Equals_ShouldReturnTrue_GivenBothAreSuccessWithSameValue() =>
            CreateSuccess(10).Equals(CreateSuccess(10)).Should().BeTrue();

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
        public void GetFailureUnsafe_ShouldReturnFailure_GivenFailure() =>
            CreateFailure().GetFailureUnsafe().Should().Be(CreateResultFailure());

        [Fact]
        public void GetFailureUnsafe_ShouldThrowException_GivenSuccess()
        {
            Action act = () => CreateSuccess(5).GetFailureUnsafe();
            act.Should().Throw<SuccessStateException<int>>().WithMessage("State is Success.")
                .Which.Success.Should().Be(5);
        }

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
        public void GetSuccessUnsafe_ShouldReturn_GivenSuccess() =>
            CreateSuccess(5).GetSuccessUnsafe().Should().Be(5);

        [Fact]
        public void GetSuccessUnsafe_ShouldThrowException_GivenFailure()
        {
            Action act = () => CreateFailure().GetSuccessUnsafe();
            act.Should().Throw<FailureStateException>()
                .WithMessage("State is Failure.")
                .Which.Failure.Should().Be(CreateResultFailure());
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
        public void IfFailure_ShouldReturnDefaultValue_GivenValueIsFailure() =>
            CreateFailure().IfFailure(10).Should().Be(10);

        [Fact]
        public void IfFailure_ShouldReturnFailureOperation_GivenValueIsFailure() =>
            CreateFailure().IfFailure(_ => 0).Should().Be(0);

        [Fact]
        public void IfFailure_ShouldReturnSuccess_GivenValueIsSuccess() =>
            CreateSuccess(50).IfFailure(0).Should().Be(50);

        [Fact]
        public void IfFailure_ShouldReturnSuccessOperation_GivenValueIsSuccess2() =>
            CreateSuccess(50).IfFailure(_ => 0).Should().Be(50);

        [Fact]
        public void IfSuccess_ShouldBeExecuted_GivenValueIsSuccess()
        {
            var test = 10;
            CreateSuccess(10).IfSuccess(value => test += value);
            test.Should().Be(20);
        }

        [Fact]
        public void IfSuccess_ShouldNotBeExecuted_GivenValueIsFailure()
        {
            var test = 10;
            CreateFailure().IfSuccess(_ => test = 0);
            test.Should().Be(10);
        }

        [Fact]
        public void IfSuccess_ShouldReturnResult_GivenValueIsFailure() =>
            CreateFailure()
                .IfSuccess(_ => { })
                .Should()
                .BeFailure(CreateResultFailure());

        [Fact]
        public void IfSuccess_ShouldReturnResult_GivenValueIsSuccess() =>
            CreateSuccess(10)
                .IfSuccess(_ => { })
                .Should()
                .BeSuccess(10);

        [Fact]
        public async Task IfSuccessAsync_ShouldBeExecuted_GivenValueIsChainedSuccess()
        {
            var test = 10;
            await CreateSuccess(0)
                .IfSuccessAsync(_ =>
                {
                    test++;
                    return Task.CompletedTask;
                })
                .IfSuccessAsync(_ =>
                {
                    test++;
                    return Task.CompletedTask;
                })
                .IfSuccessAsync(_ =>
                {
                    test++;
                    return Task.CompletedTask;
                })
                .IfSuccessAsync(_ =>
                {
                    test++;
                    return Task.CompletedTask;
                })
                .IfSuccessAsync(_ =>
                {
                    test++;
                    return Task.CompletedTask;
                });
            test.Should().Be(15);
        }

        [Fact]
        public async Task IfSuccessAsync_ShouldBeExecuted_GivenValueIsSuccess()
        {
            var test = 10;
            await CreateSuccess(10).IfSuccessAsync(value =>
            {
                test += value;
                return Task.CompletedTask;
            });
            test.Should().Be(20);
        }

        [Fact]
        public async Task IfSuccessAsync_ShouldNotBeExecuted_GivenValueIsChainedFailure()
        {
            var test = 10;
            await CreateFailure()
                .IfSuccessAsync(_ =>
                {
                    test++;
                    return Task.CompletedTask;
                })
                .IfSuccessAsync(_ =>
                {
                    test++;
                    return Task.CompletedTask;
                })
                .IfSuccessAsync(_ =>
                {
                    test++;
                    return Task.CompletedTask;
                })
                .IfSuccessAsync(_ =>
                {
                    test++;
                    return Task.CompletedTask;
                });
            test.Should().Be(10);
        }

        [Fact]
        public async Task IfSuccessAsync_ShouldNotBeExecuted_GivenValueIsFailure()
        {
            var test = 10;
            await CreateFailure().IfSuccessAsync(value =>
            {
                test += value;
                return Task.CompletedTask;
            });
            test.Should().Be(10);
        }

        [Fact]
        public async Task IfSuccessAsync_ShouldReturnFailure_GivenValueIsChainedFailure() =>
            (await CreateFailure()
                .IfSuccessAsync(_ => Task.CompletedTask)
                .IfSuccessAsync(_ => Task.CompletedTask)
                .IfSuccessAsync(_ => Task.CompletedTask)
                .IfSuccessAsync(_ => Task.CompletedTask)
                .IfSuccessAsync(_ => Task.CompletedTask))
            .Should().BeFailure(CreateResultFailure());

        [Fact]
        public async Task IfSuccessAsync_ShouldReturnResult_GivenValueIsFailure() =>
            (await CreateFailure().IfSuccessAsync(_ => Task.CompletedTask)).Should().BeFailure(CreateResultFailure());

        [Fact]
        public async Task IfSuccessAsync_ShouldReturnResult_GivenValueIsSuccess() =>
            (await CreateSuccess(10).IfSuccessAsync(_ => Task.CompletedTask)).Should().BeSuccess(10);

        [Fact]
        public async Task IfSuccessAsync_ShouldReturnSuccess_GivenValueIsChainedSuccess() =>
            (await CreateSuccess(5)
                .IfSuccessAsync(_ => Task.CompletedTask)
                .IfSuccessAsync(_ => Task.CompletedTask)
                .IfSuccessAsync(_ => Task.CompletedTask)
                .IfSuccessAsync(_ => Task.CompletedTask)
                .IfSuccessAsync(_ => Task.CompletedTask))
            .Should().BeSuccess(5);

        [Fact]
        public void ImplicitOperator_ShouldConvertToSuccess_GivenValueIsSuccess()
        {
            const int value = 55;
            Result<int> result = value;
            result.Should().BeSuccess(value);
        }

        [Fact]
        public void Map_ShouldReturnFailure_GivenValueIsFailure() =>
            CreateFailure()
                .Map(Increment)
                .Should()
                .BeFailure(CreateResultFailure());

        [Fact]
        public void Map_ShouldReturnSuccess_GivenValueIsSuccess() =>
            CreateSuccess(5)
                .Map(Increment)
                .Should()
                .BeSuccess(6);

        [Fact]
        public async Task MapAsync_ShouldReturnFailure_GivenValueIsChainedFailure() =>
            (await CreateFailure()
                .MapAsync(IncrementAsync)
                .Map(Increment)
                .MapAsync(IncrementAsync)
                .MapAsync(IncrementAsync)
                .MapAsync(IncrementAsync))
            .Should()
            .BeFailure(CreateResultFailure());

        [Fact]
        public async Task MapAsync_ShouldReturnFailure_GivenValueIsFailure() =>
            (await CreateFailure()
                .MapAsync(IncrementAsync))
            .Should()
            .BeFailure(CreateResultFailure());

        [Fact]
        public async Task MapAsync_ShouldReturnSuccess_GivenValueIsChainedSuccess() =>
            (await CreateSuccess(5)
                .MapAsync(IncrementAsync)
                .Map(Increment)
                .MapAsync(IncrementAsync)
                .MapAsync(IncrementAsync)
                .MapAsync(IncrementAsync))
            .Should().BeSuccess(10);

        [Fact]
        public async Task MapAsync_ShouldReturnSuccess_GivenValueIsSuccess() =>
            (await CreateSuccess(5)
                .MapAsync(IncrementAsync))
            .Should()
            .BeSuccess(6);

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

        private static Result<int> CreateFailure() =>
            Result<int>.FromFailure(CreateResultFailure());

        private static ResultFailure CreateResultFailure() => ResultFailure.FromErrorMessage("Some error");

        private static Result<int> CreateSuccess(int value) => Result<int>.FromSuccess(value);

        private static int Increment(int value) => value + 1;

        private static Task<int> IncrementAsync(int value) => Task.FromResult(value + 1);

        private static Result<int> IncrementBind(int value) => Result<int>.FromSuccess(value + 1);

        private static Task<Result<int>> IncrementBindAsync(int value) =>
            Task.FromResult(Result<int>.FromSuccess(value + 1));
    }
}