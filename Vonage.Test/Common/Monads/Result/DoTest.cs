#region
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Result;

public class DoTest
{
    [Fact]
    public void Do_ShouldExecuteSuccessAction_GivenStateIsSuccess()
    {
        var value = 0;
        TestBehaviors.CreateSuccess(5).Do(success => value += success, _ => { });
        value.Should().Be(5);
    }

    [Fact]
    public void Do_ShouldExecuteFailureAction_GivenStateIsFailure()
    {
        var value = 0;
        TestBehaviors.CreateFailure<int>().Do(_ => { }, _ => value = 10);
        value.Should().Be(10);
    }

    [Fact]
    public void Do_ShouldReturnInstance() =>
        TestBehaviors.CreateSuccess(5).Do(_ => { }, _ => { }).Should().BeSuccess(5);

    [Fact]
    public void DoWhenSuccess_ShouldExecuteAction_GivenStateIsSuccess()
    {
        var value = 0;
        TestBehaviors.CreateSuccess(5).DoWhenSuccess(success => value += success);
        value.Should().Be(5);
    }

    [Fact]
    public void DoWhenSuccess_ShouldNotExecuteAction_GivenStateIsFailure()
    {
        var value = 0;
        TestBehaviors.CreateFailure<int>().DoWhenSuccess(_ => value = 10);
        value.Should().Be(0);
    }

    [Fact]
    public void DoWhenSuccess_ShouldReturnInstance() =>
        TestBehaviors.CreateSuccess(5).DoWhenSuccess(_ => { }).Should().BeSuccess(5);

    [Fact]
    public void DoWhenFailure_ShouldExecuteAction_GivenStateIsFailure()
    {
        var value = 0;
        TestBehaviors.CreateFailure<int>().DoWhenFailure(_ => value = 10);
        value.Should().Be(10);
    }

    [Fact]
    public void DoWhenFailure_ShouldNotExecuteAction_GivenStateIsSuccess()
    {
        var value = 0;
        TestBehaviors.CreateSuccess(5).DoWhenFailure(_ => value = 10);
        value.Should().Be(0);
    }

    [Fact]
    public void DoWhenFailure_ShouldReturnInstance() =>
        TestBehaviors.CreateSuccess(5).DoWhenFailure(_ => { }).Should().BeSuccess(5);

    [Fact]
    public async Task DoExtension_ShouldExecuteFailureAction_GivenStateIsFailure()
    {
        var value = 0;
        await TestBehaviors.CreateFailureAsync<int>().Do(_ => { }, _ => value = 10);
        value.Should().Be(10);
    }

    [Fact]
    public async Task DoExtension_ShouldExecuteSuccessAction_GivenStateIsSuccess()
    {
        var value = 0;
        await TestBehaviors.CreateSuccessAsync(5).Do(success => value += success, _ => { });
        value.Should().Be(5);
    }

    [Fact]
    public async Task DoExtension_ShouldReturnInstance() =>
        await TestBehaviors.CreateSuccessAsync(5).Do(_ => { }, _ => { }).Should().BeSuccessAsync(5);

    [Fact]
    public async Task DoWhenFailureExtension_ShouldExecuteAction_GivenStateIsFailure()
    {
        var value = 0;
        await TestBehaviors.CreateFailureAsync<int>().DoWhenFailure(_ => value = 10);
        value.Should().Be(10);
    }

    [Fact]
    public async Task DoWhenFailureExtension_ShouldNotExecuteAction_GivenStateIsSuccess()
    {
        var value = 0;
        await TestBehaviors.CreateSuccessAsync(5).DoWhenFailure(_ => value = 10);
        value.Should().Be(0);
    }

    [Fact]
    public async Task DoWhenFailureExtension_ShouldReturnInstance() =>
        await TestBehaviors.CreateSuccessAsync(5).DoWhenFailure(_ => { }).Should().BeSuccessAsync(5);

    [Fact]
    public async Task DoWhenSuccessExtension_ShouldExecuteAction_GivenStateIsSuccess()
    {
        var value = 0;
        await TestBehaviors.CreateSuccessAsync(5).DoWhenSuccess(success => value += success);
        value.Should().Be(5);
    }

    [Fact]
    public async Task DoWhenSuccessExtension_ShouldNotExecuteAction_GivenStateIsFailure()
    {
        var value = 0;
        await TestBehaviors.CreateFailureAsync<int>().DoWhenSuccess(_ => value = 10);
        value.Should().Be(0);
    }

    [Fact]
    public async Task DoWhenSuccessExtension_ShouldReturnInstance() =>
        await TestBehaviors.CreateSuccessAsync(5).DoWhenSuccess(_ => { }).Should().BeSuccessAsync(5);
}