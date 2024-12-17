#region
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads;

public class ResultExtensionsTest
{
    private int value;

    [Fact]
    public async Task BindAsync_ShouldReturnFailure_GivenValueIsFailure()
    {
        var result = await TestBehaviors.CreateFailureAsync<int>().BindAsync(TestBehaviors.IncrementBindAsync);
        result.Should().BeFailure(TestBehaviors.CreateResultFailure());
    }

    [Fact]
    public async Task BindAsync_ShouldReturnSuccess_GivenValueIsSuccess()
    {
        var result = await TestBehaviors.CreateSuccessAsync(10).BindAsync(TestBehaviors.IncrementBindAsync);
        result.Should().BeSuccess(11);
    }

    [Fact]
    public async Task Bind_ShouldReturnFailure_GivenValueIsFailure()
    {
        var result = await TestBehaviors.CreateFailureAsync<int>().Bind(TestBehaviors.IncrementBind);
        result.Should().BeFailure(TestBehaviors.CreateResultFailure());
    }

    [Fact]
    public async Task Bind_ShouldReturnSuccess_GivenValueIsSuccess()
    {
        var result = await TestBehaviors.CreateSuccessAsync(10).Bind(TestBehaviors.IncrementBind);
        result.Should().BeSuccess(11);
    }

    [Fact]
    public async Task Map_ShouldReturnFailure_GivenValueIsFailure()
    {
        var result = await TestBehaviors.CreateFailureAsync<int>().Map(TestBehaviors.Increment);
        result.Should().BeFailure(TestBehaviors.CreateResultFailure());
    }

    [Fact]
    public async Task Map_ShouldReturnSuccess_GivenValueIsSuccess()
    {
        var result = await TestBehaviors.CreateSuccessAsync(10).Map(TestBehaviors.Increment);
        result.Should().BeSuccess(11);
    }

    [Fact]
    public async Task MapAsync_ShouldReturnFailure_GivenValueIsFailure()
    {
        var result = await TestBehaviors.CreateFailureAsync<int>().MapAsync(TestBehaviors.IncrementAsync);
        result.Should().BeFailure(TestBehaviors.CreateResultFailure());
    }

    [Fact]
    public async Task MapAsync_ShouldReturnSuccess_GivenValueIsSuccess()
    {
        var result = await TestBehaviors.CreateSuccessAsync(10).MapAsync(TestBehaviors.IncrementAsync);
        result.Should().BeSuccess(11);
    }

    [Fact]
    public async Task IfSuccessAsync_ShouldExecuteOperation_GivenSuccess()
    {
        await TestBehaviors.CreateSuccessAsync(10).IfSuccessAsync(this.SuccessAction);
        this.value.Should().Be(10);
    }

    [Fact]
    public async Task IfSuccessAsync_ShouldReturnSuccess_GivenSuccess()
    {
        var result = await TestBehaviors.CreateSuccessAsync(10).IfSuccessAsync(this.SuccessAction);
        result.Should().BeSuccess(10);
    }

    [Fact]
    public async Task IfSuccessAsync_ShouldNotExecuteOperation_GivenFailure()
    {
        await TestBehaviors.CreateFailureAsync<int>().IfSuccessAsync(this.SuccessAction);
        this.value.Should().Be(default);
    }

    [Fact]
    public async Task IfSuccessAsync_ShouldReturnFailure_GivenFailure()
    {
        var result = await TestBehaviors.CreateFailureAsync<int>().IfSuccessAsync(this.SuccessAction);
        result.Should().BeFailure(TestBehaviors.CreateResultFailure());
    }

    [Fact]
    public async Task IfFailure_ShouldReturnFallbackValue_GivenFailure()
    {
        var result = await TestBehaviors.CreateFailureAsync<int>().IfFailure(50);
        result.Should().Be(50);
    }

    [Fact]
    public async Task IfFailure_ShouldReturnSuccess_GivenSuccess()
    {
        var result = await TestBehaviors.CreateSuccessAsync(10).IfFailure(50);
        result.Should().Be(10);
    }

    private Task SuccessAction(int input)
    {
        this.value = input;
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Match_ShouldReturnFallback_GivenFailure()
    {
        var result = await TestBehaviors.CreateFailureAsync<int>().Match(_ => "some", _ => "none");
        result.Should().Be("none");
    }

    [Fact]
    public async Task Match_ShouldReturnValue_GivenSuccess()
    {
        var result = await TestBehaviors.CreateSuccessAsync(10).Match(_ => "some", _ => "none");
        result.Should().Be("some");
    }

    [Fact]
    public async Task Do_ShouldExecuteSuccessAction_GivenStateIsSuccess()
    {
        var value = 0;
        await TestBehaviors.CreateSuccessAsync(5).Do(success => value += success, _ => { });
        value.Should().Be(5);
    }

    [Fact]
    public async Task Do_ShouldExecuteFailureAction_GivenStateIsFailure()
    {
        var value = 0;
        await TestBehaviors.CreateFailureAsync<int>().Do(_ => { }, _ => value = 10);
        value.Should().Be(10);
    }

    [Fact]
    public async Task Do_ShouldReturnInstance() =>
        await TestBehaviors.CreateSuccessAsync(5).Do(_ => { }, _ => { }).Should().BeSuccessAsync(5);

    [Fact]
    public async Task DoWhenSuccess_ShouldExecuteAction_GivenStateIsSuccess()
    {
        var value = 0;
        await TestBehaviors.CreateSuccessAsync(5).DoWhenSuccess(success => value += success);
        value.Should().Be(5);
    }

    [Fact]
    public async Task DoWhenSuccess_ShouldNotExecuteAction_GivenStateIsFailure()
    {
        var value = 0;
        await TestBehaviors.CreateFailureAsync<int>().DoWhenSuccess(_ => value = 10);
        value.Should().Be(0);
    }

    [Fact]
    public async Task DoWhenSuccess_ShouldReturnInstance() =>
        await TestBehaviors.CreateSuccessAsync(5).DoWhenSuccess(_ => { }).Should().BeSuccessAsync(5);

    [Fact]
    public async Task DoWhenFailure_ShouldExecuteAction_GivenStateIsFailure()
    {
        var value = 0;
        await TestBehaviors.CreateFailureAsync<int>().DoWhenFailure(_ => value = 10);
        value.Should().Be(10);
    }

    [Fact]
    public async Task DoWhenFailure_ShouldNotExecuteAction_GivenStateIsSuccess()
    {
        var value = 0;
        await TestBehaviors.CreateSuccessAsync(5).DoWhenFailure(_ => value = 10);
        value.Should().Be(0);
    }

    [Fact]
    public async Task DoWhenFailure_ShouldReturnInstance() =>
        await TestBehaviors.CreateSuccessAsync(5).DoWhenFailure(_ => { }).Should().BeSuccessAsync(5);
}