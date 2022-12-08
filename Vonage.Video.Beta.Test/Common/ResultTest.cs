using FluentAssertions;
using Vonage.Video.Beta.Common;

namespace Vonage.Video.Beta.Test.Common;

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
            .Be(CreateFailure());

    [Fact]
    public void Map_ShouldReturnSuccess_GivenValueIsSuccess() =>
        CreateSuccess(5)
            .Map(Increment)
            .Should()
            .Be(CreateSuccess(6));

    [Fact]
    public void Match_ShouldReturnFailureOperation_GivenValueIsFailure() =>
        CreateFailure()
            .Match(Increment, _ => GetZero())
            .Should()
            .Be(0);

    [Fact]
    public void Match_ShouldReturnSuccessOperation_GivenValueIsSuccess() =>
        CreateSuccess(5)
            .Match(Increment, _ => GetZero())
            .Should()
            .Be(6);

    private static Result<int> CreateSuccess(int value) => Result<int>.FromSuccess(value);

    private static Result<int> CreateFailure() => Result<int>.FromFailure(new ResultFailure("Some error"));

    private static int Increment(int value) => value + 1;

    private static int GetZero() => 0;
}