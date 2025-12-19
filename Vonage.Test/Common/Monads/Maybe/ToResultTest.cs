#region
using Vonage.Common.Failures;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Maybe;

public class ToResultTest
{
    [Fact]
    public void ToResult_ShouldReturnFailure_GivenValueIsNone() =>
        MaybeBehaviors.CreateNone<int>().ToResult().Should().BeFailure(NoneFailure.Value);

    [Fact]
    public void ToResult_ShouldReturnSuccess_GivenValueIsSome() =>
        MaybeBehaviors.CreateSome(10).ToResult().Should().BeSuccess(10);
}