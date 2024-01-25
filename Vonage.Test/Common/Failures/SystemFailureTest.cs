using System;
using FluentAssertions;
using Vonage.Common.Failures;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.Common.Failures;

public class SystemFailureTest
{
    [Fact]
    public void FromErrorMessage_ShouldReturnFailure() =>
        SystemFailure.FromException(new InvalidOperationException("Some error")).GetFailureMessage().Should()
            .Be("Some error");

    [Fact]
    public void ToException_ShouldReturnVonageException() =>
        SystemFailure.FromException(new InvalidOperationException("Some error"))
            .ToException()
            .Should()
            .BeOfType<InvalidOperationException>()
            .Which.Message.Should().Be("Some error");

    [Fact]
    public void ToResult_ShouldReturnFailure()
    {
        var expectedException = new Exception("Test");
        SystemFailure.FromException(expectedException).ToResult<int>().Should()
            .BeFailure(SystemFailure.FromException(expectedException));
    }

    [Fact]
    public void Type_ShouldReturnResultFailure() => SystemFailure.FromException(new Exception())
        .Type
        .Should()
        .Be(typeof(SystemFailure));
}