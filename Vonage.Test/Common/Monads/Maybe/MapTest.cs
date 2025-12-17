#region
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Maybe;

public class MapTest
{
    [Fact]
    public void Map_ShouldReturnNone_GivenValueIsNone() =>
        MaybeBehaviors.CreateNone<int>().Map(MaybeBehaviors.MapToString).Should().BeNone();

    [Theory]
    [InlineData(10, "10")]
    [InlineData("10", "10")]
    [InlineData(true, "True")]
    public void Map_ShouldReturnSome_GivenValueIsSome<T>(T value, string expected) =>
        MaybeBehaviors.CreateSome(value).Map(MaybeBehaviors.MapToString).Should().BeSome(expected);

    [Fact]
    public async Task MapAsync_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await MaybeBehaviors.CreateNone<int>().MapAsync(_ => Task.FromResult("Success"));
        result.Should().BeNone();
    }

    [Theory]
    [InlineData(10, "10")]
    [InlineData("10", "10")]
    [InlineData(true, "True")]
    public void MapAsync_ShouldReturnSome_GivenValueIsSome<T>(T value, string expected) =>
        MaybeBehaviors.CreateSome(value).Map(MaybeBehaviors.MapToString).Should().BeSome(expected);

    [Fact]
    public async Task MapExtension_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await MaybeBehaviors.CreateNoneAsync<int>().Map(MaybeBehaviors.Increment);
        result.Should().BeNone();
    }

    [Fact]
    public async Task MapExtension_ShouldReturnSome_GivenValueIsSome()
    {
        var result = await MaybeBehaviors.CreateSomeAsync(10).Map(MaybeBehaviors.Increment);
        result.Should().BeSome(11);
    }

    [Fact]
    public async Task MapAsyncExtension_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await MaybeBehaviors.CreateNoneAsync<int>().MapAsync(MaybeBehaviors.IncrementAsync);
        result.Should().BeNone();
    }

    [Fact]
    public async Task MapAsyncExtension_ShouldReturnSome_GivenValueIsSome()
    {
        var result = await MaybeBehaviors.CreateSomeAsync(10).MapAsync(MaybeBehaviors.IncrementAsync);
        result.Should().BeSome(11);
    }
}