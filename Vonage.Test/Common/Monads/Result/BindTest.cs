#region
using System;
using System.Threading.Tasks;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Result;

public class BindTest
{
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
    public async Task BindExtension_ShouldReturnFailure_GivenValueIsFailure()
    {
        var result = await TestBehaviors.CreateFailureAsync<int>().Bind(TestBehaviors.IncrementBind);
        result.Should().BeFailure(TestBehaviors.CreateResultFailure());
    }

    [Fact]
    public async Task BindExtension_ShouldReturnSuccess_GivenValueIsSuccess()
    {
        var result = await TestBehaviors.CreateSuccessAsync(10).Bind(TestBehaviors.IncrementBind);
        result.Should().BeSuccess(11);
    }

    [Fact]
    public async Task BindAsyncExtension_ShouldReturnFailure_GivenValueIsFailure()
    {
        var result = await TestBehaviors.CreateFailureAsync<int>().BindAsync(TestBehaviors.IncrementBindAsync);
        result.Should().BeFailure(TestBehaviors.CreateResultFailure());
    }

    [Fact]
    public async Task BindAsyncExtension_ShouldReturnSuccess_GivenValueIsSuccess()
    {
        var result = await TestBehaviors.CreateSuccessAsync(10).BindAsync(TestBehaviors.IncrementBindAsync);
        result.Should().BeSuccess(11);
    }
}