using System;
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Common.Monads;

[Trait("Category", "Unit")]
public class ResultTest
{
    [Fact]
    public void BiMap_ShouldReturnFailure_GivenOperationThrowsException()
    {
        var expectedException = new Exception("Error");
        TestBehaviors.CreateSuccess(5)
            .BiMap(value =>
            {
                throw expectedException;
                return value;
            }, _ => _)
            .Should()
            .BeFailure(SystemFailure.FromException(expectedException));
    }

    [Fact]
    public void BiMap_ShouldReturnFailure_GivenValueIsFailure() =>
        TestBehaviors.CreateFailure<int>()
            .BiMap(TestBehaviors.Increment, f => ResultFailure.FromErrorMessage("New Failure"))
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("New Failure"));

    [Fact]
    public void BiMap_ShouldReturnSuccess_GivenValueIsSuccess() =>
        TestBehaviors.CreateSuccess(5)
            .BiMap(TestBehaviors.Increment, _ => _)
            .Should()
            .BeSuccess(6);

    [Fact]
    public void Bind_ShouldReturnFailure_GivenOperationThrowsException()
    {
        var expectedException = new Exception("Error");
        TestBehaviors.CreateSuccess(5)
            .Bind(value =>
            {
                throw expectedException;
                return Result<int>.FromSuccess(value);
            })
            .Should()
            .BeFailure(SystemFailure.FromException(expectedException));
    }

    [Fact]
    public void Bind_ShouldReturnFailure_GivenValueIsFailure() =>
        TestBehaviors.CreateFailure<int>()
            .Bind(TestBehaviors.IncrementBind)
            .Should()
            .BeFailure(TestBehaviors.CreateResultFailure());

    [Fact]
    public void Bind_ShouldReturnSuccess_GivenValueIsSuccess() =>
        TestBehaviors.CreateSuccess(5)
            .Bind(TestBehaviors.IncrementBind)
            .Should()
            .BeSuccess(6);

    [Fact]
    public async Task BindAsync_ShouldReturnFailure_GivenOperationThrowsException()
    {
        var expectedException = new Exception("Error");
        (await TestBehaviors.CreateSuccess(5)
                .BindAsync(value =>
                {
                    throw expectedException;
                    return Task.FromResult(Result<int>.FromSuccess(value));
                }))
            .Should()
            .BeFailure(SystemFailure.FromException(expectedException));
    }

    [Fact]
    public async Task BindAsync_ShouldReturnFailure_GivenValueIsFailure() =>
        (await TestBehaviors.CreateFailure<int>().BindAsync(TestBehaviors.IncrementBindAsync))
        .Should()
        .BeFailure(TestBehaviors.CreateResultFailure());

    [Fact]
    public async Task BindAsync_ShouldReturnSuccess_GivenValueIsSuccess() =>
        (await TestBehaviors.CreateSuccess(5).BindAsync(TestBehaviors.IncrementBindAsync))
        .Should()
        .BeSuccess(6);

    [Fact]
    public void Equals_ShouldReturnFalse_GivenBothAreSuccessWithDifferentValue() =>
        TestBehaviors.CreateSuccess(5).Equals(TestBehaviors.CreateSuccess(10)).Should().BeFalse();

    [Fact]
    public void Equals_ShouldReturnFalse_GivenOneIsFailureAndOtherIsSuccess() =>
        TestBehaviors.CreateSuccess(10).Equals(TestBehaviors.CreateFailure<int>()).Should().BeFalse();

    [Fact]
    public void Equals_ShouldReturnFalse_GivenOneIsSuccessAndOtherIsFailure() =>
        TestBehaviors.CreateFailure<int>().Equals(TestBehaviors.CreateSuccess(10)).Should().BeFalse();

    [Fact]
    public void Equals_ShouldReturnTrue_GivenBothAreNone() =>
        Maybe<int>.None.Equals(Maybe<int>.None).Should().BeTrue();

    [Fact]
    public void Equals_ShouldReturnTrue_GivenBothAreSuccessWithSameValue() =>
        TestBehaviors.CreateSuccess(10).Equals(TestBehaviors.CreateSuccess(10)).Should().BeTrue();

    [Fact]
    public void Failure_ShouldHaveFailureState()
    {
        var result = TestBehaviors.CreateFailure<int>();
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Success_ShouldHaveSuccessState()
    {
        var result = TestBehaviors.CreateSuccess(0);
        result.IsFailure.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void GetFailureUnsafe_ShouldReturn_GivenFailure() =>
        TestBehaviors.CreateFailure<int>().GetFailureUnsafe().Should().Be(TestBehaviors.CreateResultFailure());

    [Fact]
    public void GetFailureUnsafe_ShouldThrowResultException_GivenFailure()
    {
        Action act = () => TestBehaviors.CreateSuccess(5).GetFailureUnsafe();
        act.Should().Throw<InvalidOperationException>().Which.Message.Should()
            .Be("Result is not in Failure state.");
    }

    [Fact]
    public void GetHashCode_ShouldReturnValue_GivenFailure()
    {
        const int value = 35;
        TestBehaviors.CreateSuccess(value).GetHashCode().Should().Be(value.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ShouldReturnValue_GivenSuccess()
    {
        var failure = TestBehaviors.CreateResultFailure();
        Result<int>.FromFailure(failure).GetHashCode().Should().Be(failure.GetHashCode());
    }

    [Fact]
    public void GetSuccessUnsafe_ShouldReturn_GivenSuccess() =>
        TestBehaviors.CreateSuccess(5).GetSuccessUnsafe().Should().Be(5);

    [Fact]
    public void GetSuccessUnsafe_ShouldThrowResultException_GivenFailure()
    {
        var expectedException = TestBehaviors.CreateResultFailure().ToException();
        Action act = () => TestBehaviors.CreateFailure<int>().GetSuccessUnsafe();
        var exception = act.Should().Throw<Exception>().Which;
        exception.Should().BeOfType(expectedException.GetType());
        exception.Message.Should().Be(expectedException.Message);
    }

    [Fact]
    public void IfFailure_ShouldBeExecuted_GivenValueIsFailure()
    {
        var test = 10;
        TestBehaviors.CreateFailure<int>().IfFailure(_ => test = 0);
        test.Should().Be(0);
    }

    [Fact]
    public void IfFailure_ShouldNotBeExecuted_GivenValueIsSuccess()
    {
        var test = 10;
        TestBehaviors.CreateSuccess(50).IfFailure(_ => test = 0);
        test.Should().Be(10);
    }

    [Fact]
    public void IfFailure_ShouldReturnDefaultValue_GivenValueIsFailure() =>
        TestBehaviors.CreateFailure<int>().IfFailure(10).Should().Be(10);

    [Fact]
    public void IfFailure_ShouldReturnFailureOperation_GivenValueIsFailure() =>
        TestBehaviors.CreateFailure<int>().IfFailure(_ => 0).Should().Be(0);

    [Fact]
    public void IfFailure_ShouldReturnSuccess_GivenValueIsSuccess() =>
        TestBehaviors.CreateSuccess(50).IfFailure(0).Should().Be(50);

    [Fact]
    public void IfFailure_ShouldReturnSuccessOperation_GivenValueIsSuccess2() =>
        TestBehaviors.CreateSuccess(50).IfFailure(_ => 0).Should().Be(50);

    [Fact]
    public void IfSuccess_ShouldBeExecuted_GivenValueIsSuccess()
    {
        var test = 10;
        TestBehaviors.CreateSuccess(10).IfSuccess(value => test += value);
        test.Should().Be(20);
    }

    [Fact]
    public void IfSuccess_ShouldNotBeExecuted_GivenValueIsFailure()
    {
        var test = 10;
        TestBehaviors.CreateFailure<int>().IfSuccess(_ => test = 0);
        test.Should().Be(10);
    }

    [Fact]
    public void IfSuccess_ShouldReturnResult_GivenValueIsFailure() =>
        TestBehaviors.CreateFailure<int>()
            .IfSuccess(_ => { })
            .Should()
            .BeFailure(TestBehaviors.CreateResultFailure());

    [Fact]
    public void IfSuccess_ShouldReturnResult_GivenValueIsSuccess() =>
        TestBehaviors.CreateSuccess(10)
            .IfSuccess(_ => { })
            .Should()
            .BeSuccess(10);

    [Fact]
    public async Task IfSuccessAsync_ShouldBeExecuted_GivenValueIsSuccess()
    {
        var test = 10;
        await TestBehaviors.CreateSuccess(10).IfSuccessAsync(value =>
        {
            test += value;
            return Task.CompletedTask;
        });
        test.Should().Be(20);
    }

    [Fact]
    public async Task IfSuccessAsync_ShouldNotBeExecuted_GivenValueIsFailure()
    {
        var test = 10;
        await TestBehaviors.CreateFailure<int>().IfSuccessAsync(value =>
        {
            test += value;
            return Task.CompletedTask;
        });
        test.Should().Be(10);
    }

    [Fact]
    public async Task IfSuccessAsync_ShouldReturnResult_GivenValueIsFailure() =>
        (await TestBehaviors.CreateFailure<int>().IfSuccessAsync(_ => Task.CompletedTask)).Should()
        .BeFailure(TestBehaviors.CreateResultFailure());

    [Fact]
    public async Task IfSuccessAsync_ShouldReturnResult_GivenValueIsSuccess() =>
        (await TestBehaviors.CreateSuccess(10).IfSuccessAsync(_ => Task.CompletedTask)).Should().BeSuccess(10);

    [Fact]
    public void ImplicitOperator_ShouldConvertToSuccess_GivenValueIsSuccess()
    {
        const int value = 55;
        Result<int> result = value;
        result.Should().BeSuccess(value);
    }

    [Fact]
    public void Map_ShouldReturnFailure_GivenOperationThrowsException()
    {
        var expectedException = new Exception("Error");
        TestBehaviors.CreateSuccess(5)
            .Map(value =>
            {
                throw expectedException;
                return value;
            })
            .Should()
            .BeFailure(SystemFailure.FromException(expectedException));
    }

    [Fact]
    public void Map_ShouldReturnFailure_GivenValueIsFailure() =>
        TestBehaviors.CreateFailure<int>()
            .Map(TestBehaviors.Increment)
            .Should()
            .BeFailure(TestBehaviors.CreateResultFailure());

    [Fact]
    public void Map_ShouldReturnSuccess_GivenValueIsSuccess() =>
        TestBehaviors.CreateSuccess(5)
            .Map(TestBehaviors.Increment)
            .Should()
            .BeSuccess(6);

    [Fact]
    public async Task MapAsync_ShouldReturnFailure_GivenOperationThrowsException()
    {
        var expectedException = new Exception("Error");
        (await TestBehaviors.CreateSuccess(5)
                .MapAsync(value =>
                {
                    throw expectedException;
                    return Task.FromResult(value);
                }))
            .Should()
            .BeFailure(SystemFailure.FromException(expectedException));
    }

    [Fact]
    public async Task MapAsync_ShouldReturnFailure_GivenValueIsFailure() =>
        (await TestBehaviors.CreateFailure<int>()
            .MapAsync(TestBehaviors.IncrementAsync))
        .Should()
        .BeFailure(TestBehaviors.CreateResultFailure());

    [Fact]
    public async Task MapAsync_ShouldReturnSuccess_GivenValueIsSuccess() =>
        (await TestBehaviors.CreateSuccess(5)
            .MapAsync(TestBehaviors.IncrementAsync))
        .Should()
        .BeSuccess(6);

    [Fact]
    public void Match_ShouldExecuteFailureOperation_GivenValueIsSuccess()
    {
        var value = 0;
        TestBehaviors.CreateFailure<int>().Match(success => value = success, _ => value = -1);
        value.Should().Be(-1);
    }

    [Fact]
    public void Match_ShouldExecuteSuccessOperation_GivenValueIsSuccess()
    {
        var value = 0;
        TestBehaviors.CreateSuccess(5).Match(success => value = success, _ => value = -1);
        value.Should().Be(5);
    }

    [Fact]
    public void Match_ShouldReturnFailureOperation_GivenValueIsFailure() =>
        TestBehaviors.CreateFailure<int>()
            .Match(value => value.ToString(), failure => failure.GetFailureMessage())
            .Should()
            .Be("Error");

    [Fact]
    public void Match_ShouldReturnSuccessOperation_GivenValueIsSuccess() =>
        TestBehaviors.CreateSuccess(5)
            .Match(TestBehaviors.Increment, _ => 0)
            .Should()
            .Be(6);

    [Fact]
    public void Merge_ShouldReturnFailure_GivenFirstMonadIsFailure() =>
        TestBehaviors.CreateFailure<int>()
            .Merge(TestBehaviors.CreateSuccess(5), (first, second) => new {First = first, Second = second})
            .Should()
            .BeFailure(TestBehaviors.CreateResultFailure());

    [Fact]
    public void Merge_ShouldReturnFailure_GivenSecondMonadIsFailure() =>
        TestBehaviors.CreateSuccess(5)
            .Merge(TestBehaviors.CreateFailure<int>(), (first, second) => new {First = first, Second = second})
            .Should()
            .BeFailure(TestBehaviors.CreateResultFailure());

    [Fact]
    public void Merge_ShouldReturnSuccess_GivenBothResultsAreSuccess() =>
        TestBehaviors.CreateSuccess(5)
            .Merge(TestBehaviors.CreateSuccess(10), (first, second) => new {First = first, Second = second})
            .Should()
            .BeSuccess(success =>
            {
                success.First.Should().Be(5);
                success.Second.Should().Be(10);
            });
}