#region
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common.Monads;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Maybe;

public class IfNoneTest
{
    [Fact]
    public void IfNone_Operation_ShouldReturnOperation_GivenValueIsNone() =>
        MaybeBehaviors.CreateNone<int>().IfNone(() => 5).Should().Be(5);

    [Fact]
    public void IfNone_Operation_ShouldReturnValue_GivenValueIsSome() =>
        MaybeBehaviors.CreateSome(10).IfNone(() => 5).Should().Be(10);

    [Fact]
    public void IfNone_Value_ShouldReturnSpecifiedValue_GivenValueIsNone() =>
        MaybeBehaviors.CreateNone<int>().IfNone(5).Should().Be(5);

    [Fact]
    public void IfNone_Value_ShouldReturnValue_GivenValueIsSome() =>
        MaybeBehaviors.CreateSome(10).IfNone(5).Should().Be(10);

    [Fact]
    public async Task IfNone_ValueExtension_ShouldReturnSpecifiedValue_GivenValueIsNone()
    {
        var result = await MaybeBehaviors.CreateNoneAsync<int>().IfNone(5);
        result.Should().Be(5);
    }

    [Fact]
    public async Task IfNone_ValueExtension_ShouldReturnValue_GivenValueIsSome()
    {
        var result = await MaybeBehaviors.CreateSomeAsync(10).IfNone(5);
        result.Should().Be(10);
    }
}