#region
using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.Test.Common.Extensions;
using Xunit;
#endregion

namespace Vonage.Test.Common.Monads.Maybe;

public class BindTest
{
    [Fact]
    public void Bind_ShouldReturnNone_GivenValueIsNone() =>
        MaybeBehaviors.CreateNone<int>().Bind(MaybeBehaviors.BindToString).Should().BeNone();

    [Theory]
    [InlineData(10, "10")]
    [InlineData("10", "10")]
    [InlineData(true, "True")]
    public void Bind_ShouldReturnSome_GivenValueIsSome<T>(T value, string expected) =>
        MaybeBehaviors.CreateSome(value).Bind(MaybeBehaviors.BindToString).Should().BeSome(expected);

    [Fact]
    public async Task BindAsync_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await MaybeBehaviors.CreateNone<int>()
            .BindAsync(_ => Task.FromResult(Maybe<string>.Some(string.Empty)));
        result.Should().BeNone();
    }

    [Fact]
    public async Task BindAsync_ShouldReturnSome_GivenValueIsSome()
    {
        var result = await MaybeBehaviors.CreateSome(10)
            .BindAsync(_ => Task.FromResult(Maybe<string>.Some("Success")));
        result.Should().BeSome("Success");
    }

    [Fact]
    public async Task BindAsyncExtension_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await MaybeBehaviors.CreateNoneAsync<int>().BindAsync(MaybeBehaviors.IncrementBindAsync);
        result.Should().BeNone();
    }

    [Fact]
    public async Task BindAsyncExtension_ShouldReturnSome_GivenValueIsSome()
    {
        var result = await MaybeBehaviors.CreateSomeAsync(10).BindAsync(MaybeBehaviors.IncrementBindAsync);
        result.Should().BeSome(11);
    }

    [Fact]
    public async Task BindExtension_ShouldReturnNone_GivenValueIsNone()
    {
        var result = await MaybeBehaviors.CreateNoneAsync<int>().Bind(MaybeBehaviors.IncrementBind);
        result.Should().BeNone();
    }

    [Fact]
    public async Task BindExtension_ShouldReturnSome_GivenValueIsSome()
    {
        var result = await MaybeBehaviors.CreateSomeAsync(10).Bind(MaybeBehaviors.IncrementBind);
        result.Should().BeSome(11);
    }
}