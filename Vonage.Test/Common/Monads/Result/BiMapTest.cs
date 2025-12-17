#region
using System;
using Vonage.Common.Failures;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Result;

public class BiMap
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
            .BiMap(TestBehaviors.Increment, _ => ResultFailure.FromErrorMessage("New Failure"))
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("New Failure"));

    [Fact]
    public void BiMap_ShouldReturnSuccess_GivenValueIsSuccess() =>
        TestBehaviors.CreateSuccess(5)
            .BiMap(TestBehaviors.Increment, _ => _)
            .Should()
            .BeSuccess(6);
}