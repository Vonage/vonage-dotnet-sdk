#region
using System;
using System.Threading.Tasks;
using Vonage.Common.Failures;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Result;

public class MapTest
{
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
    public async Task MapExtension_ShouldReturnFailure_GivenValueIsFailure()
    {
        var result = await TestBehaviors.CreateFailureAsync<int>().Map(TestBehaviors.Increment);
        result.Should().BeFailure(TestBehaviors.CreateResultFailure());
    }

    [Fact]
    public async Task MapExtension_ShouldReturnSuccess_GivenValueIsSuccess()
    {
        var result = await TestBehaviors.CreateSuccessAsync(10).Map(TestBehaviors.Increment);
        result.Should().BeSuccess(11);
    }

    [Fact]
    public async Task MapAsyncExtension_ShouldReturnFailure_GivenValueIsFailure()
    {
        var result = await TestBehaviors.CreateFailureAsync<int>().MapAsync(TestBehaviors.IncrementAsync);
        result.Should().BeFailure(TestBehaviors.CreateResultFailure());
    }

    [Fact]
    public async Task MapAsyncExtension_ShouldReturnSuccess_GivenValueIsSuccess()
    {
        var result = await TestBehaviors.CreateSuccessAsync(10).MapAsync(TestBehaviors.IncrementAsync);
        result.Should().BeSuccess(11);
    }
}