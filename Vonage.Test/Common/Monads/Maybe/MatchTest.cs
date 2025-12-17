#region
using System.Threading.Tasks;
using FluentAssertions;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Maybe;

public class MatchTest
{
    [Fact]
    public void Match_ShouldReturnNoneOperation_GivenValueIsNone() =>
        MaybeBehaviors.CreateNone<int>().Match(MaybeBehaviors.MapToString, () => "Some value").Should()
            .Be("Some value");

    [Fact]
    public void Match_ShouldReturnSomeOperation_GivenValueIsSome() =>
        MaybeBehaviors.CreateSome(10).Match(MaybeBehaviors.MapToString, () => "Some value").Should().Be("10");

    [Fact]
    public void Merge_ShouldReturnNone_GivenFirstLegIsNone() =>
        MaybeBehaviors.CreateNone<int>()
            .Merge(MaybeBehaviors.CreateSome(5), (first, second) => new {First = first, Second = second})
            .Should()
            .BeNone();

    [Fact]
    public void Merge_ShouldReturnNone_GivenSecondLegIsNone() =>
        MaybeBehaviors.CreateSome(5)
            .Merge(MaybeBehaviors.CreateNone<int>(), (first, second) => new {First = first, Second = second})
            .Should()
            .BeNone();

    [Fact]
    public void Merge_ShouldReturnSome_GivenBothAreSome() =>
        MaybeBehaviors.CreateSome(5)
            .Merge(MaybeBehaviors.CreateSome(10), (first, second) => new {First = first, Second = second})
            .Should()
            .BeSome(some =>
            {
                some.First.Should().Be(5);
                some.Second.Should().Be(10);
            });

    [Fact]
    public async Task MatchExtension_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await MaybeBehaviors.CreateNoneAsync<int>().Match(_ => "some", () => "none");
        result.Should().Be("none");
    }

    [Fact]
    public async Task MatchExtension_ShouldReturnSome_GivenValueIsSome()
    {
        var result = await MaybeBehaviors.CreateSomeAsync(10).Match(_ => "some", () => "none");
        result.Should().Be("some");
    }
}