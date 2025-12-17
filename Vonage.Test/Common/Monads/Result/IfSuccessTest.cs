#region
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Result;

public class IfSuccessTest
{
    private int value;

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
    public async Task IfSuccessExtension_ShouldExecuteOperation_GivenSuccess()
    {
        await TestBehaviors.CreateSuccessAsync(10).IfSuccess(this.SuccessAction);
        this.value.Should().Be(10);
    }

    [Fact]
    public async Task IfSuccessExtension_ShouldNotExecuteOperation_GivenFailure()
    {
        await TestBehaviors.CreateFailureAsync<int>().IfSuccess(this.SuccessAction);
        this.value.Should().Be(default);
    }

    [Fact]
    public async Task IfSuccessExtension_ShouldReturnFailure_GivenFailure()
    {
        var result = await TestBehaviors.CreateFailureAsync<int>().IfSuccess(this.SuccessAction);
        result.Should().BeFailure(TestBehaviors.CreateResultFailure());
    }

    [Fact]
    public async Task IfSuccessExtension_ShouldReturnSuccess_GivenSuccess()
    {
        var result = await TestBehaviors.CreateSuccessAsync(10).IfSuccess(this.SuccessAction);
        result.Should().BeSuccess(10);
    }

    [Fact]
    public async Task IfSuccessAsyncExtension_ShouldExecuteOperation_GivenSuccess()
    {
        await TestBehaviors.CreateSuccessAsync(10).IfSuccessAsync(this.SuccessActionAsync);
        this.value.Should().Be(10);
    }

    [Fact]
    public async Task IfSuccessAsyncExtension_ShouldNotExecuteOperation_GivenFailure()
    {
        await TestBehaviors.CreateFailureAsync<int>().IfSuccessAsync(this.SuccessActionAsync);
        this.value.Should().Be(default);
    }

    [Fact]
    public async Task IfSuccessAsyncExtension_ShouldReturnFailure_GivenFailure()
    {
        var result = await TestBehaviors.CreateFailureAsync<int>().IfSuccessAsync(this.SuccessActionAsync);
        result.Should().BeFailure(TestBehaviors.CreateResultFailure());
    }

    [Fact]
    public async Task IfSuccessAsyncExtension_ShouldReturnSuccess_GivenSuccess()
    {
        var result = await TestBehaviors.CreateSuccessAsync(10).IfSuccessAsync(this.SuccessActionAsync);
        result.Should().BeSuccess(10);
    }

    private void SuccessAction(int input)
    {
        this.value = input;
    }

    private Task SuccessActionAsync(int input)
    {
        this.value = input;
        return Task.CompletedTask;
    }
}